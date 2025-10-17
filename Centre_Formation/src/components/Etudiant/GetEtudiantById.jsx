import React, { useEffect, useState } from "react";
import { getEtudiantById } from "../../Services/EtudiantController";
import { useParams } from "react-router-dom";

const GetEtudiantById = () => {
  const [etudiant, setEtudiant] = useState(null); // État pour stocker les données de l'étudiant
  const [error, setError] = useState(null); // État pour gérer les erreurs
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL

  const fetchEtudiant = async () => {
    try {
      if (!id) {
        setError("Veuillez entrer un ID valide.");
        setEtudiant(null);
        return;
      }

      const data = await getEtudiantById(id);
      console.log(data);

      // Assurez-vous que les données sont bien formatées
      setEtudiant(data);
      setError(null);
    } catch (err) {
      console.error("Erreur lors de la récupération de l'étudiant :", err);
      setError("Impossible de récupérer l'étudiant. Veuillez réessayer.");
      setEtudiant(null);
    }
  };

  useEffect(() => {
    if (id) {
      fetchEtudiant();
    }
  }, [id]);

  return (
    <div className="container mt-5">
      {/* Afficher une erreur si elle existe */}
      {error && <p className="text-danger mt-3">{error}</p>}

      {/* Afficher les détails si un étudiant est trouvé */}
      {etudiant && (
        <div className="mt-4">
          <h3>Détails de l'étudiant :</h3>
          <p><strong>Nom :</strong> {etudiant.User?.Nom || "Non disponible"}</p>
          <p><strong>Prénom :</strong> {etudiant.User?.Prenom || "Non disponible"}</p>
          <p><strong>Email :</strong> {etudiant.User?.Email || "Non disponible"}</p>
          <p><strong>Classe :</strong> {etudiant.Classe || "Non disponible"}</p>
          <p><strong>Niveau :</strong> {etudiant.Niveau || "Non disponible"}</p>
          <p><strong>Date d'inscription :</strong> {new Date(etudiant.DateInscription).toLocaleDateString()}</p>
        </div>
      )}
    </div>
  );
};

export default GetEtudiantById;
