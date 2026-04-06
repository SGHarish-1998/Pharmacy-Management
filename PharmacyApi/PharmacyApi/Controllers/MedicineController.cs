using Microsoft.AspNetCore.Mvc;
using PharmacyApi.Models;
using PharmacyApi.Services;

namespace PharmacyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;

        public MedicineController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
        {
            var medicines = await _pharmacyService.GetAllMedicinesAsync();
            return Ok(medicines);
        }

        [HttpPost]
        public async Task<ActionResult<Medicine>> AddMedicine(Medicine medicine)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _pharmacyService.AddMedicineAsync(medicine);
            return CreatedAtAction(nameof(GetMedicines), new { id = result.Id }, result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Medicine>>> SearchMedicines([FromQuery] string name)
        {
            var result = await _pharmacyService.SearchMedicinesAsync(name);
            return Ok(result);
        }

        [HttpPost("sale")]
        public async Task<ActionResult<Sale>> RecordSale([FromBody] SaleRequest request)
        {
            var result = await _pharmacyService.RecordSaleAsync(request.MedicineId, request.Quantity);
            if (result == null) return BadRequest("Medicine not found or insufficient quantity.");
            return Ok(result);
        }

        [HttpGet("sales")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            var sales = await _pharmacyService.GetAllSalesAsync();
            return Ok(sales);
        }
    }

    public class SaleRequest
    {
        public Guid MedicineId { get; set; }
        public int Quantity { get; set; }
    }
}
