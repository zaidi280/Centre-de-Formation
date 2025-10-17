import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getEtudiantById, updateEtudiant } from "../../Services/EtudiantController";

const UpdateEtudiant = () => {
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL
  const [updateData, setUpdateData] = useState({
    userName: "",
    email: "",
    nom: "",
    prenom: "",
    telephone: "",
    adresse: "",
    classe: "",
    niveau: "",
    dateInscription: "",
    password: "",
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchEtudiant = async () => {
      try {
        const etudiant = await getEtudiantById(id);
        setUpdateData({
          userName: etudiant.User.UserName || "",
          email: etudiant.User.Email || "",
          nom: etudiant.User?.Nom || "",
          prenom: etudiant.User?.Prenom || "",
          telephone: etudiant.User.Telephone || "",
          adresse: etudiant.User.Adresse || "",
          classe: etudiant.Classe || "",
          niveau: etudiant.Niveau || "",
          dateInscription: etudiant.DateInscription || "",
          password: etudiant.User.password, // Les mots de passe sont souvent exclus de la récupération
        });
        console.log(etudiant);
        setLoading(false);
      } catch (err) {
        console.error("Erreur lors de la récupération de l'étudiant :", err);
        setError("Impossible de charger les données de l'étudiant.");
        setLoading(false);
      }
    };

    if (id) {
      fetchEtudiant();
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdateData((prev) => ({ ...prev, [name]: value }));
  };

  const handleUpdate = async () => {
    try {
      const data = await updateEtudiant(id, updateData);
      alert("Mise à jour réussie !");
      console.log("Données mises à jour :", data);
      
    } catch (error) {
      console.error(`Erreur lors de la mise à jour de l'étudiant avec l'ID ${id}:`, error);
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
      <h1>Mettre à jour un Étudiant</h1>
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
          <label>Classe :</label>
          <input
            type="text"
            className="form-control"
            name="classe"
            value={updateData.classe}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Niveau :</label>
          <input
            type="text"
            className="form-control"
            name="niveau"
            value={updateData.niveau}
            onChange={handleChange}
          />
        </div>
        <div className="mb-3">
          <label>Date d'inscription :</label>
          <input
            type="date"
            className="form-control"
            name="dateInscription"
            value={updateData.dateInscription.split("T")[0]} // Format pour le champ date
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

export default UpdateEtudiant;
