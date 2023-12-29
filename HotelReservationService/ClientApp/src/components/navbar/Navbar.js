import React from "react";
import "./navbar.css";

import {Routes, Route, Link, NavLink} from 'react-router-dom';


const Navbar = ( ) => {

  return (
   
    <div className="navbar">
 
      <div className="navContainer">

        <a className="home" href="https://www.trivago.com.tr/"><i class="bi bi-tencent-qq"></i>Trivago</a>
        
        {/* <button  className="login"><i class="bi bi-person-fill"></i> Log in</button>     */}
        <Link to="/Login" className="login"><i className="bi bi-person-fill"></i> Log in</Link>

      </div>

    </div>
    
  )
}

export default Navbar;