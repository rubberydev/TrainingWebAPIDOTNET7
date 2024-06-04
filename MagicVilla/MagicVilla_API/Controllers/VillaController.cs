using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        readonly ILogger<VillaController> _logger;
        readonly ApplicationDbContext _dBContext;
        readonly IMapper _mapper;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext dBContext,IMapper mapper)
        {
            this._logger = logger;
            this._dBContext = dBContext;
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult< IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _dBContext.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villaList));
        }
            
        

        [HttpGet("id:int",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al traer la villa: {id}");
                return BadRequest();
            }
            var villa = await _dBContext.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Se obtuvo la villa");
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CrearVilla([FromBody] VillaCreateDTO crearDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _dBContext.Villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == crearDTO.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (crearDTO == null)
            {
                return BadRequest(crearDTO);
            }

            Villa modelo = _mapper.Map<Villa>(crearDTO);
            

            await _dBContext.Villas.AddAsync(modelo);
            await _dBContext.SaveChangesAsync();

            return CreatedAtRoute("GetVilla",modelo.Id,modelo);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = await _dBContext.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null) 
            { 
                return NotFound();
            }
            _dBContext.Remove(villa);
            await _dBContext.SaveChangesAsync();
            return NoContent();

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarVilla(int id, [FromBody] VillaUpdateDTO actualizarDTO)
        {
            if (actualizarDTO == null || id != actualizarDTO.Id) 
            { 
                return BadRequest();
            }

            Villa modelo = _mapper.Map<Villa>(actualizarDTO);
            
            _dBContext.Villas.Update(modelo);
            await _dBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActualizarParcialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _dBContext.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
            
            VillaUpdateDTO villaDto = _mapper.Map<VillaUpdateDTO>(villa);
           
            if (villa == null) return BadRequest();
            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Villa modelo = _mapper.Map<Villa>(villaDto);
            _dBContext.Villas.Update(modelo);
            await _dBContext.SaveChangesAsync();
            return NoContent();
        }
    }
    
}
