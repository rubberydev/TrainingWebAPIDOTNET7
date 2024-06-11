using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTO;
using MagicVilla_API.Repositorio.Contrato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        readonly ILogger<NumeroVillaController> _logger;
        readonly INumeroVillaRepositorio _numeroVillaRepo;
        readonly IVillaRepositorio _villaRepo; //_dBContext
        readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, 
            IVillaRepositorio villaRepo, 
            INumeroVillaRepositorio numeroVillaRepo, 
            IMapper mapper)
        {
            this._logger = logger;
            this._villaRepo = villaRepo;
            this._numeroVillaRepo = numeroVillaRepo;
            this._mapper = mapper;
            this._response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult< APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Obtener Numeros Villas");
                IEnumerable<NumeroVilla> numeroVillaList = await _numeroVillaRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillaList);
                _response.statusCode = HttpStatusCode.OK;
                _response.EsExitoso = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.EsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
            
        

        [HttpGet("id:int",Name ="GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError($"Error al traer numero villa con Id:  {id}");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.EsExitoso = false;
                    return BadRequest(_response);
                }
                var numeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaNo == id);
                if (numeroVilla == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.EsExitoso = false;
                    return NotFound(_response);
                }
                _logger.LogInformation("Se obtuvo la villa");
                _response.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
                _response.statusCode = HttpStatusCode.OK;
                _response.EsExitoso = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.EsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto crearDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numeroVillaRepo.Obtener(v => v.VillaNo == crearDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de villa ya existe");
                    return BadRequest(ModelState);
                }

                if(await _villaRepo.Obtener(v => v.Id == crearDTO.VillaId)== null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de la villa no existe!");
                    return BadRequest(ModelState);
                }

                if (crearDTO == null)
                {
                    return BadRequest(crearDTO);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(crearDTO);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _numeroVillaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;
                _response.EsExitoso = true;
                return CreatedAtRoute("GetNumeroVilla", modelo.VillaNo, _response);
            }
            catch (Exception ex)
            {
                _response.EsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.EsExitoso= false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var numeroVilla = await _numeroVillaRepo.Obtener(v => v.VillaNo == id);

                if (numeroVilla == null)
                {
                    _response.EsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _numeroVillaRepo.Remover(numeroVilla);
                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.EsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto actualizarDTO)
        {
            if (actualizarDTO == null || id != actualizarDTO.VillaNo) 
            {
                _response.EsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _villaRepo.Obtener(v => v.Id == actualizarDTO.VillaId) == null)
            {
                ModelState.AddModelError("ClaveForanea", "El Id de la villa no existe!");
                return BadRequest(ModelState);
            }

            NumeroVilla modelo = _mapper.Map<NumeroVilla>(actualizarDTO);
            
            await _numeroVillaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            _response.EsExitoso = true;
            return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarParcialVilla(int id, JsonPatchDocument<NumeroVillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                _response.EsExitoso = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var villa = await _numeroVillaRepo.Obtener(v => v.VillaNo == id,tracked:false);

            NumeroVillaUpdateDto villaDto = _mapper.Map<NumeroVillaUpdateDto>(villa);

            if (villa == null) 
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.EsExitoso = false;
                return BadRequest(_response);
            }
            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             NumeroVilla modelo = _mapper.Map<NumeroVilla>(villaDto);
             await _numeroVillaRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            _response.EsExitoso = true;
            return Ok(_response);
        }
    }
    
}
