using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ZitplaatsController
{
    private readonly WdprContext _context;
    
    public ZitplaatsController(WdprContext context)
    {
        _context = context;
    }
}