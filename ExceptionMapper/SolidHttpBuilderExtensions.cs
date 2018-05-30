using Microsoft.Extensions.DependencyInjection;
using Solid.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    public static class SolidHttpBuilderExtensions
    {
        public static ISolidHttpBuilder AddExceptionMappings(this ISolidHttpBuilder builder, ExceptionMappingOptions options = default(ExceptionMappingOptions))
        {
            options = options ?? new ExceptionMappingOptions();
            /*builder.Services.AddSingleton<SolidHttpExceptionMapper<SolidHttpRequestException>>(new SolidHttpDefaultExceptionMapper());
            builder.Services.AddSingleton<SolidHttpExceptionMapper<SolidHttpRequestModelException>>(new SolidHttpModelExceptionMapper());*/
            builder.Services.AddSingleton<IExceptionMapper>(new SolidHttpDefaultExceptionMapper());
            builder.Services.AddSingleton<IExceptionMapper>(new SolidHttpModelExceptionMapper());
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
