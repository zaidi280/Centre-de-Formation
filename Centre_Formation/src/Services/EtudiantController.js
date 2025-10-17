import API from "../Api/Api";
const ETUDIANT_API = "/Etudiant";

export const getAllEtudiants = async () => {
  try {
    const response = await API.get(`${ETUDIANT_API}/GetAllEtudiants`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des étudiants", error);
    throw error;
  }
};

export const getEtudiantById = async (id) => {
  try {
    const response = await API.get(`${ETUDIANT_API}/GetEtudiant/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la récupération de l'étudiant ${id}:`, error);
    throw error;
  }
};

export const updateEtudiant = async (id, etudiantData) => {
  try {
    const response = await API.put(`${ETUDIANT_API}/UpdateEtudiant/${id}`, etudiantData);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la mise à jour de l'étudiant ${id}:`, error);
    throw error;
  }
};

export const deleteEtudiant = async (id) => {
  try {
    const response = await API.delete(`${ETUDIANT_API}/DeleteEtudiant/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la suppression de l'étudiant ${id}:`, error);
    throw error;
  }
};
