using System.Collections;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Talaqah.Application.Common.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(request, cancellationToken);

        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

        dynamic handler = _serviceProvider.GetRequiredService(handlerType);

        return await handler.Handle(
            (dynamic)request,
            cancellationToken);
    }

    private async Task ValidateAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken)
    {
        var requestType = request.GetType();
        var validatorInterfaceType = typeof(IValidator<>).MakeGenericType(requestType);
        var validatorsEnumerableType = typeof(IEnumerable<>).MakeGenericType(validatorInterfaceType);

        if (_serviceProvider.GetService(validatorsEnumerableType) is not IEnumerable validators)
            return;

        var contextType = typeof(ValidationContext<>).MakeGenericType(requestType);
        dynamic context = Activator.CreateInstance(contextType, request)!;

        var failures = new List<ValidationFailure>();
        foreach (dynamic validator in validators)
        {
            ValidationResult result = await validator.ValidateAsync(context, cancellationToken);
            if (!result.IsValid)
                failures.AddRange(result.Errors);
        }

        if (failures.Count > 0)
            throw new ValidationException(failures);
    }
}
