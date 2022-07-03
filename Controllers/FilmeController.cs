using LOCACOES.API.Database;
using LOCACOES.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LOCACOES.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly MySqlContext _context;

        public FilmeController(MySqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var consulta = (from cl in _context.Clientes.AsNoTracking()
                                join lc in _context.Locacaos.AsNoTracking()
                                on cl.Id equals lc.Id_Cliente
                                select new { cl, lc }).ToList();


                var filmes = _context.Filmes.ToList();

                if (filmes.Any())
                    return Ok(filmes);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{titulo}")]
        public ActionResult GetByTitle([FromRoute] string titulo)
        {
            try
            {
                var locacao = _context.Filmes.Include(x => x.Locacoes).SingleOrDefault(x => x.Titulo == titulo);

                if (locacao == null)
                    return NotFound();


                return Ok(locacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public ActionResult GetByid([FromRoute] int id)
        {
            try
            {
                var locacao = _context.Filmes.Include(x => x.Locacoes).SingleOrDefault(x => x.Id == id);

                if (locacao == null)
                    return NotFound();


                return Ok(locacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Filme model)
        {
            try
            {
                var create = _context.Filmes.Add(model);
                _context.SaveChanges();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                var delete = _context.Filmes.SingleOrDefault(x => x.Id == id);

                if (delete == null)
                    return NotFound();

                _context.Filmes.Remove(delete);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Update(Filme model)
        {
            try
            {
                var update = _context.Filmes.SingleOrDefault(x => x.Id == model.Id);

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

        [HttpGet("NaoAlugados")]
        public ActionResult NaoAlugados()
        {
            var filmes = _context.Filmes.Include(x => x.Locacoes).ToList();

            List<Filme> filmesNaoAlugados = new List<Filme>();
            foreach (var item in filmes)
            {
                if (!item.Locacoes.Any())
                    filmesNaoAlugados.Add(item);
            }

            return Ok(filmesNaoAlugados);
        }

        [HttpGet("CincoFilmes")]
        public ActionResult CincoFilmes()
        {

            try
            {
                var clientes = _context.Filmes.Include(x => x.Locacoes).OrderByDescending(x => x.Locacoes.Count()).Take(5).ToList();

                return Ok(clientes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("TresFilmes")]
        public ActionResult TresFilmes()
        {

            try
            {
                var clientes = _context.Filmes.Include(x => x.Locacoes).OrderBy(x => x.Locacoes.Count()).Take(3).ToList();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
