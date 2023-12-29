namespace HotelReservationService.Services.Params
{
    public class HotelControllerParameters
    {
         public int? ownerIDFilter { get; set; }
         public int? addressIDFilter { get; set; }
        
        public String? storingType { get; set; }
        public String? searchWord {  get; set; }

        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public HotelControllerParameters(int? ownerID,int? addressID, String? storingType,String? searchWord, int pageIndex, int? pageSize) 
        {
            this.ownerIDFilter = ownerID;
            this.addressIDFilter = addressID;
            this.storingType = storingType;
            this.searchWord = searchWord;
            if(pageSize != null)
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
            return ((int)(pageIndex/pageSize) < totalPages);
        }
    }
}
