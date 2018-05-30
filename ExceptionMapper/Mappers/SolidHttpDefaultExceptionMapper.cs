using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HXRd.Solid.Http.Extensions.ExceptionMapper
{
    public class SolidHttpDefaultExceptionMapper : SolidHttpExceptionMapper<SolidHttpRequestException>
    {
        public async override Task<SolidHttpRequestException> MapException(HttpResponseMessage response, IServiceProvider serviceProvider, string message)
        {
            message = await response.Content.ReadAsStringAsync();
            return await base.MapException(response, serviceProvider, message);
        }
    }
}
