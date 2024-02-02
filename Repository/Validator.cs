using ENTITYAPP.DTO;
using ENTITYAPP.Repository.Models;
using FluentValidation;

namespace ENTITYAPP.Repository
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.email).EmailAddress();
            RuleFor(x => x.password).MinimumLength(10);

        }
    }
}
