using BookingApp.Data;
using BookingApp.Models;
using CsvHelper;
using System.Globalization;

namespace BookingApp.Services
{
    public class CsvUploadService
    {
        private readonly BookingDbContext _dbContext;
        public CsvUploadService(BookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UploadMembersAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Member>();
            _dbContext.Members.AddRange(records);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UploadInventoryAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<InventoryItem>();
            _dbContext.InventoryItems.AddRange(records);
            await _dbContext.SaveChangesAsync();
        }
    }
}