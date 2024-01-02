import React , { useState,useEffect } from "react";
import { Link, useParams } from 'react-router-dom';
import './owner.css';
import axios from 'axios';

export const GetOwner = () => {
    const {ownerID} = useParams();
    const [owner,setOwner] = useState('{}');
    const [hotels, setHotels] = useState([]);

    const GetOwnerInfo = async() => {
        try{
            const response = await axios.get("https://localhost:3000/api/Owner/get/"+ownerID,{
                timeout: 5000,
            });
            setOwner(response.data);
            console.log(response.data);
        }
        catch(error){
            console.error('Error getting owner.',error);
        }
    }
    const GetHotels = async() => {
        try{
            const response = await axios.get("https://localhost:3000/api/Hotel/get-hotel-with-owner-id/"+ownerID,{
                timeout: 5000,
            });
            console.log(response.data);
            setHotels(response.data);
        }
        catch(error){
            console.error("Error getting hotels.",error);
        }
    }   
useEffect(() => {
    // Fetch cusotmer and reservations when the component mounts
    GetOwnerInfo(ownerID);
    GetHotels();
    }, [ownerID]);

    const RemoveHotel = async (hotelID) => {
        try{
            const response = await axios.delete("/api/Hotel/delete-hotel/"+hotelID,{
                timeout: 5000,
            });
            console.log(response.data);
            alert('Succesfully deleted.');
            GetHotels();
        }
        catch(error){
            console.error('Error removing hotels.',error);
        }
    }
    return (
        <div className="owner-container">
          {/* Owner information */}
          <div className="customer-info">
            <p>Name: {owner.name+" "+owner.surname}</p>
            <p>Email: {owner.email_Address}</p>
            <p>Phone: {owner.phone}</p>
          </div>
      
          {/* Hotels */}
          <div className="hotels">
            {hotels.map(hotel => (
              <div key={hotel.id} className="hotel">
                <p>Name : {hotel.hotelName}</p>
                <p>Address : {hotel.hotelAddress.street+" / "+hotel.hotelAddress.city+" , "+hotel.hotelAddress.country}</p>
                <p>Total Rooms: {hotel.total_room_number}</p>
                {/* Additional reservation details */}
                <button className="remove-button" onClick={() => RemoveHotel(hotel.id)}>Remove Hotel</button>
                <Link to={`/update-hotel/${hotel.id}`}><button className="update-button">Update</button></Link>
              </div>
            ))}
            <Link to={`/add-room/${ownerID}`}><button className="add-button">Add Room to Hotels</button></Link>
          </div>
        </div>
      );

}
export default GetOwner;