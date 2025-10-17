import API from "../Api/Api";
const FORMATION_API = "/Formation";

export const getAllFormations = async () => {
  try {
    const response = await API.get(`${FORMATION_API}/GetAllFormations`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des formations", error);
    throw error;
  }
};

export const getFormationById = async (id) => {
  try {
    const response = await API.get(`${FORMATION_API}/GetFormation/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la récupération de la formation ${id}:`, error);
    throw error;
  }
};

export const addFormation = async (formationData) => {
  try {
    const response = await API.post(`${FORMATION_API}/AddFormation`, formationData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de l'ajout de la formation", error);
    throw error;
  }
};

export const updateFormation = async (id, formationData) => {
  try {
    const response = await API.put(`${FORMATION_API}/UpdateFormation/${id}`, formationData);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la mise à jour de la formation ${id}:`, error);
    throw error;
  }
};

export const deleteFormation = async (id) => {
  try {
    const response = await API.delete(`${FORMATION_API}/DeleteFormation/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la suppression de la formation ${id}:`, error);
    throw error;
  }
};
