using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using webapi.InitData;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly WdprContext _context;
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration;

        public AccountController(WdprContext context, UserManager<Account> userManager, SignInManager<Account> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            AccountDataInitializer.SeedData(_userManager, _roleManager);
        }

        /*
        All return codes https://learn.microsoft.com/en-gb/dotnet/api/microsoft.aspnetcore.http.statuscodes?view=aspnetcore-2.2
        */

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLogin al)
        {
            //username = e-mail adres
            var _user = await _userManager.FindByNameAsync(al.UserName);

            if (_user == null)
                return Problem(title: "Unknown username", detail: $"The username: {al.UserName} couldn't be found.", statusCode: StatusCodes.Status404NotFound);


            var userRoles = await _userManager.GetRolesAsync(_user);
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name, _user.UserName),
            // new Claim(ClaimTypes.MobilePhone, _user.PhoneNumber),
            // new Claim(ClaimTypes.Email, _user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(expires: DateTime.Now.AddHours(3), claims: authClaims, signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            var result = await _signInManager.PasswordSignInAsync(_user, al.Password, true, false);
            if (!result.Succeeded)
                return BadRequest();

            return Ok(new
            {
                api_key = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                user = _user,
                Role = userRoles,
                status = "User Login Successfully"
            });

            // await _signInManager.SignInAsync(_user, true);
            // return Ok();

        }

        //TEST
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [Route("Test")]
        public async Task<ActionResult> Test()
        {
            System.Console.WriteLine("TESTING\n\n\n\nTESTING");
            return Ok();
        }

        [HttpPost]
        [Route("Registreer_klant")]
        public async Task<ActionResult<Klant>> RegistreerKlant([FromBody] AccountRegistreer ar)
        {
            if (AccountExists(ar.UserName).Result)
                return Problem(title: "Username already exists", detail: $"The username: {ar.UserName} is already in use", statusCode: StatusCodes.Status409Conflict);

            Klant k = new Klant() { UserName = ar.UserName };

            var result = await _userManager.CreateAsync(k, ar.Password);

            if (result.Succeeded)
                return StatusCode(201); // 201 = Created

            return BadRequest();
        }

        // [Authorize(Roles = Roles.Admin + ", " + Roles.Directie)]
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [Route("Registreer_medewerker")]
        public async Task<ActionResult<Medewerker>> RegistreerMedewerker([FromBody] MedewerkerRegistreer mr)
        {
            if (AccountExists(mr.UserName).Result)
                return Problem(title: "Username already exists", detail: $"The username: {mr.UserName} is already in use", statusCode: StatusCodes.Status409Conflict);

            Medewerker m = new Medewerker() { UserName = mr.UserName, Functie = mr.Functie };

            var result = await _userManager.CreateAsync(m, mr.Password);
            if (!result.Succeeded)
                return BadRequest();

            result = await _userManager.AddToRoleAsync(m, Roles.Medewerker);
            if (result.Succeeded)
                return StatusCode(201); // 201 = Created

            return BadRequest();
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Directie}")]
        [HttpPost]
        [Route("Registreer_Directie")]
        public async Task<ActionResult<Medewerker>> RegistreerDirectie([FromBody] MedewerkerRegistreer mr)
        {
            if (AccountExists(mr.UserName).Result)
                return Problem(title: "Username already exists", detail: $"The username: {mr.UserName} is already in use", statusCode: StatusCodes.Status409Conflict);

            Medewerker m = new Medewerker() { UserName = mr.UserName, Functie = mr.Functie };

            // var result = await _userManager.CreateAsync(m, DataEditor.HashPassword(m.Password));
            var result = await _userManager.CreateAsync(m, mr.Password);
            if (!result.Succeeded)
                return BadRequest();

            result = await _userManager.AddToRoleAsync(m, "Directie");
            if (result.Succeeded)
                return StatusCode(201); // 201 = Created

            return BadRequest();
        }


        private async Task<bool> AccountExists(string userName)
        {
            return await _context.Account?.AnyAsync(e => e.UserName == userName);
        }


        // OLD Below 
        /*
                [Authorize(Roles = "Admin")]
                [HttpPost]
                [Route("Registreer_Medewerker")]
                public async Task<ActionResult<IEnumerable<Medewerker>>> Registreer([FromBody] Medewerker m)
                {
                    if (m.Email != null && _userManager.FindByEmailAsync(m.Email).Result == null)
                    {
                        var result = await _userManager.CreateAsync(m, DataEditor.HashPassword(m.Password));
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
                public async Task<IActionResult> PutAccount(string id, Account account)
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

                private bool AccountExists(string id)
                {
                    return (_context.Account?.Any(e => e.Id == id)).GetValueOrDefault();
                }
                */
    }
}
