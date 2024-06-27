namespace WAGym.Common.Model.Pagination.Response
{
    public class PaginationResponse<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Data { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalRecords { get; private set; }

        public PaginationResponse(IEnumerable<TEntity> data, int pageNumber, int pageSize, int totalPages, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
    }
}
