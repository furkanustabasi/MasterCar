using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.DTOs;


namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<AccessToken> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto);
        IDataResult<User> UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }

}
