import API from "../Api/Api";
const ENSEIGNANT_API = "/Enseignant";
export const getAllEnseignants = async () => {
    try {
      const response = await API.get(ENSEIGNANT_API+"/GetAllEnseignants");
      console.log('Réponse de l\'API:', response.data); // Log des données reçues de l'API
      return response.data;
    } catch (error) {
      console.error("Erreur lors de la récupération des enseignants", error);
      throw error;
    }
  };
  export const getEnseignantById = async (enseignantId) => {
    try {
      const response = await API.get(ENSEIGNANT_API+`/GetEnseignant/${enseignantId}`);
      return response.data;
    } catch (error) {
      if (error.response) {
        // L'erreur est survenue côté serveur
        console.error("Erreur serveur lors de la récupération de l'Enseignant ${enseignantId}: ${error.response.status} - ${error.response.data}");
      } else if (error.request) {
        // La requête a été envoyée mais il n'y a pas de réponse
        console.error('La requête a été envoyée mais aucune réponse reçue', error.request);
      } else {
        // Une autre erreur est survenue
        console.error('Erreur lors de la préparation de la requête', error.message);
      }
      throw error;
    }
  };
//  export  const updateEnseignant = async (enseignantId, enseignantData) => {
//     try {
//       const response = await API.put(ENSEIGNANT_API+`/UpdateEnseignant/${enseignantId}`, enseignantData);
//       return response.data;
//     } catch (error) {
//       console.error("Erreur lors de la mise à jour de l' Enseignant avec categoryId ${enseignantId}, error");
//       throw error;
//     }
//   };

export const updateEnseignant = async (enseignantId, enseignantData) => {
  try {
    const response = await API.put(ENSEIGNANT_API+`/UpdateEnseignant/${enseignantId}`, enseignantData);

    if (response.status === 204) {
      console.log("Mise à jour réussie, aucune donnée retournée.");
      return { success: true, message: "Mise à jour réussie."}  ,response.data;
    }

    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la mise à jour de l'enseignant avec ID ${enseignantId}:`, error);
    throw error;
  }
};

  export const deleteEnseignant = async (enseignantId) => {
    try {
      const response = await API.delete(ENSEIGNANT_API+`/DeleteEnseignant/${enseignantId}`);
      return response.data;
    } catch (error) {
      console.error("Erreur lors de la suppression de l'Enseignant avec categoryId ${enseignantId}", error);
      throw error;
    }
  };