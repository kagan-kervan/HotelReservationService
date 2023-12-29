import React, { Component } from 'react';
import {Routes, Route, Link, NavLink} from 'react-router-dom';
import 'bootstrap-icons/font/bootstrap-icons.css';
import Main from './pages/Main';
import HotelInfo from './pages/HotelInfo';

import Login from './login/Login';
import Register from './login/Register';
import Reservation from './pages/Reservation';


class App extends React.Component{
   
    render(){
    
        return(

            <div className='container'>

             <Routes>
                <Route path='/' element={<Main />} />
                <Route path='/hotel' element={<HotelInfo />} />
                <Route path='/login' element={<Login />} />   
                <Route path='/register' element={<Register />} />  
                <Route path='/reservation' element={<Reservation />} />       
             </Routes>    

            </div>          
            
        )
    }
}

export default App;