import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getFormationById, updateFormation } from "../../Services/FormationController";

const UpdateFormation = () => {
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL
  const [updateData, setUpdateData] = useState({
    Titre: "",
    Description: "",
    Duree: 0,
    Prix: 0,
    DateDebut: "",
    DateFin: "",
    Enseignants: "",
    Etudiants: "",
    Matieres: ""
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  };

  useEffect(() => {
    const fetchFormation = async () => {
      try {
        const formation = await getFormationById(id);
        setUpdateData({
          Titre: formation.Titre || "",
          Description: formation.Description || "",
          Duree: formation.Duree || 0,
          Prix: formation.Prix || 0,
          DateDebut: formatDate(formation.DateDebut),
          DateFin: formatDate(formation.DateFin),
          Enseignants: formation.ListeEnseignants?.$values.map(e => e.User.UserName).join(", ") || "", 
          Etudiants: formation.ListeEtudiants?.$values.map(e => e.User.UserName).join(", ") || "",
           Matieres: formation.ListeMatieres?.$values.map(m => m.NomMatiere).join(", ") || ""
        });
        console.log(formation);
        setLoading(false);
      } catch (err) {
        console.error("Erreur lors de la récupération de la formation :", err);
        setError("Impossible de charger les données de la formation.");
        setLoading(false);
      }
    };

    if (id) {
      fetchFormation();
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdateData((prev) => ({ ...prev, [name]: value }));
  };

  const handleUpdate = async () => { 
    try { 
      const formattedData = { ...updateData, Enseignants: updateData.Enseignants.split(",").map(e => e.trim()),
         Etudiants: updateData.Etudiants.split(",").map(e => e.trim()),
          Matieres: updateData.Matieres.split(",").map(m => m.trim()) };
           console.log("Données envoyées :", formattedData); 
  // Ajoutez cette ligne pour déboguer 
  const data = await updateFormation(id, formattedData); alert("Mise à jour réussie !"); 
  console.log("Données mises à jour :", data); } 
  catch (error) { console.error(`Erreur lors de la mise à jour de la formation avec l'ID ${id}:`, error); 
  alert("Erreur lors de la mise à jour."); } };
  
  if (loading) {
    return <p>Chargement des données...</p>;
  }

  if (error) {
    return <p style={{ color: "red" }}>{error}</p>;
  }

  return (
    <div className="container mt-5">
      <h1>Mettre à jour une Formation</h1>
      <form>
        <div className="mb-3">
          <label>Titre :</label>
          <input
            type="text"
            className="form-control"
            name="Titre"
            value={updateData.Titre}
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
          <label>Durée :</label>
          <input
            type="number"
            className="form-control"
            name="Duree"
            value={updateData.Duree}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Prix :</label>
          <input
            type="number"
            className="form-control"
            name="Prix"
            value={updateData.Prix}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Date de Début :</label>
          <input
            type="date"
            className="form-control"
            name="DateDebut"
            value={updateData.DateDebut}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Date de Fin :</label>
          <input
            type="date"
            className="form-control"
            name="DateFin"
            value={updateData.DateFin}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Enseignants :</label>
          <input
            type="text"
            className="form-control"
            name="Enseignants"
            value={updateData.Enseignants}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Étudiants :</label>
          <input
            type="text"
            className="form-control"
            name="Etudiants"
            value={updateData.Etudiants}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Matières :</label>
          <input
            type="text"
            className="form-control"
            name="Matieres"
            value={updateData.Matieres}
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

export default UpdateFormation;
