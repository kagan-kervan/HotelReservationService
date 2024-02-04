import React, { Component } from 'react';
import Navbar from '../navbar/Navbar';
import SearchBar from '../searchbar/SearchBar';
import HotelList from '../hotelList/HotelList';
import Footer from '../footer/Footer';
import 'bootstrap-icons/font/bootstrap-icons.css';
import axios from '../../../node_modules/axios/index';
import { error } from 'jquery';
import { jwtDecode } from 'jwt-decode';


//import Header from '../header/Header';

class Main extends React.Component {

    state = {
        hotels: [],
        pictures:[],
        searchQuery: "",
        index : 1,
        sortingType:"nameAsc",
        user: null, // Add a user property to store decoded user information
    }
    componentDidMount() {
        // Backend'den otel verilerini çekme
        this.fetchHotels();
        this.decodeUserCookie();
    }
    fetchHotels = async () => {
        try {
            const constantPageSize = 10;
            const response = await axios.get('/api/Hotel/get-hotels?pageIndex='+this.state.index+'&pageSize='+constantPageSize+"&sortingType="+this.state.sortingType, {
                timeout: 5000,
                withCredentials: true,
              });
            // Assume each hotel has an "images" property, which is an array of image URLs
            const hotelsWithImages = response.data.map(hotel => ({
              ...hotel,
              images: [], // You can fetch and populate this array with images later
            }));
            this.setState({ hotels: hotelsWithImages });
            // Call fetchPicturesForHotel for each hotel
            hotelsWithImages.forEach(hotel => {
              this.fetchPicturesForHotel(hotel.id);
            });
      
            console.log(hotelsWithImages);
        } catch (error) {
            console.error('Error fetching hotels:', error);
        }
    };
    decodeUserCookie = () => {
      // Get the cookie value
      const cookieValue = this.getCookieValue('HotelService.Auth');
      console.log(cookieValue);
      
      // Decode the JWT token (assuming it's a JWT)
      try {
        const decodedUser = jwtDecode(cookieValue);
        
        // Set the decoded user information in the state
        this.setState({ user: decodedUser });
      } catch (error) {
        console.error('Error decoding user cookie:', error);
      }
    };
    getCookieValue = (name) => {
      const cookies = document.cookie.split(';').map(cookie => cookie.trim());
      const cookie = cookies.find(cookie => cookie.startsWith(`${name}=`));
      return cookie ? cookie.split('=')[1] : null;
    };

// Add a function to fetch pictures (images) for a specific hotel
    fetchPicturesForHotel = async (hotelId) => {
    try {
      const response = await axios.get(`/api/Picture/get-pictures/${hotelId}`);
      const hotelIndex = this.state.hotels.findIndex(hotel => hotel.id === hotelId);
  
      if (hotelIndex !== -1) {
        const updatedHotels = [...this.state.hotels];
        updatedHotels[hotelIndex].images = response.data; // Assuming response.data is an array of image URLs
        this.setState({ hotels: updatedHotels });
      }
    } catch (error) {
      console.error(`Error fetching pictures for hotel ${hotelId}:`, error);
    }
  };

  
  handleNextPage = () => {
    // Increment the index and fetch hotels for the next page
    this.setState(prevState => ({ index: prevState.index + 1 }), () => {
        this.fetchHotels();
    });
  };
  handlePreviousPage = () => {
    // Increment the index and fetch hotels for the next page
    if (this.state.index > 1) {
      this.setState(prevState => ({ index: prevState.index - 1 }), () => {
          this.fetchHotels();
      });
    }
  };
handleSearch = async () => {
    // Perform search based on this.state.searchQuery
    // You can make a separate API call for search or filter the existing hotels list
    // Update this.state.hotels accordingly
    console.log("Searchin");
    try {
        const response = await axios.get(`/api/Hotel/get-hotels?searchWord=${this.state.searchQuery}&pageIndex=`+this.state.index+"&sortingType="+this.state.sortingType);
        const searchedHotels = response.data.map(hotel => ({
            ...hotel,
            images: [], // You can fetch and populate this array with images later
        }));
        this.setState({ hotels: searchedHotels });
        searchedHotels.forEach(hotel => {
          this.fetchPicturesForHotel(hotel.id);
        });
    } catch (error) {
        console.error('Error searching hotels:', error);
    }
};

handleSearchInputChange = (event) => {
    console.log(this.state.searchQuery);
    console.log(event);
    this.setState({ searchQuery: event });
};

handleSortingChange = (event) => {
  // Update sortingType in state when the user selects a sorting option
  this.setState({ sortingType: event.target.value }, () => {
    // Fetch hotels with the new sorting type
    this.fetchHotels();
  });
};


    render() {
      const { user } = this.state;

        return (

            <div className='container'>


              
                
                <Navbar />
                {/*<hr></hr>*/}

                {/*<Header />*/}
                <SearchBar
                    onSearchQueryChange={this.handleSearchInputChange}
                    onSearch={this.handleSearch}
                />
                  
              {/* Display user information if available */}
                {user && (
                  <div className="user-info">
                  <p>Welcome, {user.name}!</p>
                  <p>Role: {user.role}</p>
                 </div>
              )}
                {/* Sorting Box */}
             <div>
                <label htmlFor="sorting">Sort By: </label>
                  <select id="sorting" onChange={this.handleSortingChange} value={this.state.sortingType}>
                    <option value="nameAsc">Name (Ascending)</option>
                    <option value="nameDesc">Name (Descending)</option>
                    <option value="reviewAsc">Rating (Ascending)</option>
                    <option value="reviewDesc">Rating (Descending)</option>
                  </select>
              </div>
                <div className='row'><HotelList hotels={this.state.hotels} fetchPicturesForHotel={this.fetchPicturesForHotel} /></div>
                <button className='next-button' onClick={this.handleNextPage}>Next Page</button>
                <button className='prev-button' onClick={this.handlePreviousPage}>Previous Page</button>
                <Footer />
                <hr></hr>

              



            </div>

        )
    }
}

export default Main;