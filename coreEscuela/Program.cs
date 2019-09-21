using System;

namespace coreEscuela
{
    class Escuela
    {
        public string nombre;
        public string dirección;
        public int añoFundación;
        public string CEO;

        public void Timbrar()
        {
          Console.Beep(2000, 3000);
        }
    }
    class Program // esta es la clase programa
    {
        static void Main(string[] args)
        {
          var miEscuela = new Escuela();
          miEscuela.nombre = "Platzi Academy";
          miEscuela.dirección = "Carrera 9 calle 72";
          miEscuela.añoFundación = 2012;

          Console.WriteLine("TIMBRE");
          miEscuela.Timbrar();
            //Console.WriteLine("Hello World!");
        }
    }
}
