import React from 'react';
import "./searchbar.css";

class SearchBar extends React.Component {

    render() {

        return (
            
            <div className='search'>

                <i class="bi bi-search"></i><input type='text' className='search_hotel'placeholder='Search a hotel' />
                <i class="bi bi-calendar"></i><input type='text' className='check_in' placeholder='Check in' />
                <i class="bi bi-calendar-check-fill"></i><input type='text' className='check_out' placeholder='Check out' />
                <i class="bi bi-people-fill"></i><input type='text' className='guest_room' placeholder='Guests/Rooms' />
                <button className='search_button'>Search</button>

            </div>
        )
    }
}

export default SearchBar;