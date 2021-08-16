using FluentValidation;

namespace MicroRabbit.Banking.Application.Validators.AccountTransfer
{
    /// <summary>
    /// Regular command validation using Fluent Validation
    /// </summary>
    public class AccountTransferValidator : AbstractValidator<Models.AccountTransferDto>
    {
        public AccountTransferValidator()
        {
            RuleFor(x => x.FromAccount).NotEmpty().WithMessage("From Account is required!");
            RuleFor(x => x.ToAccount).NotEmpty().WithMessage("To Account is required!");
            RuleFor(x => x.TransferAmount).NotEmpty().WithMessage("Transfer Amount is required!");
            RuleFor(x => x.TransferAmount).GreaterThan(0).WithMessage("Transfer Amount must be greater than 0!");}
    }
}
