using HotelReservationService.Data.Models;
using System.Net.WebSockets;

namespace HotelReservationService.Services
{
    public class PictureService
    {
        private AppDBContext _dbContext;
        public PictureService(AppDBContext dBContext)
        {
            this._dbContext = dBContext;
        }
        public void AddPicture(string url, int hotel_id)
        {
            var pic = new Picture()
            {
                Pic_Url = url,
                HotelId = hotel_id
            };
            _dbContext.Pictures.Add(pic);
            _dbContext.SaveChanges();
        }
        public ICollection<Picture> GetPictures(int hotelID)
        {
            var lsit = _dbContext.Pictures.Where(x => x.HotelId == hotelID).ToList();
            return lsit;
        }
        public Picture GetPicture(int id)
        {
            var pic = _dbContext.Pictures.SingleOrDefault(x => x.Id == id);
            return pic;
        }
        public void DeletePicture(int id)
        {
            var pic = _dbContext.Pictures.SingleOrDefault(x => x.Id == id);
            if (pic != null)
            {
                _dbContext.Remove(pic);
                _dbContext.SaveChanges();
            }
        }
        public void UpdatePicture(int id, string url)
        {
            var pic = _dbContext.Pictures.SingleOrDefault(x => x.Id == id);
            if (pic != null)
            {
                pic.Pic_Url = url;
                _dbContext.SaveChanges();
            }
        }
    }
}
