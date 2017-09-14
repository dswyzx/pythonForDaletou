using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotNetCoreTest.Models
{
    public class DLTInfo : IEquatable<DLTInfo>
    {
        public string DLTNum { get; set; }

        public string BeforeNum1 { get; set; }
        public string BeforeNum2 { get; set; }
        public string BeforeNum3 { get; set; }
        public string BeforeNum4 { get; set; }
        public string BeforeNum5 { get; set; }


        public string AfterNum1 { get; set; }
        public string AfterNum2 { get; set; }
        public string TheDate { get; set; }

        public bool Equals(DLTInfo other)
        {
            return this.DLTNum == other.DLTNum;
        }
    }
}
