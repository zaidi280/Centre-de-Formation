import React, { useEffect, useState } from "react";
import { getFormationById } from "../../Services/FormationController";
import { useParams } from "react-router-dom";

const GetFormationById = () => {
  const [formation, setFormation] = useState(null); // État pour stocker les données de la formation
  const [error, setError] = useState(null); // État pour gérer les erreurs
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL

  const fetchFormation = async () => {
    try {
      if (!id) {
        setError("Veuillez entrer un ID valide.");
        setFormation(null);
        return;
      }

      const data = await getFormationById(id);
      console.log(data);

      // Assurez-vous que les données sont bien formatées
      setFormation(data);
      setError(null);
    } catch (err) {
      console.error("Erreur lors de la récupération de la formation :", err);
      setError("Impossible de récupérer la formation. Veuillez réessayer.");
      setFormation(null);
    }
  };

  useEffect(() => {
    if (id) {
      fetchFormation();
    }
  }, [id]);

  return (
    <div className="container mt-5">
      {/* Afficher une erreur si elle existe */}
      {error && <p className="text-danger mt-3">{error}</p>}

      {/* Afficher les détails si une formation est trouvée */}
      {formation && (
        <div className="mt-4">
          <h3>Détails de la formation :</h3>
          <p><strong>Titre :</strong> {formation.Titre}</p>
          <p><strong>Description :</strong> {formation.Description}</p>
          <p><strong>Durée :</strong> {formation.Duree}</p>
          <p><strong>Prix :</strong> {formation.Prix}</p>
          <p><strong>Date de Début :</strong> {new Date(formation.DateDebut).toLocaleDateString()}</p>
          <p><strong>Date de Fin :</strong> {new Date(formation.DateFin).toLocaleDateString()}</p>
        </div>
      )}
    </div>
  );
};

export default GetFormationById;
