using System;
using System.Collections.Generic;

namespace TEST.Model
{
    public class Error    {
        public string code { get; set; } 
        public string description { get; set; } 
    }

    public class RespostaSso    {
        public bool succeeded { get; set; } 
        public List<Error> errors { get; set; } 
    
    }
}
