namespace Loader.Core.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }

        public ValidationException() 
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors
                .AddRange(failures.Select(failure => failure.ErrorMessage));
        }
    }
}
