using System;
using System.Collections.Generic;

namespace Dsw2026Ej15.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyList<string> Errors { get; }

        public ValidationException(string message) : base(message)
        {
            Errors = new List<string> { message };
        }

        public ValidationException(IEnumerable<string> errors) : base("Se encontraron errores de validación")
        {
            Errors = new List<string>(errors);
        }
    }
}
