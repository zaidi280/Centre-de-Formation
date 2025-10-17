import API from "../Api/Api";
const COUR_API = "/Cour";

export const getAllCours = async () => {
  try {
    const response = await API.get(`${COUR_API}/GetAllCours`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des cours", error);
    throw error;
  }
};

export const getCourById = async (id) => {
  try {
    const response = await API.get(`${COUR_API}/GetCour/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la récupération du cours ${id}:`, error);
    throw error;
  }
};

export const addCour = async (courData) => {
  try {
    const response = await API.post(`${COUR_API}/AddCour`, courData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de l'ajout du cours", error);
    throw error;
  }
};

export const updateCour = async (id, courData) => {
  try {
    const response = await API.put(`${COUR_API}/UpdateCour/${id}`, courData);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la mise à jour du cours ${id}:`, error);
    throw error;
  }
};

export const deleteCour = async (id) => {
  try {
    const response = await API.delete(`${COUR_API}/DeleteCour/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la suppression du cours ${id}:`, error);
    throw error;
  }
};
