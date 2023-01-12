
using webapi.Data;
using webapi.Models;

namespace webapi.Services;
public class RepositoryAccount : IRepository<Account>
{
    private readonly WdprContext _context;
    private readonly ILogger _logger;
    public RepositoryAccount(ILogger<Account> logger)
    {
        _logger = logger;
    }
    public async Task<Account> Create(Account account)
    {
        try
        {
            if (account != null)
            {
                var obj = _context.Add<Account>(account);
                await _context.SaveChangesAsync();
                return obj.Entity;
            }
            else
            {
                return null;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void Delete(Account account)
    {
        try
        {
            if (account != null)
            {
                var obj = _context.Remove(account);
                if (obj != null)
                {
                    _context.SaveChangesAsync();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public IEnumerable<Account> GetAll()
    {
        try
        {
            var obj = _context.Account.ToList();
            if (obj != null) return obj;
            else return null;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public Account GetById(string Id)
    {
        try
        {
            if (Id != null)
            {
                var Obj = _context.Account.FirstOrDefault(x => x.Id == Id);
                if (Obj != null) return Obj;
                else return null;
            }
            else
            {
                return null;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void Update(Account account)
    {
        try
        {
            if (account != null)
            {
                var obj = _context.Update(account);
                if (obj != null) _context.SaveChanges();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}