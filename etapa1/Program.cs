using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
  class Program
  {
    static void Main(string[] args)
    {
      var engine = new EscuelaEngine(); // instancia de escuela Engine
      engine.Inicializar();
      Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
      // Printer.Beep(10000, cantidad: 10);
      ImpimirCursosEscuela(engine.Escuela);

      int dummy = 0;

      var listaObjetos = engine.GetObjetosEscuela(); // var es lo que devuelva el método
      // listaObjetos.Add(new Curso { Nombre = "Curso Loco"});
    }


    private static void ImpimirCursosEscuela(Escuela escuela)
    {

      Printer.WriteTitle("Cursos de la Escuela");


      if (escuela?.Cursos != null)
      {
        foreach (var curso in escuela.Cursos)
        {
          WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
        }
      }
    }
  }
}