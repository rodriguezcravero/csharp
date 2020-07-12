/*14. Utilizar la clase Stack (pila) para implementar un programa que pase un número en base 10 a otra base realizando divisiones sucesivas. Por ejemplo para pasar 35 en base 10 a binario dividimos sucesivamente por dos hasta encontrar un cociente menor que el divisor, luego el resultado se obtiene leyendo de abajo hacia arriba el cociente de la última división seguida por todos los restos.
*/

using System;
using System.Collections;

namespace P3_E14
{
    class Program
    {
        static void Main(string[] args)
        {

            Stack pila = new Stack();
            Console.Write("Ingrese un numero en decimal para pasarlo a binario: ");
            int numero = Int32.Parse(Console.ReadLine());
            Console.Write($"\n{numero} en sistema binario es: ");

            //Guardo los restos en la pila, y luego voy decrementando el valor del numero, dividiéndolo por 2. Al ser entero, siempre llegará a 0.
            while (numero != 0)
            {
                pila.Push(numero % 2);
                numero /= 2;
            }

            while (pila.Count != 0) Console.Write(pila.Pop() + " ");

            Console.ReadKey();
        }
    }
}
