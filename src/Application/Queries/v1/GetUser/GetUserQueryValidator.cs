using FluentValidation;

namespace Autenticacao.Jwt.Application.Queries.v1.GetUser
{
    public class GetuserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetuserQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name not found");
        }
    }
}