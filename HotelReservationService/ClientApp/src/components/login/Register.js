import React, { useState } from "react";
import { Link } from 'react-router-dom';
import './Register.css';
import axios, { formToJSON } from '../../../node_modules/axios/index';

export const Register = (props) => {
    const [email, setEmail] = useState('');
    const [pass, setPass] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [phone, setPhone] = useState('');
    const [accountType, setAccountType] = useState('customer');

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(name);
        console.log(surname);
        console.log(email);
        console.log(phone);
        console.log(pass);
        console.log(e);
        if(accountType == 'customer'){

            axios.post("https://localhost:3000/api/Customer/add-customer",
            {
               "name": name,
               "surname": surname,
               "email_Address": email,
               "password": pass,
               "phone": phone
            }).then(response => {console.log(response.data)}).catch(error => {console.error("error postin",error)});
        }
        else{
            axios.post("https://localhost:3000/api/Owner/add",
            {
               "name": name,
               "surname": surname,
               "email_Address": email,
               "password": pass,
               "phone": phone
            }).then(response => {console.log(response.data)}).catch(error => {console.error("error postin",error)});
        }
    }

    return (
        <div className="auth-form-container">
        <form className="register-form" onSubmit={handleSubmit}>
            <h2 style={{textAlign:"center"}}>Register</h2>
            <label htmlFor="name">Name</label>
            <input value={name} name="name" onChange={(e) => setName(e.target.value)} id="name" placeholder="Name" />
            <label htmlFor="surname">Surname</label>
            <input value={surname} surname="surname" onChange={(e) => setSurname(e.target.value)} id="surname" placeholder="Surname" />
            <label htmlFor="email">Email</label>
            <input value={email} onChange={(e) => setEmail(e.target.value)}type="email" placeholder="youremail@gmail.com" id="email" name="email" />
            <label htmlFor="phone">PhoneNumber</label>
            <input value={phone} phone="phone" onChange={(e) => setPhone(e.target.value)} id="phone" placeholder="Phone" />
            <label htmlFor="password">Password</label>
            <input value={pass} onChange={(e) => setPass(e.target.value)} type="password" placeholder="********" id="password" name="password" />
            <label>
            Hesap Türü:
            <select
              value={accountType}
              onChange={(e) => setAccountType(e.target.value)}
            >
              <option value="customer">Kullanıcı</option>
              <option value="owner">Sahip</option>
            </select>
          </label>
            <button type="submit" onClick={handleSubmit}>Register</button>
            <p></p>
            {/* <Link to="/login">Already have an account? Login here.</Link> */}
            <Link to="/login"> <button type="button" className="btn btn-md btn-outline-danger">Already have an account? Login here.</button></Link>
        </form>
    </div>
    )
}

export default Register;
