using HotelReservationService.Data.Models;

namespace HotelReservationService.Services
{
    public class ReviewService
    {
        private AppDBContext _dbContext;
        public ReviewService(AppDBContext dbContext) {  _dbContext = dbContext; }
        public void AddReview(string desc, int score,DateTime reviewTime)
        {
            var rev = new Review()
            {
                Rating = score,
                Comment = desc,
                Comment_Date = reviewTime,
            };
            _dbContext.Reviews.Add(rev);
            _dbContext.SaveChanges();
        }
        public ICollection<Review> GetReviews()
        {
            var lsit = _dbContext.Reviews.ToList();
            return lsit;
        }
        public Review GetReview(int id)
        {
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
