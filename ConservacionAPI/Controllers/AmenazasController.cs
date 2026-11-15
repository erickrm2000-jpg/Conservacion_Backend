
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Conservacion.Entidades;
using Conservacion_LogicaNegocio;

namespace ConservacionAPI.Controllers
{
    [Route("api/amenazas")]
    [ApiController]
    [Authorize]
    public class AmenazasController : ControllerBase
    {
        private readonly AmenazaLN _amenazaLN;

        public AmenazasController(AmenazaLN amenazaLN)
        {
            _amenazaLN = amenazaLN;
        }

        // GET: api/amenazas
        [HttpGet]
        public ActionResult<List<Amenaza>> Get()
        {
            try
            {
                var lista = _amenazaLN.Listar();

                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar las amenazas.");
            }
        }

        // GET: api/amenazas/1
        [HttpGet("{id:int}")]
        public ActionResult<Amenaza> GetPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador debe ser mayor que cero.");
            }

            try
            {
                var amenaza = _amenazaLN.ObtenerPorId(id);

                if (amenaza == null)
                {
                    return NotFound("No se encontró la amenaza solicitada.");
                }

                return Ok(amenaza);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar la amenaza.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult<Amenaza> Post(
            [FromBody] Amenaza amenaza)
        {
            try
            {
                int nuevoId = _amenazaLN.Insertar(amenaza);

                amenaza.IdAmenaza = nuevoId;

                return CreatedAtAction(nameof(GetPorId),
                    new { id = nuevoId },
                    amenaza);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return BadRequest("El área de conservación seleccionada no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar amenaza: " + ex);

                return StatusCode(500,"Error al registrar la amenaza.");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Put(
            int id,
            [FromBody] Amenaza amenaza)
        {
            if (id <= 0 || id != amenaza.IdAmenaza)
            {
                return BadRequest(
                    "El identificador no coincide.");
            }

            try
            {
                bool actualizado = _amenazaLN.Actualizar(amenaza);

                if (!actualizado)
                {
                    return NotFound("No se encontró la amenaza solicitada.");
                }

                return Ok("Amenaza actualizada correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return BadRequest("El área de conservación seleccionada no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar amenaza: " + ex);

                return StatusCode(500,"Error al actualizar la amenaza.");
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
                bool eliminado = _amenazaLN.Eliminar(id);

                if (!eliminado)
                {
                    return NotFound("No se encontró la amenaza solicitada.");
                }

                return Ok(
                    "Amenaza eliminada correctamente.");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return Conflict("No se puede eliminar la amenaza porque tiene registros relacionados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar amenaza: " + ex);

                return StatusCode(500,
                    "Error al eliminar la amenaza.");
            }
        }


    }
}

