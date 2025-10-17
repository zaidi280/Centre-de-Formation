import React, { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { signin } from "../../Services/authservice";

import {
  MDBBtn,
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBInput,
  MDBIcon
} from "mdb-react-ui-kit";

const AuthForm = () => {
  const navigate = useNavigate();
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!userName || !password) {
      alert("Veuillez remplir tous les champs !");
      return;
    }

    const objetuser = { userName, password };

    try {
      const result = await signin(objetuser);
      const { token, username, role } = result;

      if (token && username && role) {
        localStorage.setItem("CC_Token", token);
        localStorage.setItem("user", JSON.stringify(username));

        if (role === "Admin") {
          navigate("/dashboard");
        } else {
          navigate("/");
        }
      } else {
        alert("Données de connexion invalides ou manquantes !");
      }
    } catch (error) {
      console.error("Erreur de connexion :", error);
      alert(
        error.response?.data?.message ||
          "Une erreur est survenue lors de la connexion."
      );
    }
  };

  return (
    <MDBContainer fluid className="p-5 background-radial-gradient overflow-hidden">
      <MDBRow>
        <MDBCol
          md="6"
          className="text-center text-md-start d-flex flex-column justify-content-center"
        >
          <h1 className="my-5 display-3 fw-bold ls-tight px-3" style={{ color: "hsl(218, 81%, 95%)" }}>
            Bienvenue ! <br />
            <span style={{ color: "hsl(218, 81%, 75%)" }}>Connectez-vous à votre compte</span>
          </h1>
          <p className="px-3" style={{ color: "hsl(218, 81%, 85%)" }}>
            Accédez à nos fonctionnalités avancées et découvrez tout ce que nous avons à offrir.
          </p>
        </MDBCol>

        <MDBCol md="6" className="position-relative">
          <div id="radius-shape-1" className="position-absolute rounded-circle shadow-5-strong"></div>
          <div id="radius-shape-2" className="position-absolute shadow-5-strong"></div>

          <MDBCard className="my-5 bg-glass">
            <MDBCardBody className="p-5">
              <form onSubmit={handleSubmit}>
                <MDBInput
                  wrapperClass="mb-4"
                  label="Nom d'utilisateur"
                  id="userName"
                  type="text"
                  value={userName}
                  onChange={(e) => setUserName(e.target.value)}
                  required
                />
                <MDBInput
                  wrapperClass="mb-4"
                  label="Mot de passe"
                  id="password"
                  type="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  required
                />
                <MDBBtn className="w-100 mb-4" size="md" type="submit">
                  Se connecter
                </MDBBtn>
              </form>
              <div className="text-center">
                <p>ou connectez-vous avec :</p>
                <MDBBtn tag="a" color="none" className="mx-3" style={{ color: "#1266f1" }}>
                  <MDBIcon fab icon="facebook-f" size="sm" />
                </MDBBtn>
                <MDBBtn tag="a" color="none" className="mx-3" style={{ color: "#1266f1" }}>
                  <MDBIcon fab icon="twitter" size="sm" />
                </MDBBtn>
                <MDBBtn tag="a" color="none" className="mx-3" style={{ color: "#1266f1" }}>
                  <MDBIcon fab icon="google" size="sm" />
                </MDBBtn>
                <MDBBtn tag="a" color="none" className="mx-3" style={{ color: "#1266f1" }}>
                  <MDBIcon fab icon="github" size="sm" />
                </MDBBtn>
              </div>
              <div className="text-center mt-4">
                <Link to="/register">Vous navez pas de compte ? Inscrivez-vous</Link>
              </div>
            </MDBCardBody>
          </MDBCard>
        </MDBCol>
      </MDBRow>
    </MDBContainer>
  );
};

export default AuthForm;
