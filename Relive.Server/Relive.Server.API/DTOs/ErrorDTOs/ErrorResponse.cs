using Relive.Server.Core.Entities.Errors;
using System.Collections.Generic;
namespace Relive.Server.API.DTOs.ErrorDTOs
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<Error> ErrorList { get; set; }
    }
}
