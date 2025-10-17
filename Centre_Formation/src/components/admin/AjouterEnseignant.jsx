import React, { useState } from 'react';
import { CreerEnseignant } from '../../Services/AdminController';

const AjouterEnseignant = () => {
  const [enseignantData, setEnseignantData] = useState({
    UserName: '',
    Email: '',
    Password: '',
    Nom: '',
    Prenom: '',
    Telephone: '',
    Adresse: '',
    Specialite: '',
    AnneesExperience: '',
    Diplome: '',
    DateEmbauche: ''
  });

  const [successMessage, setSuccessMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  // Gestion des changements dans les champs de formulaire
  const handleChange = (e) => {
    const { name, value } = e.target;
    setEnseignantData({ ...enseignantData, [name]: value });
  };

  // Soumission du formulaire
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      setSuccessMessage('');
      setErrorMessage('');

      // Validation des champs obligatoires
      if (!enseignantData.UserName || !enseignantData.Email || !enseignantData.Password) {
        setErrorMessage("Veuillez remplir tous les champs obligatoires.");
        return;
      }

      await CreerEnseignant(enseignantData);
      setSuccessMessage("L'enseignant a été créé avec succès !");
      setEnseignantData({
        UserName: '',
        Email: '',
        Password: '',
        Nom: '',
        Prenom: '',
        Telephone: '',
        Adresse: '',
        Specialite: '',
        AnneesExperience: '',
        Diplome: '',
        DateEmbauche: ''
      });
    } catch (error) {
      setErrorMessage(error || "Une erreur s'est produite. Veuillez réessayer.");
    }
  };

  return (
    <div style={styles.container}>
      <h2>Ajouter un Enseignant</h2>
      {successMessage && <p style={styles.success}>{successMessage}</p>}
      {errorMessage && <p style={styles.error}>{errorMessage}</p>}
      <form onSubmit={handleSubmit} style={styles.form}>
        <input
          type="text"
          name="UserName"
          placeholder="Nom d'utilisateur *"
          value={enseignantData.UserName}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="email"
          name="Email"
          placeholder="Email *"
          value={enseignantData.Email}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="password"
          name="Password"
          placeholder="Mot de passe *"
          value={enseignantData.Password}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="text"
          name="Nom"
          placeholder="Nom"
          value={enseignantData.Nom}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="text"
          name="Prenom"
          placeholder="Prénom"
          value={enseignantData.Prenom}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="tel"
          name="Telephone"
          placeholder="Téléphone"
          value={enseignantData.Telephone}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="text"
          name="Adresse"
          placeholder="Adresse"
          value={enseignantData.Adresse}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="text"
          name="Specialite"
          placeholder="Spécialité"
          value={enseignantData.Specialite}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="number"
          name="AnneesExperience"
          placeholder="Années d'expérience"
          value={enseignantData.AnneesExperience}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="text"
          name="Diplome"
          placeholder="Diplôme"
          value={enseignantData.Diplome}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="date"
          name="DateEmbauche"
          placeholder="Date d'embauche"
          value={enseignantData.DateEmbauche}
          onChange={handleChange}
          style={styles.input}
        />
        <button type="submit" style={styles.button}>
          Créer Enseignant
        </button>
      </form>
    </div>
  );
};

export default AjouterEnseignant;

// Styles CSS en JavaScript
const styles = {
  container: {
    margin: '20px auto',
    maxWidth: '600px',
    padding: '20px',
    borderRadius: '8px',
    boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
    fontFamily: 'Arial, sans-serif'
  },
  form: {
    display: 'flex',
    flexDirection: 'column',
    gap: '15px'
  },
  input: {
    padding: '10px',
    border: '1px solid #ccc',
    borderRadius: '4px',
    fontSize: '14px'
  },
  button: {
    padding: '10px',
    border: 'none',
    borderRadius: '4px',
    backgroundColor: '#28a745',
    color: '#fff',
    fontSize: '16px',
    cursor: 'pointer',
    marginTop: '10px'
  },
  success: {
    color: '#28a745',
    fontWeight: 'bold'
  },
  error: {
    color: '#dc3545',
    fontWeight: 'bold'
  }
};