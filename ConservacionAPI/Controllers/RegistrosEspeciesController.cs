using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Conservacion.Entidades;
using Conservacion_LogicaNegocio;

namespace ConservacionAPI.Controllers
{
    [Route("api/registros-especies")]
    [ApiController]
    [Authorize]
    public class RegistrosEspeciesController : ControllerBase
    {
        private readonly RegistroEspecieLN _registroLN;

        public RegistrosEspeciesController(RegistroEspecieLN registroLN)
        {
            _registroLN = registroLN;
        }

        // GET: api/registros-especies
        [HttpGet]
        public ActionResult<List<RegistroEspecie>> Get()
        {
            try
            {
                var lista = _registroLN.Listar();

                return Ok(lista);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al consultar los registros de especies.");
            }
        }

        // GET: api/registros-especies/1
        [HttpGet("{id:int}")]
        public ActionResult<RegistroEspecie> GetPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El identificador debe ser mayor que cero.");
            }

            try
            {
                var registro = _registroLN.ObtenerPorId(id);

                if (registro == null)
                {
                    return NotFound("No se encontró el registro solicitado.");
                }

                return Ok(registro);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al consultar el registro.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult<RegistroEspecie> Post(
            [FromBody] RegistroEspecie registro)
        {
            try
            {
                int nuevoId = _registroLN.Insertar(registro);

                registro.IdRegistro = nuevoId;

                return CreatedAtAction(nameof(GetPorId), new { id = nuevoId },registro);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return BadRequest("El área o la especie seleccionada no existe.");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict("Ya existe un registro de esta especie " + "en la misma área y fecha.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al registrar la especie en el área.");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Put(int id, [FromBody] RegistroEspecie registro)
        {
            if (id <= 0 || id != registro.IdRegistro)
            {
                return BadRequest("El identificador no coincide.");
            }

            try
            {
                bool actualizado = _registroLN.Actualizar(registro);

                if (!actualizado)
                {
                    return NotFound("No se encontró el registro solicitado.");
                }

                return Ok("Registro de especie actualizado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return BadRequest("El área o la especie seleccionada no existe.");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 2601 || ex.Number == 2627)
            {
                return Conflict("Ya existe un registro para esta especie " + "en la misma área y fecha.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al actualizar el registro de especie.");
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
                bool eliminado = _registroLN.Eliminar(id);

                if (!eliminado)
                {
                    return NotFound("No se encontró el registro solicitado.");
                }

                return Ok("Registro de especie eliminado correctamente.");
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
                when (ex.Number == 547)
            {
                return Conflict("No se puede eliminar el registro porque tiene datos relacionados.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error al eliminar el registro de especie.");
            }
        }



    }
}

