using System.Runtime.Serialization;

namespace Api_GestionDeTurnos.Domain.Enum
{
    public enum EstadoEnum
    {
        [EnumMember(Value = "PENDIENTE")]
        Pendiente,

        [EnumMember(Value = "CONFIRMADO")]
        Confirmado,

        [EnumMember(Value = "CANCELADO")]
        Cancelado,

        [EnumMember(Value = "FINALIZADO")]
        Finalizado
    }
}
