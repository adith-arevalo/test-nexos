﻿namespace ApiAutores.Entidades
{
    public class AutorLibro
    {

        public int LibroId { get; set; }
        public int AutorId { get; set; }
        public Libro Libro { get; set; }
        public Autor Autor { get; set; }
    }
}
