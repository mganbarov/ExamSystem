﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTOs.Identity
{
    public record LoginRequestDto(string Username, string Password);
    
}
