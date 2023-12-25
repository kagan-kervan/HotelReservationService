using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationService.Services
{
    public class ReservationService
    {
        private AppDBContext _dbContext;
        public ReservationService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddReservation(int room_id,int customer_id, ReservationVM reservationVM)
        {
            var reserv = new Reservation()
            {
                RoomId = room_id,
                CustomerId = customer_id,
                Total_Guest_Number = reservationVM.Total_Guest_Number,
                CheckInDate = reservationVM.CheckInDate,
                CheckOutDate = reservationVM.CheckOutDate,
            };
            _dbContext.Reservations.Add(reserv);
            _dbContext.SaveChanges();
        }
        public ICollection<Reservation> GetReservations()
        {
            return _dbContext.Reservations.Include(c => c.Customer).Include(r => r.Room).ToList();
        }
        public Reservation GetReservation(int reservation_id)
        {
            return _dbContext.Reservations.Include(c => c.Customer).Include(r => r.Room).SingleOrDefault(x => x.Id == reservation_id);
        }
        public ICollection<Reservation> GetReservationsFromCustomerID(int customer_id)
        {
            var reservations = _dbContext.Reservations.Where(x => x.CustomerId == customer_id).Include(c => c.Customer).Include(r => r.Room).ToList();
            return reservations;
        }
        public ICollection<Reservation> GetReservationsFromRoomID(int room_id)
        {
            var reservations = _dbContext.Reservations.Where(x => x.RoomId == room_id).Include(c => c.Customer).Include(r => r.Room).ToList();
            return reservations;
        }
        public ICollection<Reservation> GetReservationFromReviewId(int review_id)
        {
            var reservations = _dbContext.Reservations.Where(x => x.ReviewId == review_id).Include(c => c.Customer).Include(r => r.Room).ToList();
            return reservations;
        }

        public void UpdateReservation(int id,ReservationVM newReserv)
        {
            var reserv = _dbContext.Reservations.Find(id);
            if( reserv != null)
            {
                reserv.Total_Guest_Number = newReserv.Total_Guest_Number;
                reserv.CheckInDate = newReserv.CheckInDate;
                reserv.CheckOutDate = newReserv.CheckOutDate;
            }
            _dbContext.SaveChanges();
        }
        public void UpdateReservationRoomID(int id,int room_id)
        {
            var reserv = _dbContext.Reservations.Find(id);
            if (reserv != null)
            {
                reserv.RoomId = room_id;
            }
            _dbContext.SaveChanges();
        }

        public void PutReviewToReservation(int id, Review review)
        {
            var reserv = _dbContext.Reservations.Find(id);
            reserv.ReviewId = review.Id;
            reserv.Review = review;
            _dbContext.SaveChanges();
        }
        public void RemoveReservation(int id)
        {
            var reserv = _dbContext.Reservations.Find(id);
            _dbContext.Reservations.Remove(reserv);
            _dbContext.SaveChanges();
        }
        public void RemoveReservationWithCustomerID(int customer_id)
        {
            var list = GetReservationsFromCustomerID(customer_id);
            foreach (var reservation in list)
            {
                _dbContext.Reservations.Remove(reservation);
            }
            _dbContext.SaveChanges();
        }
        public void RemoveReservationWithRoomID(int room_id)
        {
            var list = GetReservationsFromRoomID(room_id);
            foreach (var reservation in list)
            {
                _dbContext.Reservations.Remove(reservation);
            }
            _dbContext.SaveChanges();
        }
    }
}
