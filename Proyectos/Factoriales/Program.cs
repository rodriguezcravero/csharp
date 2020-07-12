/*16. Escribir una función (método static int fac(int n)) que calcule el factorial de un
número n pasado al programa como parámetro por la línea de comando
a) Definiendo una función no recursiva
b) Definiendo una función recursiva
c) idem a b) pero con expression-bodied methods (Tip: utilizar el operador condicional
ternario)
*/
using System;

namespace P2_E16
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Ingrese un numero para conocer su factorial: ");
            //Podría mejorarse con un try/catch para que no tire error en tiempo de ejecución en caso de no ser ingresado un entero.
            int numero = Int32.Parse(Console.ReadLine());
            Console.WriteLine($"Resultado sin recursividad: {fac(numero)}");

            Console.Write("Ahora pero con una función recursiva: ");
            numero = Int32.Parse(Console.ReadLine());
            Console.WriteLine($"Resultado con recursividad: {facRecursiva(numero)}");

            Console.Write("Y por último con un método 'expression-bodied': ");
            numero = Int32.Parse(Console.ReadLine());
            Console.WriteLine($"Resultado con 'expression-bodied': {facRecursivaExpBodied(numero)}");


            Console.ReadKey();
        }

        //Calcula n veces la sumatoria de la multiplicación de 1 por 1, por 2, por 3, por n.
        static int fac(int n)
        {
            int num = 1;
            for (int i = 1; i <= n; i++) num *= i;
            return num;
        }

        //Tanto en este método como en el que sigue, se establece el corte de la recursividad con 1, porque tanto el factorial de 0 y de 1, dan 1
        //Desde 2 para arriba, se aguarda el return de n, por el factorial de n-1, y a su vez, de n-2, etc.
        static int facRecursiva(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return n * (facRecursiva(--n));
            }
        }

        static int facRecursivaExpBodied(int n) => n == 0 ? 1 : n * facRecursivaExpBodied(--n);
    }
}
