using System.Globalization;

namespace Api_GestionDeTurnos.Application.Helpers
{
    public static class FechaHelpers
    {
        public static DateTime? ConvertirStringADateTime(string fechaString)
        {
            DateTime fecha;

            if (!DateTime.TryParseExact(fechaString, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out fecha))
            {
                return null;
            }
            return fecha;
        }
    }
}
