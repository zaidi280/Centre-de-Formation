import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getSalleById, updateSalle } from "../../Services/SalleController";

const UpdateSalle = () => {
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL
  const [updateData, setUpdateData] = useState({
    nomSalle: "",
    capacite: 0,
    typeSalle: "",
    equipement: "",
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchSalle = async () => {
      try {
        const salle = await getSalleById(id);
        setUpdateData({
            nomSalle: salle.NomSalle || "",
            capacite: salle.Capacite || 0,
            typeSalle: salle.TypeSalle || "",
            equipement: salle.Equipement || "",
        });
        console.log(salle);
        setLoading(false);
      } catch (err) {
        console.error("Erreur lors de la récupération de la salle :", err);
        setError("Impossible de charger les données de la salle.");
        setLoading(false);
      }
    };

    if (id) {
      fetchSalle();
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdateData((prev) => ({ ...prev, [name]: value }));
  };

  const handleUpdate = async () => {
    try {
      const data = await updateSalle(id, updateData);
      alert("Mise à jour réussie !");
      console.log("Données mises à jour :", data);
      
    } catch (error) {
      console.error(`Erreur lors de la mise à jour de la salle avec l'ID ${id}:`, error);
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
      <h1>Mettre à jour une Salle</h1>
      <form>
        <div className="mb-3">
          <label>Nom de la Salle :</label>
          <input
            type="text"
            className="form-control"
            name="nomSalle"
            value={updateData.nomSalle}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Capacité :</label>
          <input
            type="number"
            className="form-control"
            name="capacite"
            value={updateData.capacite}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Type de Salle :</label>
          <input
            type="text"
            className="form-control"
            name="typeSalle"
            value={updateData.typeSalle}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Équipement :</label>
          <input
            type="text"
            className="form-control"
            name="equipement"
            value={updateData.equipement}
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

export default UpdateSalle;
