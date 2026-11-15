using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Conservacion_LogicaNegocio;
using Conservacion.Entidades;

namespace ConservacionAPI.Controllers
{
    [Route("api/areas")]
    [ApiController]
    [Authorize]
    public class AreasController : ControllerBase
    {
        private readonly AreaConservacionLN _areaLN;

        public AreasController(AreaConservacionLN areaLN)
        {
            _areaLN = areaLN;
        }

        [HttpGet]
        public ActionResult<List<AreaConservacion>> Get()
        {
            try
            {
                var lista = _areaLN.Listar();

                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar las áreas.");
            }
        }


        [HttpGet("{id:int}")]
        public ActionResult<AreaConservacion> GetPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador debe ser mayor que cero.");
            }
            try
            {
                var area = _areaLN.ObtenerPorId(id);
                if (area == null)
                {
                    return NotFound("No se encontró el área solicitada.");
                }
                return Ok(area);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar el área.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult<AreaConservacion> Post([FromBody] AreaConservacion area)
        {
            try
            {
                int nuevoId = _areaLN.Insertar(area);
                area.IdArea = nuevoId;

                return CreatedAtAction(
                    nameof(GetPorId),new { id = nuevoId },
                    area);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict("Ya existe un área con ese nombre.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al registrar el área.");
            }
        }



        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Put(int id, [FromBody] AreaConservacion area)
        {
            if (id <= 0 || id != area.IdArea)
            {
                return BadRequest("El identificador no coincide.");
            }

            try
            {
                bool actualizado = _areaLN.Actualizar(area);

                if (!actualizado)
                {
                    return NotFound("No se encontró el área solicitada.");
                }

                return Ok("Área actualizada correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict("Ya existe un área con ese nombre.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al actualizar el área.");
            }
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("El identificador no es válido.");

            try
            {
                bool eliminado = _areaLN.Eliminar(id);

                if (!eliminado)
                    return NotFound("No se encontró el área.");

                return Ok("Área eliminada correctamente.");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return Conflict("No se puede eliminar el área porque tiene registros asociados.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al eliminar el área.");
            }
        }

    }
}
