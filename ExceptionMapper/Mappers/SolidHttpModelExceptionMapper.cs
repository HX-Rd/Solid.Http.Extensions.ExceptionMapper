using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    public class SolidHttpModelExceptionMapper : SolidHttpExceptionMapper<SolidHttpRequestModelException>
    {
        public async override Task<SolidHttpRequestModelException> MapException(HttpResponseMessage response, IServiceProvider serviceProvider, string message = null)
        {
            var content = response.Content;
            var mime = response.Content?.Headers?.ContentType?.MediaType;
            var deserializers = serviceProvider.GetServices<IDeserializer>();
            var deserializer = deserializers.FirstOrDefault(d => d.CanDeserialize(mime));
            var modelState = null as Dictionary<string, string[]>;
            if (deserializer != null)
            {
                try
                {
                    var errorObj = await deserializer.DeserializeAsync<ModelStateModel>(content);
                    message = errorObj.Message;
                    modelState = errorObj.ModelState;
                }
                catch (Exception) { }
            }

            message = message ?? "SolidHttpModelException";

            var ex = await base.MapException(response, serviceProvider, message);
            ex.ModelState = modelState;
            return ex;
        }
    }

}
