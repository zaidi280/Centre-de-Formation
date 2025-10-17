import React, { useEffect, useState } from "react";
import { getCourById } from "../../Services/CourController";
import { useParams } from "react-router-dom";

const GetCourById = () => {
  const [cour, setCour] = useState(null); // État pour stocker les données du cours
  const [error, setError] = useState(null); // État pour gérer les erreurs
  const { id } = useParams(); // Récupération de l'ID à partir de l'URL

  const fetchCour = async () => {
    try {
      if (!id) {
        setError("Veuillez entrer un ID valide.");
        setCour(null);
        return;
      }

      const data = await getCourById(id);
      console.log(data);

      // Assurez-vous que les données sont bien formatées
      setCour(data);
      setError(null);
    } catch (err) {
      console.error("Erreur lors de la récupération du cours :", err);
      setError("Impossible de récupérer le cours. Veuillez réessayer.");
      setCour(null);
    }
  };

  useEffect(() => {
    if (id) {
      fetchCour();
    }
  }, [id]);

  return (
    <div className="container mt-5">
      {/* Afficher une erreur si elle existe */}
      {error && <p className="text-danger mt-3">{error}</p>}

      {/* Afficher les détails si un cours est trouvé */}
      {cour && (
        <div className="mt-4">
          <h3>Détails du cours :</h3>
          <p><strong>Chapitre :</strong> {cour.Chapitre}</p>
          <p><strong>Description :</strong> {cour.Description}</p>
          <p><strong>Date et Heure :</strong> {new Date(cour.DateHeure).toLocaleString()}</p>
          <p><strong>Matière :</strong> {cour.Matiere.NomMatiere}</p>
          <p><strong>Enseignant :</strong> {cour.Enseignant.User.UserName}</p>
        </div>
      )}
    </div>
  );
};

export default GetCourById;
