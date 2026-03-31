using MediatR;
using PhoneBook.Application.Commom;

namespace PhoneBook.Application.RegisterContact;

public class RegisterContactCommand : IRequest<Result>
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public int PhoneNumber { get; init; }
    public string? Email { get; init; }
    public DateOnly? BirthDate { get; set; }
}

public class RegisterContactCommandHandler : IRequestHandler<RegisterContactCommand, Result>
{
    public Task<Result> Handle(RegisterContactCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}