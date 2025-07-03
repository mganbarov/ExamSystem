using ExamSystem.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Exceptions.Password
{
    public class InvalidPasswordException : BaseException
    {
        public InvalidPasswordException(string mess, HttpStatusCode status = HttpStatusCode.BadRequest) : base(mess,status)
        {
            
        }
    }
}
