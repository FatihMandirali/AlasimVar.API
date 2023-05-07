using AlasimVar.API.Localize;
using AlasimVar.Application.Enums;
using AlasimVar.Application.Exceptions;
using AlasimVar.Application.Models.BaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

namespace AlasimVar.API.Filters;

public class HttpResponseExceptionFilter:IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var _stringLocalizer = context.HttpContext.RequestServices.GetService<IStringLocalizer<Resource>>();
        var exceptionHandled = context.ExceptionHandled;
        context.ExceptionHandled = true;
        switch (context.Exception)
        {
            case ErrorException ex:
                context.Result = new ObjectResult(new BaseResponse<ModelStateDictionary>(ProcessStatusEnum.Error,
                    new FriendlyMessage(_stringLocalizer[ex.Message], _stringLocalizer[ex.Message]), null));
                break;
            default:
                context.ExceptionHandled = exceptionHandled;
                break;

        }
    }
}