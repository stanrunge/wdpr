using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class KlantController
{
    private readonly WdprContext _context;
    
    public KlantController(WdprContext context)
    {
        _context = context;
    }
}