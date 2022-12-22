using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UitvoeringController
{
    private readonly WdprContext _context;
    
    public UitvoeringController(WdprContext context)
    {
        _context = context;
    }
}