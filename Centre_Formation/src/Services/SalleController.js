import API from "../Api/Api";
const SALLE_API = "/Salle";

export const getAllSalles = async () => {
  try {
    const response = await API.get(`${SALLE_API}/GetAllSalles`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des salles", error);
    throw error;
  }
};

export const getSalleById = async (id) => {
  try {
    const response = await API.get(`${SALLE_API}/GetSalle/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la récupération de la salle ${id}:`, error);
    throw error;
  }
};

export const addSalle = async (salleData) => {
  try {
    const response = await API.post(`${SALLE_API}/AddSalle`, salleData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de l'ajout de la salle", error);
    throw error;
  }
};

export const updateSalle = async (id, salleData) => {
  try {
    const response = await API.put(`${SALLE_API}/UpdateSalle/${id}`, salleData);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la mise à jour de la salle ${id}:`, error);
    throw error;
  }
};

export const deleteSalle = async (id) => {
  try {
    const response = await API.delete(`${SALLE_API}/DeleteSalle/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la suppression de la salle ${id}:`, error);
    throw error;
  }
};
