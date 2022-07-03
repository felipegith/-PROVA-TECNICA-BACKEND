using LOCACOES.API.Database;
using LOCACOES.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace LOCACOES.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly MySqlContext _context;

        public LocacaoController(MySqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var locacoes = (from fl in _context.Filmes.AsNoTracking()
                                join lc in _context.Locacaos.AsNoTracking()
                                on fl.Id equals lc.Id_Filme
                                select new { fl, lc }
                      ).Distinct().ToList();


                if (locacoes.Any())
                    return Ok(locacoes);

                return NotFound();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            try
            {
                var locacao = _context.Locacaos.SingleOrDefault(x => x.Id == id);

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
        public ActionResult Post([FromBody] Locacao model)
        {

            try
            {
                model.DataLocacao = DateTime.Now;

                var buscarFilme = _context.Filmes.Where(x => x.Id == model.Id_Filme).FirstOrDefault();
                if (buscarFilme == null)
                    return NotFound();

                if (buscarFilme.Lancamento == 2)
                {
                    model.DataDevolucao = DateTime.Now.AddDays(2);
                }
                else
                {
                    model.DataDevolucao = DateTime.Now.AddDays(3);
                }

                _context.Locacaos.Add(model);
                _context.SaveChanges();

                return Ok(model);
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
                var delete = _context.Locacaos.SingleOrDefault(x => x.Id == id);

                if (delete == null)
                    return NotFound();

                _context.Locacaos.Remove(delete);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Update(Locacao model)
        {

            try
            {
                var update = _context.Locacaos.SingleOrDefault(x => x.Id == model.Id);

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
    }
}
