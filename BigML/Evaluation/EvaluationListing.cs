using System;
using System.Threading.Tasks;

namespace BigML
{
    public class EvaluationListing : Query<Evaluation.Filterable, Evaluation.Orderable, Evaluation>
    {
        public EvaluationListing(Func<string, Task<Listing<Evaluation>>> client)
            : base(client)
        {
        }
    }
}
