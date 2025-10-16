using Microsoft.EntityFrameworkCore;
using ResturantBooking.Data;
using ResturantBooking.Models;
using ResturantBooking.Repositories.IRepositories;


namespace ResturantBooking.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly AppDBContext _context;

        public TableRepository( AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ResturantTable>> GetAllTablesAsync()
        {
            var tables = await _context.Tables.ToListAsync();
            return tables;
        }
        public async Task<ResturantTable> GetTableByIDAsync(int tableID)
        {
            var tables = await _context.Tables.FirstOrDefaultAsync(t => t.Id == tableID);
            return tables;
        }
        public async Task<int> CreateTableAsync(ResturantTable Table)
        {
            _context.Tables.Add(Table);
            await _context.SaveChangesAsync();
            return Table.Id;
        }   

        public async Task<bool> UpdateTableAsync(ResturantTable Table)
        {
            _context.Tables.Add(Table);
             var result = await _context.SaveChangesAsync();
            if(result != 0) { return true; }
            return false;
        }
        public async Task<bool> DeleteTableAsync(int tableID)
        {
            var rowsAffected = await _context.Tables.Where(t => t.Id == tableID).ExecuteDeleteAsync();
            if(rowsAffected > 0) { return true; }
            return false;
        }
    }
}
