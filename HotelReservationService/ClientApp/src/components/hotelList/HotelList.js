import React from 'react';
import { Link } from 'react-router-dom';
import './hotellist.css'

const HotelList = (props) => {

    return (
        
        <div className="row">

            {props.hotels.map((hotel) => (

                <div className="col-lg-4" key={hotel.id}>
                    <div className="card mb-4 shadow-sm">
                    {hotel.images && hotel.images.length > 0 ? (
                        <img src={hotel.images[0].pic_Url} className="card-img-top" alt="Sample Hotel" />
                    ): 
                    (
                        <img src="fallback-image-url.jpg" className="card-img-top" alt="Sample Hotel" />
                    )}
                        <div className="card-body">
                            <h5 className="card-title">{hotel.hotelName}</h5>
                            <p className="card-text">{hotel.hotelAddress.city+" , "+hotel.hotelAddress.country}</p>
                            <p className="card-text">{hotel.hotelOwner.name+" "+hotel.hotelOwner.surname}</p>
                            <p className="card-text">{"Empty Rooms: "+(hotel.total_room_number - hotel.full_room_number)}</p>
                            <div className="d-flex justify-content-between align-items-center">
                                {/* <button type="button" className="btn btn-md btn-outline-danger">Search</button> */}
                                <Link to={`/hotel/${hotel.id}`}> <button type="button" className="btn btn-md btn-outline-danger">DETAILS</button></Link>
                                <h2><span className="badge bg-info">{hotel.rating}</span></h2>
                            </div>
                        </div>
                    </div>
                </div>


            ))}


        </div>
    )

}

export default HotelList;