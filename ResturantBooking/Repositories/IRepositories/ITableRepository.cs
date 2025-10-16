using ResturantBooking.Models;

namespace ResturantBooking.Repositories.IRepositories
{
    public interface ITableRepository
    {
        Task<List<ResturantTable>> GetAllTablesAsync();
        Task<ResturantTable> GetTableByIDAsync(int tableID);
        Task<int> CreateTableAsync(ResturantTable Table);
        Task<bool> UpdateTableAsync(ResturantTable Table);
        Task<bool> DeleteTableAsync(int tableID);
    }
}
