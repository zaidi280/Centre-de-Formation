import axios from "axios";

const API = axios.create({
  baseURL: 'https://localhost:7288/api', // Remplacez par l'URL de votre API
});
// Intercepteur de requêtes
API.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("CC_Token"); // Récupérer le token depuis localStorage
    if (token) {
      console.log("Token utilisé:", token); // Pour déboguer
      config.headers["Authorization"] = `Bearer ${token}`; // Ajouter le token dans l'entête
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Intercepteur de réponse pour gérer le rafraîchissement du token en cas d'erreur 401
API.interceptors.response.use(
  (response) => {
    console.log("Réponse reçue:", response); // Pour déboguer
    return response;
  },
  (error) => {
    const originalRequest = error.config;

    // Si la réponse est une erreur 401 (token expiré)
    if (error.response && error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      const refreshToken = localStorage.getItem("refreshToken"); // Récupérer le refresh token

      if (!refreshToken) {
        return Promise.reject(error); // Si aucun refresh token n'est trouvé, refuser la requête
      }

      // Appel pour rafraîchir le token
      return axios
        .post("/Account/RefreshToken", { refreshToken }) // Envoie de la requête de rafraîchissement
        .then((res) => {
          if (res.status === 200) {
            // Si le rafraîchissement du token est réussi
            const newToken = res.data.token;
            const newRefreshToken = res.data.refreshToken;

            // Sauvegarder les nouveaux tokens dans localStorage
            localStorage.setItem("CC_Token", newToken);
            localStorage.setItem("refreshToken", newRefreshToken);

            // Mettre à jour l'entête Authorization avec le nouveau token
            axios.defaults.headers.common["Authorization"] = `Bearer ${newToken}`;

            // Refaire la requête initiale avec le nouveau token
            return axios(originalRequest);
          }
        })
        .catch((err) => {
          // Si le rafraîchissement du token échoue, rediriger vers la page de connexion
          console.error("Échec du rafraîchissement du token:", err);
          // Optionnel: Rediriger vers la page de connexion
          window.location.href = "/login";
          return Promise.reject(err); // Rejeter la requête
        });
    }

    return Promise.reject(error); // Rejeter l'erreur si ce n'est pas une erreur 401
  }
);

export default API;
