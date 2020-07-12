using System;
using System.Collections.Generic;

namespace P9_E5
{
    class Program
    {
        static void Main(string[] args)
        {
            Nodo<int> n = new Nodo<int>(7);
            n.Insertar(3);
            n.Insertar(1);
            n.Insertar(5);
            n.Insertar(12);
            foreach (int elem in n.InOrder)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"Altura: {n.Altura}");
            Console.WriteLine($"Cantidad: {n.CantNodos}");
            Console.WriteLine($"Mínimo: {n.ValorMinimo}");
            Console.WriteLine($"Máximo: {n.ValorMaximo}");
            Nodo<string> n2 = new Nodo<string>("hola");
            n2.Insertar("Mundo");
            n2.Insertar("XYZ");
            n2.Insertar("ABC");
            foreach (string elem in n2.InOrder)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"Altura: {n2.Altura}");
            Console.WriteLine($"Cantidad: {n2.CantNodos}");
            Console.WriteLine($"Mínimo: {n2.ValorMinimo}");
            Console.WriteLine($"Máximo: {n2.ValorMaximo}");


            Console.ReadKey();
        }
    }

    //Al implementar la cláusula del where, me aseguro que el tipo que se instancie, sea sí o sí comparable
    class Nodo<T> where T : IComparable<T>
    {
        //Tengo las 3 propiedades típicas de una clase Nodo
        public T Valor { get; private set; }
        public Nodo<T> hijoIzq { get; set; } = null;
        public Nodo<T> hijoDer { get; set; } = null;
        //Propiedad que se incrementa al emplearse el constructor y al insertarse nuevos nodos.
        public int CantNodos { get; set; }

        //Constructor con Valor, ya que tiene el set privado
        public Nodo(T valor)
        {
            Valor = valor;
            CantNodos++;
        }

        //Se inserta como siempre, con la salvedad que sólo guardo si tengo valores más altos o bajos.
        //En caso de que el CompareTo sea igual la 0, no se hace nada
        public void Insertar(T dato)
        {
            if (this.Valor.CompareTo(dato) == 1)
            {
                if (hijoIzq == null)
                {
                    hijoIzq = new Nodo<T>(dato);
                }
                else hijoIzq.Insertar(dato);
                CantNodos++;
            }
            else if (this.Valor.CompareTo(dato) == -1)
            {
                if (hijoDer == null)
                {
                    hijoDer = new Nodo<T>(dato);
                }
                else hijoDer.Insertar(dato);
                CantNodos++;
            }
        }

        //Al igual que en el ejercicio P4E7, Máximo y Mínimo se establecen sin argumentos, porque lo que yo invoco a un método aparte,
        //que sí lleva parámetros para poder hacerlos recursivos.
        public T ValorMaximo { get => Maximo(this); }
        public T ValorMinimo { get => Minimo(this); }

        private T Maximo(Nodo<T> nodo) => nodo.hijoDer == null ? nodo.Valor : Maximo(nodo.hijoDer);
        private T Minimo(Nodo<T> nodo) => nodo.hijoIzq == null ? nodo.Valor : Minimo(nodo.hijoIzq);

        //Altura, al igual que Máximo y Mínimo, no lleva parámetros. Uso otro método, con recursividad.
        //Calculo la altura de la izquierda, luego de la derecha, y los comparo, devolviendo el mayor.
        public int Altura
        {
            get
            {
                int cantIzq = Altitud(this.hijoIzq);
                int cantDer = Altitud(this.hijoDer);
                return cantIzq > cantDer ? cantIzq : cantDer;
            }
        }

        //Este método hace lo siguiente:
        //1)Recibe un nodo: si es null, retorna 0 y termina;
        //2) Si no es null, pero sus 2 hijos son null, retorna 1 y termina;
        //3) si ninguna de las anteriores, envía sus hijos recursivamente, los que podrían volver con 0 o 1;
        //4) Se van acumulando de a 1 según los nodos que se extiendan.
        private int Altitud(Nodo<T> nodo)
        {
            if (nodo == null) return 0;
            if ((nodo.hijoIzq == null) && (nodo.hijoDer == null)) return 1;
            int izquierda = Altitud(nodo.hijoIzq);
            int derecha = Altitud(nodo.hijoDer);
            return izquierda > derecha ? izquierda + 1 : derecha + 1;
        }

        public List<T> InOrder
        {
            get
            {
                List<T> lista = new List<T>();
                return GetInOrder(lista);
            }
        }

        //Para devolver una lista de nodos ordenada, debo hacer lo siguiente:
        //1) Pregunto si tengo hijo izquierdo, y de ser así, lo envío recursivamente;
        //2) Luego de analizar hijo izquierdo, devuelvo el valor del nodo actual;
        //3) Por último, envío el hijo derecho en caso de no ser null.
        private List<T> GetInOrder(List<T> lista)
        {
            if (this.hijoIzq != null) this.hijoIzq.GetInOrder(lista);
            lista.Add(this.Valor);
            if (this.hijoDer != null) this.hijoDer.GetInOrder(lista);
            return lista;
        }

        //Al hacerlo la primera vez, pensé que había que instanciar el método CompareTo, pero al comentarlo, no tira error.
        //public int CompareTo(T obj) => Valor.CompareTo(obj);

    }

}
