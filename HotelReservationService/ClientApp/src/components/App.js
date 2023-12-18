import React, { Component } from 'react';
//import React from 'react';

import Navbar from './navbar/Navbar';
import SearchBar from './searchbar/SearchBar';
import HotelList from './hotelList/HotelList';
import Footer from './footer/Footer';
import Hotel from './hotel_page/Hotel';
import 'bootstrap-icons/font/bootstrap-icons.css';

import {Routes, Route, Link, NavLink} from 'react-router-dom';

import Login from './login/Login';



class App extends React.Component{

    state = {
        hotels : [
            {
                "id": 1,
                "name": "Kosa Otel Cesme",
                "rating": 8.8,
                "overview": "This is a wider card with supporting text below as a natural lead-in to additional content.",
                "imageURL": "https://imgcy.trivago.com/c_lfill,d_dummy.jpeg,e_sharpen:60,f_auto,h_240,q_auto,w_320//partnerimages/16/98/1698711658.jpeg"
            },
            {
                "id": 2,
                "name": "Unique Fethiye",
                "rating": 9.6,
                "overview": "This is a wider card with supporting text below as a natural lead-in to additional content.",
                "imageURL": "https://imgcy.trivago.com/c_lfill,d_dummy.jpeg,e_sharpen:60,f_auto,h_240,q_auto,w_320//partnerimages/15/99/1599394608.jpeg"
            },
            {
                "id": 3,
                "name": "Melas Lara",
                "rating": 9.4,
                "overview": "This is a wider card with supporting text below as a natural lead-in to additional content.",
                "imageURL": "https://imgcy.trivago.com/c_lfill,d_dummy.jpeg,e_sharpen:60,f_auto,h_240,q_auto,w_320//uploadimages/28/47/28476686.jpeg"
            },
            {
                "id": 4,
                "name": "The Beachfront Hotel",
                "rating": 8.7,
                "overview": "This is a wider card with supporting text below as a natural lead-in to additional content.",
                "imageURL": "https://imgcy.trivago.com/c_lfill,d_dummy.jpeg,e_sharpen:60,f_auto,h_240,q_auto,w_320//partnerimages/16/07/1607248880.jpeg"
              },
              {
                "id": 5,
                "name": "Montana Pine Resort",
                "rating": 9.2,
                "overview": "This is a wider card with supporting text below as a natural lead-in to additional content.",
                "imageURL": "https://imgcy.trivago.com/c_lfill,d_dummy.jpeg,e_sharpen:60,f_auto,h_240,q_auto,w_320//partnerimages/17/14/1714259638.jpeg"
              },
              {
                "id": 6,
                "name": "Baga Hotel Akyaka",
                "rating": 8.6,
                "overview": "This is a wider card with supporting text below as a natural lead-in to additional content.",
                "imageURL": "https://imgcy.trivago.com/c_lfill,d_dummy.jpeg,e_sharpen:60,f_auto,h_240,q_auto,w_320//uploadimages/20/97/20976022.jpeg"
              }
        
        ],

        searchQuery: "",

    }


    
    render(){

    
        return(

            <div className='container'>
{/* 
            anapath olarak şimdilik navbar var düzeltmek lazım
            yeni bir pages componenti oluşturulup 
            anamenu,otel,log in page'e atılıp burada birleştirilebilir
            buranın anamenu gibi olması dogru degil sanırım */}


             <Routes>
                <Route path='/' element={<Navbar />} />
                <Route path='/login' element={<Login />} />
                <Route path='/search' element={<SearchBar />} />
                <Route path='/foot' element={<Footer />} />
             </Routes>    

            </div>          
            
        )
    }
}

export default App;