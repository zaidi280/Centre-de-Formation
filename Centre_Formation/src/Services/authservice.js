
import API from "../Api/Api";
const USER_API = "/Account";
export const signup = async (userCredentials) => {
  try {
    const response = await API.post(USER_API+"/Inscription", userCredentials);

    // Vérifie si le statut HTTP est un succès
    if (response.status === 201 || response.status === 200) {
      console.log("Inscription réussie", response.data);
      return response.data; // Retourne les données du backend
    } else {
      throw new Error("Échec de l'inscription. Veuillez réessayer.");
    }
  } catch (error) {
    console.error("Il y a eu une erreur !", error);
    // Ajoute un message plus clair en cas d'erreur réseau ou serveur
    throw error.response?.data || "Une erreur réseau s'est produite.";
  }
};

export const logout = async () => {
  return await API.post(USER_API + "/logout");
};




// import axios from 'axios';
export const signin = async (userCredentials) => {
  try {
    const response = await API.post(USER_API + "/Connexion", userCredentials);
    console.log(response.data); // Vérifiez la réponse de l'API pour débogage
    return response.data; // Retournez directement les données définies dans le backend
  } catch (error) {
    console.error("Une erreur s'est produite lors de la connexion :", error);
    // Vous pouvez personnaliser le message d'erreur pour le remonter à l'appelant
    throw new Error(
      error.response?.data?.message || "Une erreur inconnue est survenue"
    );
  }
};

