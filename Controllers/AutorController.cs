using ApiAutores.DTOS;
using ApiAutores.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
namespace ApiAutores.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {

            var autores = await context.Autores.ToListAsync();

            return mapper.Map<List<Autor>>(autores);
        }


        [HttpGet("{id:int}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutorDTOConLibros>> Get(int id)
        {


            var autor = await context.Autores
                .Include(autorDB => autorDB.AutoresLibros)
                .ThenInclude(autorLibroDB => autorLibroDB.Libro)
                .FirstOrDefaultAsync(autorBD => autorBD.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return mapper.Map<AutorDTOConLibros>(autor);
        }

     


        [HttpPost]
       
        public async Task<ActionResult> Post(AutorCreacionDTO autorCreacionDTO)
        {

            var existeAutorConElMismoNombre = await context.Autores.AnyAsync(autorBD => autorBD.Name == autorCreacionDTO.Name);
            if (existeAutorConElMismoNombre)
            {
                return BadRequest($"ya existe un autor con ese nombre {autorCreacionDTO.Name}");
            }

            var existeAutorConElMismoEmail = await context.Autores.AnyAsync(autorBD => autorBD.Email == autorCreacionDTO.Email);
            if (existeAutorConElMismoEmail)
            {
                return BadRequest($"Ya existe un autor con ese email {autorCreacionDTO.Email}");
            }
            var autor = mapper.Map<Autor>(autorCreacionDTO);

            
            context.Add(autor);
            await context.SaveChangesAsync();

            var autorDTO = mapper.Map<AutorDTO>(autor);

            return CreatedAtRoute("ObtenerAutor", new { id = autor.Id }, autorDTO);
        }

        [HttpPut("{id:int}")] // api/autores/1 
        
        public async Task<ActionResult> Put(AutorCreacionDTO autorCreacionDTO, int id)
        {

            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            var autor = mapper.Map<Autor>(autorCreacionDTO);
            autor.Id = id;

            context.Update(autor);
            await context.SaveChangesAsync();

            var autorDTO = mapper.Map<AutorDTO>(autor);

            return CreatedAtRoute("ObtenerAutor", new { id = autor.Id }, autorDTO);
        }

        [HttpDelete("{id:int}")] // api/autores/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
