using System.Collections.Generic;

namespace Backend.Models.Implementation
{
    public class Expression
    {
        public string RegularExpression { get; set; }
    }

    public class JsonExpression
    {
        public List<Expression> Expressions { get; set; }
    }
}