import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'; // Importer Router et Routes
import './App.css'; // Importer le fichier CSS pour le styling
import Login from './components/authentification/Login'; // Importer le composant Login
import Dashboard from './components/admin/Dashboard';
import Logout from './components/authentification/Logout';
import RegisterForm from './components/authentification/Register';
import MyNavbar from './components/MyNavbar';
import 'bootstrap/dist/css/bootstrap.min.css';
import GetAllEtudiants from './components/Etudiant/GetAllEtudiants'; // Importer le composant GetAllEtudiants
import GetEtudiantById from './components/Etudiant/GetEtudiantById'; // Importer le composant GetEtudiantById
import UpdateEtudiant from './components/Etudiant/UpdateEtudiant'; // Importer le composant UpdateEtudiant
// import DeleteEtudiant from './components/Etudiant/DeleteEtudiant'; // Importer le composant DeleteEtudiant
import GetAllEnseignants from './components/Enseignant/GetAllEnseignants';
import  AjouterEnseignant  from './components/admin/AjouterEnseignant';
import AjouterEtudiant from './components/admin/AjouterEtudiant';
import GetEnseignantById from './components/Enseignant/GetEnseignantById';
import UpdateEnseignant from './components/Enseignant/UpdateEnseignant';



import GetAllSalles from './components/Salle/GetAllSalles';
import GetSalleById from "./components/Salle/GetSalleById";
import AddSalle from "./components/Salle/AddSalle"; 
import UpdateSalle from "./components/Salle/UpdateSalle";


import GetAllMatieres from './components/Matiere/GetAllMatieres'; 
import GetMatiereById from './components/Matiere/GetMatiereById'; 
import AddMatiere from "./components/Matiere/AddMatiere";
 import UpdateMatiere from "./components/Matiere/UpdateMatiere";



import GetAllCours from "./components/Cour/GetAllCours"; 
import GetCourById from "./components/Cour/GetCourById";
import AddCour from "./components/Cour/AddCour";
import UpdateCour from "./components/Cour/UpdateCour";


import GetAllFormations from "./components/Formation/GetAllFormations";
 import GetFormationById from "./components/Formation/GetFormationById"; 
 import AddFormation from "./components/Formation/AddFormation"; 
import UpdateFormation from "./components/Formation/UpdateFormation";




function App() {
  return (
    <Router>
      <MyNavbar/>
      <Routes>
        {/* Routes protégées */}
        {/* <Route element={<ProtectedRoutes />}> */}
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/logout" element={<Logout />} />
        {/* </Route> */}

        {/* Routes non protégées */}
       
        <Route path="/CreerEnseignant" element={<AjouterEnseignant />} />
        CreerEtudiant
        <Route path="/CreerEtudiant" element={<AjouterEtudiant />} />
        <Route path="/login" element={<Login/>} />
        <Route path="/register" element={<RegisterForm />} />
       



 
        {/* Routes pour Enseignants */}
        <Route path="/GetAllEnseignants" element={<GetAllEnseignants />} />
        <Route path="/GetEnseignantById/:id" element={<GetEnseignantById />} />
        {/* Mise à jour et suppression */}
        <Route path="/updateEnseignant/:id" element={<UpdateEnseignant />} />






        {/* Routes pour la gestion des étudiants */}
        <Route path="/getAllEtudiants" element={<GetAllEtudiants />} />  {/* Liste des étudiants */}
        <Route path="/getEtudiantById/:id" element={<GetEtudiantById />} />  {/* Détails d'un étudiant */}
        <Route path="/updateEtudiant/:id" element={<UpdateEtudiant />} />  {/* Mettre à jour un étudiant*/}



        {/* Routes pour la gestion des salles */}
        <Route path="/salles" element={<GetAllSalles/>} /> 
        <Route path="/getSalleById/:id" element={<GetSalleById/>} /> 
        <Route path="/addSalle" element={<AddSalle/>} /> 
        <Route path="/updateSalle/:id" element={<UpdateSalle/>} />

        <Route path="/matieres" element={<GetAllMatieres/>} />
         <Route path="/GetMatiereById/:id" element={<GetMatiereById/>} /> 
         <Route path="/addMatiere" element={<AddMatiere/>} />
         <Route path="/updateMatiere/:id" element={<UpdateMatiere/>} />




         <Route path="/cours" element={<GetAllCours/>} /> 
         <Route path="/GetCourById/:id" element={<GetCourById/>} />
          <Route path="/addCour" element={<AddCour/>} />
          <Route path="/updateCour/:id" element={<UpdateCour/>} />

          <Route path="/formations" element={<GetAllFormations/>} />
           <Route path="/GetFormationById/:id" element={<GetFormationById/>} />
            <Route path="/addFormation" element={<AddFormation/>} />
           <Route path="/updateFormation/:id" element={<UpdateFormation/>} />



      </Routes>
    </Router>
  );
}

export default App;