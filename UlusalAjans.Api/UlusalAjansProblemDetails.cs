using Microsoft.AspNetCore.Mvc;
using UIusalAjans.Domain.Exceptions;

namespace UlusalAjans.Api
{
    public class UlusalAjansProblemDetails : ProblemDetails
    {
        public UlusalAjansProblemDetails(UlusalAjansException exception)
        {
            Title = exception.Message;
            Detail = exception.Detail;
            Status = StatusCodes.Status400BadRequest;
            Type = "ulusal-ajans-exception";
        }
    }
}
