using Api_GestionDeTurnos.Application.DTOs;
using Api_GestionDeTurnos.Application.Exceptions;
using Api_GestionDeTurnos.Application.Interfaces;
using Api_GestionDeTurnos.Domain.Entities;
using Api_GestionDeTurnos.Domain.Enum;
using Api_GestionDeTurnos.Domain.Interfaces;
using Api_GestionDeTurnos.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace Api_Turnos.Application.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ILogger<TurnoService> _logger;
        private readonly ITurnoRepository _turnoRepository;
        public TurnoService(ILogger<TurnoService> logger, ITurnoRepository turnoRepository) 
        {
            _logger = logger;
            _turnoRepository = turnoRepository;
        }

      
        public async Task<Turno> CrearTurnoAsync(CrearTurnoDTO createTurnoDTO)
        {
            try
            {
                if (createTurnoDTO.IdUsuario == Guid.Empty)
                    throw new ValidationException("IdUsuario es requerido");

                var turno = Turno.Crear(createTurnoDTO.IdUsuario, createTurnoDTO.FechaTurno, createTurnoDTO.Descripcion);
                await _turnoRepository.AddAsync(turno);
                return turno;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el turno.");
                throw new ConflictException(ex.Message);
            }
        }
        public async Task EliminarTurnoAsync(Guid idTurno)
        {
            await _turnoRepository.DeleteAsync(idTurno);  
        }

        public Task<List<Turno>> GetAllTurnosIdUsuarioAsync(Guid idUsuario)
        {
            try
            {
                if (idUsuario == Guid.Empty)
                {
                    throw new ValidationException("IdUsuario es requerido.");
                }
                return _turnoRepository.GetAllTurnosByIdUsuarioAsync(idUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al traer la lista de turnos.");
                throw new ConflictException(ex.Message);
            }
        }
    }
}
