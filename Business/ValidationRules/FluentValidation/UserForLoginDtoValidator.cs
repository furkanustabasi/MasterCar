using Entities.DTOs;
using FluentValidation;
using System.Linq;
using System.Text.RegularExpressions;

namespace Business.ValidationRules.FluentValidation
{
    public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginDtoValidator()
        {
            RuleFor(u => u.Email).NotNull();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();

            RuleFor(u => u.Password).NotNull();
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).MinimumLength(8);
            RuleFor(p => p.Password).Matches(@"[A-Z]+").WithMessage("Şifreniz büyük harf içermelidir.");
            RuleFor(p => p.Password).Matches(@"[a-z]+").WithMessage("Şifreniz küçük harf içermelidir.");
            RuleFor(p => p.Password).Matches(@"[0-9]+").WithMessage("Şifreniz sayı içermelidir.");
            RuleFor(p => p.Password).Matches(@"[\!\?\*\.]+").WithMessage("şifreniz en az bir karakter içermelidir(!? *.)."); ;
        }
    }
}
