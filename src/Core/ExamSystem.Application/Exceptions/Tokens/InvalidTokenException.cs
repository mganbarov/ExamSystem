using ExamSystem.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Exceptions.Tokens
{
    public class InvalidTokenException : BaseException
    {
        public InvalidTokenException(string mess, HttpStatusCode status = HttpStatusCode.BadRequest) : base(mess, status)
        {
            
        }
    }
}
