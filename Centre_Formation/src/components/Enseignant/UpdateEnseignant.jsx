import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getEnseignantById, updateEnseignant } from "../../Services/EnseignantController";

const UpdateEnseignant = () => {
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL
  const [updateData, setUpdateData] = useState({
    userName: "",
    email: "",
    nom: "",
    prenom: "",
    telephone: "",
    adresse: "",
    specialite: "",
    anneesExperience: 0,
    diplome: "",
    dateEmbauche: "",
    password: "",
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchEnseignant = async () => {
      try {
        const enseignant = await getEnseignantById(id);
        setUpdateData({
          userName: enseignant.User.UserName || "",
          email: enseignant.User.Email || "",
          nom: enseignant.User?.Nom || "",
          prenom: enseignant.User?.Prenom || "",
          telephone: enseignant.User.Telephone || "",
          adresse: enseignant.User.Adresse || "",
          specialite: enseignant.Specialite || "",
          anneesExperience: enseignant.AnneesExperience || 0,
          diplome: enseignant.Diplome || "",
          dateEmbauche: enseignant.DateEmbauche || "",
          password: enseignant.User.password, // Les mots de passe sont souvent exclus de la récupération
        });
        console.log(enseignant);
        setLoading(false);
      } catch (err) {
        console.error("Erreur lors de la récupération de l'enseignant :", err);
        setError("Impossible de charger les données de l'enseignant.");
        setLoading(false);
      }
    };

    if (id) {
      fetchEnseignant();
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdateData((prev) => ({ ...prev, [name]: value }));
  };

  const handleUpdate = async () => {
    try {
      const data = await updateEnseignant(id, updateData);
      alert("Mise à jour réussie !");
      console.log("Données mises à jour :", data);
      
    } catch (error) {
      console.error(`Erreur lors de la mise à jour de l'enseignant avec l'ID ${id}:`, error);
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
      <h1>Mettre à jour un Enseignant</h1>
      <form>
        <div className="mb-3">
          <label>Nom d'utilisateur :</label>
          <input
            type="text"
            className="form-control"
            name="userName"
            value={updateData.userName}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Email :</label>
          <input
            type="email"
            className="form-control"
            name="email"
            value={updateData.email}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Nom :</label>
          <input
            type="text"
            className="form-control"
            name="nom"
            value={updateData.nom}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Prénom :</label>
          <input
            type="text"
            className="form-control"
            name="prenom"
            value={updateData.prenom}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Téléphone :</label>
          <input
            type="text"
            className="form-control"
            name="telephone"
            value={updateData.telephone}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Adresse :</label>
          <input
            type="text"
            className="form-control"
            name="adresse"
            value={updateData.adresse}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Spécialité :</label>
          <input
            type="text"
            className="form-control"
            name="specialite"
            value={updateData.specialite}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Années d'expérience :</label>
          <input
            type="number"
            className="form-control"
            name="anneesExperience"
            value={updateData.anneesExperience}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Diplôme :</label>
          <input
            type="text"
            className="form-control"
            name="diplome"
            value={updateData.diplome}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Date d'embauche :</label>
          <input
            type="date"
            className="form-control"
            name="dateEmbauche"
            value={updateData.dateEmbauche.split("T")[0]} // Format pour le champ date
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Mot de passe :</label>
          <input
            type="password"
            className="form-control"
            name="password"
            value={updateData.password}
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

export default UpdateEnseignant;
