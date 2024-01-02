import React, { useEffect, useState } from "react";
import { Link, parsePath, useParams } from 'react-router-dom';
import './UpdateHotel.css';
import axios from 'axios';

export const UpdateHotel = (props) => {
    const {hotelID} = useParams();
    const [hotel, SetHotel] = useState('{}');
    const [addresses,setAddresses] = useState([]);
    const [updatedHotel, setUpdatedHotel] = useState({
        hotelName: null,
        total_room_number: null,
        full_room_number: null,
        overall_score:null,
        hotelAddressId: null,
    });

    const HandleUpdate = async () => {
        try {
            // Filter out null values and create a new object
            const filteredUpdatedOwner = Object.fromEntries(Object.entries(updatedHotel).filter(([key, value]) => value !== null));

            // Make a POST request with the updated owner information
            const response = await axios.put(`https://localhost:3000/api/Hotel/Update-hotel/`+hotelID, filteredUpdatedOwner);
            if(updatedHotel.hotelAddressId != hotel.hotelAddressId)
            {
                const resp = await axios.put(`https://localhost:3000/api/Hotel/update-hotel-address/`+hotelID+"/"+updatedHotel.hotelAddressId, filteredUpdatedOwner);
                console.log(resp.data);
            }

            console.log(response.data);
            alert('Successfully updated.');
        } catch (error) {
            console.error('Error updating owner.', error);
        }
    }

    const FetchHotelInfo = async () => {
        const response = await axios.get("/api/Hotel/get-hotel-id/"+hotelID,{
            timeout : 5000,
        });
        SetHotel(response.data);
        console.log(response.data);
    }
    const fetchAddresses = async () => {
        try {
          const response = await axios.get("https://localhost:3000/api/Address/get-all", { timeout: 5000 });
          setAddresses(response.data);
        } catch (error) {
          console.error("Error fetching addresses:", error);
        }
      };
    useEffect(() => {
        // Fetch hotel info 
        FetchHotelInfo();
        fetchAddresses();
        }, [hotelID]);

        return(
            <div className="update-hotel">
                <p>Update Hotel Information:</p>
                <label>Name: <input type="text" onChange={(e) => setUpdatedHotel({ ...updatedHotel, hotelName: e.target.value })} /></label>
                <label>Total Rooms: <input type="text" onChange={(e) => setUpdatedHotel({ ...updatedHotel, total_room_number: e.target.value })} /></label>
                <div className="address-container">
                    <label htmlFor="addressId">Address</label>
                    <select onChange={(e) => setUpdatedHotel({ ...updatedHotel, hotelAddressId: e.target.value })} id="addressId" style={{ marginBottom: "1em" }}>
                    <option value="" disabled>Select address</option>
                    {addresses.map(address => (
                    <option key={address.id} value={address.id}>{address.city + ", " + address.country + " / " + address.street}</option>
                    ))}
                    </select>
                </div>
                <button onClick={HandleUpdate}>Update</button>

            </div>
        );
}
export default UpdateHotel;