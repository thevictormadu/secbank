using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecBank.Abstractions;
using SecBank.Core;
using SecBank.Entities.DTO;

namespace SecBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<TransactionController> _logger;
        private readonly ApiResponse _response;
        private readonly ITransactionService _transactionService;

        public TransactionController(AppDbContext db, ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            _db = db;
            _response = new ApiResponse();
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet("GetToken")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken()
        {
            _logger.LogInformation("||GetToken|| endpoint triggered");
            try
            {
                var result = await _transactionService.GetToken();
                if (result == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Unsuccessful");
                    return BadRequest(_response);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetTransactions()
        {
            _logger.LogInformation("||GetAllTransactions|| endpoint triggered");
            try
            {
                var result = _transactionService.GetTransactions();
                if (result == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Unsuccessful");
                    return BadRequest(_response);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("PostTransaction")]
        public async Task<ActionResult<ApiResponse>> PostTransaction(PostTransactionDto transaction)
        {
            _logger.LogInformation("||PostTransaction|| endpoint triggered");

            try
            {
                if (ModelState.IsValid)
                {
                    var posted = await _transactionService.PostTransaction(transaction);
                    if (posted)
                    {
                        _response.StatusCode = System.Net.HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = transaction;
                        return Ok(_response);
                    };
                }

                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Unsuccessful");
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("PostTransactions")]
        public async Task<ActionResult> PostTransactions(IEnumerable<PostTransactionDto> transactions)
        {
            _logger.LogInformation("||PostTransactions|| endpoint triggered");
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var transaction in transactions)
                    {
                        var posted = await _transactionService.PostTransaction(transaction);
                        if (posted)
                        {
                            _response.StatusCode = System.Net.HttpStatusCode.OK;
                            _response.IsSuccess = true;
                            _response.Result = transactions;

                            return Ok(_response);
                        };

                        break;
                    }
                }
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Unsuccessful");
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}