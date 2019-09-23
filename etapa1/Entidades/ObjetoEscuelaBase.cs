using System;

namespace CoreEscuela.Entidades
{
  public abstract class ObjetoEscuelaBase
  {
    public string UniqueId { get; private set; }
    public string Nombre { get; set; }

    public ObjetoEscuelaBase() // constructor
    {
      UniqueId = Guid.NewGuid().ToString();
    }

    public override string ToString() // para que muestre información clara en el debugger
    {
      return $"{Nombre},{UniqueId}";
    }
  }
}