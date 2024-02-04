using Microsoft.EntityFrameworkCore;
using ConnectingApps.SmartInject;
using HotelReservationService.Data.Models;

namespace HotelReservationService.Services
{
    public class TableRelationService
    {
        private readonly Lazy<ReservationService> reservationService;
        private readonly Lazy<HotelService> hotelService;
        private readonly Lazy<OwnerService> ownerService;
        private readonly Lazy<RoomService> roomService;
        private readonly Lazy<AddressService> addressService;
        private readonly Lazy<HotelFeaturesService> featureService;
        private readonly Lazy<ReviewService> reviewService;
        public TableRelationService(Lazy<ReservationService> reservationService, Lazy<HotelService> hotelService, 
           Lazy<OwnerService> ownerService, Lazy<RoomService> roomService, Lazy<AddressService> address, Lazy<HotelFeaturesService> featureService, Lazy<ReviewService> reviewService)
        {
            this.reservationService = reservationService;
            this.hotelService = hotelService;
            this.ownerService = ownerService;
            this.roomService = roomService;
            this.addressService = address;
            this.featureService = featureService;
            this.reviewService = reviewService;
        }

        //Checks if the owner has any hotel in hotel service class.
        public bool DoesOwnerHaveAnyHotel(int owner_id)
        {
            var hotels = hotelService.Value.GetHotelsFromOwnerID(owner_id);
            if (hotels.Any())
                return true;
            return false;
        }
        public void DeleteHotelsWithOwnerID(int owner_id)
        {
            hotelService.Value.DeleteHotelWithOwnerID(owner_id);
        }
        public bool DoesCustomerHaveAnyReservations(int customer_id)
        {
            var customer = reservationService.Value.GetReservationsFromCustomerID(customer_id);
            if(customer.Any()) return true;
            return false;
        }
        public void DeleteReservationWithCustomerID(int customer_id)
        {
            reservationService.Value.RemoveReservationWithCustomerID(customer_id);
        }


        //I don't know, most likely don't use these 3.
        public bool DoesDBHasGivenHotelID(int hotel_id)
        {
            var hotel = hotelService.Value.GetHotel(hotel_id);
            if(hotel != null) return true;
            return false;
        }

        public bool HasOwnerWithGivenID(int owner_id)
        {
            var tempOwner = ownerService.Value.GetOwner(owner_id);
            if (tempOwner != null)
            {
                return true;
            }
            return false;
        }
        public bool HasAddressWithGivenID(int address_id)
        {
            var tempAddress = addressService.Value.GetAddress(address_id);
            if (tempAddress != null)
                return true;
            return false;
        }
        /// /////////////////////////////////////////              
        public bool DoesRoomHaveAnyReservations(int  room_id)
        {
            var reservs = reservationService.Value.GetReservationsFromRoomID(room_id);
            if (reservs.Any()) return true;
            return false;
        }
        public void DeleteReservationsAttachedToRoom(int room_id)
        {
            reservationService.Value.RemoveReservationWithRoomID(room_id);
        }

        public bool DoesHotelHaveAnyRooms(int hotel_id)
        {
            var list = roomService.Value.GetRoomsFromHotelID(hotel_id);
            if (list.Any()) return true;
            return false;
        }
        public void DeleteRoomsAttachedToHotel(int hotel_id)
        {
            roomService.Value.RemoveRoomsWithHotelID(hotel_id);
        }

        public void DeleteFeatureForGivenHotel(int hotel_id)
        {
            featureService.Value.RemoveHotelFeaturesFromHotelID(hotel_id);
        }

        public void AttachReviewToReservation(int reservation_id, Review review)
        {
            reservationService.Value.PutReviewToReservation(reservation_id, review);
            var room = roomService.Value.GetRoom((int)reservationService.Value.GetReservation(reservation_id).RoomId);
            if (room != null)
                UpdateHotelReviewScore((int)room.HotelId);
        }

        public void UpdateHotelReviewScore(int hotel_id)
        {
            var hotel = hotelService.Value.GetHotel(hotel_id);
            var score = hotel.overall_score == null ? 0 : hotel.overall_score;
            var rooms = roomService.Value.GetRoomsFromHotelID(hotel_id);
            // Get through each room.
            foreach (var room in rooms)
            {
                // Take all of their reservations.
                var reservs = reservationService.Value.GetReservationsFromRoomID(room.Id);
                foreach (var reservation in reservs)
                {
                    
                    var review = reviewService.Value.GetReview(reservationService.Value.GetReviewIDFromReservation(reservation.Id));
                    if (review != null)
                    {
                        score = score + review.Rating;
                    }
                }
                // Calculate the average score
                if (reservs.Count > 0)
                {
                    score /= reservs.Count;
                }
            }
            score = score / rooms.Count;
            hotelService.Value.UpdateHotelScore(hotel.Id, (float)score);
        }
        public void UpdateTotalRoomNumberForHotel(int hotel_id)
        {
            hotelService.Value.IncrementTotalRoomNum(hotel_id);
        }
        public void IncrementFullRoomNumberForHotel(int room_id)
        {
            var hotel = roomService.Value.GetRoom(room_id).HotelId;
            if(hotel != null)
            {
                hotelService.Value.IncrementFullRoomNum((int)hotel);
            }
        }

    }

}
