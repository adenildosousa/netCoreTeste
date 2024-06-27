namespace WAGym.Common.Model.Pagination.Request
{
    public class PaginationRequest
    {
        public long CompanyId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationRequest()
        {   
        }

        public PaginationRequest(PaginationRequest request)
        {
            CompanyId = request.CompanyId;
            PageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            PageSize = request.PageSize > 10 ? 10 : request.PageSize;
        }
    }
}
