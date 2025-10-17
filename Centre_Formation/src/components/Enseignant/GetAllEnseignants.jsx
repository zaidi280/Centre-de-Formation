import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { deleteEnseignant, getAllEnseignants } from "../../Services/EnseignantController";
import { Link } from "react-router-dom";

const GetAllEnseignants = () => {
  const [error, setError] = useState(null);
  const [enseignants, setEnseignants] = useState([]);


  const handleDelete = async (enseignantId) => {
    try {
      await deleteEnseignant(enseignantId);
      alert("Suppression réussie !");
      setEnseignantId(""); // Effacer le champ après la soumission
      onDeleteSuccess();
    } catch (error) {
      console.error(`Erreur lors de la suppression de l'enseignant avec l'ID ${enseignantId}:`, error);
      alert("Erreur lors de la suppression.");
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getAllEnseignants();
        console.log(data.$values);
        setEnseignants(data.$values);
      } catch (err) {
        console.error("Erreur lors de la récupération des enseignants:", err);
        setError(err.message);
      }
    };
    fetchData();
  }, []);

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Liste des Enseignants</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Nom</th>
            <th>Prénom</th>
            <th>Spécialité</th>
            <th>Diplôme</th>
            <th>Années d'expérience</th>
            <th>Date d'embauche</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {enseignants.map((enseignant) => (
            <tr key={enseignant.IdEnseignant}>
              <td>{enseignant.User?.Nom || "N/A"}</td>
              <td>{enseignant.User?.Prenom || "N/A"}</td>
              <td>{enseignant.Specialite}</td>
              <td>{enseignant.Diplome}</td>
              <td>{enseignant.AnneesExperience}</td>
              <td>{new Date(enseignant.DateEmbauche).toLocaleDateString()}</td>
              <td><Link to={`/updateEnseignant/${enseignant.IdEnseignant}`}>
              <button> update</button>
              </Link>
              
              <button onClick={()=>{handleDelete(enseignant.IdEnseignant)}}>supprimer</button>
             
                <Link to={ `/GetEnseignantById/${enseignant.IdEnseignant}`}>
          
            <button>getById</button>
            </Link>
           
              
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
};

export default GetAllEnseignants;
