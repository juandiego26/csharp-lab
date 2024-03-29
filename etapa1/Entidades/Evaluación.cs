using System;

namespace CoreEscuela.Entidades
{
    public class Evaluación : ObjetoEscuelaBase // hereda de ObjetoEscuelaBase
    {
        public Alumno Alumno { get; set; }
        public Asignatura Asignatura  { get; set; }

        public float Nota { get; set; }

        public override string ToString() // sobreescrimos en metodo para formatearlo
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}";
        }
    }
}
