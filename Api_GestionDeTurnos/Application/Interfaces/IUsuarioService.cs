using Api_GestionDeTurnos.Application.DTOs;

namespace Api_GestionDeTurnos.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioValidadoDTO> CrearUsuarioAsync(UsuarioDTO usuarioDTO);
        Task<UsuarioValidadoDTO> LoginAsync(LoginDTO loginDTO);
        Task EliminarUsuario(Guid idUsuario);
        Task ModificarUsuario(UsuarioDTO usuarioDTO);   //No se usa este metodo por ahora, futura mejora

    }
}
