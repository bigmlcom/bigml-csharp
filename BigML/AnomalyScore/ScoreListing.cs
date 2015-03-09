using System;
using System.Threading.Tasks;

namespace BigML
{
    public class AnomalyScoreListing : Query<AnomalyScore.Filterable, AnomalyScore.Orderable, AnomalyScore>
    {
        public AnomalyScoreListing(Func<string, Task<Listing<AnomalyScore>>> client)
            : base(client)
        {
        }
    }
}
