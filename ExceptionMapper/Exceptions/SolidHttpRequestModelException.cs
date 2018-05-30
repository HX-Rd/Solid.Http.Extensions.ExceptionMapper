using System;
using System.Collections.Generic;
using System.Text;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    [Serializable]
    public class SolidHttpRequestModelException : SolidHttpRequestException
    {
        public SolidHttpRequestModelException() { }
        public SolidHttpRequestModelException(string message) : base(message) { }
        public SolidHttpRequestModelException(string message, Exception inner) : base(message, inner) { }
        protected SolidHttpRequestModelException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public Dictionary<string, string[]> ModelState
        {
            get
            {
                return this.Data["ModelState"] as Dictionary<string, string[]>;
            }
            set
            {
                this.Data["ModelState"] = value;
            }
        }
    }
}
