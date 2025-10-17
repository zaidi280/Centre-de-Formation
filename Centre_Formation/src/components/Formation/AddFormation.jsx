import React, { useState } from "react";
import { addFormation } from "../../Services/FormationController";

const AddFormation = () => {
  const [formationData, setFormationData] = useState({
    Titre: "",
    Description: "",
    Duree: 0,
    Prix: 0,
    DateDebut: "",
    DateFin: "",
    Enseignants: [], // Utilisé pour associer des enseignants à la formation
    Etudiants: [], // Utilisé pour associer des étudiants à la formation
    Matieres: [] // Utilisé pour associer des matières à la formation
  });
  const [error, setError] = useState(null);
  const [successMessage, setSuccessMessage] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormationData((prev) => ({ ...prev, [name]: value }));
  };

  const handleAddFormation = async () => {
    try {
      const data = await addFormation(formationData);
      setSuccessMessage("Formation ajoutée avec succès !");
      console.log("Formation ajoutée:", data);
      setFormationData({
        Titre: "",
        Description: "",
        Duree: 0,
        Prix: 0,
        DateDebut: "",
        DateFin: "",
        Enseignants: [],
        Etudiants: [],
        Matieres: []
      });
      setError(null);
    } catch (error) {
      console.error("Erreur lors de l'ajout de la formation:", error);
      setError("Impossible d'ajouter la formation. Veuillez réessayer.");
      setSuccessMessage(null);
    }
  };

  return (
    <div className="container mt-5">
      <h1>Ajouter une Nouvelle Formation</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      {successMessage && <p style={{ color: "green" }}>{successMessage}</p>}
      <form>
        <div className="mb-3">
          <label>Titre :</label>
          <input
            type="text"
            className="form-control"
            name="Titre"
            value={formationData.Titre}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Description :</label>
          <textarea
            className="form-control"
            name="Description"
            value={formationData.Description}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Durée :</label>
          <input
            type="number"
            className="form-control"
            name="Duree"
            value={formationData.Duree}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Prix :</label>
          <input
            type="number"
            className="form-control"
            name="Prix"
            value={formationData.Prix}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Date de Début :</label>
          <input
            type="date"
            className="form-control"
            name="DateDebut"
            value={formationData.DateDebut}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Date de Fin :</label>
          <input
            type="date"
            className="form-control"
            name="DateFin"
            value={formationData.DateFin}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Enseignants (séparés par des virgules) :</label>
          <input
            type="text"
            className="form-control"
            name="Enseignants"
            value={formationData.Enseignants.join(",")}
            onChange={(e) => handleChange({ target: { name: "Enseignants", value: e.target.value.split(",") } })}
          />
        </div>
        <div className="mb-3">
          <label>Étudiants (séparés par des virgules) :</label>
          <input
            type="text"
            className="form-control"
            name="Etudiants"
            value={formationData.Etudiants.join(",")}
            onChange={(e) => handleChange({ target: { name: "Etudiants", value: e.target.value.split(",") } })}
          />
        </div>
        <div className="mb-3">
          <label>Matières (séparés par des virgules) :</label>
          <input
            type="text"
            className="form-control"
            name="Matieres"
            value={formationData.Matieres.join(",")}
            onChange={(e) => handleChange({ target: { name: "Matieres", value: e.target.value.split(",") } })}
          />
        </div>
        <button type="button" className="btn btn-primary" onClick={handleAddFormation}>
          Ajouter Formation
        </button>
      </form>
    </div>
  );
};

export default AddFormation;
