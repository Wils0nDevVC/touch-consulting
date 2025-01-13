using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchConsulting.GestorInventario.Application.Dto
{
    public class AuthDto 
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class AuthResponseDto : UserDto
    {
       
        public required string Token { get; set; }
        [JsonIgnore]
        public new  string Password { get; set; }
    }
}
