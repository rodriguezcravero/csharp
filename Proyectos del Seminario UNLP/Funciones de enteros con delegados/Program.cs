using System;
using System.Collections;

namespace P8_E3
{
    class Program
    {
        static void Main(string[] args)
        {
            ListaDeEnteros lista1 = new ListaDeEnteros();
            for (int i = 1; i <= 10; i++)
            {
                lista1.Agregar(i);
            }
            foreach (int i in lista1)
            {
                Console.Write(i + "-");
            }
            Console.WriteLine();
            ListaDeEnteros lista2 = lista1.Seleccionar(n => n % 2 == 0);
            ListaDeEnteros lista3 = lista2.Aplicar(n => n * 5);
            ListaDeEnteros lista4 = lista1.Seleccionar(n => n > 7).Aplicar(n => n + 10);
            ListaDeEnteros lista5 = ListaDeEnteros.Combinar(lista3, lista4, (x, y) => x + y);
            lista1.Imprimir();
            lista2.Imprimir();
            lista3.Imprimir();
            lista4.Imprimir();
            lista5.Imprimir();
            ListaDeEnteros.Combinar(lista5, lista3, (x, y) => x + 2 * y).Imprimir();

            Console.ReadKey();

        }
    }
    //Para este programa se necesitan 3 delegados
    //1) Un método que reciba un entero y devuelva un booleano;
    //2) Un método que reciba un entero y devuelva un entero;
    //3) Un método que sí o sí reciba 2 enteros y devuelva uno.
    delegate bool Seleccionador(int n);
    delegate int Aplicador(int n);
    delegate int Combinador(int x, int y);

    class ListaDeEnteros : IEnumerable
    {
        ArrayList lista = new ArrayList();
        public void Agregar(int i) => lista.Add(i);
        public int Cantidad => lista.Count;

        //Este método recibe un método del tipo Seleccionador
        public ListaDeEnteros Seleccionar(Seleccionador f)
        {
            ListaDeEnteros listaAux = new ListaDeEnteros();
            foreach (int numero in lista)
            {
                if (f(numero)) listaAux.Agregar(numero);
            }
            return listaAux;
        }

        //Igual que arriba, este método recibe un método que agregará los elementos resultantes.
        public ListaDeEnteros Aplicar(Aplicador f)
        {
            ListaDeEnteros listaAux = new ListaDeEnteros();
            foreach (int numero in lista)
            {
                listaAux.Agregar(f(numero));
            }
            return listaAux;
        }

        //Este es un método estático, que recibe 2 objetos ListaDeEnteros, y además un método que las combina
        public static ListaDeEnteros Combinar(ListaDeEnteros l1, ListaDeEnteros l2, Combinador f)
        {
            //Puedo tener listas de distinta cantidad de elementos, así que primero se chequea eso.
            //Se recorre la lista más larga, y mientras tenga elementos de las 2, invoco la función, sino, solo agrego el elemento que tenga.
            ListaDeEnteros listaAux = new ListaDeEnteros();
            if (l1.Cantidad >= l2.Cantidad)
            {
                for (int i = 0; i < l1.Cantidad; i++)
                {
                    listaAux.Agregar(i < l2.Cantidad ? f((int)l1.lista[i], (int)l2.lista[i]) : (int)l1.lista[i]);
                }
            }
            else
            {
                for (int i = 0; i < l2.Cantidad; i++)
                {
                    listaAux.Agregar(i < l1.Cantidad ? f((int)l2.lista[i], (int)l1.lista[i]) : (int)l2.lista[i]);
                }
            }
            return listaAux;
        }

        //Al implementarse la interfaz IEnumerable, tengo que implementar el método GetEnumerator, que me permite recorrer con un foreach
        public IEnumerator GetEnumerator()
        {
            foreach (int item in lista)
            {
                yield return item;
            }
        }

        //En la práctica, se deja ver que el último elemento se imprime distinto.
        public void Imprimir()
        {
            for (int i = 0; i < lista.Count - 1; i++) Console.Write(lista[i] + ", ");
            Console.Write(lista[lista.Count - 1] + "\n");
        }

    }
}
