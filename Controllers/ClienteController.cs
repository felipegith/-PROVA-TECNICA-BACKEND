using LOCACOES.API.Database;
using LOCACOES.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LOCACOES.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly MySqlContext _context;

        public ClienteController(MySqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var clientes = _context.Clientes.ToList();
                return Ok(clientes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{cpf}")]
        public ActionResult GetByCpf([FromRoute] string cpf)
        {
            try
            {
                var cliente = _context.Clientes.Include(x => x.Locacoes).SingleOrDefault(x => x.Cpf == cpf);

                if (cliente == null)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("id/{id}")]
        public ActionResult GetById([FromRoute] int id)
        {

            try
            {
                var cliente = _context.Clientes.Include(x => x.Locacoes).SingleOrDefault(x => x.Id == id);

                if (cliente == null)
                    return NotFound();

                return Ok(cliente);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Cliente model, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var validandoCpf = _context.Clientes.Where(x => x.Cpf == model.Cpf).ToList();

                if (validandoCpf.Any())
                    return BadRequest();

                var create = _context.Clientes.Add(model);

                _context.SaveChanges();

                return Ok(model);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] Cliente model)
        {
            try
            {
                var update = _context.Clientes.SingleOrDefault(x => x.Id == model.Id);

                if (update == null)
                    return NotFound();

                _context.Entry(update).CurrentValues.SetValues(model);
                _context.SaveChanges();

                return Ok(update);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                var delete = _context.Clientes.SingleOrDefault(x => x.Id == id);

                if (delete == null)
                    return NotFound();

                _context.Clientes.Remove(delete);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SegundoCliente")]
        public ActionResult SegundoCliente()
        {
            var segundoCliente = _context.Clientes.Include(x => x.Locacoes).OrderByDescending(x => x.Locacoes.Count()).Skip(1).Take(1).ToList();
            return Ok(segundoCliente);
        }

        [HttpGet("Devolucao")]
        public ActionResult Devolucao()
        {
            var consulta = (from cl in _context.Clientes.AsNoTracking()
                            join lc in _context.Locacaos.AsNoTracking() on cl.Id equals lc.Id_Cliente
                            join fl in _context.Filmes.AsNoTracking() on lc.Id_Filme equals fl.Id
                            select new { cl, lc, fl });

            consulta = consulta.Where(x => x.lc.DataDevolucao < DateTime.Now);

            return Ok(consulta);
        }

    }
}
