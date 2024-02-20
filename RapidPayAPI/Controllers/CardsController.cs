using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidPayAPI.Models;
using RapidPayAPI.Services;

namespace RapidPayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly RapidPayDbContext AppDbContext;

        public CardsController(RapidPayDbContext AppDbContext)
        {
            this.AppDbContext = AppDbContext;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(Card card)
        {
            if (card.CardNumber.Length != 15)
            {
                return BadRequest("Invalid card number format. Must be 15 digits.");
            }

            try
            {
                AppDbContext.Add(card);
                await AppDbContext.SaveChangesAsync();
                return Ok("Card added correctly");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(int cardId, decimal amount)
        {
            if (cardId <= 0)
            {
                return BadRequest("Invalid card ID.");
            }

            try
            {
                var updateBalance = await AppDbContext.Cards.FirstOrDefaultAsync(x => x.CardId == cardId);
                if (updateBalance == null)
                {
                    return NotFound("Card not found.");
                }

                PaymentFeeCalculator feeCalculator = PaymentFeeCalculator.Instance;
                decimal fee = feeCalculator.CalculateFee();
                if (amount + fee > updateBalance.Balance)
                {
                    return BadRequest("Insufficient balance.");
                }

                updateBalance.Balance -= amount + fee;
                await AppDbContext.SaveChangesAsync();

                return Ok("Payment applied.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(string cardNum)
        {
            if (string.IsNullOrEmpty(cardNum))
            {
                return BadRequest("Invalid card number.");
            }

            try
            {
                var card = await AppDbContext.Cards.FirstOrDefaultAsync(x => x.CardNumber == cardNum);
                if (card == null)
                {
                    return NotFound("Card not found.");
                }

                return Ok(card.Balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

