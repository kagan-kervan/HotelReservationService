import React from "react";
import "./navbar.css";
import {Routes, Route, Link, NavLink} from 'react-router-dom';


const Navbar = ( ) => {

  return (
   
    <div className="navbar">
 
      <div className="navContainer">

        {/* <a className="home"><i class="bi bi-tencent-qq"></i>Trivago</a> */}
        {/* <Link to="/" className="main"><i className="bi bi-person-fill"></i>Trivago</Link> */}
        <Link to="/"> <a className="home"><i class="bi bi-tencent-qq"></i>Trivago</a></Link>

        {/* <button  className="login"><i class="bi bi-person-fill"></i> Log in</button>     */}
        <Link to="/Login" className="login"><i className="bi bi-person-fill"></i>Log in</Link>
        {/* <Link to="/Login"><i className="bi bi-person-fill"></i><button type="button" className="btn btn-md btn-outline-danger">Log in</button></Link> */}

      </div>

    </div>
    
  )
}

export default Navbar;