using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Conservacion.Entidades;
using Conservacion_LogicaNegocio;

namespace ConservacionAPI.Controllers
{
    [Route("api/personal")]
    [ApiController]
    [Authorize]
    public class PersonalController : ControllerBase
    {
        private readonly PersonalLN _personalLN;

        public PersonalController(PersonalLN personalLN)
        {
            _personalLN = personalLN;
        }

        // GET: api/personal
        [HttpGet]
        public ActionResult<List<Personal>> Get()
        {
            try
            {
                var lista = _personalLN.Listar();

                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar el personal.");
            }
        }

        // GET: api/personal/1
        [HttpGet("{id:int}")]
        public ActionResult<Personal> GetPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador debe ser mayor que cero.");
            }

            try
            {
                var personal = _personalLN.ObtenerPorId(id);

                if (personal == null)
                {
                    return NotFound("No se encontró el personal solicitado.");
                }

                return Ok(personal);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al consultar el personal.");
            }
        }

        // POST: api/personal

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult<Personal> Post([FromBody] Personal personal)
        {
            try
            {
                int nuevoId = _personalLN.Insertar(personal);

                personal.IdPersonal = nuevoId;

                return CreatedAtAction(nameof(GetPorId), new { id = nuevoId }, personal);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al registrar el personal.");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Put(int id,[FromBody] Personal personal)
        {
            if (id <= 0 || id != personal.IdPersonal)
            {
                return BadRequest("El identificador no coincide.");
            }

            try
            {
                bool actualizado = _personalLN.Actualizar(personal);

                if (!actualizado)
                    return NotFound("No se encontró el personal solicitado.");

                return Ok("Personal actualizado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al actualizar el personal.");
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
                bool eliminado = _personalLN.Eliminar(id);

                if (!eliminado)
                    return NotFound("No se encontró el personal.");

                return Ok("Personal eliminado correctamente.");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return Conflict("No se puede eliminar el personal porque tiene actividades asociadas.");
            }
            catch (Exception)
            {
                return StatusCode(500,
                    "Error al eliminar el personal.");
            }
        }



    }
}
