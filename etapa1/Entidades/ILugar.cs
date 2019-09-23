namespace CoreEscuela.Entidades
{
  public interface ILugar // una interface no tiene modificadores de acceso
  {
      string Dirección { get; set; }
      void LimpiarLugar();
  }
}