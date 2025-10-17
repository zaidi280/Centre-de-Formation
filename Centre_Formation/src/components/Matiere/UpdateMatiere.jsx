import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getMatiereById, updateMatiere } from "../../Services/MatiereController";

const UpdateMatiere = () => {
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL
  const [updateData, setUpdateData] = useState({
    NomMatiere: "",
    Description: "",
    VolumeHoraire: 0,
    NomSalle: "", // Utilisé pour trouver l'ID de la salle
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchMatiere = async () => {
      try {
        const matiere = await getMatiereById(id);
        setUpdateData({
          NomMatiere: matiere.NomMatiere || "",
          Description: matiere.Description || "",
          VolumeHoraire: matiere.VolumeHoraire || 0,
          NomSalle: matiere.Salle.NomSalle || "", // Assurez-vous de récupérer le nom de la salle si disponible
        });
        console.log(matiere);
        setLoading(false);
      } catch (err) {
        console.error("Erreur lors de la récupération de la matière :", err);
        setError("Impossible de charger les données de la matière.");
        setLoading(false);
      }
    };

    if (id) {
      fetchMatiere();
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdateData((prev) => ({ ...prev, [name]: value }));
  };

  const handleUpdate = async () => {
    try {
      const data = await updateMatiere(id, updateData);
      alert("Mise à jour réussie !");
      console.log("Données mises à jour :", data);
    } catch (error) {
      console.error(`Erreur lors de la mise à jour de la matière avec l'ID ${id}:`, error);
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
      <h1>Mettre à jour une Matière</h1>
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
          <label>Description :</label>
          <input
            type="text"
            className="form-control"
            name="Description"
            value={updateData.Description}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Volume Horaire :</label>
          <input
            type="number"
            className="form-control"
            name="VolumeHoraire"
            value={updateData.VolumeHoraire}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Nom de la Salle :</label>
          <input
            type="text"
            className="form-control"
            name="NomSalle"
            value={updateData.NomSalle}
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

export default UpdateMatiere;
