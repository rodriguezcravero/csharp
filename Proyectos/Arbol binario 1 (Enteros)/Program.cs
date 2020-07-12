/*7) Codificar la clase Nodo de un árbol binario de búsqueda de valores enteros. Un árbol binario de
búsqueda no tiene valores duplicados y el valor de un nodo cualquiera es mayor a todos los valores
de su subárbol izquierdo y es menor a todos los valores de su subárbol derecho.
Desarrollar los métodos métodos:
1. Insertar(valor): Inserta valor en el árbol descartándolo en caso que ya exista.
2. GetInorden: devuelve un ArrayList con los valores ordenados en forma creciente.
3. GetAltura(): devuelve la altura del árbol.
4. GetCantNodos(): devuelve la cantidad de nodos que posee el árbol.
5. GetValorMáximo(): devuelve el valor máximo que contiene el árbol.
6. GetValorMínimo(): devuelve el valor mínimo que contiene el árbol.
*/
using System;
using System.Collections;

namespace P4_E7
{
    class Program
    {
        static void Main(string[] args)
        {
            Nodo n = new Nodo(7);
            n.Insertar(8);
            n.Insertar(10);
            n.Insertar(3);
            n.Insertar(1);
            n.Insertar(5);
            n.Insertar(14);
            foreach (int i in n.GetInOrder())
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine(n.GetAltura());
            Console.WriteLine(n.GetCantNodos());
            Console.WriteLine(n.GetValorMaximo());
            Console.WriteLine(n.GetValorMinimo());


            Console.ReadKey();
        }
    }

    class Nodo
    {
        int num;
        Nodo izq = null;
        Nodo der = null;

        public Nodo(int numero)
        {
            num = numero;
        }

        public void Insertar(int n)
        {
            //Tanto si el numero a insertar es mayor o menor a la raiz, este método será recursivo si el nodo izq o der tiene elemento.
            //Sino, se asigna un nuevo nodo con el valor.
            //Si llega un numero con el mismo valor de algún nodo, no se hace nada y por lo tanto no se guarda.
            if (n > this.num)
            {
                if (this.der != null) this.der.Insertar(n);
                else this.der = new Nodo(n);
            }
            else if (n < this.num)
            {
                if (this.izq != null) this.izq.Insertar(n);
                else this.izq = new Nodo(n);
            }
        }

        public int GetNum()
        {
            return this.num;
        }

        //Los siguientes métodos buscan recursivamente el nodo de izq (minimo) o der (maximo) que tenga el hijo izq o der nulo.
        public int GetValorMinimo() => this.izq == null ? this.num : izq.GetValorMinimo();
        public int GetValorMaximo() => this.der == null ? this.num : der.GetValorMaximo();

        //Según la consigna, GetInOrder no lleva argumentos, por lo que creo un ArrayList y llamo a un método propio que si lleva argumentos, así funciona recursivamente.
        public ArrayList GetInOrder()
        {
            ArrayList arr = new ArrayList();
            numeros(arr);
            return arr;
        }
        //El método 'numeros' es recursivo, y tiene la siguiente metodología:
        //1) si tengo hijo izquierdo con nodo, llamo a este método con ese hijo izquierdo;
        //2) cuando izq es nulo o retornó, agrego el número del nodo actual;
        //3) repito el primer paso pero con el hijo derecho.
        private void numeros(ArrayList arreglo)
        {
            if (this.izq != null) this.izq.numeros(arreglo);
            arreglo.Add(this.GetNum());
            if (this.der != null) this.der.numeros(arreglo);
        }

        //Como GetInOrder tiene todos los nodos, lo que hago es llamar a ese método, y contar la cantidad que haya en dicha lista.
        public int GetCantNodos() => this.GetInOrder().Count;

        //Al igual que GetInOrder, GetAltura no tiene parámetros. Hago un método recursivo para calulcar los hijos a la izquierda, los hijos a la derecha, y devuelvo el mayor de ellos.
        public int GetAltura()
        {
            int cantIzq = Altitud(this.izq);
            int cantDer = Altitud(this.der);

            return cantIzq > cantDer ? cantIzq : cantDer;
        }

        //Este método es recursivo, y tiene la siguiente metología:
        //1) Si soy un nodo nulo, devuelvo 0 y termina.
        //2) Si ambos hijos son nulos, soy un nodo terminal, devuelvo 1 y termina.
        //3) Si tengo algún hijo, llamo a este método recursivamente, y voy devolviendo la altura de a 1.
        public int Altitud(Nodo n)
        {
            if (n == null) return 0;
            if ((n.der == null) && (n.izq == null)) return 1;

            int izquierda = Altitud(n.izq);
            int derecha = Altitud(n.der);

            return izquierda > derecha ? izquierda + 1 : derecha + 1;
        }

    }
}
