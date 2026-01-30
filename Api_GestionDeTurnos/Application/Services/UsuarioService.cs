using Api_GestionDeTurnos.Application.DTOs;
using Api_GestionDeTurnos.Application.Exceptions;
using Api_GestionDeTurnos.Application.Helpers;
using Api_GestionDeTurnos.Application.Interfaces;
using Api_GestionDeTurnos.Domain.Entities;
using Api_GestionDeTurnos.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using ValidationException = Api_GestionDeTurnos.Application.Exceptions.ValidationException;

namespace Api_GestionDeTurnos.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ILogger<UsuarioService> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IValidator<UsuarioDTO> _UsuarioDtoValidator;
        private readonly IValidator<LoginDTO> _LoginDtoValidator;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public UsuarioService(ILogger<UsuarioService> logger, IUsuarioRepository usuarioRepository,
            IValidator<UsuarioDTO> usuarioDtoValidator,
            IValidator<LoginDTO> loginDtoValidator,
            IMapper mapper, ITokenService tokenService) 
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _UsuarioDtoValidator = usuarioDtoValidator;
            _LoginDtoValidator = loginDtoValidator;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<UsuarioValidadoDTO> CrearUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            try
            {
                var result = await _UsuarioDtoValidator.ValidateAsync(usuarioDTO);
                if (!result.IsValid)
                    throw new ValidationException(result.Errors.Select(e => e.ErrorMessage));

                //Valido si ya existe un usuario con ese email
                if (await _usuarioRepository.GetByEmailAsync(usuarioDTO.Email) is not null)
                    throw new ConflictException("Ya existe un usuario con ese email.");

                var usuario = new Usuario
                {
                    Nombre = usuarioDTO.Nombre ?? string.Empty,
                    Apellido = usuarioDTO.Apellido ?? string.Empty,
                    Email = usuarioDTO.Email,
                    FechaAlta = DateTime.Now,
                    Sexo = usuarioDTO.Sexo,
                    FechaNacimiento = FechaHelpers.ConvertirStringADateTime(usuarioDTO.FechaNacimiento),
                    Password = PasswordHelpers.HashPassword(usuarioDTO.Password)

                };

                await _usuarioRepository.AddAsync(usuario);
                return _mapper.Map<UsuarioDTO, UsuarioValidadoDTO>(usuarioDTO);
                    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario.");
                throw new ConflictException(ex.Message);
            }
        }

        public async Task<UsuarioValidadoDTO> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                var result = await _LoginDtoValidator.ValidateAsync(loginDTO);
                if (!result.IsValid)
                    throw new ValidationException(result.Errors.Select(e => e.ErrorMessage));
                var usuario = await _usuarioRepository.GetByEmailAsync(loginDTO.Email);
                if (usuario is null)
                {
                    throw new ConflictException("No existe el usuario en el sistema.");
                }
                if(!PasswordHelpers.VerifyPassword(loginDTO.Password, usuario.Password))
                {
                    throw new ValidationException("La contraseña no es correcta.");
                }
                UsuarioValidadoDTO usuarioValidadoDTO = _mapper.Map<Usuario, UsuarioValidadoDTO>(usuario);
                usuarioValidadoDTO.Token = _tokenService.GenerateToken(usuario);
             
                return usuarioValidadoDTO; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al loggear.");
                throw new ConflictException(ex.Message);
            }
        }
        public async Task EliminarUsuario(Guid idUsuario)
        {
            await _usuarioRepository.DeleteAsync(idUsuario);
        }

        public async Task ModificarUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var result = _UsuarioDtoValidator.Validate(usuarioDTO);
                if (!result.IsValid)
                    throw new ValidationException(
                        result.Errors.Select(e => e.ErrorMessage)
                    );

                var usuario = await _usuarioRepository.GetByIdAsync(usuarioDTO.IdUsuario.Value);
                if (usuario is null)
                    throw new ConflictException("El usuario no existe");

                if (usuarioDTO.Email != usuario.Email &&
                    await _usuarioRepository.GetByEmailAsync(usuarioDTO.Email) is not null)
                {
                    throw new ConflictException("El email ya está en uso");
                }

                usuario.ActualizarEmail(usuarioDTO.Email);
                usuario.ActualizarNombre(usuarioDTO.Nombre);

                await _usuarioRepository.UpdateAsync(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el usuario.");
                throw new ConflictException(ex.Message);
            }
            
  
        }
    }
}
