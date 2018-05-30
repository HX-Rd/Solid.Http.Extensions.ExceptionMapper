using System;
using System.Collections.Generic;
using System.Text;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    public class ExceptionMappingSettingsProvider : IExceptionMappingSettingsProvider
    {
        private ExceptionMappingOptions _options;

        public ExceptionMappingSettingsProvider(ExceptionMappingOptions options)
        {
            _options = options;
        }
        public ExceptionMappingOptions GetMappingOptions()
        {
            return _options;
        }
    }
}
