using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

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