using System;
using System.Collections.Generic;
using System.Text;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    public class ExceptionMappingOptions
    {
        public ExceptionMappingOptions()
        {
            Mappers = new List<IExceptionMapper>();
        }
        public IEnumerable<IExceptionMapper> Mappers { get; set; }
        public IExceptionMapper DefaultMapper { get; set; }
    }
}
