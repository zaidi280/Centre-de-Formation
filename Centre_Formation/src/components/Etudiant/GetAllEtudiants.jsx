import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { getAllEtudiants, deleteEtudiant } from "../../Services/EtudiantController";
import { Link } from "react-router-dom";

const GetAllEtudiants = () => {
  const [error, setError] = useState(null);
  const [etudiants, setEtudiants] = useState([]);

  const handleDelete = async (etudiantId) => {
    try {
      await deleteEtudiant(etudiantId);
      alert("Suppression réussie !");
      const updatedEtudiants = etudiants.filter(etudiant => etudiant.id !== etudiantId);
      setEtudiants(updatedEtudiants);
    } catch (error) {
      console.error(`Erreur lors de la suppression de l'étudiant avec l'ID ${etudiantId}:`, error);
      alert("Erreur lors de la suppression.");
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getAllEtudiants();
        console.log(data.$values);
        setEtudiants(data.$values);
      } catch (err) {
        console.error("Erreur lors de la récupération des étudiants:", err);
        setError(err.message);
      }
    };
    fetchData();
  }, []);

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Liste des Étudiants</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Nom</th>
            <th>Prénom</th>
            <th>Classe</th>
            <th>Niveau</th>
            <th>Email</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {etudiants.map((etudiant) => (
            <tr key={etudiant.IdEtudiant}>
                        <td>{etudiant.User?.Nom || "N/A"}</td>
                        <td>{etudiant.User?.Prenom || "N/A"}</td>
              <td>{etudiant.classe}</td>
              <td>{etudiant.niveau}</td>
              <td>{etudiant.User?.email}</td>
              <td>
                <Link to={`/updateEtudiant/${etudiant.IdEtudiant}`}>
                  <button>Mettre à jour</button>
                </Link>
                <button onClick={() => handleDelete(etudiant.IdEtudiant)}>Supprimer</button>
                <Link to={`/getEtudiantById/${etudiant.IdEtudiant}`}>
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

export default GetAllEtudiants;
