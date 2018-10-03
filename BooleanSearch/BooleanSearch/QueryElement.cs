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
        public bool notCondition { get; set; }
        public QueryElement()
        {

        }
        public QueryElement(string name, bool notCondition)
        {
            this.name = name;
            this.notCondition = notCondition;
        }
        public QueryElement(string name, bool notCondition, string operation)
        {
            this.name = name;
            this.notCondition = notCondition;
            this.operation = operation;
        }
    }
}
