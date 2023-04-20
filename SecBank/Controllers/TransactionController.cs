
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SecBank.Data;
using SecBank.Entities.DTO;
using SecBank.Services;

namespace SecBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ApiResponse _response;
        private readonly ITransactionService _transactionService;
        public TransactionController(AppDbContext db, ITransactionService transactionService)
        {
            _db = db;
            _response = new ApiResponse();
            _transactionService = transactionService;
        }




        [HttpPost("PostTransaction")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> PostTransaction(PostTransactionDto transaction)
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


        [HttpPost("PostTransactions")]
        [Authorize]
        public async Task<ActionResult> PostTransactions(IEnumerable<PostTransactionDto> transactions)
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

                    
                }
                
            }
            _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Unsuccessful");
            return BadRequest(_response);
        }



        [HttpGet("GetToken")]
        
        public async Task<IActionResult> GetToken()
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


        [HttpGet("GetAllTransactions")]
        [Authorize]
        public async Task<IActionResult> GetTransactions()
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
    }
}
