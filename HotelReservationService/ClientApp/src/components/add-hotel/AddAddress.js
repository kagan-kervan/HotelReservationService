import './AddAddress.css';
import React, { useState } from "react";
import { Link } from 'react-router-dom';
import axios, { formToJSON } from '../../../node_modules/axios/index';
import { post } from 'jquery';

export const AddAddress = (props) =>  
{
    const [country,setCountry] = useState('');
    const[city,setCity] = useState('');
    const[region,setRegion] = useState('');
    const[street,setStreet] = useState('');
    const[postalCode,setPostalCode] = useState(1);
    const[no,setNo] = useState(1);
    
    const handleSubmit  = async (e) => {
        e.preventDefault();
        console.log(country);
        console.log(city);
        console.log(region);
        console.log(street);
        console.log(postalCode);
        console.log(no);
        console.log(e);
        //POST EXAMPLE
        try {
            // POST request example
            const response = await axios.post("https://localhost:3000/api/Address/add-address", {
                "City": city,
                "Region": region,
                "PostalCode": postalCode,
                "Country": country,
                "Street": street,
                "No": no,
            });

            console.log(response.data);

            // Redirect to "/add-room" after successful submission
            window.location.href = '/add-hotel';
        } catch (error) {
            console.error("Error posting:", error);
        }

    }
    return (
        <div className="auth-form-container">
        <form className="add-address-form" onSubmit={handleSubmit}>
            <h2 style={{textAlign:"center"}}>Add Address</h2>
            <label htmlFor="city">City</label>
            <input value={city} city="city" onChange={(e) => setCity(e.target.value)} id="city" placeholder="City" />
            <label htmlFor="country">Country</label>
            <input value={country} counrty="country" onChange={(e) => setCountry(e.target.value)} id="country" placeholder="Country" />
            <label htmlFor="region">Region</label>
            <input value={region} onChange={(e) => setRegion(e.target.value)} placeholder="Region" id="region"/>
            <label htmlFor="Street">Street</label>
            <input value={street} street="street" onChange={(e) => setStreet(e.target.value)} id="street" placeholder="Street" />
            <label htmlFor="postalCode">Postal Code</label>
            <input value={postalCode} postalCode="postalCode" onChange={(e) => setPostalCode(e.target.value)} id="postalCode" placeholder="Postal Code" />
            <label htmlFor="no">Building NO</label>
            <input value={no} no="no" onChange={(e) => setNo(e.target.value)} id="no" placeholder="NO" />
            <button type="submit" onClick={handleSubmit}>ADD</button>
            <p></p>
            <Link to="/add-hotel"> <button type="button" className="btn btn-md btn-outline-danger"><b>BACK TO HOTEL PAGE</b></button></Link>
        </form>
    </div>
    )

}
export default AddAddress;