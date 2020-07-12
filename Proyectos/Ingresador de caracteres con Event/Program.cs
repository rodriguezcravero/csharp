using System;

namespace P8_E6
{
    class Program
    {
        static void Main(string[] args)
        {
            Ingresador ingresador = new Ingresador();
            ingresador.LineaVacia += (sender, e) => Console.WriteLine("Se ingresó una línea en blanco");
            ingresador.ValorNumerico += (sender, e) => Console.WriteLine($"Se ingresó el número {e.Valor}");

            Console.WriteLine("Para salir ingrese fin");
            ingresador.Ingresar();

        }
    }

    //Sólo creo la clase de Argumentos que voy a usar en el caso de que se ingrese un número
    class IngresandoNumeroEventArgs : EventArgs
    {
        public int Valor { get; set; }
    }

    //Lo mismo, voy a usar este Event Handler con argumentos para el caso de que se ingrese un número
    delegate void IngresandoNumeroEventHandler(object sender, IngresandoNumeroEventArgs e);

    class Ingresador
    {
        public event EventHandler LineaVacia;
        public event IngresandoNumeroEventHandler ValorNumerico;

        public void Ingresar()
        {
            string str; int n;
            str = Console.ReadLine();
            while (str != "fin")
            {
                bool esNumero = int.TryParse(str, out n);
                if (str == "") LineaVacia?.Invoke(this, EventArgs.Empty);
                if (esNumero) ValorNumerico?.Invoke(this, new IngresandoNumeroEventArgs() { Valor = n });
                str = Console.ReadLine();
            }
        }
    }
}
