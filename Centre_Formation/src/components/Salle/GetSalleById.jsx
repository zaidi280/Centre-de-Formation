import React, { useEffect, useState } from "react";
import { getSalleById } from "../../Services/SalleController";
import { useParams } from "react-router-dom";

const GetSalleById = () => {
  const [salle, setSalle] = useState(null); // État pour stocker les données de la salle
  const [error, setError] = useState(null); // État pour gérer les erreurs
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL

  const fetchSalle = async () => {
    try {
      if (!id) {
        setError("Veuillez entrer un ID valide.");
        setSalle(null);
        return;
      }

      const data = await getSalleById(id);
      console.log(data);

      // Assurez-vous que les données sont bien formatées
      setSalle(data);
      setError(null);
    } catch (err) {
      console.error("Erreur lors de la récupération de la salle :", err);
      setError("Impossible de récupérer la salle. Veuillez réessayer.");
      setSalle(null);
    }
  };

  useEffect(() => {
    if (id) {
      fetchSalle();
    }
  }, [id]);

  return (
    <div className="container mt-5">
      {/* Afficher une erreur si elle existe */}
      {error && <p className="text-danger mt-3">{error}</p>}

      {/* Afficher les détails si une salle est trouvée */}
      {salle && (
        <div className="mt-4">
          <h3>Détails de la salle :</h3>
          <p><strong>Nom de la Salle :</strong> {salle.NomSalle}</p>
          <p><strong>Capacité :</strong> {salle.Capacite}</p>
          <p><strong>Type de Salle :</strong> {salle.TypeSalle}</p>
          <p><strong>Équipement :</strong> {salle.Equipement}</p>
        </div>
      )}
    </div>
  );
};

export default GetSalleById;
