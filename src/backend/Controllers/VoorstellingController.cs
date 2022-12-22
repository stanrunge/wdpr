using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class VoorstellingController : ControllerBase
{
    private readonly WdprContext _context;
    
    public VoorstellingController(WdprContext context)
    {
        _context = context;
    }
}