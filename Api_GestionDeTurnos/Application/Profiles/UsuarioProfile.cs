using Api_GestionDeTurnos.Application.DTOs;
using Api_GestionDeTurnos.Domain.Entities;
using AutoMapper;

namespace Api_GestionDeTurnos.Application.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<Usuario, UsuarioValidadoDTO>();
            CreateMap<UsuarioDTO, UsuarioValidadoDTO>();
        }
    }
}
