using webapi.Data;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketController
{
    private readonly WdprContext _context;
    
    public TicketController(WdprContext context)
    {
        _context = context;
    }
}