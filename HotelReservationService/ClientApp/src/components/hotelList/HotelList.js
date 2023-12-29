import React from 'react';
import { Link } from 'react-router-dom';
import './hotellist.css'

const HotelList = (props) => {

    return (
        
        <div className="row">

            {props.hotels.map((hotel) => (

                <div className="col-lg-4" key={hotel.id}>
                    <div className="card mb-4 shadow-sm">
                        <img src={hotel.imageURL} className="card-img-top" alt="Sample Hotel" />
                        <div className="card-body">
                            <h5 className="card-title">{hotel.name}</h5>
                            <p className="card-text">{hotel.overview}</p>
                            <div className="d-flex justify-content-between align-items-center">
                                {/* <button type="button" className="btn btn-md btn-outline-danger">Search</button> */}
                                <Link to="/hotel"> <button type="button" className="btn btn-md btn-outline-danger">Search</button></Link>
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