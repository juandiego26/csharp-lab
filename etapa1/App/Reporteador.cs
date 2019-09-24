using System;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
  public class Reporteador
  {
    Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
    Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObjEsc)
    {
      if(dicObjEsc == null)
        throw new ArgumentNullException(nameof(dicObjEsc));
      _diccionario =  dicObjEsc;
    }
  }
}