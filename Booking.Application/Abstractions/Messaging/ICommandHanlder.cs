using Booking.Domain.Abstractions;
using MediatR;

namespace Booking.Application.Abstractions.Messaging;

public interface ICommandHanlder<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHanlder<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}