import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getCourById, updateCour } from "../../Services/CourController";

const UpdateCour = () => {
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL
  const [updateData, setUpdateData] = useState({
    NomMatiere: "",
    EnseignantNom: "",
    Chapitre: "",
    Description: "",
    DateHeure: "",
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCour = async () => {
      try {
        const cour = await getCourById(id);
        setUpdateData({
          NomMatiere: cour.Matiere.NomMatiere || "",
          EnseignantNom: cour.Enseignant.User.UserName || "",
          Chapitre: cour.Chapitre || "",
          Description: cour.Description || "",
          DateHeure: cour.DateHeure || "",
        });
        console.log(cour);
        setLoading(false);
      } catch (err) {
        console.error("Erreur lors de la récupération du cours :", err);
        setError("Impossible de charger les données du cours.");
        setLoading(false);
      }
    };

    if (id) {
      fetchCour();
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdateData((prev) => ({ ...prev, [name]: value }));
  };

  const handleUpdate = async () => {
    try {
      const data = await updateCour(id, updateData);
      alert("Mise à jour réussie !");
      console.log("Données mises à jour :", data);
    } catch (error) {
      console.error(`Erreur lors de la mise à jour du cours avec l'ID ${id}:`, error);
      alert("Erreur lors de la mise à jour.");
    }
  };

  if (loading) {
    return <p>Chargement des données...</p>;
  }

  if (error) {
    return <p style={{ color: "red" }}>{error}</p>;
  }

  return (
    <div className="container mt-5">
      <h1>Mettre à jour un Cours</h1>
      <form>
        <div className="mb-3">
          <label>Nom de la Matière :</label>
          <input
            type="text"
            className="form-control"
            name="NomMatiere"
            value={updateData.NomMatiere}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Nom de l'Enseignant :</label>
          <input
            type="text"
            className="form-control"
            name="EnseignantNom"
            value={updateData.EnseignantNom}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Chapitre :</label>
          <input
            type="text"
            className="form-control"
            name="Chapitre"
            value={updateData.Chapitre}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Description :</label>
          <textarea
            className="form-control"
            name="Description"
            value={updateData.Description}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Date et Heure :</label>
          <input
            type="datetime-local"
            className="form-control"
            name="DateHeure"
            value={updateData.DateHeure}
            onChange={handleChange}
          />
        </div>
        <button type="button" className="btn btn-primary" onClick={handleUpdate}>
          Mettre à jour
        </button>
      </form>
    </div>
  );
};

export default UpdateCour;
