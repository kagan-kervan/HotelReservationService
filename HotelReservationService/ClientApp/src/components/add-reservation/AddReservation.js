import './Reservation.css';
import { useAsyncError, useParams } from "react-router-dom";
// import SearchBar from "../../components/searchbar/SearchBar";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCircleArrowLeft,
  faCircleArrowRight,
  faCircleXmark,
  faLocationDot,
} from "@fortawesome/free-solid-svg-icons";
import { Link } from 'react-router-dom';
import axios, { formToJSON } from '../../../node_modules/axios/index';
import React, { useState,useEffect } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

export const AddReservation = () => {
  const {hotelID} = useParams();
  const [roomId,setRoomId] = useState('');
  const [email,setMail] = useState('');
  const [customerID,setCustomerID] = useState();
  const [rooms, setRooms] = useState([]);
  const [total_guest,setTotalGuest] = useState();
  const [checkindate,setCheckInDate] = useState(null);
  const [checkoutdate,setCheckOutDate] = useState(null);

const FetchRooms = async () => {
    try {
        if (checkindate && checkoutdate) {
        const response = await axios.get(`https://localhost:3000/api/Room/get-with-hotelID/${hotelID}?checkin=${checkindate.toISOString()}&checkout=${checkoutdate.toISOString()}`, { timeout: 5000 });
        if (response.status === 204) {
            // Handle case where the response is empty
            alert('No rooms available for the selected dates. Please try different dates or another hotel.');
        }else{
            console.log(response.data);
            setRooms(response.data);
        }
      }
      } catch (error) {
        alert('Cannot get rooms. Please check your calendar selections or try another hotel.');
        console.error("Error fetching rooms:", error);
      }
    };
    useEffect(() => {
        FetchRooms();
        console.log(rooms);
    }, [checkindate, checkoutdate, hotelID]);

const HandleSubmit = async (custID) => {
        try{
            const response = await axios.post("https://localhost:3000/api/Reservation/add/"+roomId+"/"+custID, {
                "CheckInDate":checkindate,
                "CheckOutDate":checkoutdate,
                "Total_Guest_Number": total_guest,
                timeout : 5000,
            })
            console.log(response.data);
        }
        catch(error){
            alert("Couldn't create reservation!");
            console.error("Error creating reservation:", error);
        }
    }

    const CheckForMail = async () => {
        try {
            console.log(email);
            const respon = await axios.get("https://localhost:3000/api/Customer/get-customer?email="+encodeURIComponent(email), { timeout: 5000 });
            const data = respon.data;
            console.log(data);
            if (email === data.email_Address) {
                console.log("Setting customer ID:", data.id);
                setCustomerID(data.id);
                HandleSubmit(data.id);
            } else {
                alert("Given e-mail has not been found in customers.");
                setMail('');
            }
        }
        catch(error){   
            console.error('Error fetching customer data:', error);
        }
    };

    return (
        <div className="auth-form-container">
        <form className="reservation-form">
            <h2 style={{textAlign:"center"}}>ADD RESERVATION</h2>
            <label htmlFor="email">Email</label>
            <input value={email} onChange={(e) => setMail(e.target.value)}type="email" placeholder="Please enter your account's mail" id="email" name="email" />
            <label htmlFor="total_guest">Total Guest</label>
            <input value={total_guest} onChange={(e) => setTotalGuest(e.target.value)} placeholder="Total Guest" id="total_guest"/>
            <div className='checkin-container'>
                <label htmlFor="checkindate">Check-In Date</label>
                <DatePicker
                selected={checkindate}
                 onChange={(date) => setCheckInDate(date)}
                dateFormat="yyyy-MM-dd"
                 placeholderText="Select Date"
                 id="checkindate"
                 />
            </div>
            <div className='checkout-container'>
                <label htmlFor="checkoutdate">Check-Out Date</label>
                <DatePicker
                selected={checkoutdate}
                onChange={(date) => setCheckOutDate(date)}
                dateFormat="yyyy-MM-dd"
                placeholderText="Select Date"
                id="checkoutdate"
                 />
            </div>
            <div className="room-container">
                <label htmlFor="roomId">Room</label>
                <select value={roomId} onChange={(e) => setRoomId(e.target.value)} id="roomId" style={{ marginBottom: "1em" }}>
                <option value="" disabled>Select Room</option>
                {rooms.map(room => (
                 <option key={room.id} value={room.id}>{room.room_Number +" / "+room.roomType.typeName}</option>
                ))}
                </select>
            </div>
            <button type="button" onClick={CheckForMail}>ADD RESERVATION</button>
            <p></p>
            <Link to="/register"> <button type="button" className="btn btn-md btn-outline-danger">You don't have any account? Register here!</button></Link>
        </form>
    </div>
    )
  
}

export default AddReservation;