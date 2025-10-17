import React, { useEffect, useState } from "react";
import { getEnseignantById } from "../../Services/EnseignantController";
import { useParams } from "react-router-dom";
const GetEnseignantById = () => {

  const [enseignant, setEnseignant] = useState(null); // État pour stocker les données de l'enseignant
  const [error, setError] = useState(null); // État pour gérer les erreurs
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL
  const fetchEnseignant = async () => {
    try {
      if (!id) {
        setError("Veuillez entrer un ID valide.");
        setEnseignant(null);
        return;
      }

      const data = await getEnseignantById(id);
      console.log(data);

      // Assurez-vous que les données sont bien formatées
      setEnseignant(data);
      setError(null);
    } catch (err) {
      console.error("Erreur lors de la récupération de l'enseignant :", err);
      setError("Impossible de récupérer l'enseignant. Veuillez réessayer.");
      setEnseignant(null);
    }
  };
  useEffect(()=>{
    if (id) {
      fetchEnseignant();
    }
  },[])
  return (
    <div className="container mt-5">
      {/* Afficher une erreur si elle existe */}
      {error && <p className="text-danger mt-3">{error}</p>}

      {/* Afficher les détails si un enseignant est trouvé */}
      {enseignant && (
        <div className="mt-4">
          <h3>Détails de l'enseignant :</h3>
          <p><strong>Nom :</strong> {enseignant.User?.Nom || "Non disponible"}</p>
          <p><strong>Prénom :</strong> {enseignant.User?.Prenom || "Non disponible"}</p>
          <p><strong>Spécialité :</strong> {enseignant.Specialite || "Non disponible"}</p>
          <p><strong>Années d'expérience :</strong> {enseignant.AnneesExperience || "Non disponible"}</p>
          <p><strong>Diplôme :</strong> {enseignant.Diplome || "Non disponible"}</p>
          <p><strong>Date d'embauche :</strong> {new Date(enseignant.DateEmbauche).toLocaleDateString()}</p>
        </div>
      )}
    </div>
  );
};

export default GetEnseignantById;
