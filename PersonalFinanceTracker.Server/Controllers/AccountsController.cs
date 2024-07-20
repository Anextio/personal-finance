using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Shared.Models;
using System.Security.Principal;

namespace PersonalFinanceTracker.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private static List<Account> _accounts = new List<Account>();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_accounts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpPost]
        public IActionResult Post(Account account)
        {
            account.Id = _accounts.Count + 1;
            _accounts.Add(account);
            return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Account account)
        {
            var existingAccount = _accounts.FirstOrDefault(a => a.Id == id);
            if (existingAccount == null)
                return NotFound();

            existingAccount.Name = account.Name;
            existingAccount.Balance = account.Balance;
            existingAccount.Type = account.Type;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
                return NotFound();

            _accounts.Remove(account);
            return NoContent();
        }
    }
}