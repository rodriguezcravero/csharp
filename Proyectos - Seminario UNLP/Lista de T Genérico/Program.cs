/*4) Dada la siguiente clase genérica
class Nodo<T>
{
public T Valor { get; private set; }
public Nodo<T> Proximo { get; set; } = null;
public Nodo(T valor) => Valor = valor;
}

Salida por
consola
0 100 3 10 11*/
using System;
using System.Collections.Generic;

namespace P9_E4
{
    class Program
    {
        static void Main(string[] args)
        {

            ListaGenerica<int> lista = new ListaGenerica<int>();
            lista.AgregarAdelante(3);
            lista.AgregarAdelante(100);
            lista.AgregarAtras(10);
            lista.AgregarAtras(11);
            lista.AgregarAdelante(0);
            //El enumerador daba error hasta que agregué el using System.Collections.Generic;
            IEnumerator<int> enumerador = lista.GetEnumerator();
            while (enumerador.MoveNext())
            {
                int i = enumerador.Current;
                Console.Write(i + " ");
            }

            Console.ReadKey();
        }
    }

    //Cada objeto de Nodo tendrá una propiedad de tipo T con set privado, un Nodo asociado, y un constructor que setea el Valor.
    class Nodo<T>
    {
        public T Valor { get; private set; }
        public Nodo<T> Proximo { get; set; } = null;
        public Nodo(T valor) => Valor = valor;
    }

    class ListaGenerica<T>
    {
        //Tengo dos nodos, por defecto en null
        //Uno será el que marca el inicio de mi lista de nodos, y otro que apunta al último
        private Nodo<T> Inicio;
        private Nodo<T> Final;

        public void AgregarAdelante(T valor)
        {
            //Creo un nuevo Nodo y le asigno el valor.
            Nodo<T> aux = new Nodo<T>(valor);
            //Chequeo el caso de que el primer nodo esté vacío.
            if (Inicio == null)
            {
                Inicio = aux;
                Final = aux;
            }
            //Si no lo está, lo apunto al primero, y luego hago el intercambio de direcciones.
            else
            {
                aux.Proximo = Inicio;
                Inicio = aux;
            }
        }

        public void AgregarAtras(T valor)
        {
            //Creo un nuevo Nodo y le asigno el valor.
            Nodo<T> aux = new Nodo<T>(valor);
            //Chequeo el caso de que el primer nodo esté vacío.
            if (Inicio == null)
            {
                Inicio = aux;
                Final = aux;
            }
            //Si no lo está, al del final lo apunto al nuevo, y luego hago el intercambio de direcciones.
            else
            {
                Final.Proximo = aux;
                Final = aux;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            //Creo un nodo, con el que voy recorriendo desde el inicio al final, y voy devolviendo valores de cada uno.
            Nodo<T> aux = Inicio;
            while (aux != null)
            {
                yield return aux.Valor;
                aux = aux.Proximo;
            }
        }

    }
}
