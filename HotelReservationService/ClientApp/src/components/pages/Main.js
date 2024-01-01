import React, { Component } from 'react';
import Navbar from '../navbar/Navbar';
import SearchBar from '../searchbar/SearchBar';
import HotelList from '../hotelList/HotelList';
import Footer from '../footer/Footer';
import 'bootstrap-icons/font/bootstrap-icons.css';
import axios from '../../../node_modules/axios/index';
import { error } from 'jquery';


//import Header from '../header/Header';

class Main extends React.Component {

    state = {
        hotels: [],
        pictures:[],
        searchQuery: "",
        index : 1,

    }
    componentDidMount() {
        // Backend'den otel verilerini çekme
        this.fetchHotels();
    }
    fetchHotels = async () => {
        try {
            const constantPageSize = 10;
            const response = await axios.get('/api/Hotel/get-hotels?pageIndex='+this.state.index+'&pageSize='+constantPageSize, {
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
        const response = await axios.get(`/api/Hotel/get-hotels?searchWord=${this.state.searchQuery}&pageIndex=`+this.state.index);
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


    render() {


        return (

            <div className='container'>


              
                
                <Navbar />
                {/*<hr></hr>*/}

                {/*<Header />*/}
                <SearchBar
                    onSearchQueryChange={this.handleSearchInputChange}
                    onSearch={this.handleSearch}
                />

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