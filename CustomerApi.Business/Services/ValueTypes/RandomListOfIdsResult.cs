using System.Collections.Generic;

namespace CustomerApi.Business.ValueTypes
{
    public class RandomListOfIdsResult
    {
        public int Count { get; set; }
#pragma warning disable CA2227 // Collection properties should be read only
        public List<string> Values { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
