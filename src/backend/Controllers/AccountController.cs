using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly WdprContext _context;
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(WdprContext context, UserManager<Account> userManager, SignInManager<Account> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            AccountDataInitializer.SeedData(_userManager, _roleManager);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Registreer_Medewerker")]
        public async Task<ActionResult<IEnumerable<Medewerker>>> Registreer([FromBody] Medewerker m)
        {
            if (m.Email != null && _userManager.FindByEmailAsync(m.Email).Result == null)
            {
                var result = await _userManager.CreateAsync(m, AccountDataInitializer.HashPassword(m.Password));
                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(result);
                }
                var addRoleResult = await _userManager.AddToRoleAsync(m, "Admin");
                return !addRoleResult.Succeeded ? new BadRequestObjectResult(addRoleResult) : StatusCode(201);
            }
            return m.Email == null ? new BadRequestObjectResult("No email provided") : StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLogin accountLogin)
        {
            var _user = await _userManager.FindByNameAsync(accountLogin.UserName);

            if (_user != null)
            {
                await _signInManager.SignInAsync(_user, true);
                return Ok();
            }

            return Unauthorized();
        }

        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccount()
        {
            if (_context.Account == null)
            {
                return NotFound();
            }
            return await _context.Account.ToListAsync();
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            if (_context.Account == null)
            {
                return NotFound();
            }
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            if (_context.Account == null)
            {
                return Problem("Entity set 'WdprContext.Account'  is null.");
            }
            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Account == null)
            {
                return NotFound();
            }
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return (_context.Account?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
