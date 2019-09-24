using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
  public class Reporteador
  {
    Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
    public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObjEsc)
    {
      if (dicObjEsc == null)
        throw new ArgumentNullException(nameof(dicObjEsc));
      _diccionario = dicObjEsc;
    }

    // Reporte de evaluaciones
    public IEnumerable<Evaluación> GetListaEvaluaciones()
    {

      if (_diccionario.TryGetValue(LlaveDiccionario.Evaluación,
                                          out IEnumerable<ObjetoEscuelaBase> lista))
      {
        return lista.Cast<Evaluación>();
      }
      {
        return new List<Evaluación>();
      }
    }
    // reporte de asignuturas aprobadas

    // sobrecarga
    public IEnumerable<string> GetListaAsignaturas()
    {
      return GetListaAsignaturas(out var dummy);
    }

    public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluación> listaEvaluaciones)
    {
      listaEvaluaciones = GetListaEvaluaciones();
      // consulta de linq
      return (from Evaluación ev in listaEvaluaciones
              where ev.Nota >= 3.0f
              select ev.Asignatura.Nombre).Distinct();
    }

    // Reporte de evaluaciones por asignatura
    public Dictionary<string, IEnumerable<Evaluación>> GetDicEvaluaXAsig()
    {
      var dicRta = new Dictionary<string, IEnumerable<Evaluación>>();

      var listaAsig = GetListaAsignaturas(out var listaEval);

      foreach (var asig in listaAsig)
      {
        var evalsAsig = from eval in listaEval
                        where eval.Asignatura.Nombre == asig
                        select eval;

        dicRta.Add(asig, evalsAsig);
      }

      return dicRta;
    }

    // Promedio de cada alumno que esté cursando la asignatura
    public Dictionary<string, IEnumerable<object>> GetPromedioAlumPorAsignatura()
    {
      var rta = new Dictionary<string, IEnumerable<object>>();
      var dicEvalXAsig = GetDicEvaluaXAsig();

      foreach (var asigConEval in dicEvalXAsig)
      {
          var promsAlum = from eval in asigConEval.Value
                      group eval by new
                      {
                        eval.Alumno.UniqueId,
                        eval.Alumno.Nombre
                      }
                      into grupoEvalsAlumno
                      select new AlumnoPromedio
                      {
                        alumnoid = grupoEvalsAlumno.Key.UniqueId,
                        alumnoNombre = grupoEvalsAlumno.Key.Nombre,
                        promedio = grupoEvalsAlumno.Average(evaluación => evaluación.Nota)
                      };
          rta.Add(asigConEval.Key, promsAlum);
      }

      return rta;
    }
  }
}