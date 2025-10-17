import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { getAllMatieres, deleteMatiere } from "../../Services/MatiereController";
import { Link } from "react-router-dom";
import { getSalleById } from "../../Services/SalleController";
const GetAllMatieres = () => {
  const [error, setError] = useState(null);
  const [matieres, setMatieres] = useState([]);

  const handleDelete = async (matiereId) => {
    try {
      await deleteMatiere(matiereId);
      alert("Suppression réussie !");
      const updatedMatieres = matieres.filter(matiere => matiere.IdMatiere !== matiereId);
      setMatieres(updatedMatieres);
    } catch (error) {
      console.error(`Erreur lors de la suppression de la matière avec l'ID ${matiereId}:`, error);
      alert("Erreur lors de la suppression.");
    }
  };

  const fetchSalle = async (salleId) => {
    try {
      const salle = await getSalleById(salleId);
      return salle.NomSalle;
    } catch (error) {
      console.error(`Erreur lors de la récupération de la salle avec l'ID ${salleId}:`, error);
      return 'N/A';
    }
  };

  const fetchMatieres = async () => {
    try {
      const data = await getAllMatieres();
      const matieresWithSalle = await Promise.all(data.$values.map(async (matiere) => {
        if (matiere.SalleId) {
          matiere.NomSalle = await fetchSalle(matiere.SalleId);
        } else {
          matiere.NomSalle = 'N/A';
        }
        return matiere;
      }));
      setMatieres(matieresWithSalle);
    } catch (err) {
      console.error("Erreur lors de la récupération des matières:", err);
      setError(err.message);
    }
  };

  useEffect(() => {
    fetchMatieres();
    
  }, []);

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Liste des Matières</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Nom de la Matière</th>
            <th>Description</th>
            <th>Volume Horaire</th>
            <th>Salle</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {matieres.map((matiere) => (
            <tr key={matiere.IdMatiere}>
              <td>{matiere.NomMatiere}</td>
              <td>{matiere.Description}</td>
              <td>{matiere.VolumeHoraire}</td>
              <td>{matiere.NomSalle}</td>
              <td>
                <Link to={`/updateMatiere/${matiere.IdMatiere}`}>
                  <button>Mettre à jour</button>
                </Link>
                <button onClick={() => handleDelete(matiere.IdMatiere)}>Supprimer</button>
                <Link to={`/getMatiereById/${matiere.IdMatiere}`}>
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

export default GetAllMatieres;
