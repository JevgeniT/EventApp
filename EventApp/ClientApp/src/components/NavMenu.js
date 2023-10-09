import React from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import {Link, useNavigate} from 'react-router-dom';
import './NavMenu.css';
import LogInForm from "./LogInForm";

const Nav = () => {
    const navigate = useNavigate();

    const logOut =  () => {
        localStorage.clear();
        setTimeout(() => {
            navigate('/');
        }, 500);
    }
    const jwt = localStorage.getItem('jwt')
    const isLogged = jwt !== null && jwt.length >0;
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light style={{backgroundColor : "#ede9e9"}}>
          <NavbarBrand tag={Link} to="/events">EventApp</NavbarBrand>
          <NavbarToggler className="mr-2" />
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/events">Events</NavLink>
              </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="https://localhost:7012/swagger/index.html">Swagger</NavLink>
                </NavItem>
              {isLogged ? (
                  <NavItem>
                      <NavLink tag={Link} className="text-dark" onClick={logOut}>Log out</NavLink>
                  </NavItem>
              ) : (
                  <NavItem>
                    <LogInForm/>
                  </NavItem>
              )}
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
}
export default Nav;
