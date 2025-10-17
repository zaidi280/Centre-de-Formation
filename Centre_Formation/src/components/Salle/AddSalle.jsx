import React, { useState } from "react";
import { addSalle } from "../../Services/SalleController";

const AddSalle = () => {
  const [salleData, setSalleData] = useState({
    nomSalle: "",
    capacite: 0,
    typeSalle: "",
    equipement: "",
  });
  const [error, setError] = useState(null);
  const [successMessage, setSuccessMessage] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setSalleData((prev) => ({ ...prev, [name]: value }));
  };

  const handleAddSalle = async () => {
    try {
      const data = await addSalle(salleData);
      setSuccessMessage("Salle ajoutée avec succès !");
      console.log("Salle ajoutée:", data);
      setSalleData({
        nomSalle: "",
        capacite: 0,
        typeSalle: "",
        equipement: "",
      });
      setError(null);
    } catch (error) {
      console.error("Erreur lors de l'ajout de la salle:", error);
      setError("Impossible d'ajouter la salle. Veuillez réessayer.");
      setSuccessMessage(null);
    }
  };

  return (
    <div className="container mt-5">
      <h1>Ajouter une Nouvelle Salle</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      {successMessage && <p style={{ color: "green" }}>{successMessage}</p>}
      <form>
        <div className="mb-3">
          <label>Nom de la Salle :</label>
          <input
            type="text"
            className="form-control"
            name="nomSalle"
            value={salleData.nomSalle}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Capacité :</label>
          <input
            type="number"
            className="form-control"
            name="capacite"
            value={salleData.capacite}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Type de Salle :</label>
          <input
            type="text"
            className="form-control"
            name="typeSalle"
            value={salleData.typeSalle}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Équipement :</label>
          <input
            type="text"
            className="form-control"
            name="equipement"
            value={salleData.equipement}
            onChange={handleChange}
          />
        </div>
        <button type="button" className="btn btn-primary" onClick={handleAddSalle}>
          Ajouter Salle
        </button>
      </form>
    </div>
  );
};

export default AddSalle;
