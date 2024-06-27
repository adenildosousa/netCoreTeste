using WAGym.Common.Model.Pagination.Response;

namespace WAGym.Common.Helper
{
    public static class PaginationHelper
    {
        public static PaginationResponse<TEntity> CreatePagedResponse<TEntity>(IEnumerable<TEntity> data, int pageNumber, int pageSize, int totalRecords) where TEntity : class
        {
            double totalPages = ((double)totalRecords / (double)pageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            return new PaginationResponse<TEntity>(data, pageNumber, pageSize, roundedTotalPages, totalRecords);
        }
    }
}
