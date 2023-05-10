using Common.Validators;
public class MessageValidator : BaseViewModelValidator<MessageViewModel>
{
public MessageValidator()
    {
        RuleFor(m => m.MessageText).NotEmpty().NotNull().WithMessage("The field is Required");
        RuleFor(m => m.Subject).NotEmpty().NotNull().WithMessage("The field is Required");
    }
}
