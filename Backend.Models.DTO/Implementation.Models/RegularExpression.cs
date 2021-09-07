using System.Collections.Generic;

namespace Models.ImplementationModels
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