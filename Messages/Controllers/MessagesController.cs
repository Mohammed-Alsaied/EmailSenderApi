using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Messages.Server;
[Route("api/[controller]")]
[ApiController]
public class MessagesController : BaseController<Message, MessageViewModel>
{
    private readonly IEmailService _emailService;

    public MessagesController(IMessageUnitOfWork unitOfWork, IMapper mapper, IValidator<MessageViewModel> validator, ILogger<BaseController<Message, MessageViewModel>> logger, IEmailService emailService)
        : base(unitOfWork, mapper, validator, logger)
    {
        _emailService = emailService;
    }

    [HttpGet("GetSubjects")]
    public async Task<IActionResult> GetSubjects()
    {
        var subjects = await _unitOfWork.SelectByExpressionAsync(m => m.Subject);
        return Ok(subjects);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmail([FromForm] string[] emails, Guid messageId)
    {
        var message = await _unitOfWork.ReadByIdAsync(messageId);

        if (message != null)
        {
            var emailSender = new EmailSender(emails, message.Subject, message.MessageText!);
            _emailService.SendEmail(emailSender);
            return StatusCode(StatusCodes.Status200OK,
               new Response
               {
                   Status = "Success",
                   Message = "Email Sent Successfully",
               });
        }
        return StatusCode(StatusCodes.Status400BadRequest,
                new Response
                {
                    Status = "Error",
                    Message = "Couldnot send message to email, please try again."
                });

    }

    [Authorize(Roles = "Admin")]
    public override Task Delete(Guid id)
    {
        return base.Delete(id);
    }

    [Authorize(Roles = "Admin")]
    public override Task<IActionResult> Post([FromForm] MessageViewModel productViewModel)
    {
        return base.Post(productViewModel);
    }
}