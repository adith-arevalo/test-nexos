using ApiAutores.DTOS;
using ApiAutores.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;


namespace ApiAutores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Libro>>> Get()
        {

            var libros = await context.Libros.ToListAsync();

            return mapper.Map<List<Libro>>(libros);
        }
            
        [HttpGet("{id:int}", Name = "ObtenerLibro")]
        public async Task<ActionResult<LibroDTOConAutores>> Get(int id)
        {
            var libro = await context.Libros
                .Include(libroDB => libroDB.AutoresLibros)
                .ThenInclude(autorLibroDB => autorLibroDB.Autor)
                .FirstOrDefaultAsync(libroDB => libroDB.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            return mapper.Map<LibroDTOConAutores>(libro);
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {

            if (libroCreacionDTO.AutorIdd == null)
            {

                return BadRequest("no se puede crear un libro sin autores");//me sirve
            }

            var autor = await context.Autores
           .Include(a => a.AutoresLibros)
           .SingleOrDefaultAsync(a => a.Id == libroCreacionDTO.AutorIdd);
            if (autor == null)
            {
                return NotFound("El autor ingresado no se encuentra registrado");
            }

            if (autor.AutoresLibros.Count >= 5) 
            {
                return Forbid("El autor ya tiene el número máximo de libros permitidos.");
            }

          

            var libro = mapper.Map<Libro>(libroCreacionDTO);
            var autorLibro = new AutorLibro { Autor = autor, Libro = libro };
            libro.AutoresLibros.Add(autorLibro);


            context.Add(libro);
            await context.SaveChangesAsync();

            var libroDTO = mapper.Map<LibroDTO>(libro);
            return CreatedAtRoute("ObtenerLibro", new { id = libro.Id }, libroDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO)
        {
            var libroDB = await context.Libros
                .Include(x => x.AutoresLibros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libroDB == null)
            {
                return BadRequest();
            }

            libroDB = mapper.Map(libroCreacionDTO, libroDB);


            await context.SaveChangesAsync();

            return NoContent();
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Libros.AnyAsync(x => x.Id == id);

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

