using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigML
{
    public static class Constants
    {
        public const string numeric = "numeric";
        public const string categorical = "categorical";
        public const string datetime = "datetime";
        public const string text = "text";
        public const string items = "items";

        public static OpType getOpType(string type)
        {
            switch (type)
            {
                case Constants.numeric:
                    return OpType.Numeric;
                case Constants.categorical:
                    return OpType.Categorical;
                case Constants.datetime:
                    return OpType.Datetime;
                case Constants.text:
                    return OpType.Text;
                case Constants.items:
                    return OpType.Items;
                default:
                    return OpType.Error;
            }

        }
    }
}
