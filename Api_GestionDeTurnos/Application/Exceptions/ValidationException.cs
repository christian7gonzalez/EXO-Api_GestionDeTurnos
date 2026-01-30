using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api_GestionDeTurnos.Application.Exceptions
{
   
    public class ValidationException : Exception
    {
        public IEnumerable<string> Errors { get; }
        public ValidationException(string message) : base(message) { }
        public ValidationException(IEnumerable<string> errors)
            : base("Se produjeron errores de validación")
        {
            Errors = errors;
        }
    }
    
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}
