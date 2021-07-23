﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTest.Options
{
    public class TokenGeneratorOptions
    {
        public string Secret { get; set; }
        public TimeSpan AccessExpiration { get; set; }
    }
}
