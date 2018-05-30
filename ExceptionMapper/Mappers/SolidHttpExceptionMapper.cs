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
    public class SolidHttpExceptionMapper<T> : IExceptionMapper where T : System.Exception, new()
    {
        public async virtual Task<T> MapException(HttpResponseMessage response, IServiceProvider serviceProvider, string message = null)
        {
            var contentRaw = await response.Content.ReadAsStringAsync();
            var ex = (T)Activator.CreateInstance(typeof(T), message);

            ex.Data["Body"] = contentRaw;
            ex.Data["StatusCode"] = response.StatusCode;
            ex.Data["ReasonPhrase"] = response.ReasonPhrase;
            var headers = response.Headers.AsEnumerable();
            if (response.Content != null)
                headers = headers.Union(response.Content.Headers);
            ex.Data["Headers"] = headers.ToDictionary(p => p.Key, p => p.Value, StringComparer.OrdinalIgnoreCase);
            return ex;
        }

        protected async Task<Q> DeserializeError<Q>(HttpResponseMessage response, IServiceProvider serviceProvider) where Q : class
        {
            var content = response.Content;
            var mime = response.Content?.Headers?.ContentType?.MediaType;
            var deserializers = serviceProvider.GetServices<IDeserializer>();
            var deserializer = deserializers.FirstOrDefault(d => d.CanDeserialize(mime));
            var serializedObj = null as Q;
            if (deserializer != null)
            {
                serializedObj = await deserializer.DeserializeAsync<Q>(content);
            }
            return serializedObj;
        }
    }
}
