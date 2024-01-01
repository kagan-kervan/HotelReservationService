import "./hotel.css";
import Navbar from "../../components/navbar/Navbar";
import { useParams } from "react-router-dom";
// import SearchBar from "../../components/searchbar/SearchBar";
import Footer from "../../components/footer/Footer";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCircleArrowLeft,
  faCircleArrowRight,
  faCircleXmark,
  faLocationDot,
} from "@fortawesome/free-solid-svg-icons";
import { Link } from 'react-router-dom';
import axios, { formToJSON } from '../../../node_modules/axios/index';
import React, { useState,useEffect } from "react";

const HotelInfo = () => {
  const [slideNumber, setSlideNumber] = useState(0);
  const [open, setOpen] = useState(false);
  const [hotel,setHotel] = useState('');
  const [address,setAddress] = useState('');
  const [feature,setFeature] = useState('');
  const [pics, setPics] = useState([]);
  const { hotelId } = useParams();

  // state = {
  //   pictures:[],
  //   searchQuery: "",
//}
  useEffect(() => {
  // Fetch owners and addresses when the component mounts
  fetchHotel(hotelId);
  fetchAddress();
  fetchFeature();
  fetchPics();
  console.log(address);
    }, [hotelId]);


  const handleOpen = (i) => {
    setSlideNumber(i);
    setOpen(true);
  };

  const handleMove = (direction) => {
    let newSlideNumber;

    if (direction === "l") {
      newSlideNumber = slideNumber === 0 ? 5 : slideNumber - 1;
    } else {
      newSlideNumber = slideNumber === 5 ? 0 : slideNumber + 1;
    }

    setSlideNumber(newSlideNumber)
  };

  const fetchHotel = async () => {
    console.log(hotelId);
    const response = axios.get('/api/Hotel/get-hotel-id/'+hotelId, {
      timeout: 5000,
      withCredentials: true,
    }).then(response => 
      response.data
    ).then(data => {
     setHotel(data);
     console.log(data);
     console.log(hotel);
     
    });
  }

  const fetchAddress = async () =>{
    setAddress(hotel.hotelAddress);
  }

  const fetchFeature = async () =>
  {
    const response = await axios.get('/api/Feature/Get-Feature-With-HotelID/'+hotelId, {
      timeout: 5000,
      withCredentials: true,
    }).then(response => 
      response.data
    ).then(data => {
     setFeature(data.value);
     console.log(data);
     console.log(feature);
    });

  }

  const fetchPics = async () =>{
    const response = await axios.get('/api/Picture/get-pictures/'+hotelId, {
      timeout: 5000,
      withCredentials: true,
    }).then(response => 
      response.data
    ).then(data => {
     setPics(data);
     console.log(data);
     console.log(pics);
    });

  }

  return (
    <div>
      <Navbar />
      {/* <SearchBar type="list" /> */}
      <div className="hotelContainer">
        {open && (
          <div className="slider">
            <FontAwesomeIcon
              icon={faCircleXmark}
              className="close"
              onClick={() => setOpen(false)}
            />
            <FontAwesomeIcon
              icon={faCircleArrowLeft}
              className="arrow"
              onClick={() => handleMove("l")}
            />
            <div className="sliderWrapper">
              <img src={pics[0].pic_Url} alt="" className="sliderImg" />
            </div>
            <FontAwesomeIcon
              icon={faCircleArrowRight}
              className="arrow"
              onClick={() => handleMove("r")}
            />
          </div>
        )}
        <div className="hotelWrapper">
        <Link to={`/add-reservation/${hotelId}`}>  <button className="bookNow">Reserve or Book Now!</button> </Link>
          <h1 className="hotelTitle">{hotel.hotelName}</h1>
          <div className="hotelAddress">
            <FontAwesomeIcon icon={faLocationDot} />
           {hotel.hotelAddress && (
          <span>
              {hotel.hotelAddress.street}, {hotel.hotelAddress.city} / {hotel.hotelAddress.country}
          </span>
          )}
          </div>
          <span className="hotelDistance">
            Excellent location – 500m from center
          </span>
          <span className="hotelPriceHighlight">
            Book a stay over $114 at this property and get a free airport taxi
          </span>
          <div className="hotelImages">
            {/* {photos.map((photo, i) => (
              <div className="hotelImgWrapper" key={i}>
                <img
                  onClick={() => handleOpen(i)}
                  src={photo.src}
                  alt=""
                  className="hotelImg"
                />
              </div>
            ))} */}
          </div>
          <div className="hotelDetails">
            <div className="hotelDetailsTexts">
              <h1 className="hotelTitle">Stay in the heart of City</h1>
              <p className="hotelDesc">
                Located a 5-minute walk from St. Florian's Gate in Krakow, Tower
                Street Apartments has accommodations with air conditioning and
                free WiFi. The units come with hardwood floors and feature a
                fully equipped kitchenette with a microwave, a flat-screen TV,
                and a private bathroom with shower and a hairdryer. A fridge is
                also offered, as well as an electric tea pot and a coffee
                machine. Popular points of interest near the apartment include
                Cloth Hall, Main Market Square and Town Hall Tower. The nearest
                airport is John Paul II International Kraków–Balice, 16.1 km
                from Tower Street Apartments, and the property offers a paid
                airport shuttle service.
              </p>
              <p className="hotelFeatures">
              </p>
            </div>
            <div className="hotelDetailsPrice">
              <h1>Perfect for a 9-night stay!</h1>
              <span>
                Located in the real heart of Krakow, this property has an
                excellent location score of 9.8!
              </span>
              <h2>
                <b>$945</b> (9 nights)
              </h2>
              <Link to={`/add-reservation/${hotelId}`}>  <button >Reserve or Book Now!</button> </Link>
            </div>
          </div>
        </div>

        <p></p>
        <p></p>
        <p></p>
        <p></p>
        <Footer />
      </div>
    </div>
  );
};

export default HotelInfo;
