import React, { useState } from "react";
import {signup  } from "../../Services/authservice";
import {
  MDBContainer,
  MDBCard,
  MDBCardBody,
  MDBInput,
  MDBBtn,
  MDBRow,
  MDBCol,
  MDBTypography,
  MDBIcon,
} from "mdb-react-ui-kit";

import { useNavigate } from "react-router-dom";

const RegisterForm = () => {
  const [formData, setFormData] = useState({
    userName: "",
    email: "",
    password: "",
    nom: "",
    prenom: "",
    telephone: "",
    adresse: "",
    niveau: "",
    classe: "",
    dateInscription: "",
  });

  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);
  const navigate = useNavigate();
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

// Gestion de l'événement handleSubmit
const handleSubmit = async (e) => {
  e.preventDefault();
  setError(null); // Réinitialise les erreurs avant la tentative
  setSuccess(null); // Réinitialise les succès avant la tentative

  try {
    await signup(formData); // Appelle la fonction signup avec les données utilisateur

    // Si l'inscription est réussie
    setSuccess("Inscription réussie ! Vous pouvez maintenant vous connecter.");

    // Navigue vers la page de connexion après un délai pour montrer le message de succès
    setTimeout(() => {
      navigate("/login");
    }, 2000); // Délai de 2 secondes

    // Réinitialise le formulaire après une inscription réussie
    setFormData({
      userName: "",
      email: "",
      password: "",
      confirmPassword: "",
      nom: "",
      prenom: "",
      telephone: "",
      adresse: "",
      niveau: "",
      classe: "",
      dateInscription: "",
    });
  } catch (err) {
    // Affiche un message d'erreur approprié
    setError(err || "Une erreur s'est produite. Veuillez réessayer.");
  }
};

return (
  <MDBContainer fluid className="p-10 background1-radial-gradient overflow-hidden">
    <MDBRow>
      <MDBCol
        md="10"
        className="text-center text-md-start d-flex flex-column justify-content-center"
      >
        <MDBCard>
          <MDBCardBody>
            <h3 className="text-center mb-4">Inscription</h3>
            {error && (
              <MDBTypography color="danger" className="text-center">
                {error}
              </MDBTypography>
            )}
            {success && (
              <MDBTypography color="success" className="text-center">
                {success}
              </MDBTypography>
            )}
            <form onSubmit={handleSubmit}>
              <MDBInput
                className="mb-3"
                label="Nom d'utilisateur"
                name="userName"
                value={formData.userName}
                onChange={handleChange}
                required
              />
              <MDBInput
                className="mb-4"
                type="email"
                label="Email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                required
              />
              <MDBInput
                className="mb-4"
                type="password"
                label="Mot de passe"
                name="password"
                value={formData.password}
                onChange={handleChange}
                required
              />
              <MDBInput
                className="mb-4"
                type="password"
                label="Confirmer le mot de passe"
                name="confirmPassword"
                value={formData.confirmPassword}
                onChange={handleChange}
                required
              />
              <MDBInput
                className="mb-4"
                label="Nom"
                name="nom"
                value={formData.nom}
                onChange={handleChange}
                required
              />
              <MDBInput
                className="mb-4"
                label="Prénom"
                name="prenom"
                value={formData.prenom}
                onChange={handleChange}
                required
              />
              <MDBInput
                className="mb-4"
                label="Téléphone"
                name="telephone"
                value={formData.telephone}
                onChange={handleChange}
              />
              <MDBInput
                className="mb-4"
                label="Adresse"
                name="adresse"
                value={formData.adresse}
                onChange={handleChange}
              />
              <MDBInput
                className="mb-4"
                label="Niveau"
                name="niveau"
                value={formData.niveau}
                onChange={handleChange}
                required
              />
              <MDBInput
                className="mb-4"
                label="Classe"
                name="classe"
                value={formData.classe}
                onChange={handleChange}
                required
              />
              <MDBInput
                className="mb-4"
                type="date"
                label="Date d'inscription"
                name="dateInscription"
                value={formData.dateInscription}
                onChange={handleChange}
                required
              />
              <MDBBtn className="w-100 mb-4" size="md" type="submit">
                <MDBIcon icon="user-plus" className="mr-2" />
                Sinscrire
              </MDBBtn>
            </form>
          </MDBCardBody>
        </MDBCard>
      </MDBCol>
    </MDBRow>
  </MDBContainer>
);
};

export default RegisterForm;
