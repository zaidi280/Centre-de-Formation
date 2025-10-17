import API from "../Api/Api";

const ADMIN_API = "/Admin";

/**
 * Récupérer le profil de l'administrateur.
 * @returns {Object} Données du profil de l'administrateur.
 * @throws Lance une erreur si l'appel échoue.
 */
export const getProfilAdmin = async () => {
  try {
    const response = await API.get(ADMIN_API + "/ProfilAdmin");

    // Affiche les données du profil pour un contrôle visuel
    console.log("Données du profil admin :", response.data);

    // Retourne les données reçues du backend
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération du profil admin :", error);

    // Lance une erreur pour gestion ultérieure
    throw error.response?.data || "Erreur réseau ou serveur.";
  }
};

/**
 * Créer un enseignant dans le système.
 * @param {Object} enseignantData - Données de l'enseignant à créer.
 * @returns {Object} Données de l'enseignant créé.
 * @throws Lance une erreur si l'appel échoue.
 */
export const CreerEnseignant = async (enseignantData) => {
  try {
    const response = await API.post(ADMIN_API + "/CreerEnseignant", enseignantData);

    // Vérifie le statut de la réponse pour confirmer la création
    if (response.status === 201 || response.status === 200) {
      console.log("Enseignant créé avec succès :", response.data);

      // Retourne les données de l'enseignant créé
      return response.data;
    } else {
      throw new Error("Échec de la création de l'enseignant.");
    }
  } catch (error) {
    console.error("Erreur lors de la création de l'enseignant :", error);

    // Lance une erreur plus explicite basée sur la réponse de l'API ou un défaut
    throw error.response?.data || "Une erreur réseau s'est produite.";
  }
};

/**
 * Créer un étudiant dans le système.
 * @returns {Object} Données de l'étudiant créé.
 * @throws Lance une erreur si l'appel échoue.
 */
export const CreerEtudiant = async (etudiantData) => {
  try {
    const response = await API.post(ADMIN_API + "/CreerEtudiant",etudiantData);

   // Vérifie le statut de la réponse pour confirmer la création
   if (response.status === 201 || response.status === 200) {
    console.log("Etudiant créé avec succès :", response.data);

    // Retourne les données de l'enseignant créé
    return response.data;
  } else {
    throw new Error("Échec de la création de l'étudiant.");
  }
  } catch (error) {
    console.error("Erreur lors de la création de l'étudiant :", error);

    // Lance une erreur pour gestion ultérieure
    throw error.response?.data || "Erreur réseau ou serveur.";
  }
};
