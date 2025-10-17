import API from "../Api/Api";
const MATIERE_API = "/Matiere";

export const getAllMatieres = async () => {
  try {
    const response = await API.get(`${MATIERE_API}/GetAllMatieres`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des matières", error);
    throw error;
  }
};

export const getMatiereById = async (id) => {
  try {
    const response = await API.get(`${MATIERE_API}/GetMatiere/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la récupération de la matière ${id}:`, error);
    throw error;
  }
};

export const addMatiere = async (matiereData) => {
  try {
    const response = await API.post(`${MATIERE_API}/AddMatiere`, matiereData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de l'ajout de la matière", error);
    throw error;
  }
};

export const updateMatiere = async (id, matiereData) => {
  try {
    const response = await API.put(`${MATIERE_API}/UpdateMatiere/${id}`, matiereData);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la mise à jour de la matière ${id}:`, error);
    throw error;
  }
};

export const deleteMatiere = async (id) => {
  try {
    const response = await API.delete(`${MATIERE_API}/DeleteMatiere/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la suppression de la matière ${id}:`, error);
    throw error;
  }
};
