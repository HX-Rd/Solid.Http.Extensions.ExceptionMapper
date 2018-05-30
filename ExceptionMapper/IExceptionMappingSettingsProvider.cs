using System;
using System.Collections.Generic;
using System.Text;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    public interface IExceptionMappingSettingsProvider
    {
        ExceptionMappingOptions GetMappingOptions();
    }
}
