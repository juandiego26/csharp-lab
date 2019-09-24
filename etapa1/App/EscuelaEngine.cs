using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela
{
  // public class dummy : EscuelaEngine // no se puede heredar de EscuelaEngine porque es sealed
  // {

  // }
  public sealed class EscuelaEngine // sealed se puede crear instancias(objetos) de escuelaEngine
  {
    public Escuela Escuela { get; set; }

    public EscuelaEngine()
    {

    }

    public void Inicializar()
    {
      Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria,
      ciudad: "Bogotá", pais: "Colombia"
      );

      CargarCursos();
      CargarAsignaturas();
      CargarEvaluaciones();

    }

    public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dic,
                                    bool imprEval = false)
    {
      foreach (var objdic in dic)
      {
        Printer.WriteTitle(objdic.Key.ToString());

        foreach (var val in objdic.Value)
        {
          switch (objdic.Key)
          {
            case LlaveDiccionario.Evaluación:
              if (imprEval)
                Console.WriteLine(val);
            break;
            case LlaveDiccionario.Escuela:
              Console.WriteLine("Escuela: " + val);
            break;
            case LlaveDiccionario.Alumno:
              Console.WriteLine("Alumno: " + val.Nombre);
            break;
            case LlaveDiccionario.Curso:
            var curtmp = val as Curso;
              if(curtmp != null)
              {
                int count = curtmp.Alumnos.Count;
                Console.WriteLine("Curso: " + val.Nombre + " Cantidad Alumnos: " + count);
              }
            break;
            default:
              Console.WriteLine(val);
            break;
          }
        }
      }
    }

    // add diccionario

    public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
    {

      var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

      diccionario.Add(LlaveDiccionario.Escuela, new[] { Escuela });
      diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

      var listTmp = new List<Evaluación>();
      var listTmpas = new List<Asignatura>();
      var listTmpal = new List<Alumno>();

      foreach (var cur in Escuela.Cursos)
      {
        listTmpas.AddRange(cur.Asignaturas);
        listTmpal.AddRange(cur.Alumnos);

        foreach (var alum in cur.Alumnos)
        {
          listTmp.AddRange(alum.Evaluaciones);
        }
      }
      diccionario.Add(LlaveDiccionario.Evaluación, listTmp.Cast<ObjetoEscuelaBase>());
      diccionario.Add(LlaveDiccionario.Asignatura, listTmpas.Cast<ObjetoEscuelaBase>());
      diccionario.Add(LlaveDiccionario.Alumno, listTmpal.Cast<ObjetoEscuelaBase>());

      return diccionario;
    }

    // sobrecargar para no traer nada
    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
        bool traeEvaluaciones = true,
        bool traeAlumnos = true,
        bool traeAsignaturas = true,
        bool traeCursos = true
      )
    {
      return GetObjetosEscuela(out int dummy, out dummy, out dummy, out dummy);
    }
    // sobrecarga del metodo getObjetosEscuela solo para el conteoEvaluciones
    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
        out int conteoEvaluaciones,
        bool traeEvaluaciones = true,
        bool traeAlumnos = true,
        bool traeAsignaturas = true,
        bool traeCursos = true
      )
    {
      return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out dummy);
    }
    // sobrecarga del metodo getObjetosEscuela solo para el conteoCursos
    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
        out int conteoEvaluaciones, out int conteoCursos,
        bool traeEvaluaciones = true,
        bool traeAlumnos = true,
        bool traeAsignaturas = true,
        bool traeCursos = true
      )
    {
      return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out int dummy, out dummy);
    }
    // sobrecarga del metodo getObjetosEscuela solo para el conteoCursos
    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
        out int conteoEvaluaciones, out int conteoCursos, out int conteoAsignaturas,
        bool traeEvaluaciones = true,
        bool traeAlumnos = true,
        bool traeAsignaturas = true,
        bool traeCursos = true
      )
    {
      return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out conteoAsignaturas, out int dummy);
    }
    // metodo con paramatros de salida out y con opcionales bool
    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela( // para listas de solo lectura
      out int conteoEvaluaciones,
      out int conteoCursos,
      out int conteoAsignaturas,
      out int conteoAlumnos,
      bool traeEvaluaciones = true,
      bool traeAlumnos = true,
      bool traeAsignaturas = true,
      bool traeCursos = true
    )
    {
      conteoAlumnos = conteoAsignaturas = conteoEvaluaciones = 0;

      var listaObj = new List<ObjetoEscuelaBase>();
      listaObj.Add(Escuela);

      if (traeCursos)
        listaObj.AddRange(Escuela.Cursos);

      conteoCursos = Escuela.Cursos.Count;
      foreach (var curso in Escuela.Cursos)
      {

        conteoAsignaturas += curso.Asignaturas.Count;
        conteoAlumnos += curso.Alumnos.Count;

        if (traeAsignaturas)
          listaObj.AddRange(curso.Asignaturas);
        if (traeAlumnos)
          listaObj.AddRange(curso.Alumnos);
        if (traeEvaluaciones)
        {
          foreach (var alumno in curso.Alumnos)
          {
            listaObj.AddRange(alumno.Evaluaciones);
            conteoEvaluaciones += alumno.Evaluaciones.Count;
          }
        }
      }

      return listaObj.AsReadOnly();
    }

    #region Métodos de carga

    private void CargarEvaluaciones()
    {
      var rnd = new Random();

      foreach (var curso in Escuela.Cursos)
      {
        foreach (var asignatura in curso.Asignaturas)
        {
          foreach (var alumno in curso.Alumnos)
          {
            for (int i = 0; i < 5; i++)
            {
              var ev = new Evaluación
              {
                Asignatura = asignatura,
                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                Nota = MathF.Round(5 * (float)rnd.NextDouble(), 2),
                Alumno = alumno
              };
              alumno.Evaluaciones.Add(ev);
            }
          }
        }
      }

    }

    private void CargarAsignaturas()
    {
      foreach (var curso in Escuela.Cursos)
      {
        var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
        curso.Asignaturas = listaAsignaturas;
      }
    }

    private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
    {
      string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
      string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
      string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

      var listaAlumnos = from n1 in nombre1
                         from n2 in nombre2
                         from a1 in apellido1
                         select new Alumno { Nombre = $"{n1} {n2} {a1}" };

      return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
    }

    private void CargarCursos()
    {
      Escuela.Cursos = new List<Curso>(){
                        new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                        new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                        new Curso{Nombre = "301", Jornada = TiposJornada.Mañana},
                        new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde },
                        new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde},
            };

      Random rnd = new Random();
      foreach (var c in Escuela.Cursos)
      {
        int cantRandom = rnd.Next(5, 20);
        c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
      }
    }

    #endregion
  }
}