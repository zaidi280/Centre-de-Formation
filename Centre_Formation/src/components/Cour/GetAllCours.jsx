import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { getAllCours, deleteCour } from "../../Services/CourController";
import { Link } from "react-router-dom";

const GetAllCours = () => {
  const [error, setError] = useState(null);
  const [cours, setCours] = useState([]);

  const handleDelete = async (courId) => {
    try {
      await deleteCour(courId);
      alert("Suppression réussie !");
      const updatedCours = cours.filter(cour => cour.IdCour !== courId);
      setCours(updatedCours);
    } catch (error) {
      console.error(`Erreur lors de la suppression du cours avec l'ID ${courId}:`, error);
      alert("Erreur lors de la suppression.");
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getAllCours();
        console.log(data.$values);
        setCours(data.$values);
      } catch (err) {
        console.error("Erreur lors de la récupération des cours:", err);
        setError(err.message);
      }
    };
    fetchData();
  }, []);

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Liste des Cours</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Chapitre</th>
            <th>Description</th>
            <th>Date et Heure</th>
            <th>Matière</th>
            <th>Enseignant</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {cours.map((cour) => (
            <tr key={cour.IdCour}>
              <td>{cour.Chapitre}</td>
              <td>{cour.Description}</td>
              <td>{new Date(cour.DateHeure).toLocaleString()}</td>
              <td>{cour.Matiere.NomMatiere}</td>
              <td>{cour.Enseignant.User.UserName}</td>
              <td>
                <Link to={`/updateCour/${cour.IdCour}`}>
                  <button>Mettre à jour</button>
                </Link>
                <button onClick={() => handleDelete(cour.IdCour)}>Supprimer</button>
                <Link to={`/getCourById/${cour.IdCour}`}>
                  <button>Voir Détails</button>
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
};

export default GetAllCours;
