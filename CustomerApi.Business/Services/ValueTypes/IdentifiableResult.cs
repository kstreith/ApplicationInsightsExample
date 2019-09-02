using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Business.Services.ValueTypes
{
    public class IdentifiableResult<T>
    {
        public IdentifiableResult(string id, T value)
        {
            Id = id;
            Value = value;
        }

        public string Id { get; set; }

        public T Value { get; set; }
    }
}
