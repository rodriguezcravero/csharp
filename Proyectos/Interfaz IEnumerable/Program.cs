/*8) Codificar usando iteradores los métodos:
Rango(i, j, p) que devuelve la secuencia de enteros desde i hasta j con un paso de p
Potencia(b,k) que devuelve la secuencia b
1,b2,....bk
DivisiblePor(e,i) retorna los elementos de e que son divisibles por i
Observar la salida que debe producir el siguiente código:
*/

using System;
using System.Collections;

namespace P7_E4
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable rango = Rango(6, 30, 3);
            IEnumerable potencias = Potencias(2, 10);
            IEnumerable divisibles = DivisiblesPor(rango, 6);
            foreach (int i in rango)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            foreach (int i in potencias)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            foreach (int i in divisibles)
            {
                Console.Write(i + " ");
            }


            Console.ReadKey();
        }

        //Mientras el primer numero sea menor al segundo, devuelvo el primero más el intervalo.
        //El yield return lo hago antes de la suma, así siempre devuelvo el que va.
        static IEnumerable Rango(int inicio, int fin, int intervalo)
        {
            while (inicio <= fin)
            {
                yield return inicio;
                inicio += intervalo;
            }
        }

        //Puedo devolver la potencia e ir sumando el aux en la misma expresión.
        static IEnumerable Potencias(int b, int exponente)
        {
            int aux = 1;
            while (aux <= exponente)
            {
                yield return (int)Math.Pow(b, aux++);
            }
        }

        //Me llama la atención que a pesar del if no tire error ni warning, ya que no todas las rutas devuelven algo.
        //Lo que recibo es un IEnumerable, que es tratado como la colección de elementos que es.
        static IEnumerable DivisiblesPor(IEnumerable n, int divisor)
        {
            foreach (int numero in n)
            {
                if (numero % divisor == 0) yield return numero;
            }
        }
    }

}

