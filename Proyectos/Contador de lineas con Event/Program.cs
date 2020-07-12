using System;

namespace P8_E5_Contador_de_lineas_con_Event_
{
    class Program
    {
        static void Main(string[] args)
        {

            ContadorDeLineas contador = new ContadorDeLineas();
            contador.Contar();


            Console.ReadKey();
        }
    }

    class ContadorDeLineas
    {
        private int _cantLineas = 0;
        public void Contar()
        {
            Ingresador _ingresador = new Ingresador();
            _ingresador.Contando += UnaLineaMas;
            _ingresador.Ingresar();
            Console.WriteLine($"Cantidad de líneas ingresadas: {_cantLineas}");
        }
        public void UnaLineaMas(object sender, EventArgs e)
        {
            _cantLineas++;
        }
    }

    class Ingresador
    {
        //Uso este delegado "genérico" que no necesita argumentos.
        public event EventHandler Contando;

        public void Ingresar()
        {
            if (Contando != null)
            {
                string st = Console.ReadLine();
                while (st != "")
                {
                    //En el caso de haberse encolado un método, lo invoco, y le mando argumentos vacíos
                    Contando(this, EventArgs.Empty);
                    st = Console.ReadLine();
                }
            }
        }
    }

}
