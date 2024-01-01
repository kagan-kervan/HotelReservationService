import React , { useState,useEffect } from "react";
import { Link, useParams } from 'react-router-dom';
import './customer.css';
import axios from 'axios';

const GetCustomer = () => {
    const {customerID} = useParams();
    const [customer,SetCustomer] = useState('{}');
    const [reservations, setReservations] = useState([]);
const GetCustomerInfo = async() => {
    try{
        const response = await axios.get("https://localhost:3000/api/Customer/get/"+customerID,{
            timeout: 5000,
        });
        SetCustomer(response.data);
        console.log(response.data);
    }
    catch(error){
        console.error('Error getting customer.',error);
    }
}
const GetReservations = async () => {
    try{
        const response = await axios.get("https://localhost:3000/api/Reservation/get-by-customerID/"+customerID,{
            timeout: 5000,
        });
        console.log(response.data);
        setReservations(response.data);
    }
    catch(error){
        console.error("Error getting reservations.",error);
    }
}
useEffect(() => {
    // Fetch cusotmer and reservations when the component mounts
    GetCustomerInfo(customerID);
    GetReservations();
    }, [customerID]);

const RemoveReservation = async (reservID) => {
    try{
        const response = await axios.delete("/api/Reservation/delete/"+reservID,{
            timeout: 5000,
        });
        console.log(response.data);
        alert('Succesfully deleted.');
        GetReservations();
    }
    catch(error){
        console.error('Error removing reservation.',error);
    }
}

return (
    <div className="reservation-container">
      {/* Customer information */}
      <div className="customer-info">
        <p>Name: {customer.name+" "+customer.surname}</p>
        <p>Email: {customer.email_Address}</p>
        <p>Phone: {customer.phone}</p>
      </div>
  
      {/* Reservations */}
      <div className="reservations">
        {reservations.map(reservation => (
          <div key={reservation.id} className="reservation">
            <p>Check-In Date: {new Date(reservation.checkInDate).toLocaleString('en-UK', { year: 'numeric', month: 'numeric', day: 'numeric'})}</p>
            <p>Check-Out Date: {new Date(reservation.checkOutDate).toLocaleString('en-UK', { year: 'numeric', month: 'numeric', day: 'numeric'})}</p>
            <p>Total Guests: {reservation.total_Guest_Number}</p>
            {/* Additional reservation details */}
            <button className="remove-button" onClick={() => RemoveReservation(reservation.id)}>Remove Reservation</button>
          </div>
        ))}
      </div>
    </div>
  );

}
export default GetCustomer;
