using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    class Escuela
    {
        string nombre;
        public string Nombre {
            get{ return nombre; }
            set{ nombre = value.ToUpper(); }
        }
        public int AñoDeCreación { get; set; }
        public string País { get; set; }
        public string Ciudad { get; set; }
        public TiposEscuela TipoEscuela { get; set; }
        public List<Curso> Cursos { get; set; }
        // public Escuela(string nombre, int año)
        // {
        //     this.nombre = nombre;
        //     AñoDeCreación = año;
        // }
        public Escuela(string nombre, int año) => (Nombre, AñoDeCreación) = (nombre, año);
        public Escuela(string nombre,
                        int año,
                        TiposEscuela tipo,
                        string país = "",
                        string ciudad = "")
        {
            (Nombre, AñoDeCreación) = (nombre, año);
            País = país;
            Ciudad = ciudad;
        }

        public override string ToString()
        {
            return $"Nombre: \"{Nombre}\",\nTipo: {TipoEscuela}, {System.Environment.NewLine}País: {País},\nCiudad: {Ciudad}";
        }
    }
}