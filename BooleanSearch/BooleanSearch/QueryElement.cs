using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanSearch
{
    public class QueryElement
    {
        public string name { get; set; }
        public string operation { get; set; }
        public bool type { get; set; }
        public QueryElement(string name, bool type)
        {
            this.name = name;
            this.type = type;
        }
        public QueryElement(string name, bool type, string operation)
        {
            this.name = name;
            this.type = type;
            this.operation = operation;
        }
    }
}
