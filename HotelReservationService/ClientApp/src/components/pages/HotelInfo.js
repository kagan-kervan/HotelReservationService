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
  const { hotelId } = useParams();
  const [photos,setPhotos] = useState([]);

  useEffect(() => {
  // Fetch owners and addresses when the component mounts
  fetchHotel(hotelId);
  fetchAddress(hotel.hotelAddress);
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
    const slideLength = photos.length;
    if (direction === "l") {
      newSlideNumber = slideNumber === 0 ? slideLength - 1 : slideNumber - 1;
    } else {
      newSlideNumber = slideNumber === slideLength - 1 ? 0 : slideNumber + 1;
    }

    setSlideNumber(newSlideNumber)
  };

  const fetchHotel = async () => {
    console.log(hotelId);
    const response = await axios.get('/api/Hotel/get-hotel-id/'+hotelId, {
      timeout: 5000,
      withCredentials: true,
    });
    setHotel(response.data);
    console.log(response.data);
  }

  const fetchAddress = async (htl) =>{
    setAddress(htl);
  }

  const fetchFeature = async () =>
  {
    const response = await axios.get('/api/Feature/Get-Feature-With-HotelID/'+hotelId, {
      timeout: 5000,
      withCredentials: true,
    });
    setFeature(response.data);

  }

  const fetchPics = async () =>{
    const response = await axios.get('/api/Picture/get-pictures/'+hotelId, {
      timeout: 5000,
      withCredentials: true,
    });
    console.log(response);
    setPhotos(response.data);



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
              <img src={photos[slideNumber].pic_Url} alt="" className="sliderImg" />
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
          <div className="hotelImages">
             {photos.map((photo, i) => (
              <div className="hotelImgWrapper" key={i}>
                <img
                  onClick={() => handleOpen(i)}
                  src={photo.pic_Url}
                  alt=""
                  className="hotelImg"
                />
              </div>
            ))}
          </div>
          <div className="hotelDetails">
            <div className="hotelDetailsTexts">
              <h1 className="hotelTitle">Stay in the heart of City</h1>
              <p className="hotelDesc">
              {feature.description}
              </p>
              </div>
              <div className="hotelFeaturesTexts">
              <h2> <b>FEATURES</b></h2>
              <p className="hotelFeatures">
                  {feature.hasWifi && <span> <b>Wi-Fi</b>  </span>}
                  {feature.hasParking && <span> <b>Parking</b>  </span>}
                  {feature.hasPool && <span> <b>Pool </b> </span>}
                  {feature.hasSpa && <span> <b>Spa</b>  </span>}
                  {feature.hasAquapark && <span> <b>Aquapark </b> </span>}
                  {feature.hasBeach && <span> <b>Beach</b>  </span>}
                  {feature.hasBar && <span> <b>Bar</b>  </span>}
                  {feature.hasParkingLot && <span> <b>Parking Lot </b> </span>}
                  {feature.hasRoomService && <span> <b>Room Service</b>  </span>}
                  {feature.hasRestaurant && <span> <b>Room Service</b>  </span>}
                  {feature.hasBuffet && <span> <b>Buffet</b>  </span>}
              </p>
              <p className="hotelFeatures">
              </p>
            </div>
            <div className="hotelDetailsPrice">
              <h1>Perfect for a 9-night stay!</h1>
              <span>
                Located in the real heart of City, this property has an
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
