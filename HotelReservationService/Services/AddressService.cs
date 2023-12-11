using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;

namespace HotelReservationService.Services
{
    public class AddressService
    {
            AppDBContext dbContext;
            public AddressService(AppDBContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public void AddAddress(AddressVM address)
            {
                var newAddress = new Address()
                {
                    City = address.City,
                    Region = address.Region,
                    PostalCode = address.PostalCode,
                    Country = address.Country,
                    Street = address.Street,
                    No = address.No
                };
                dbContext.Add(newAddress);
                dbContext.SaveChanges();
            }
        public void RemoveAddress(int id) 
        {
            //After deletion what happens to identified hotels?
            // 1- Cannot delete already signed addresses.
            var address = dbContext.Addresses.Find(id);
            if(address != null)
            {
                dbContext.Addresses.Remove(address);
                dbContext.SaveChanges();
            }
        }
        public Address GetAddress(int id)
        {
            return dbContext.Addresses.Find(id);
        }
        public ICollection<Address> GetAddresses()
        {
            return dbContext.Addresses.ToList();
        }
        public Address UpdateAddressGivenID(int id,AddressVM addressVM)
        {
            var address = dbContext.Addresses.Find(id);
            if(address != null)
            {
                address.City = addressVM.City;
                address.Region = addressVM.Region;
                address.PostalCode = addressVM.PostalCode;
                address.Country = addressVM.Country;
                address.Street = addressVM.Street;
                address.No = addressVM.No;
                dbContext.SaveChanges() ;
            }
            return address;
        }
        
    }
}
