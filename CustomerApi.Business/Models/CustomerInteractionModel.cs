using System;

namespace CustomerApi.Business.Models
{
    public class CustomerInteractionModel
    {
        public string InteractionType { get; set; }
        public DateTimeOffset InteractionDateTime { get; set; }
    }
}
