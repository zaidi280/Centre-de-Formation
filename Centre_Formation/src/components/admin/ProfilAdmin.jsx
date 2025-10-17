import React, { useEffect, useState } from 'react';
import { getProfilAdmin } from "../../Services/AdminController";
import { MDBContainer, MDBRow, MDBCol, MDBCard, MDBCardBody, MDBTypography, MDBSpinner, MDBCardText } from 'mdb-react-ui-kit';

const ProfilAdmin = () => {
  const [adminProfile, setAdminProfile] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const data = await getProfilAdmin(); // Récupérer les données du profil
        setAdminProfile(data); // Stocker les données dans l'état
        setLoading(false); // Fin du chargement
      } catch (err) {
        setError("Impossible de charger le profil"); // Message d'erreur
        setLoading(false); // Fin du chargement
      }
    };

    fetchProfile();
  }, []); // Le tableau vide [] assure que l'effet ne se déclenche qu'une seule fois

  if (loading) {
    return (
      <MDBContainer className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
        <MDBSpinner size="lg" />
      </MDBContainer>
    );
  }

  if (error) {
    return (
      <div className="alert alert-danger" role="alert">
        {error}
      </div>
    );
  }

  return (
    <section className="vh-100" style={{ backgroundColor: '#f4f5f7' }}>
      <MDBContainer className="py-5">
        <MDBRow className="justify-content-center">
          <MDBCol lg="6">
            <MDBCard>
              <MDBCardBody>
                <MDBTypography tag="h2" className="text-center mb-4">Profil Administrateur</MDBTypography>
                {adminProfile && (
                  <div>
                    <div className="mb-3">
                      <MDBTypography tag="h5">Nom:</MDBTypography>
                      <MDBCardText>{adminProfile.nom}</MDBCardText>
                    </div>
                    <div className="mb-3">
                      <MDBTypography tag="h5">Prénom:</MDBTypography>
                      <MDBCardText>{adminProfile.prenom}</MDBCardText>
                    </div>
                    <div className="mb-3">
                      <MDBTypography tag="h5">Email:</MDBTypography>
                      <MDBCardText>{adminProfile.email}</MDBCardText>
                    </div>
                    {/* Vous pouvez ajouter d'autres informations ici */}
                  </div>
                )}
              </MDBCardBody>
            </MDBCard>
          </MDBCol>
        </MDBRow>
      </MDBContainer>
    </section>
  );
};

export default ProfilAdmin;
