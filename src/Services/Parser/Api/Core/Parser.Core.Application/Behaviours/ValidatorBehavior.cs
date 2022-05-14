namespace Parser.Core.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest :
        IRequest<TResponse>
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, 
            ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var typeName = typeof(TRequest).FullName;

                var context = new ValidationContext<TRequest>(request);

                var validationResults =
                    await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(result => result.Errors)
                    .Where(error => error != null).ToList();

                if (failures.Any())
                {
                    _logger.LogWarning(
                        "Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
                        typeName, request, failures);

                    throw new ValidationException("Command Validation Errors for type {typeof(TRequest).Name}",
                        failures);
                }
            }

            return await next();
        }
    }
}
