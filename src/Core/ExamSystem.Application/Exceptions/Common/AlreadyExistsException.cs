using ExamSystem.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Exceptions.Common
{
    public class AlreadyExistsException : BaseException
    {
        public AlreadyExistsException(string mess) : base(mess, System.Net.HttpStatusCode.BadRequest)
        {
            
        }
    }
}
