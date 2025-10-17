import React, { useState } from "react";
import { addCour } from "../../Services/CourController";

const AddCour = () => {
  const [courData, setCourData] = useState({
    NomMatiere: "",
    EnseignantNom: "",
    Chapitre: "",
    Description: "",
    DateHeure: "",
  });
  const [error, setError] = useState(null);
  const [successMessage, setSuccessMessage] = useState(null);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCourData((prev) => ({ ...prev, [name]: value }));
  };

  const handleAddCour = async () => {
    try {
      const data = await addCour(courData);
      setSuccessMessage("Cours ajouté avec succès !");
      console.log("Cours ajouté:", data);
      setCourData({
        NomMatiere: "",
        EnseignantNom: "",
        Chapitre: "",
        Description: "",
        DateHeure: "",
      });
      setError(null);
    } catch (error) {
      console.error("Erreur lors de l'ajout du cours:", error);
      setError("Impossible d'ajouter le cours. Veuillez réessayer.");
      setSuccessMessage(null);
    }
  };

  return (
    <div className="container mt-5">
      <h1>Ajouter un Nouveau Cours</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      {successMessage && <p style={{ color: "green" }}>{successMessage}</p>}
      <form>
        <div className="mb-3">
          <label>Nom de la Matière :</label>
          <input
            type="text"
            className="form-control"
            name="NomMatiere"
            value={courData.NomMatiere}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Nom de l'Enseignant :</label>
          <input
            type="text"
            className="form-control"
            name="EnseignantNom"
            value={courData.EnseignantNom}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Chapitre :</label>
          <input
            type="text"
            className="form-control"
            name="Chapitre"
            value={courData.Chapitre}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Description :</label>
          <textarea
            className="form-control"
            name="Description"
            value={courData.Description}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Date et Heure :</label>
          <input
            type="datetime-local"
            className="form-control"
            name="DateHeure"
            value={courData.DateHeure}
            onChange={handleChange}
          />
        </div>
        <button type="button" className="btn btn-primary" onClick={handleAddCour}>
          Ajouter Cours
        </button>
      </form>
    </div>
  );
};

export default AddCour;
