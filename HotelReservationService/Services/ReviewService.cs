using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;

namespace HotelReservationService.Services
{
    public class ReviewService
    {
        private AppDBContext _dbContext;
        private readonly TableRelationService _tableRelationService;
        public ReviewService(AppDBContext dbContext, TableRelationService tableRelationService)
        {
            _dbContext = dbContext; 
            _tableRelationService = tableRelationService;
        }
        public Review AddReview(int reservID,ReviewVM reviewVM)
        {
            var rev = new Review()
            {
                Rating = reviewVM.Rating,
                Comment = reviewVM.Comment,
                Comment_Date = reviewVM.Comment_Date,
            };
            _dbContext.Reviews.Add(rev);
            _dbContext.SaveChanges();
            _tableRelationService.AttachReviewToReservation(reservID, rev);
            return rev;
        }
        public ICollection<Review> GetReviews()
        {
            var lsit = _dbContext.Reviews.ToList();
            return lsit;
        }
        public Review GetReview(int id)
        {
            if (id == 0)
                return null;
            var rev = _dbContext.Reviews.SingleOrDefault( x => x.Id == id);
            return rev;
        }
        public void DeleteReview(int id)
        {
            var rev = _dbContext.Reviews.SingleOrDefault(x => x.Id == id);
            if (rev != null)
            {
                _dbContext.Remove(rev); 
                _dbContext.SaveChanges();
            }
        }
        public void UpdateReview(int id,string? newComment,int? newScore)
        {
            var rev = _dbContext.Reviews.SingleOrDefault(x =>x.Id == id);
            if (rev != null)
            {
                if(newComment != null)
                    rev.Comment = newComment;
                if (newScore != null)
                    rev.Rating = (int)newScore;
                _dbContext.SaveChanges();
            }
        }
    }
}
