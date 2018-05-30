using Solid.Http.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    public static class SolidHttpCoreBuilderExtensions
    {
        public static ISolidHttpCoreBuilder AddExceptionMappings(this ISolidHttpCoreBuilder builder, ExceptionMappingOptions options = default(ExceptionMappingOptions))
        {
            options = options ?? new ExceptionMappingOptions();
            builder.Services.AddSingleton<SolidHttpExceptionMapper<SolidHttpRequestException>>(new SolidHttpDefaultExceptionMapper());
            builder.Services.AddSingleton<SolidHttpExceptionMapper<SolidHttpRequestModelException>>(new SolidHttpModelExceptionMapper());
            var provider = new ExceptionMappingSettingsProvider(options);
            builder.Services.AddSingleton<IExceptionMappingSettingsProvider>(provider);
            foreach(var mapper in options.Mappers)
            {
                builder.Services.AddSingleton(mapper);
            }
            return builder;
        }
    }
}
