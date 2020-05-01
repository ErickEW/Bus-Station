using System;

namespace Autobus
{
    class Autobuss
    {
        string nombre { get; set; }
        char ruta { get; set; }

       public Autobuss(string nombre, char ruta)
        {
            this.nombre = nombre;
            this.ruta = ruta;
        } 

         public string Nombre()
        {
            return nombre;
        }

        public Char Ruta()
        {
            return ruta;
        }

    }
}