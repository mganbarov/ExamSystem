using ExamSystem.Application.DTOs.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Abstraction.Services
{
    public interface ITokenHandler
    {
        AuthTokenDto CreateJwt(string userName);
    }
}
