import React from "react";
import { Link } from "react-router-dom";
import { Navbar, Nav, Container } from "react-bootstrap";
import { FaSignInAlt, FaUserPlus, FaUserCircle, FaSignOutAlt } from "react-icons/fa";

const MyNavbar = () => {
  return (
    <Navbar bg="dark" variant="dark" >
      <Container>
        <Navbar.Brand href="/">MyApp</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="ml-auto">
            <Link to="/login" className="text-light me-3">
              <FaSignInAlt /> {/* Login Icon */}
            </Link>
            <Link to="/register" className="text-light me-3">
              <FaUserPlus /> {/* Register Icon */}
            </Link>
            <Link to="/profile" className="text-light me-3">
              <FaUserCircle /> {/* Profile Icon */}
            </Link>
            <Link to="/logout" className="text-light">
              <FaSignOutAlt /> {/* Logout Icon */}
            </Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default MyNavbar;
