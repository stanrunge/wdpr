
using webapi.Models;

namespace webapi.Services;

// Honestly not sure if this is needed comes from:
// https://www.c-sharpcorner.com/article/jwt-token-authentication-in-asp-net-core-6-web-api-using-three-tier-architecture/

//https://www.tektutorialshub.com/asp-net-core/jwt-authentication-in-asp-net-core/#testing-jwt-authentication-in-aspnet-core
public class ServiceAccount
{
    public readonly IRepository<Account> _repository;
    public ServiceAccount(IRepository<Account> repository)
    {
        _repository = repository;
    }
    //Create Method
    public async Task<Account> AddAccount(Account account)
    {
        try
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            else
            {
                return await _repository.Create(account);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void DeleteAccount(string Id)
    {
        try
        {
            if (Id != null)
            {
                var obj = _repository.GetAll().Where(x => x.Id == Id).FirstOrDefault();
                _repository.Delete(obj);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public void UpdateAccount(string Id)
    {
        try
        {
            if (Id != null)
            {
                var obj = _repository.GetAll().Where(x => x.Id == Id).FirstOrDefault();
                if (obj != null) _repository.Update(obj);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public IEnumerable<Account> GetAllAccounts()
    {
        try
        {
            return _repository.GetAll().ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}