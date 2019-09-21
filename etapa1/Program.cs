using System;
using System.Collections.Generic;
using CoreEscuela.Entidades;
using static System.Console;

namespace etapa1
{
    class Program
    {
        static void Main(string[] args)
        {
            var escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria,
                                        país: "Colombia", ciudad: "Bogotá");

            escuela.Cursos = new List<Curso>(){
                new Curso() { Nombre = "101", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "201", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "301", Jornada = TiposJornada.Mañana }
            };

            // adicionamos a la coleccion
            escuela.Cursos.Add( new Curso {Nombre = "102", Jornada = TiposJornada.Tarde} );
            escuela.Cursos.Add( new Curso {Nombre = "202", Jornada = TiposJornada.Tarde} );

            var otraColección = new List<Curso>(){
                new Curso() { Nombre = "401", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "501", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "501", Jornada = TiposJornada.Tarde }
            };
            escuela.Cursos.AddRange(otraColección);
            imprimirCursosEscuela(escuela);

            escuela.Cursos.RemoveAll(delegate(Curso cur)
            {
                return cur.Nombre == "301";
            });

            escuela.Cursos.RemoveAll((cur) => cur.Nombre == "501" && cur.Jornada == TiposJornada.Mañana);

            WriteLine("======================");
            // quitar todos miembros de una colección
            // otraColección.Clear();

// Como un Arreglo
            // escuela.Cursos = new Curso[] {
            //     new Curso() { Nombre = "101" },
            //     new Curso() { Nombre = "201" },
            //     new Curso() { Nombre = "301" }
            // };
            imprimirCursosEscuela(escuela);
        }

        // private static bool Predicado(Curso cursoobj)
        // {
        //     return cursoobj.Nombre == "301";
        // }

        private static void imprimirCursosEscuela(Escuela escuela)
        {
            WriteLine("===========================");
            WriteLine("Cursos de la Escuela");
            WriteLine("===========================");

            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre}, Id {curso.UniqueId}");
                }
            }
        }
        private static void ImprimirCursosWhile(Curso[] arregloCursos)
        {
            int contador = 0;
            while (contador < arregloCursos.Length)
            {
                Console.WriteLine($"Nombre {arregloCursos[contador].Nombre}, Id {arregloCursos[contador].UniqueId}");
                contador++;
            }
        }
        private static void ImprimirCursosDoWhile(Curso[] arregloCursos)
        {
            int contador = 0;
            do
            {
                Console.WriteLine($"Nombre {arregloCursos[contador].Nombre}, Id {arregloCursos[contador].UniqueId}");
                contador++;
            }while (contador < arregloCursos.Length);
        }
        private static void ImprimirCursosFor(Curso[] arregloCursos)
        {
            for (int i = 0; i < arregloCursos.Length; i++)
            {
                Console.WriteLine($"Nombre {arregloCursos[i].Nombre}, Id {arregloCursos[i].UniqueId}");
            }
        }
        private static void ImprimirCursosForEach(Curso[] arregloCursos)
        {
            foreach (var curso in arregloCursos)
            {
                Console.WriteLine($"Nombre {curso.Nombre}, Id {curso.UniqueId}");

            }
        }
    }
}
