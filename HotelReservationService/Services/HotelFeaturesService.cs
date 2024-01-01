using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace HotelReservationService.Services
{
    public class HotelFeaturesService
    {
        private AppDBContext _dbContext;
        public HotelFeaturesService(AppDBContext dbContext) 
        { 
            _dbContext = dbContext; 
        }
        public void AddHotelFeatures(int hotel_id, HotelFeaturesVM vm)
        {
            var hotelFeature = new HotelFeatures()
            {
                HotelId = hotel_id,
                hasWifi = vm.hasWifi,
                hasBeach = vm.hasBeach,
                hasSauna = vm.hasSauna,
                hasSpa = vm.hasSpa,
                hasAquapark = vm.hasAquapark,
                hasPool = vm.hasPool,
                hasBar = vm.hasBar,
                hasParkingLot = vm.hasParkingLot,
                hasRoomService = vm.hasRoomService,
                hasRestaurant = vm.hasRestaurant,
                hasBuffet = vm.hasBuffet

            };
            _dbContext.Features.Add(hotelFeature);
            _dbContext.SaveChanges();
        }
        public ICollection<HotelFeatures> GetHotelFeatures()
        {
           var features = _dbContext.Features.Include(p => p.Hotel).ToList<HotelFeatures>();
            return features;
        }
        public HotelFeatures GetFeaturesFromID(int id)
        {
            var feature = _dbContext.Features.Include(p => p.Hotel).SingleOrDefault(x => x.Id == id);
            return feature;
        }
        public HotelFeatures GetFeatureFromHotelID(int hotel_id)
        {
            var feature = _dbContext.Features.Where(s => s.HotelId == hotel_id).Include(p => p.Hotel).FirstOrDefault();
            return feature;
        }
        public void RemoveHotelFeatures(int id)
        {
            var feature = _dbContext.Features.Find(id);
            if(feature != null)
            {

                if (IsFeatureHasAnyHotel(id))
                {
                    IQueryable<Hotel> hotelQuery = _dbContext.Hotels.Where(x => x.Id == feature.HotelId);
                   var hotel = hotelQuery.FirstOrDefault();
                    if(hotel != null)
                        _dbContext.Hotels.Remove(hotel);
                }
                _dbContext.Features.Remove(feature);
                _dbContext.SaveChanges();
            }
        }
        public void RemoveHotelFeaturesFromHotelID(int hotel_id)
        {
            var feature = _dbContext.Features.Where(x => x.HotelId == hotel_id).SingleOrDefault();
            if(feature != null)
            {
                _dbContext.Features.Remove(feature);
                _dbContext.SaveChanges();
            }
        }
        public HotelFeatures UpdateHotelFeatures(int id,HotelFeaturesVM featuresVM)
        {
            var hotelFeatures = _dbContext.Features.Find(id);
            if(hotelFeatures != null)
            {
                hotelFeatures.hasWifi = featuresVM.hasWifi;
                hotelFeatures.hasBeach = featuresVM.hasBeach;
                hotelFeatures.hasSauna = featuresVM.hasSauna;
                hotelFeatures.hasSpa = featuresVM.hasSpa;
                hotelFeatures.hasAquapark = featuresVM.hasAquapark;
                hotelFeatures.hasPool = featuresVM.hasPool;
                hotelFeatures.hasBar    = featuresVM.hasBar;
                hotelFeatures.hasParkingLot = featuresVM.hasParkingLot;
                hotelFeatures.hasRestaurant = featuresVM.hasRestaurant;
                hotelFeatures.hasBuffet = featuresVM.hasBuffet;
                _dbContext.SaveChanges();
            }
            return hotelFeatures;
        }
        public HotelFeatures UpdateFeatureRegisteredHotel(int id, int new_hotel_id)
        {
            var feature = _dbContext.Features.Find(id);
            if(feature != null)
            {
                feature.HotelId = new_hotel_id;
                _dbContext.SaveChanges(true);
            }
            return feature;
        }
        public bool IsFeatureHasAnyHotel(int hotel_id)
        {
            var hotelFeat = _dbContext.Features.Where(s => s.HotelId == hotel_id);
            if (hotelFeat != null)
                return true;
            return false;
        }
    }
}
