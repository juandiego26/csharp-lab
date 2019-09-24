using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
  class Program
  {
    static void Main(string[] args)
    {
      AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
      AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.DrawLine(20);

      var engine = new EscuelaEngine(); // instancia de escuela Engine
      engine.Inicializar();
      Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

      var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
      var evalList = reporteador.GetListaEvaluaciones();
      var listaAsg = reporteador.GetListaAsignaturas();
      var listaEvalXAsig = reporteador.GetDicEvaluaXAsig();
      var listaPromXAsig = reporteador.GetPromedioAlumPorAsignatura();
      var topProm = reporteador.GetTopMejoresProm(3);

      Printer.WriteTitle("Captura de una evaluación por consola");
      var newEval = new Evaluación();
      string nombre, notastring;
      float nota;

      WriteLine("Ingrese el nombre de la evaluación");
      Printer.PresioneENTER();
      nombre = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(nombre))
      {
        Printer.WriteTitle("El valor del nombre no puede ser vacio");
        WriteLine("Saliendo del programa");
      }
      else
      {
        newEval.Nombre = nombre.ToLower();
        WriteLine("El nombre de la evaluación ha sido ingresado correctamente");
      }


      WriteLine("Ingrese la nota de la evaluación");
      Printer.PresioneENTER();
      notastring = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(notastring))
      {
        Printer.WriteTitle("El valor de la nota no puede ser vacio");
        WriteLine("Saliendo del programa");
      }
      else
      {
        try
        {
          newEval.Nota = float.Parse(notastring);
          if(newEval.Nota < 0 || newEval.Nota > 5)
          {
            throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
          }
          WriteLine("La nota de la evaluación ha sido ingresado correctamente");
          return;
        }
        catch(ArgumentOutOfRangeException arge)
        {
          Printer.WriteTitle(arge.Message);
          WriteLine("Saliendo del programa");
        }
        finally
        {
          Printer.WriteTitle("FINALLY");
          // Printer.Beep(2500, 500, 3);
        }
        // catch(Exception)
        // {
        //   Printer.WriteTitle("El valor de la nota no es un número válido");
        //   WriteLine("Saliendo del programa");
        // }
      }
    }

    private static void AccionDelEvento(object sender, EventArgs e)
    {
      Printer.WriteTitle("SALIENDO");
      // Printer.Beep(3000, 1000, 3);
      Printer.WriteTitle("SALIÓ");
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