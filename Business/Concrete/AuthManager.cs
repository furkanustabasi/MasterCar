using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [ValidationAspect(typeof(UserForRegisterDtoValidator))]
        public IDataResult<AccessToken> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var userExists = UserExists(userForRegisterDto.Email);
            if (userExists.Success)
            {
                return new ErrorDataResult<AccessToken>(Messages.UserEmailExist);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(user);

            return CreateAccessToken(user);
        }

        [ValidationAspect(typeof(UserForLoginDtoValidator))]
        public IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto)
        {
            var isExists = UserExists(userForLoginDto.Email);
            if (!isExists.Success)
            {
                return new ErrorDataResult<AccessToken>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, isExists.Data.PasswordHash, isExists.Data.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>(Messages.PasswordError);
            }

            return CreateAccessToken(isExists.Data);
        }

        public IDataResult<User> UserExists(string email)
        {
            return _userService.GetUserByMail(email);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);

            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

    }
}