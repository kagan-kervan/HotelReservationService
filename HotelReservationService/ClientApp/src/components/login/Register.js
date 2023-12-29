import React, { useState } from "react";
import { Link } from 'react-router-dom';
import './Register.css';


export const Register = (props) => {
    const [email, setEmail] = useState('');
    const [pass, setPass] = useState('');
    const [name, setName] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(email);
    }

    return (
        <div className="auth-form-container">
        <form className="register-form" onSubmit={handleSubmit}>
            <h2 style={{textAlign:"center"}}>Register</h2>
            <label htmlFor="name">Full name</label>
            <input value={name} name="name" onChange={(e) => setName(e.target.value)} id="name" placeholder="full Name" />
            <label htmlFor="email">email</label>
            <input value={email} onChange={(e) => setEmail(e.target.value)}type="email" placeholder="youremail@gmail.com" id="email" name="email" />
            <label htmlFor="password">password</label>
            <input value={pass} onChange={(e) => setPass(e.target.value)} type="password" placeholder="********" id="password" name="password" />
            <button type="submit">Log In</button>
            <p></p>
            {/* <Link to="/login">Already have an account? Login here.</Link> */}
            <Link to="/login"> <button type="button" className="btn btn-md btn-outline-danger">Already have an account? Login here.</button></Link>
        </form>
    </div>
    )
}

export default Register;
