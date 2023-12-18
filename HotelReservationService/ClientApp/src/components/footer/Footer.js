import "./footer.css";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faEnvelope } from '@fortawesome/free-solid-svg-icons'

const Footer = () => {
  return (
    <div className="footer">

    
      <div className="footer_about">
        <ul>
          <li>
            <p>Trivago's global hotel search
              trivago’s hotel search allows users
              to compare hotel prices in just a few
              clicks from hundreds of booking sites
              for more than 5.0 million hotels and other
              types of accommodation in over 190 countries.
              We help millions of travelers each year compare
              deals for hotels and accommodations. Get information
              for trips to cities like Las Vegas or Orlando and
              you can find the right hotel on trivago quickly and
              easily. New York City and its surrounding area are
              great for trips that are a week or longer with the
              numerous hotels available.
            </p>
          </li>

          <i class="fa-solid fa-ghost"></i>
          <FontAwesomeIcon icon="fa-solid fa-ghost" />

          <li>
            <p>
              Find cheap hotels on trivago
              With trivago you can easily find your ideal hotel and
              compare prices from different websites. Simply enter where
              you want to go and your desired travel dates, and let our
              hotel search engine compare accommodation prices for you.
              To refine your search results, simply filter by price,
              distance (e.g. from the beach), star category, facilities
              and more. From budget hostels to luxury suites, trivago
              makes it easy to book online. You can search from a large
              variety of rooms and locations across the USA, like San
              Francisco and Chicago to popular cities and holiday destinations
              abroad!
            </p>
          </li>

          <li>
            <p>
              Hotel reviews help you find your ideal hotel
              Over 175 million aggregated hotel ratings and more than 19 million
              images allow you to find out more about where you're traveling.
              To get an extended overview of a hotel property, trivago shows the
              average rating and extensive reviews from other booking sites,
              e.g. Hotels.com, Expedia, Agoda, leading hotels, etc. trivago makes
              it easy for you to find information about your trip to Miami Beach,
              including the ideal hotel for you.
            </p>

            <p></p>

          </li>
        </ul>
      </div>


      <div className="social_media">
          <i class="bi bi-instagram"></i><a href="https://www.instagram.com/trivago/">instagram  </a>  
          <i class="bi bi-threads"></i><a href="https://www.threads.net/@trivago">Threads  </a> 
          <i class="bi bi-twitter-x"></i><a href="https://twitter.com/trivago">X</a> 
      </div>
      

      <p></p>


      <div className="fText">Copyright © 2023 Trivago.</div>

    </div>
  );
};

export default Footer;

