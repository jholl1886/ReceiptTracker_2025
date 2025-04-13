using Microsoft.AspNetCore.Mvc;
using ReceiptTracker.API.Services;
using ReceiptTracker.Shared.DTO;


namespace ReceiptTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        private readonly ILogger<ReceiptsController> _logger;
        private readonly IWebHostEnvironment _environment;

        public ReceiptsController(
            IReceiptService receiptService,
            ILogger<ReceiptsController> logger,
            IWebHostEnvironment environment)
        {
            _receiptService = receiptService;
            _logger = logger;
            _environment = environment;
        }

        // GET: api/Receipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptDTO>>> GetReceipts()
        {
            var receipts = await _receiptService.GetAllReceiptsAsync();
            return Ok(receipts);
        }

        // GET: api/Receipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptDTO>> GetReceipt(string id)
        {
            var receipt = await _receiptService.GetReceiptByIdAsync(id);

            if (receipt == null)
            {
                return NotFound();
            }

            return receipt;
        }

        // PUT: api/Receipts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceipt(string id, ReceiptDTO receiptDto)
        {
            if (id != receiptDto.Id)
            {
                return BadRequest();
            }

            bool result = await _receiptService.UpdateReceiptAsync(receiptDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Receipts
        [HttpPost]
        public async Task<ActionResult<ReceiptDTO>> PostReceipt([FromForm] ReceiptDTO receiptDto, IFormFile? file)
        {
            byte[]? fileData = null;
            string? fileContentType = null;
            string? fileName = null;

            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Create a unique filename
                fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);

                // Save the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // reads file data correctly now
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileData = memoryStream.ToArray();

                    //this whole part saves to file system
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        memoryStream.Position = 0; //resets the position
                        await memoryStream.CopyToAsync(fileStream);
                    }
                }
                //ensures content type is valid for later
                fileContentType = !string.IsNullOrWhiteSpace(file.ContentType) ?
                  file.ContentType : "application/octet-stream";
            }

            // Set file information in the receipt DTO
            receiptDto.FileName = fileName;

            // Add receipt via service
            var createdReceipt = await _receiptService.AddReceiptAsync(receiptDto, fileData, fileContentType);

            return CreatedAtAction(nameof(GetReceipt), new { id = createdReceipt.Id }, createdReceipt);
        }

        // DELETE: api/Receipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(string id)
        {
            // Get the receipt to check if we need to delete a file
            var receipt = await _receiptService.GetReceiptByIdAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            // Delete the associated file if it exists
            if (!string.IsNullOrEmpty(receipt.FileName))
            {
                string filePath = Path.Combine(_environment.ContentRootPath, "Uploads", receipt.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error deleting file {fileName}", receipt.FileName);
                    }
                }
            }

            // Delete the receipt from the database
            bool result = await _receiptService.DeleteReceiptAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Receipts/download/5
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadReceipt(string id)
        {
            var receiptDto = await _receiptService.GetReceiptByIdAsync(id);
            if (receiptDto == null || receiptDto.FileData == null || string.IsNullOrEmpty(receiptDto.FileName))
            {
                return NotFound();
            }

            //return File(receiptDto.FileData, receiptDto.FileContentType ?? "application/octet-stream", receiptDto.FileName); didnt like this, not that good at these yet
            string contentType = "application/octet-stream"; 
            if (!string.IsNullOrWhiteSpace(receiptDto.FileContentType) &&
                receiptDto.FileContentType.IndexOfAny(new[] { '\r', '\n', ':' }) == -1)
            {
                contentType = receiptDto.FileContentType;
            }

            return File(receiptDto.FileData, contentType, receiptDto.FileName);
        }
    }
}