using ResturantBooking.DTOs.TableDTOs;

namespace ResturantBooking.Services.IServices
{
    public interface ITableService
    {
        Task<List<TableDTO>> GetAllTablesAsync();
        Task<TableDTO> GetTableByIDAsync(int tableID);
        Task<int> CreateTableAsync(TableDTO tableDTO);
        Task<bool> UpdateTableAsync(TableDTO tableDTO);
        Task<bool> DeleteTableAsync(int tableID);
    }
}
