using System.Text.RegularExpressions;

namespace Api_GestionDeTurnos.Domain.Helpers
{
    public static class DomainHelpers
    {
        public static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Validación básica de email
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
    }
}
