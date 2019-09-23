using System;
using System.Collections.Generic;
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
      var listaObjetos = engine.GetObjetosEscuela();
      // var obj = new ObjetoEscuelaBase(); // no se puede crear una instancia (objeto) de una clase abstracta
      Printer.DrawLine(20);
      Printer.DrawLine(20);
      Printer.DrawLine(20);
      Printer.WriteTitle("Pruebas de polimorfismo");

      // Un Alumno es un objeto de Escuela y cualquier clase que herede del objeto escuela
      // sigue siendo un objeto escuela

      var alumnoTest = new Alumno{Nombre = "Claire UnderWood"};
      /****** Es el mismo Objeto  visto de maneras diferentes, es decir,
      Este objeto tiene múltiples formas es polimorfico */
      Printer.WriteTitle("Alumno");
      WriteLine($"Alumno: {alumnoTest.Nombre}" ); // Imprimimos el objeto como un alumnoTest
      WriteLine($"Alumno: {alumnoTest.UniqueId}" ); // Imprimimos el objeto como un alumnoTest
      WriteLine($"Alumno: {alumnoTest.GetType()}" ); // Imprimimos el objeto como un alumnoTest

      ObjetoEscuelaBase ob = alumnoTest; // usamos polimorfismo para que alumnoTest se pueda trabajar como un objeto escuela
      Printer.WriteTitle("ObjetoEscuela");
      WriteLine($"Alumno: {ob.Nombre}" ); // Imprimimos el objeto como objetoEscuelaBase
      WriteLine($"Alumno: {ob.UniqueId}" ); // Imprimimos el objeto como objetoEscuelaBase
      WriteLine($"Alumno: {ob.GetType()}" ); // Imprimimos el objeto como objetoEscuelaBase

      var objDummy = new ObjetoEscuelaBase(){Nombre = "Frank Underwood"};
      Printer.WriteTitle("ObjetoEscuelaBase");
      WriteLine($"Alumno: {objDummy.Nombre}" ); // Imprimimos el objeto como objetoEscuelaBase
      WriteLine($"Alumno: {objDummy.UniqueId}" ); // Imprimimos el objeto como objetoEscuelaBase
      WriteLine($"Alumno: {objDummy.GetType()}" ); // Imprimimos el objeto como objetoEscuelaBase

      var evaluación = new Evaluación(){Nombre = "Evaluación de matematicas", Nota = 4.5f};
      Printer.WriteTitle("Evaluación");
      WriteLine($"Evaluación: {evaluación.Nombre}" ); // Imprimimos el objeto como un evaluación
      WriteLine($"Evaluación: {evaluación.UniqueId}" ); // Imprimimos el objeto como un evaluación
      WriteLine($"Evaluación: {evaluación.Nota}" ); // Imprimimos el objeto como un evaluación
      WriteLine($"Evalución: {evaluación.GetType()}" );

      // ob = evaluación; // usamos polimorfismo para que alumnoTest se pueda trabajar como un objeto escuela

      if(ob is Alumno) // para preguntar si un objeto es de un tipo determinado
      {
        Alumno alumnoRecuperado = (Alumno)ob;
      }
        Alumno alumnoRecuperado2 = ob as Alumno; // tome este objeto como si fuera alumno
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