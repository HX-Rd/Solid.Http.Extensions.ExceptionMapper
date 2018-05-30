using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    [System.Serializable]
    public class SolidHttpRequestException : System.Exception
    {
        public SolidHttpRequestException() { }
        public SolidHttpRequestException(string message) : base(message) { }
        public SolidHttpRequestException(string message, System.Exception inner) : base(message, inner) { }
        protected SolidHttpRequestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public HttpStatusCode? StatusCode
        {
            get
            {
                return this.Data["StatusCode"] as HttpStatusCode?;
            }
            set
            {
                this.Data["StatusCode"] = value;
            }
        }

        public string Body
        {
            get
            {
                return this.Data["Body"] as string;
            }
            set
            {
                this.Data["Body"] = value;
            }
        }
        public string ReasonPhrase
        {
            get
            {
                return this.Data["ReasonPhrase"] as string;
            }
            set
            {
                this.Data["ReasonPhrase"] = value;
            }
        }


        public Dictionary<string, IEnumerable<string>> Headers
        {
            get
            {
                return this.Data["Headers"] as Dictionary<string, IEnumerable<string>>;
            }
            set
            {
                this.Data["Headers"] = value;
            }
        }
    }
}
