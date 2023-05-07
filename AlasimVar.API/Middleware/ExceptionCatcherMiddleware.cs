using AlasimVar.Application.Enums;
using AlasimVar.Application.Models.BaseModel;

namespace AlasimVar.API.Middleware;

public class ExceptionCatcherMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionCatcherMiddleware> _logger;

    public ExceptionCatcherMiddleware(ILogger<ExceptionCatcherMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Occured");
            await context.Response.WriteAsJsonAsync(new BaseResponse<object>(ProcessStatusEnum.Error,
                new FriendlyMessage("Sunucu Kaynaklı Hata Meydana Geldi")));
        }
    }
}