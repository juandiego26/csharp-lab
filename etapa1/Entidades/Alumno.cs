using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
  public class Alumno : ObjetoEscuelaBase // asi se hace una herencia
  {
    public List<Evaluación> Evaluaciones { get; set; } = new List<Evaluación>();
  }
}
