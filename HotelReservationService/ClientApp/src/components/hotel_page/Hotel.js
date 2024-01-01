import React from 'react';
import React, { useState,useEffect } from "react";
import './hotel.css'
import { Link } from 'react-router-dom';
import axios, { formToJSON } from '../../../node_modules/axios/index';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEnvelope } from '@fortawesome/free-solid-svg-icons';

const Hotel = () => {

    return (

        <div >



            <div className='header'>

                <div className='hotel_header' id='hotel_name'>
                    <h1>Kosa Otel Cesme</h1>
                </div>

                <div className='hotel_header' id='hotel_amenities'>
                    <i class="bi bi-wifi"></i>
                    <i class="bi bi-p-circle"></i>
                    <i class="bi bi-water"></i>
                </div>

                <div className='hotel_header' id='hotel_raiting'>
                    <h1>9.6</h1>
                </div>

            </div>


            <div className='hotel_image'>
                <h1>Hotel Resimleri</h1>              
            </div>


            <div className='post_comment'>
                <h1>Yorum yap</h1>
            </div>

            <div className='review'>
                <h1>Review</h1>
            </div>

          



        </div >
    )

}

export default Hotel;