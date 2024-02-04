using HotelReservationService.Data.Identity;
using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationService.Services
{
    public class OwnerService
    {
        private readonly AppDBContext _dbContext;
        private readonly TableRelationService _tableRelationService;
        private readonly UserManager<ApplicationOwner> _userManager;
        public OwnerService(AppDBContext dbContext, TableRelationService tableRelationService, UserManager<ApplicationOwner> userManager)
        {
            _dbContext = dbContext;
            _tableRelationService = tableRelationService;
            _userManager = userManager;
        }
        public async Task<Owner> AddOwnerAsync(OwnerVM ownerVM)
        {
            string display = ownerVM.Name + " " + ownerVM.Surname;
            var newUser = new ApplicationOwner
            {
                DisplayName = display,
                Email = ownerVM.Email_Address,
                UserName = ownerVM.Email_Address,
                PhoneNumber = ownerVM.Phone,
            };

            var result = await _userManager.CreateAsync(newUser, ownerVM.Password);
            if(result.Succeeded)
            {
                Owner owner1 = new Owner()
                {
                    Name = ownerVM.Name,
                    Surname = ownerVM.Surname,
                    Email_Address = ownerVM.Email_Address,
                    Password = ownerVM.Password,
                    Phone = ownerVM.Phone,
                    ApplicationOwner = newUser,
                };
                _dbContext.Owners.Add(owner1);
                _dbContext.SaveChanges();
                return owner1;
            }
            return null;
        }
        public ICollection<Owner> GetAll()
        {
           var list = _dbContext.Owners.Include(o => o.ApplicationOwner).ToList();
            return list;
        }
        public Owner GetOwner(int id)
        {
            var own = _dbContext.Owners.Include(o => o.ApplicationOwner).FirstOrDefault(o => o.Id == id);
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
        public Owner UpdateOwner(int id, OwnerVM ownerVM)
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
            return own;
        }

        internal object GetOwnerWithMail(string email)
        {
            IQueryable<Owner> que = _dbContext.Owners;
            que = que.Where(q => q.Email_Address == email);
            return que.Include(o => o.ApplicationOwner).FirstOrDefault();
        }
    }
}
