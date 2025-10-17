import React, { useEffect, useState } from "react";
import { getMatiereById } from "../../Services/MatiereController";
import { useParams } from "react-router-dom";

const GetMatiereById = () => {
  const [matiere, setMatiere] = useState(null); // État pour stocker les données de la matière
  const [error, setError] = useState(null); // État pour gérer les erreurs
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL

  const fetchMatiere = async () => {
    try {
      if (!id) {
        setError("Veuillez entrer un ID valide.");
        setMatiere(null);
        return;
      }

      const data = await getMatiereById(id);
      console.log(data);

      // Assurez-vous que les données sont bien formatées
      setMatiere(data);
      setError(null);
    } catch (err) {
      console.error("Erreur lors de la récupération de la matière :", err);
      setError("Impossible de récupérer la matière. Veuillez réessayer.");
      setMatiere(null);
    }
  };

  useEffect(() => {
    if (id) {
      fetchMatiere();
    }
  }, [id]);

  return (
    <div className="container mt-5">
      {/* Afficher une erreur si elle existe */}
      {error && <p className="text-danger mt-3">{error}</p>}

      {/* Afficher les détails si une matière est trouvée */}
      {matiere && (
        <div className="mt-4">
          <h3>Détails de la matière :</h3>
          <p><strong>Nom de la Matière :</strong> {matiere.NomMatiere}</p>
          <p><strong>Description :</strong> {matiere.Description}</p>
          <p><strong>Volume Horaire :</strong> {matiere.VolumeHoraire}</p>
          <p><strong>Salle :</strong> {matiere.SalleId}</p>
        </div>
      )}
    </div>
  );
};

export default GetMatiereById;
