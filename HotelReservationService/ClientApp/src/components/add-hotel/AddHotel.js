import React, { useState,useEffect } from "react";
import { Link } from 'react-router-dom';
import './AddHotel.css';
import axios, { formToJSON } from '../../../node_modules/axios/index';

export const AddHotel = (props) => {
    
    //Hotel Attributes
const [name,setName] = useState('');
const [ownerId,setOwnerId] = useState('');
const [addressId,setAddressId] = useState('');
const [totalRoomNum,setTotalRoomNum] = useState('');
const [owners, setOwners] = useState([]);
const [addresses, setAddresses] = useState([]);
//Hotel features attributes.
const [hasWifi, setHasWifi] = useState(false);
const [hasSauna, setHasSauna] = useState(false);
const [hasBeach, setHasBeach] = useState(false);
const [hasSpa, setHasSpa] = useState(false);
const [hasAquapark, setHasAquapark] = useState(false);
const [hasParkingLot, setHasParkingLot] = useState(false);
const [hasRestaurant, setHasRestaurant] = useState(false);
const [hasRoomService, setHasRoomService] = useState(false);
const [hasBar, setHasBar] = useState(false);
const [hasBuffet, setHasBuffet] = useState(false);
const [hasPool, setHasPool] = useState(false);
    
    useEffect(() => {
    // Fetch owners and addresses when the component mounts
    fetchOwners();
    fetchAddresses();
    }, []);

    const handleSubmit = (e) =>{
     e.preventDefault();
     const totalRoomNumInt = parseInt(totalRoomNum, 10);
        axios.post("https://localhost:3000/api/Hotel/add-hotel/"+ownerId+'/'+addressId,
         {
            "HotelName" : name,
            "total_room_num": totalRoomNumInt,
            "full_room_num": 0,
      }).then(response => {
        console.log(response.data)
        const createdHotelId = response.data.id;
        // Use the created hotel ID to create features
        createFeatures(createdHotelId);
    }).catch(error => {console.error("error postin",error)});
    }
    const fetchOwners = async () => {
        try {
          const response = await axios.get("https://localhost:3000/api/Owner/get-all", { timeout: 5000 });
          setOwners(response.data);
        } catch (error) {
          console.error("Error fetching owners:", error);
        }
      };
    
      const fetchAddresses = async () => {
        try {
          const response = await axios.get("https://localhost:3000/api/Address/get-all", { timeout: 5000 });
          setAddresses(response.data);
        } catch (error) {
          console.error("Error fetching addresses:", error);
        }
      };

      const createFeatures = (hotelId) => {
        // Add logic to create features using the created hotel ID
        // Example:
        axios.post(`https://localhost:3000/api/Feature/add-features/${hotelId}`, {
          // Include boolean attributes in the request body
          "hasWifi": hasWifi,
          "hasSauna": hasSauna,
          "hasBeach": hasBeach,
          "hasBar": hasBar,
          "hasPool": hasPool,
          "hasAquapark": hasAquapark,
          "hasRoomService": hasRoomService,
          "hasRestaurant": hasRestaurant,
          "hasParkingLot": hasParkingLot,
          "hasSpa": hasSpa,
          "hasBuffet": hasBuffet,
          // Add more boolean attributes as needed
        })
          .then(response => {
            console.log('Features added successfully:', response.data);
          })
          .catch(error => {
            console.error('Error adding features:', error);
          });
      };

    return (
        <div className="auth-form-container">
        <form className="register-form" onSubmit={handleSubmit}>
            <h2 style={{textAlign:"center"}}>Add Hotel</h2>
            <label htmlFor="name">Hotel Name</label>
            <input value={name} name="name" onChange={(e) => setName(e.target.value)} id="name" placeholder="Hotel name" />
            <label htmlFor="totalRoomNum">Total Room Numbers</label>
            <input value={totalRoomNum} totalRoomNum="Total Room Num" onChange={(e) => setTotalRoomNum(e.target.value)} id="totalRoomNum" placeholder="TotalRoomNum" />
            <label htmlFor="ownerId">Owner</label>
            <select value={ownerId} onChange={(e) => setOwnerId(e.target.value)} id="ownerId">
            <option value="" disabled>Select owner</option>
            {owners.map(owner => (
                <option key={owner.id} value={owner.id}>{owner.name+" "+owner.surname}</option>
            ))}
            </select>

          {/* Wrap address-related elements in a div */}
          <div className="address-container">
            <label htmlFor="addressId">Address</label>
            <select value={addressId} onChange={(e) => setAddressId(e.target.value)} id="addressId" style={{ marginBottom: "1em" }}>
            <option value="" disabled>Select address</option>
            {addresses.map(address => (
              <option key={address.id} value={address.id}>{address.city + ", " + address.country + " / " + address.street}</option>
            ))}
            </select>
            <Link to="/add-address"> <button type="button" className="btn btn-md btn-outline-danger"><b>ADD NEW ADDRESS</b></button></Link>
          </div>
            <h3 style={{textAlign:"center"}}>Features</h3>
            <label htmlFor="hasWifi"> Wifi
            <input type="checkbox" checked={hasWifi} onChange={() => setHasWifi(!hasWifi)} id="hasWifi" /></label>
            <label htmlFor="hasSauna">Sauna
            <input type="checkbox" checked={hasSauna} onChange={() => setHasSauna(!hasSauna)} id="hasSauna" /></label>
            <label htmlFor="hasBeach">Beach
            <input type="checkbox" checked={hasBeach} onChange={() => setHasBeach(!hasBeach)} id="hasBeach" /></label>
            <label htmlFor="hasSpa">Spa
            <input type="checkbox" checked={hasSpa} onChange={() => setHasSpa(!hasSpa)} id="hasSpa" /></label>
            <label htmlFor="hasAquapark">Aquapark
            <input type="checkbox" checked={hasAquapark} onChange={() => setHasAquapark(!hasAquapark)} id="hasAquapark" /></label>
            <label htmlFor="hasPool">Pool
            <input type="checkbox" checked={hasPool} onChange={() => setHasPool(!hasPool)} id="hasPool" /></label>
            <label htmlFor="hasBar">Bar
            <input type="checkbox" checked={hasBar} onChange={() => setHasBar(!hasBar)} id="hasBar" /></label>
            <label htmlFor="hasParkingLot">ParkingLot
            <input type="checkbox" checked={hasParkingLot} onChange={() => setHasParkingLot(!hasParkingLot)} id="hasParkingLot" /></label>
            <label htmlFor="hasRoomService">RoomService
            <input type="checkbox" checked={hasRoomService} onChange={() => setHasRoomService(!hasRoomService)} id="hasRoomService" /></label>
            <label htmlFor="hasRestaurant">Restaurant
            <input type="checkbox" checked={hasRestaurant} onChange={() => setHasRestaurant(!hasRestaurant)} id="hasRestaurant" /></label>
            <label htmlFor="hasBuffet">Buffet
            <input type="checkbox" checked={hasBuffet} onChange={() => setHasBuffet(!hasBuffet)} id="hasBuffet" /></label>
            <button type="submit" onClick={handleSubmit}>ADD</button>
            <p></p>
        </form>
    </div>
    )
}

export default AddHotel;