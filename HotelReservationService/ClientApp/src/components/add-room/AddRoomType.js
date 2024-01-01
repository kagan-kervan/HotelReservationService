import './AddRoomType.css';import React, { useState } from "react";
import { Link } from 'react-router-dom';
import axios, { formToJSON } from '../../../node_modules/axios/index';
import { useHistory } from 'react-router';

export const AddRoomType = (props) =>  
{
    const [typeName,setTypeName] = useState('');
    const[desc,setDesc] = useState('');
    const[price,setPrice] = useState(0);
    const[capacity,setCApacity] = useState(1);
    
    const handleSubmit  = async (e) => {
        e.preventDefault();
        console.log(typeName);
        console.log(desc);
        console.log(price);
        console.log(capacity);
        console.log(e);
        //POST EXAMPLE
        try {
            // POST request example
            const response = await axios.post("https://localhost:3000/api/RoomType/add", {
                "TypeName": typeName,
                "Description": desc,
                "Price": price,
                "Capacity": capacity,
            });

            console.log(response.data);

            // Redirect to "/add-room" after successful submission
            window.location.href = '/add-room';
        } catch (error) {
            console.error("Error posting:", error);
        }

    }
    return (
        <div className="auth-form-container">
        <form className="add-type-form" onSubmit={handleSubmit}>
            <h2 style={{textAlign:"center"}}>Add Room Type</h2>
            <label htmlFor="typename">Name</label>
            <input value={typeName} TypeName="typeName" onChange={(e) => setTypeName(e.target.value)} id="typeName" placeholder="Type Name" />
            <label htmlFor="description">Description</label>
            <input value={desc} desc="desc" onChange={(e) => setDesc(e.target.value)} id="desc" placeholder="Description" />
            <label htmlFor="price">Price</label>
            <input value={price} onChange={(e) => setPrice(e.target.value)} placeholder="price" id="price"/>
            <label htmlFor="capacity">Capacity</label>
            <input value={capacity} capacity="capacity" onChange={(e) => setCApacity(e.target.value)} id="capacity" placeholder="Capacity" />
            <button type="submit" onClick={handleSubmit}>ADD</button>
            <p></p>
            <Link to="/add-room"> <button type="button" className="btn btn-md btn-outline-danger"><b>BACK TO ROOM PAGE</b></button></Link>
        </form>
    </div>
    )

}
export default AddRoomType;