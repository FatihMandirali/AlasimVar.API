namespace AlasimVar.Application.Enums;

public enum ProcessStatusEnum
{
    Undefined = 0,
    Success = 200,
    BadRequest = 400,
    AccessDenied = 403,
    NotFound = 404,
    AccountLocked = 423,
    InternalServerError = 500,
    Error = 501,
}