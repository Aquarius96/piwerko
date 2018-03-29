using System;
using System.Collections.Generic;
using System.Text;
using Piwerko.Data.Dto;
using Piwerko.Data.Models;

namespace Piwerko.Data.Interfaces
{
    public interface IUserService
    {
        ResultDto<LoginResultDto> Login(LoginModel loginModel);
        ResultDto<BaseDto> Register(RegisterModel registerModel);
    }
}
