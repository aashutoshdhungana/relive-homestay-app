using Relive.Server.Core.Interfaces;
using System.Net;

namespace Relive.Server.Core.Entities.ServiceResponseAggregate
{
    public class ServiceResponse<TEntity> : IAggregateRoot
    {
        public string Code { get; set; }
        public HttpStatusCode HttpCode { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }
    }
}
