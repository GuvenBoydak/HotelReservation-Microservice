namespace Shared.Filters;

public class ValidatorFilterAttribute : ActionFilterAttribute
{   
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            List<string> errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            
            context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(404, errors, "Hatalı işlem"));
        }
    }
}