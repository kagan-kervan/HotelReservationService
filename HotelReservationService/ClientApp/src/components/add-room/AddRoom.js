import React, { useState,useEffect } from "react";
import { Link, useParams } from 'react-router-dom';
import './AddRoom.css';
import axios, { formToJSON } from '../../../node_modules/axios/index';

export const AddRoom = (props) => {
  const {ownerID} = useParams();
const [roomNumber,setRoomNumber] = useState('');
const [available,setAvailable] = useState('false');
const [typeId,setTypeId] = useState('');
const [types, setTypes] = useState([]);
const [hotelId,setHotelId] = useState('');
const [hotels, setHotels] = useState([]);
const [selectedRoomType, setSelectedRoomType] = useState('');


useEffect(() => {
    // Fetch owners and addresses when the component mounts
    fetchHotels();
    fetchTypes();
    }, []);


const handleSubmit = (e) => {
    e.preventDefault();
    const tempInt = parseInt(roomNumber,10);
    const isAvailableBool = available === true;
    axios.post("https://localhost:3000/api/Room/add/"+hotelId+'/'+typeId,
    {
       "Room_Number" : tempInt,
       "isAvailable": isAvailableBool,
 }).then(response => {
   console.log(response.data)
}).catch(error => {console.error("error postin",error)});
}

const fetchTypes = async () => {
    try {
      const response = await axios.get("https://localhost:3000/api/RoomType/get-all", { timeout: 5000 });
      setTypes(response.data);
    } catch (error) {
      console.error("Error fetching types:", error);
    }
  };
  
  //Bunun yerine hotel id çekmek lazım
const fetchHotels = async () => {
    try {
      const response = await axios.get("https://localhost:3000/api/Hotel/get-hotel-with-owner-id/"+ownerID, { timeout: 5000 });
      setHotels(response.data);
    } catch (error) {
      console.error("Error fetching hotels:", error);
    }
  };
  
  return (
    <div className="auth-form-container">
    <form className="register-form" onSubmit={handleSubmit}>
        <h2 style={{textAlign:"center"}}>Add Room</h2>
        <label htmlFor="roomNumber">Room Number</label>
        <input value={roomNumber} roomNumber="roomNubmer" onChange={(e) => setRoomNumber(e.target.value)} id="roomNumber" placeholder="Room Number" />
        <label htmlFor="available">Available
        <input type="checkbox" checked={available} onChange={() => setAvailable(!available)} id="available" /></label>
        <div className="hotel-container">
         <label htmlFor="hotelId">Hotel</label>
          <select value={hotelId} onChange={(e) => setHotelId(e.target.value)} id="hotelId">
          <option value="" disabled>Select Hotel</option>
          {hotels.map(hotel => (
              <option key={hotel.id} value={hotel.id}>{hotel.hotelName}</option>
          ))}
          </select>
          <Link to="/add-hotel"><button type="button" className="btn btn-md btn-outline-danger"><b>ADD NEW HOTEL</b></button></Link>
        </div>
        <div className="type-container">
          <label htmlFor="typeId">Type : </label>
          <select value={typeId} onChange={(e) => setTypeId(e.target.value)} id="typeId">
          <option value="" disabled>Select Type</option>
          {types.map(type => (
          <option key={type.id} value={type.id}>{type.typeName}</option>
          ))}
          </select>
          <Link to="/add-room-type"><button type="button" className="btn btn-md btn-outline-danger"><b>ADD NEW TYPE</b></button></Link>
        </div>
        <button type="submit" onClick={handleSubmit}>ADD</button>
        <p></p>
    </form>
</div>
)


}
export default AddRoom;