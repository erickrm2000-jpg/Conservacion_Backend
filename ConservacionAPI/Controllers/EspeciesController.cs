
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Conservacion.Entidades;
using Conservacion_LogicaNegocio;

namespace ConservacionAPI.Controllers
{
    [Route("api/especies")]
    [ApiController]
    [Authorize]
    public class EspeciesController : ControllerBase
    {
        private readonly EspecieLN _especieLN;

        public EspeciesController(EspecieLN especieLN)
        {
            _especieLN = especieLN;
        }

        // GET: api/especies
        [HttpGet]
        public ActionResult<List<Especie>> Get()
        {
            try
            {
                var lista = _especieLN.Listar();
                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar las especies.");
            }
        }

        // GET: api/especies/1
        [HttpGet("{id:int}")]
        public ActionResult<Especie> GetPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador debe ser mayor que cero.");
            }

            try
            {
                var especie = _especieLN.ObtenerPorId(id);

                if (especie == null)
                {
                    return NotFound("No se encontró la especie solicitada.");
                }

                return Ok(especie);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar la especie.");
            }
        }

        // solicitud POST llama lógica de negocio y devolverá el código HTTP

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult<Especie> Post([FromBody] Especie especie)
        {
            try
            {
                int nuevoId = _especieLN.Insertar(especie);

                especie.IdEspecie = nuevoId;

                return CreatedAtAction(nameof(GetPorId), new { id = nuevoId }, especie);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict("Ya existe una especie con ese nombre científico.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al registrar la especie.");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Put(int id, [FromBody] Especie especie)
        {
            if (id <= 0 || id != especie.IdEspecie)
            {
                return BadRequest("El identificador no coincide.");
            }
            try
            {
                bool actualizado = _especieLN.Actualizar(especie);

                if (!actualizado)
                {
                    return NotFound("No se encontró la especie solicitada.");
                }

                return Ok("Especie actualizada correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict("Ya existe una especie con ese nombre científico.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al actualizar la especie.");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador no es válido.");
            }

            try
            {
                bool eliminado = _especieLN.Eliminar(id);

                if (!eliminado)
                {
                    return NotFound("No se encontró la especie solicitada.");
                }

                return Ok("Especie eliminada correctamente.");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return Conflict("No se puede eliminar la especie porque tiene registros asociados.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al eliminar la especie.");
            }
        }




    }
}
