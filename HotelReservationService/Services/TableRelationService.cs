using Microsoft.EntityFrameworkCore;
using ConnectingApps.SmartInject;

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
        public TableRelationService(Lazy<ReservationService> reservationService, Lazy<HotelService> hotelService, 
           Lazy<OwnerService> ownerService, Lazy<RoomService> roomService, Lazy<AddressService> address, Lazy<HotelFeaturesService> featureService)
        {
            this.reservationService = reservationService;
            this.hotelService = hotelService;
            this.ownerService = ownerService;
            this.roomService = roomService;
            this.addressService = address;
            this.featureService = featureService;
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
    }

}
