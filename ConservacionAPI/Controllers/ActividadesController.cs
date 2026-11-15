using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Conservacion.Entidades;
using Conservacion_LogicaNegocio;

namespace ConservacionAPI.Controllers
{
    [Route("api/actividades")]
    [ApiController]
    [Authorize]
    public class ActividadesController : ControllerBase
    {
        private readonly ActividadConservacionLN _actividadLN;

        public ActividadesController(ActividadConservacionLN actividadLN)
        {
            _actividadLN = actividadLN;
        }

        // GET: api/actividades
        [HttpGet]
        public ActionResult<List<ActividadConservacion>> Get()
        {
            try
            {
                var lista = _actividadLN.Listar();

                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar las actividades.");
            }
        }

        // GET: api/actividades/1
        [HttpGet("{id:int}")]
        public ActionResult<ActividadConservacion> GetPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador debe ser mayor que cero.");
            }

            try
            {
                var actividad = _actividadLN.ObtenerPorId(id);

                if (actividad == null)
                {
                    return NotFound(
                        "No se encontró la actividad solicitada.");
                }

                return Ok(actividad);
            }
            catch (Exception)
            {
                return StatusCode(500,
                    "Error al consultar la actividad.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult<ActividadConservacion> Post([FromBody] ActividadConservacion actividad)
        {
            try
            {
                int nuevoId = _actividadLN.Insertar(actividad);

                actividad.IdActividad = nuevoId;

                return CreatedAtAction(nameof(GetPorId), new { id = nuevoId }, actividad);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return BadRequest("El área o el personal responsable no existe.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al registrar la actividad.");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Put(int id,[FromBody] ActividadConservacion actividad)
        {
            if (id <= 0 || id != actividad.IdActividad)
            {
                return BadRequest("El identificador no coincide.");
            }

            try
            {
                bool actualizado = _actividadLN.Actualizar(actividad);

                if (!actualizado)
                {
                    return NotFound("No se encontró la actividad solicitada.");
                }

                return Ok("Actividad actualizada correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return BadRequest("El área o el responsable seleccionado no existe.");
            }

            catch (Exception ex)
            {
                Console.WriteLine("ERROR AL ACTUALIZAR ACTIVIDAD:");
                Console.WriteLine(ex.ToString());

                return StatusCode(500,"Error al actualizar la actividad. " + "Revisar la consola de Visual Studio.");
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
                bool eliminado = _actividadLN.Eliminar(id);

                if (!eliminado)
                {
                    return NotFound("No se encontró la actividad solicitada.");
                }

                return Ok("Actividad eliminada correctamente.");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return Conflict("No se puede eliminar la actividad porque tiene registros asociados.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar actividad: " + ex);

                return StatusCode(500,"Error al eliminar la actividad.");
            }
        }



    }
}
