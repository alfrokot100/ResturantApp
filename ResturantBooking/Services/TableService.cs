using ResturantBooking.Data;
using ResturantBooking.DTOs.TableDTOs;
using ResturantBooking.Models;
using ResturantBooking.Repositories.IRepositories;
using ResturantBooking.Services.IServices;

namespace ResturantBooking.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepo;
        public TableService(ITableRepository tableRepo)
        {
            _tableRepo = tableRepo;
        }
        public async Task<List<TableDTO>> GetAllTablesAsync()
        {
            var tables = await _tableRepo.GetAllTablesAsync();
            var tableDTO = tables.Select(t => new TableDTO
            {
                Id = t.Id,
                Number = t.Number,
                Capacity = t.Capacity
            }).ToList();
            return tableDTO;
        }

        public async Task<TableDTO> GetTableByIDAsync(int tableID)
        {
            var table = await _tableRepo.GetTableByIDAsync(tableID);
            if(table == null) { return null; }

            var tableDTO = new TableDTO
            {
                Id = table.Id,
                Number = table.Number,
                Capacity = table.Capacity
            };
            return tableDTO;
        }
        public async Task<int> CreateTableAsync(TableDTO tableDTO)
        {
            var table = new ResturantTable
            {
                Id = tableDTO.Id,
                Number = tableDTO.Number,
                Capacity = tableDTO.Capacity
            };
            var newTableID = await _tableRepo.CreateTableAsync(table);
            return newTableID;
        }

        public async Task<bool> UpdateTableAsync(TableDTO tableDTO)
        {
            var table = new ResturantTable
            {
                Id = tableDTO.Id,
                Number = tableDTO.Number,
                Capacity = tableDTO.Capacity
            };
            return await _tableRepo.UpdateTableAsync(table);
        }
        public async Task<bool> DeleteTableAsync(int tableID)
        {
            return await _tableRepo.DeleteTableAsync(tableID);
        }
    }
}
