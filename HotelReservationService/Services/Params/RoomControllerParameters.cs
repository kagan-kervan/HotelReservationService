namespace HotelReservationService.Services.Params
{
    public class RoomControllerParameters
    {
        public String? sortingType;
        public int? hotelIDFilter;
        public int? typeIDFilter;
        public int pageSize;
        public int pageIndex;
        public RoomControllerParameters(String? sortingType,int?hotelIDfilter,int?typeIDFilter,int pageIndex,int? pageSize) 
        {
            this.sortingType = sortingType;
            this.hotelIDFilter = hotelIDfilter;
            this.typeIDFilter = typeIDFilter;
            if (pageSize != null)
            {
                this.pageSize = (int)pageSize;
            }
            else
            {
                this.pageSize = 10;
            }
            this.pageIndex = pageIndex;
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
