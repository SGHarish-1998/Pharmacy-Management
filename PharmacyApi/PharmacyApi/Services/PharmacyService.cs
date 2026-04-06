using System.Text.Json;
using PharmacyApi.Models;

namespace PharmacyApi.Services
{
   //after model create a service for the api call design
    public interface IPharmacyService
    {
        //as per the requrement add the rules in it.
        Task<IEnumerable<Medicine>> GetAllMedicinesAsync();
        Task<Medicine> AddMedicineAsync(Medicine medicine);
        Task<IEnumerable<Medicine>> SearchMedicinesAsync(string name);
        Task<Sale?> RecordSaleAsync(Guid medicineId, int quantity);
        Task<IEnumerable<Sale>> GetAllSalesAsync();
    }

    public class PharmacyService : IPharmacyService
    {
        private readonly string _medicinesFilePath;
        private readonly string _salesFilePath;
        private readonly JsonSerializerOptions _jsonOptions; // this for the json thing json to C# object

        //do the di
        public PharmacyService(IWebHostEnvironment env)
        {
            var dataDir = Path.Combine(env.ContentRootPath, "Data");
            if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);  //

            _medicinesFilePath = Path.Combine(dataDir, "medicines.json");
            _salesFilePath = Path.Combine(dataDir, "sales.json");

            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, WriteIndented = true };  //for the ease of read in json
        }

        public async Task<IEnumerable<Medicine>> GetAllMedicinesAsync()
        {
            if (!File.Exists(_medicinesFilePath)) return new List<Medicine>();
            var json = await File.ReadAllTextAsync(_medicinesFilePath);
            return JsonSerializer.Deserialize<List<Medicine>>(json, _jsonOptions) ?? new List<Medicine>();
        }

        public async Task<Medicine> AddMedicineAsync(Medicine medicine)
        {
            var medicines = (await GetAllMedicinesAsync()).ToList();
            medicines.Add(medicine);
            await File.WriteAllTextAsync(_medicinesFilePath, JsonSerializer.Serialize(medicines, _jsonOptions));
            return medicine;
        }

        public async Task<IEnumerable<Medicine>> SearchMedicinesAsync(string name)
        {
            var medicines = await GetAllMedicinesAsync();
            if (string.IsNullOrWhiteSpace(name)) return medicines;
            return medicines.Where(m => m.FullName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Sale?> RecordSaleAsync(Guid medicineId, int quantity)
        {
            var medicines = (await GetAllMedicinesAsync()).ToList();
            var medicine = medicines.FirstOrDefault(m => m.Id == medicineId);

            if (medicine == null || medicine.Quantity < quantity) return null;

            medicine.Quantity -= quantity;
            await File.WriteAllTextAsync(_medicinesFilePath, JsonSerializer.Serialize(medicines, _jsonOptions));

            var sales = (await GetAllSalesAsync()).ToList();
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                MedicineId = medicineId,
                MedicineName = medicine.FullName,
                Quantity = quantity,
                TotalPrice = medicine.Price * quantity,
                SaleDate = DateTime.UtcNow
            };
            sales.Add(sale);
            await File.WriteAllTextAsync(_salesFilePath, JsonSerializer.Serialize(sales, _jsonOptions));

            return sale;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            if (!File.Exists(_salesFilePath)) return new List<Sale>();
            var json = await File.ReadAllTextAsync(_salesFilePath);
            return JsonSerializer.Deserialize<List<Sale>>(json, _jsonOptions) ?? new List<Sale>();
        }
    }
}
