using ExamSystem.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Exceptions.Pagination
{
    public class PaginationInvalidException : BaseException
    {
        public PaginationInvalidException(string mess) : base(mess, HttpStatusCode.BadRequest)
        {
            
        }
    }
}
