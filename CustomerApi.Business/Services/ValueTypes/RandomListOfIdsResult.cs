using System.Collections.Generic;

namespace CustomerApi.Business.ValueTypes
{
    public class RandomListOfIdsResult
    {
        public int Count { get; set; }
        public List<string> Values { get; set; }
    }
}
