using FluentValidation;
using UserService.Application.Features.User.Commands.Update;

namespace UserService.Application.Common.Validation;

public class UpdateProfilePictureValidator : AbstractValidator<UpdateProfilePictureCommand>
{
    public UpdateProfilePictureValidator()
    {
        RuleFor(x => x.ProfilePictureUrl)
            .NotEmpty()
            .WithMessage("Profile picture URL is required.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Profile picture URL is not a valid URL.");
    }
}
