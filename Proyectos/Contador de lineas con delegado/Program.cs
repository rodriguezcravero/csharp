using System;
using System.Collections;

namespace P8_E5
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

    //La clase Ingresador tiene como única utilidad leer líneas hasta que se ingresa una vacía. 
    //Lo que hace ahora es ejecutar una función, que se debe ingresar por parámetro, que se llama cada vez que se ingresa texto.
    //La clase ContadorDeLineas lo que hace es instanciar un Ingresador, y pasarle por parámetro una función, que es la que suma de a 1 las lineas ingresadas.
    //A modo de ejemplo, si por cada linea se quisiera hacer un Console.Write de algo, se puede hacer y enviar por parámetro.
    delegate void Contar();

    class ContadorDeLineas
    {
        private int _cantLineas = 0;
        public void Contar()
        {
            Ingresador _ingresador = new Ingresador();
            _ingresador.Ingresar(UnaLineaMas);
            Console.WriteLine($"Cantidad de líneas ingresadas: {_cantLineas}");
        }
        public void UnaLineaMas() => _cantLineas++;
    }
    class Ingresador
    {
        public void Ingresar(Contar funcion)
        {
            string st = Console.ReadLine();
            while (st != "")
            {
                funcion?.Invoke();
                st = Console.ReadLine();
            }
        }
    }
}
