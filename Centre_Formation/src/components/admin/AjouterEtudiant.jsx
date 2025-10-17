import React, { useState } from 'react';
import { CreerEtudiant } from '../../Services/AdminController';

const AjouterEtudiant = () => {
  // État pour les données de l'étudiant
  const [etudiantData, setEtudiantData] = useState({
    UserName: '',
    Email: '',
    Password: '',
    Nom: '',
    Prenom: '',
    Telephone: '',
    Adresse: '',
    Classe: '',
    Niveau: '',
    DateInscription: '',
  });

  // États pour les messages de succès et d'erreur
  const [successMessage, setSuccessMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  // Fonction pour gérer les changements dans les champs du formulaire
  const handleChange = (e) => {
    const { name, value } = e.target;
    setEtudiantData({ ...etudiantData, [name]: value });
  };

  // Fonction pour gérer la soumission du formulaire
  const handleSubmit = async (e) => {
    e.preventDefault();
    setSuccessMessage(''); // Réinitialiser les messages
    setErrorMessage('');
    try {
      const response = await CreerEtudiant(etudiantData);
      setSuccessMessage('Étudiant créé avec succès !'); // Afficher un message de succès
      console.log('Réponse de l\'API:', response);
    } catch (error) {
      setErrorMessage(
        error || 'Erreur lors de la création de l\'étudiant.' // Afficher le message d'erreur
      );
      console.error(error);
    }
  };

  // Styles CSS en JavaScript
  const styles = {
    container: {
      margin: '20px auto',
      maxWidth: '600px',
      padding: '20px',
      borderRadius: '8px',
      boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
      fontFamily: 'Arial, sans-serif',
    },
    form: {
      display: 'flex',
      flexDirection: 'column',
      gap: '15px',
    },
    input: {
      padding: '10px',
      border: '1px solid #ccc',
      borderRadius: '4px',
      fontSize: '14px',
    },
    button: {
      padding: '10px',
      border: 'none',
      borderRadius: '4px',
      backgroundColor: '#28a745',
      color: '#fff',
      fontSize: '16px',
      cursor: 'pointer',
      marginTop: '10px',
    },
    success: {
      color: '#28a745',
      fontWeight: 'bold',
    },
    error: {
      color: '#dc3545',
      fontWeight: 'bold',
    },
  };

  return (
    <div style={styles.container}>
      <h2>Ajouter un Étudiant</h2>

      {/* Affichage des messages */}
      {successMessage && <div style={styles.success}>{successMessage}</div>}
      {errorMessage && <div style={styles.error}>{errorMessage}</div>}

      <form onSubmit={handleSubmit} style={styles.form}>
        <input
          type="text"
          name="UserName"
          placeholder="Nom d'utilisateur"
          value={etudiantData.UserName}
          onChange={handleChange}
          style={styles.input}
          required
        />
        <input
          type="email"
          name="Email"
          placeholder="Email"
          value={etudiantData.Email}
          onChange={handleChange}
          style={styles.input}
          required
        />
        <input
          type="password"
          name="Password"
          placeholder="Mot de passe"
          value={etudiantData.Password}
          onChange={handleChange}
          style={styles.input}
          required
        />
        <input
          type="text"
          name="Nom"
          placeholder="Nom"
          value={etudiantData.Nom}
          onChange={handleChange}
          style={styles.input}
          required
        />
        <input
          type="text"
          name="Prenom"
          placeholder="Prénom"
          value={etudiantData.Prenom}
          onChange={handleChange}
          style={styles.input}
          required
        />
        <input
          type="tel"
          name="Telephone"
          placeholder="Téléphone"
          value={etudiantData.Telephone}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="text"
          name="Adresse"
          placeholder="Adresse"
          value={etudiantData.Adresse}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="text"
          name="Classe"
          placeholder="Classe"
          value={etudiantData.Classe}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="text"
          name="Niveau"
          placeholder="Niveau"
          value={etudiantData.Niveau}
          onChange={handleChange}
          style={styles.input}
        />
        <input
          type="date"
          name="DateInscription"
          placeholder="Date d'inscription"
          value={etudiantData.DateInscription}
          onChange={handleChange}
          style={styles.input}
        />
        <button type="submit" style={styles.button}>
          Ajouter Étudiant
        </button>
      </form>
    </div>
  );
};

export default AjouterEtudiant;
