using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using workspace.DTO;
using workspace.Models;


namespace workspace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
     
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly WorkspaceContext dbContext;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
           IConfiguration configuration, WorkspaceContext dbContext)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
           
        }
        

        [HttpPost("Inscription")]
        public async Task<IActionResult> Inscription(InscriptionDTO inscriptionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérifier si le rôle "Etudiant" existe
            var roleExists = await roleManager.RoleExistsAsync("Etudiant");
            if (!roleExists)
            {
                var role = new ApplicationRole
                {
                    Name = "Etudiant",
                    Description = "Role for students"
                };
                await roleManager.CreateAsync(role);
            }

            // Vérifier si l'utilisateur existe déjà
            var user = await userManager.FindByNameAsync(inscriptionDTO.UserName);
            if (user != null)
            {
                return BadRequest("L'utilisateur existe déjà");
            }

            // Créer l'utilisateur ApplicationUser
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = inscriptionDTO.UserName,
                Email = inscriptionDTO.Email,
                Nom = inscriptionDTO.Nom,
                Prenom = inscriptionDTO.Prenom,
                Telephone = inscriptionDTO.Telephone,
                Adresse = inscriptionDTO.Adresse
            };

            var result = await userManager.CreateAsync(applicationUser, inscriptionDTO.Password);
            if (result.Succeeded)
            {
                // Attribuer le rôle "Etudiant" à l'utilisateur
                await userManager.AddToRoleAsync(applicationUser, "Etudiant");

                // Créer l'étudiant
                var etudiant = new Etudiant
                {
                    IdEtudiant = Guid.NewGuid().ToString(), // Génération automatique de l'ID
                    Niveau = inscriptionDTO.Niveau,
                    Classe = inscriptionDTO.Classe,
                    DateInscription = inscriptionDTO.DateInscription,
                    UserId = applicationUser.Id,
               

                  
                };

                dbContext.Etudiants.Add(etudiant);
                await dbContext.SaveChangesAsync();

                return Ok(new { Message = "Inscription réussie", User = applicationUser.UserName });
            }
            return BadRequest("Problème de création de l'utilisateur");
        }



        [HttpPost("Connexion")]
        public async Task<IActionResult> Connexion(ConnexionDTO connexionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(connexionDTO.UserName);
            if (user == null)
            {
                return BadRequest("Wrong credentials");
            }

            if (await userManager.CheckPasswordAsync(user, connexionDTO.Password))
            {
                List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, connexionDTO.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                var roles = await userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    issuer: configuration["JWT:issuer"],
                    audience: configuration["JWT:audience"],
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Mise à jour du token actif de l'utilisateur
                user.ActiveToken = tokenString;
                user.TokenExpiration = token.ValidTo;
                await userManager.UpdateAsync(user);
                var refreshToken = await GenerateRefreshToken(user.Id);


                return Ok(new
                {
                    token = tokenString,
                    expiration = token.ValidTo,
                    username = connexionDTO.UserName,
                   role = roles.FirstOrDefault(),
                    refreshToken = refreshToken

                });
                
            }

            return BadRequest("Wrong credentials");
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var storedToken = dbContext.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);

            if (storedToken == null || storedToken.Expiration < DateTime.Now)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            var user = await userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
            {
                return Unauthorized("Invalid user.");
            }

            // Générer un nouveau token d'accès
            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var newToken = new JwtSecurityToken(
                claims: claims,
                issuer: configuration["JWT:issuer"],
                audience: configuration["JWT:audience"],
                expires: DateTime.Now.AddSeconds(5),
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(newToken);

            // Réponse avec le nouveau token
            return Ok(new
            {
                token = tokenString,
                expiration = newToken.ValidTo
            });
        }

        private async Task<string> GenerateRefreshToken(string userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                Expiration = DateTime.Now.AddDays(1)
            };

            dbContext.RefreshTokens.Add(refreshToken);
            await dbContext.SaveChangesAsync();

            return refreshToken.Token;
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Utilisateur non authentifié.");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Utilisateur non trouvé.");
            }

            // Invalidate the token
            user.ActiveToken = user.ActiveToken;
            await userManager.UpdateAsync(user);

            return Ok(new { Message = "Déconnexion réussie." });
        }






    }
}
