using Api_GestionDeTurnos.Domain.Entities;

namespace Api_GestionDeTurnos.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }
}
