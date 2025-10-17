import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { getAllSalles, deleteSalle } from "../../Services/SalleController";
import { Link } from "react-router-dom";

const GetAllSalles = () => {
  const [error, setError] = useState(null);
  const [salles, setSalles] = useState([]);

  const handleDelete = async (salleId) => {
    try {
      await deleteSalle(salleId);
      alert("Suppression réussie !");
      const updatedSalles = salles.filter(salle => salle.id !== salleId);
      setSalles(updatedSalles);
    } catch (error) {
      console.error(`Erreur lors de la suppression de la salle avec l'ID ${salleId}:`, error);
      alert("Erreur lors de la suppression.");
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getAllSalles();
        console.log(data.$values);
        setSalles(data.$values);
      } catch (err) {
        console.error("Erreur lors de la récupération des salles:", err);
        setError(err.message);
      }
    };
    fetchData();
  }, []);

  return (
    <div className="container mt-5">
      <h1 className="mb-4">Liste des Salles</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Nom de la Salle</th>
            <th>Capacité</th>
            <th>Type de Salle</th>
            <th>Équipement</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {salles.map((salle) => (
            <tr key={salle.IdSalle}>
              <td>{salle.NomSalle}</td>
              <td>{salle.Capacite}</td>
              <td>{salle.TypeSalle}</td>
              <td>{salle.Equipement}</td>
              <td>
                <Link to={`/updateSalle/${salle.IdSalle}`}>
                  <button>Mettre à jour</button>
                </Link>
                <button onClick={() => handleDelete(salle.IdSalle)}>Supprimer</button>
                <Link to={`/getSalleById/${salle.IdSalle}`}>
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

export default GetAllSalles;
