/*14. Implementar un programa que muestre todos los números primos entre 1 y un número natural
dado (pasado al programa como argumento por la línea de comandos). Definir el método
static bool esPrimo(int n) que devuelve true sólo si n es primo. Esta función debe
comprobar si n es divisible por algún número entero entre 2 y la raíz cuadrada de n. (Nota:
Math.sqrt(d) devuelve la raíz cuadrada de d)
*/

using System;

namespace P2_E14
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Ingrese un numero para saber los primos entre 1 y tal número: ");
            //Debo parsear el string que lee Consola a un entero.
            //Se podría mejorar con un Try/Catch ya que si no se ingresa un numero, tira error en tiempo de ejecución.
            int numero = Int32.Parse(Console.ReadLine());

            // Console.WriteLine($"El numero {numero} es primo? => {esPrimo(numero)}");
            Console.Write("Los primos entre 1 y {0} son: ", numero);
            //Hago una iteración a partir de 2, y sólo se imprimen los numeros con true;
            for (int i = 2; i <= numero; i++) if (esPrimo(i)) Console.Write(i + " ");


            Console.ReadKey();
        }

        //Sólo devuelve true aquellos que no son divisibles por 2, 3, 4, ... en adelante. 
        static bool esPrimo(int n)
        {
            for (int i = 2; i <= n; i++)
            {
                if (n % Math.Sqrt(i) == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
