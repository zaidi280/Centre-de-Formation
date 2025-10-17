import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { getAllFormations, deleteFormation } from "../../Services/FormationController";
import { Link } from "react-router-dom";

const GetAllFormations = () => {
  const [error, setError] = useState(null);
  const [formations, setFormations] = useState([]);

  const handleDelete = async (formationId) => {
    try {
      await deleteFormation(formationId);
      alert("Suppression réussie !");
      const updatedFormations = formations.filter(formation => formation.IdFormation !== formationId);
      setFormations(updatedFormations);
    } catch (error) {
      console.error(`Erreur lors de la suppression de la formation avec l'ID ${formationId}:`, error);
      alert("Erreur lors de la suppression.");
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getAllFormations();
        console.log(data.$values);
        setFormations(data.$values);
      } catch (err) {
        console.error("Erreur lors de la récupération des formations:", err);
        setError(err.message);
      }
    };
    fetchData();
  }, []);

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Liste des Formations</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Titre</th>
            <th>Description</th>
            <th>Durée</th>
            <th>Prix</th>
            <th>Date de Début</th>
            <th>Date de Fin</th>
            <th>Enseignants</th>
            <th>Étudiants</th>
            <th>Matières</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {formations.map((formation) => (
            <tr key={formation.IdFormation}>
              <td>{formation.Titre}</td>
              <td>{formation.Description}</td>
              <td>{formation.Duree}</td>
              <td>{formation.Prix}</td>
              <td>{new Date(formation.DateDebut).toLocaleDateString()}</td>
              <td>{new Date(formation.DateFin).toLocaleDateString()}</td>
              <td>
                {formation.ListeEnseignants && formation.ListeEnseignants.$values.length > 0 ? (
                  formation.ListeEnseignants.$values.map((enseignant, index) => (
                    <div key={enseignant.IdEnseignant}>{enseignant.User.UserName}</div>
                  ))
                ) : (
                  <span>Aucun enseignant</span>
                )}
              </td>
              <td>
                {formation.ListeEtudiants && formation.ListeEtudiants.$values.length > 0 ? (
                  formation.ListeEtudiants.$values.map((etudiant, index) => (
                    <div key={etudiant.IdEtudiant}>{etudiant.User.UserName}</div>
                  ))
                ) : (
                  <span>Aucun étudiant</span>
                )}
              </td>
              <td>
                {formation.ListeMatieres && formation.ListeMatieres.$values.length > 0 ? (
                  formation.ListeMatieres.$values.map((matiere, index) => (
                    <div key={matiere.IdMatiere}>{matiere.NomMatiere}</div>
                  ))
                ) : (
                  <span>Aucune matière</span>
                )}
              </td>
              <td>
                <Link to={`/updateFormation/${formation.IdFormation}`}>
                  <button>Mettre à jour</button>
                </Link>
                <button onClick={() => handleDelete(formation.IdFormation)}>Supprimer</button>
                <Link to={`/getFormationById/${formation.IdFormation}`}>
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

export default GetAllFormations;
