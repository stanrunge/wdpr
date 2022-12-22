using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ZaalController
{
    private readonly WdprContext _context;
    
    public ZaalController(WdprContext context)
    {
        _context = context;
    }
}