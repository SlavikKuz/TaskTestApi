using System;
using System.Collections.Generic;
using System.Text;

namespace TaskApi1a
{
    public class ResponseRate
    {
        public bool Success { get; set; }
        public string Timestamp { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
        public ConvertFail Error { get; set; }
    }

    public class ConvertFail
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
    }
}
