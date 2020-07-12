using System;

namespace P8_E8
{
    class Program
    {
        static void Main(string[] args)
        {
            Articulo a = new Articulo();
            a.PrecioCambiado += precioCambiado;
            a.Codigo = 1;
            a.Precio = 10;
            a.Precio = 12;
            a.Precio = 12;
            a.Precio = 14;

            Console.ReadKey();
        }
        //Este método estático será enviado como delegado.
        //Cuando se produce el cambio de precio distinto, se invoca a este método, que sólo imprime en pantalla los valores de los argumentos del evento.
        public static void precioCambiado(object sender, PrecioCambiadoEventArgs e)
        {
            string texto = $"Artículo {e.Codigo} valía {e.PrecioAnterior}";
            texto += $" y ahora vale {e.PrecioNuevo}";
            Console.WriteLine(texto);
        }
    }

    //La clase EventArgs, en este caso, tiene 3 propiedades, que son las que se emplean en el método estático precioCambiado de Main
    class PrecioCambiadoEventArgs
    {
        public int Codigo { get; set; }
        public int PrecioAnterior { get; set; }
        public int PrecioNuevo { get; set; }
    }

    //Se crea un delegado EventHandler.
    delegate void PrecioCambiadoEventHandler(object sender, PrecioCambiadoEventArgs e);

    class Articulo
    {
        //Esta clase tiene dos propiedades, Codigo es auto-implementada, y Precio tiene backing field.
        //Cuando se intenta setear un valor distinto al que tiene (le agrego por defecto 0), chequea si tiene delegado, y lo invoca, con los 3 valores de EventArgs.
        private int _precio = 0;
        public int Codigo { get; set; }
        public int Precio
        {
            get => _precio;
            set
            {
                if (Precio != value)
                {
                    PrecioCambiado?.Invoke(this, new PrecioCambiadoEventArgs() { Codigo = this.Codigo, PrecioAnterior = this.Precio, PrecioNuevo = value });
                    _precio = value;
                }

            }
        }

        //Instancio un evento de tipo EventHandler, que luego al ser invocado, debe enviarse su delegado como sender, y los EventArgs con los valores correspondientes
        public event PrecioCambiadoEventHandler PrecioCambiado;
    }

}
