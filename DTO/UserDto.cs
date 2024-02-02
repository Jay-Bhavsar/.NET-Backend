using FluentValidation;
using System;

namespace ENTITYAPP.DTO
{
    public class UserDto
    {
  

        public string email { get; set; }

        public string password { get; set; }
    }

    //public class UserValidator : AbstractValidator<UserDto>
    //{
    //    public UserValidator()
    //    {
    //        RuleFor(x => x.email).EmailAddress();
    //        RuleFor(x => x.password).MinimumLength(10);
    //    }
    //}
}
