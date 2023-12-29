namespace HotelReservationService.Services.Params
{
    public class ReservationControllerParams
    {
        public int? customerIDFilter;
        public int? roomIDfilter;
        public DateTime? searchTime;
        public int pageIndex;
        public int pageSize;
        public ReservationControllerParams(int? customerIDFilter, int? roomIDfilter, DateTime? searchTime, int pageIndex, int? pageSize)
        {
            this.searchTime = searchTime;
            this.pageIndex = pageIndex;
            if (pageSize != null)
            {
                this.pageSize = (int)pageSize;
            }
            else
            {
                this.pageSize = 10;
            }
            this.customerIDFilter = customerIDFilter;
            this.roomIDfilter = roomIDfilter;
        }
        public bool HasPreviousPage()
        {
            return pageIndex > 0;
        }
        public bool HasNextPage(int totalPages)
        {
            return ((int)(pageIndex / pageSize) < totalPages);
        }
    }
}
