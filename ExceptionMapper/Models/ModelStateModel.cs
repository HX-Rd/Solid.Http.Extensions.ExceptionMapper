using System;
using System.Collections.Generic;
using System.Text;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    [Serializable]
    public class ModelStateModel
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> ModelState { get; set; }
    }
}
