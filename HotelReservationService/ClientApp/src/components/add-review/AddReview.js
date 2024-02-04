import React, { useState, useEffect } from "react";
import { Link, useParams } from 'react-router-dom';
import './Review.css';
import axios from 'axios';

const AddReview = () => {
  const { reservation_id } = useParams();
  const [comment, setComment] = useState("");
  const [rating, setRating] = useState(0);

  const handleReviewSubmit = async () => {
    // Additional logic to get the current date in the desired format
    const currentDate = new Date();
    const formattedDate = currentDate.toISOString().split('T')[0];
    console.log(reservation_id);

    try {
      // Send a POST request to create a new review
      const response = await axios.post('https://localhost:3000/api/Review/add/'+reservation_id, {
        comment: comment,
        rating: rating,
        commentDate: formattedDate,
      });

      console.log(response.data);
      // Handle success or show an alert
    } catch (error) {
      console.error('Error submitting review:', error);
      // Handle error or show an alert
    }
  };

  return (
    <div className="review-container">
      <h2>Add Review</h2>
      <div>
        <label htmlFor="comment">Comment:</label>
        <textarea
          id="comment"
          value={comment}
          onChange={(e) => setComment(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="rating">Rating (0-10):</label>
        <input
          type="number"
          id="rating"
          min="0"
          max="10"
          value={rating}
          onChange={(e) => setRating(e.target.value)}
        />
      </div>
      <button onClick={handleReviewSubmit}>Submit Review</button>
    </div>
  );
};

export default AddReview;