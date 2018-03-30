using System;
using System.Collections.Generic;
using System.Text;

namespace Piwerko.Data.Dto
{
    public class LoginResultDto : BaseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
