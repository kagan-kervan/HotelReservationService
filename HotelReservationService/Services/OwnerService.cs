using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;

namespace HotelReservationService.Services
{
    public class OwnerService
    {
        private readonly AppDBContext _dbContext;
        private readonly TableRelationService _tableRelationService;
        public OwnerService(AppDBContext dbContext, TableRelationService tableRelationService)
        {
            _dbContext = dbContext;
            _tableRelationService = tableRelationService;
        }
        public void AddOwner(OwnerVM ownerVM)
        {
            Owner owner1 = new Owner()
            {
                Name = ownerVM.Name,
                Surname = ownerVM.Surname,
                Email_Address = ownerVM.Email_Address,
                Password = ownerVM.Password,
                Phone = ownerVM.Phone,
            };
            _dbContext.Owners.Add(owner1);
            _dbContext.SaveChanges();
        }
        public ICollection<Owner> GetAll()
        {
           var list = _dbContext.Owners.ToList();
            return list;
        }
        public Owner GetOwner(int id)
        {
            var own = _dbContext.Owners.FirstOrDefault(o => o.Id == id);
            return own;
        }
        public void DeleteOwner(int id)
        {
            var own = _dbContext.Owners.SingleOrDefault(o => o.Id == id);
            if (own != null)
            {
                //If owner has any hotel.
                if (_tableRelationService.DoesOwnerHaveAnyHotel(id))
                {
                    throw new Exception();
                }
                _dbContext.Owners.Remove(own);
                _dbContext.SaveChanges();
            }
        }
        public void UpdateOwner(int id, OwnerVM ownerVM)
        {
            var own = GetOwner(id);
            if(own != null)
            {
                own.Name = ownerVM.Name;
                own.Surname = ownerVM.Surname;
                own.Email_Address = ownerVM.Email_Address;
                own.Password = ownerVM.Password;
                own.Phone = ownerVM.Phone;
                _dbContext.SaveChanges();
            }
        }
    }
}
