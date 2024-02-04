import React, { Component } from 'react';
import {Routes, Route, Link, NavLink} from 'react-router-dom';
import 'bootstrap-icons/font/bootstrap-icons.css';
import Main from './pages/Main';
import HotelInfo from './pages/HotelInfo';
import AddHotel from './add-hotel/AddHotel';
import AddAddress from './add-hotel/AddAddress';
import AddRoom from './add-room/AddRoom';
import AddRoomType from './add-room/AddRoomType';
import Login from './login/Login';
import Register from './login/Register';
import Reservation from './pages/Reservation';
import AddReservation from './add-reservation/AddReservation';
import Customer from './customer/Customer';
import GetOwner from './owner/Owner';
import UpdateHotel from './hotel_page/UpdateHotel';
import AddReview from './add-review/AddReview';

class App extends React.Component{
   
    render(){
    
        return(

            <div className='container'>

             <Routes>
                <Route path='/' element={<Main />} />
                <Route path="/hotel/:hotelId" element={<HotelInfo/>} />
                <Route path='/login' element={<Login />} />   
                <Route path='/register' element={<Register />} />  
                <Route path='/reservation' element={<Reservation />} />
                <Route path='/add-hotel' element={<AddHotel/>} />
                <Route path='/add-room/:ownerID' element={<AddRoom/>} />
                <Route path='/add-room-type' element={<AddRoomType/>} />
                <Route path='/add-address' element={<AddAddress/>} />
                <Route path='/add-reservation/:hotelID' element={<AddReservation/>} />
                <Route path='/customer-page/:customerID' element={<Customer/>} />
                <Route path='/owner-page/:ownerID' element={<GetOwner/>} />
                <Route path='/update-hotel/:hotelID' element={<UpdateHotel/>} />
                <Route path='/add-review/:reservation_id' element={<AddReview/>} />
             </Routes>    

            </div>          
            
        )
    }
}

export default App;