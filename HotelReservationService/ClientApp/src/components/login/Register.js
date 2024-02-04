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

    
    const isValidPhoneNumber = (value) => {
      // Simple regex for validating a numeric phone number with optional spaces, dashes, or parentheses
      const phoneRegex = /^[0-9\s\-()+]+$/;
      return phoneRegex.test(value);
  };

    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log(name);
        console.log(surname);
        console.log(email);
        console.log(phone);
        console.log(pass);
        if (!isValidPhoneNumber(phone)) {
          alert('Please enter a valid phone number.');
          return;
      }
        if(accountType == 'customer'){
          try{
            
            const response = await axios.post("https://localhost:3000/api/Customer/add-customer",
            {
               "name": name,
               "surname": surname,
               "email_Address": email,
               "password": pass,
               "phone": phone
            });
            //console.log(response);
            if(response.status == 200){
              alert('Successfully created user!');
            }
            else if(response.status == 400)
              alert('Invalid email or password');
          } catch (error) {
            console.error('Error making the request:', error);
            alert('Invalid email or password.');
            // Handle other errors if needed
        }
        }
        else{
          try{
            const response = await axios.post("https://localhost:3000/api/Owner/add",
            {
               "name": name,
               "surname": surname,
               "email_Address": email,
               "password": pass,
               "phone": phone
            });
            
            if(response.status == 200){
              alert("Successfully created user!");
            }
            else if(response.status == 500)
              alert("Invalid email or password");
          } catch (error) {
            console.error('Error making the request:', error);
            alert('Invalid email or password.');
            // Handle other errors if needed
        }
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
            <input
                    value={phone}
                    onChange={(e) => setPhone(e.target.value)}
                    type="text" // Use text type to allow numeric characters
                    pattern="[0-9\s\-()+]*" // Allow only numeric characters, spaces, dashes, and parentheses
                    placeholder="Phone"
                    id="phone"
                    name="phone"
                    required
                />
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
