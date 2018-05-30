using Solid.Http;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    public static class SolidResponseExtensions
    {

        public static SolidHttpRequest ThrowsException<T>(this SolidHttpRequest request, Func<HttpResponseMessage, bool> predicate) where T : System.Exception, new()
        {
            request.OnResponse += async (sender, args) =>
            {
                if (predicate(args.Response))
                {
                    var exMapper = args.Services.GetServices<IExceptionMapper>()
                                                .OfType<SolidHttpExceptionMapper<T>>()
                                                .FirstOrDefault();
                    var ex = await exMapper.MapException(args.Response, args.Services);
                    throw ex;
                }
            };
            return request;
        }

        public static SolidHttpRequest ThrowsException(this SolidHttpRequest request)
        {
            ThrowsException(
                request,
                (r) => !r.IsSuccessStatusCode
            );
            return request;
        }
        public static SolidHttpRequest ThrowsException(this SolidHttpRequest request, Func<HttpResponseMessage, bool> predicate)
        {
            request.OnResponse += async (sender, args) =>
            {
                if (predicate(args.Response))
                {
                    var options = args.Services.GetService<IExceptionMappingSettingsProvider>();
                    var mappingOptions = options.GetMappingOptions();
                    if(mappingOptions.CustomDefaultMapper != null)
                    {
                        dynamic defaultCustomMapper = options.GetMappingOptions().CustomDefaultMapper;
                        var cex = await defaultCustomMapper.MapException(args.Response, args.Services) as System.Exception;
                        throw cex;
                    }
                    if(mappingOptions.UseModelStateExceptionsAsDefault)
                    {
                        var modelMapper = args.Services.GetServices<IExceptionMapper>()
                                                    .OfType<SolidHttpExceptionMapper<SolidHttpRequestModelException>>()
                                                    .FirstOrDefault();
                        var mex = await modelMapper.MapException(args.Response, args.Services);
                        throw mex;
                    }
                    var defaultMapper = args.Services.GetServices<IExceptionMapper>()
                                                .OfType<SolidHttpExceptionMapper<SolidHttpRequestException>>()
                                                .FirstOrDefault();
                    var ex = await defaultMapper.MapException(args.Response, args.Services);
                    throw ex;
                }
            };
            return request;
        }

        public static SolidHttpRequest ThrowsException<T>(this SolidHttpRequest request) where T : System.Exception, new()
        {
            ThrowsException<T>(
                request,
                (r) => !r.IsSuccessStatusCode
            );
            return request;
        }
    }
}
