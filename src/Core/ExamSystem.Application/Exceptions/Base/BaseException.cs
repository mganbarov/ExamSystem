using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Exceptions.Base
{
    public class BaseException : Exception
    {
        public BaseException(string mess, HttpStatusCode status) : base(mess)
        {
            Status = status;
        }
        public HttpStatusCode Status { get; set; }
    }
}
