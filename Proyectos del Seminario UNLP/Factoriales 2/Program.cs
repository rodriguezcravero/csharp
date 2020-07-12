/*16. Escribir una función (método static int fac(int n)) que calcule el factorial de un
número n pasado al programa como parámetro por la línea de comando
a) Definiendo una función no recursiva
b) Definiendo una función recursiva

17. Ídem. al ejercicio 16.a) y 16.b) pero devolviendo el resultado en un parámetro de salida
static void fac(int n, out int f)
*/
using System;

namespace P2_E17
{
    class Program
    {
        static void Main(string[] args)
        {
            //Out debe estar declarada antes de enviarse como parámetro, pero puede no estar inicializada.
            //A diferencia de ref, que sí debe estar inicializada.
            //Ref puede no usarse o leerse, pero out sí debe ser asignada antes de ser leída.

            Console.Write("Ingrese un numero para conocer su factorial: ");
            //Podría mejorarse con un try/catch para que no tire error en tiempo de ejecución en caso de no ser ingresado un entero.
            int numero = Int32.Parse(Console.ReadLine());
            //Declaro la variable f, pero sin asignarle valor, y la envío con la palabra clave out en la invocación.
            int f;
            facOut(numero, out f);
            Console.WriteLine($"Resultado sin recursividad con out: {f}");

            Console.Write("Ahora pero con una función recursiva: ");
            numero = Int32.Parse(Console.ReadLine());
            facRecursivaOut(numero, out f);
            Console.WriteLine($"Resultado con recursividad con out: {f}");


            Console.ReadKey();
        }
        //Calcula n veces la sumatoria de la multiplicación de 1 por 1, por 2, por 3, por n.
        static void facOut(int n, out int f)
        {
            //Ahora a f le doy valor antes de emplearla.
            f = 1;
            for (int i = 1; i <= n; i++) f *= i;
        }

        //Tanto en este método como en el que sigue, se establece el corte de la recursividad con 1, porque tanto el factorial de 0 y de 1, dan 1
        //Desde 2 para arriba, se aguarda el return de n, por el factorial de n-1, y a su vez, de n-2, etc.
        static void facRecursivaOut(int n, out int f)
        {
            if (n == 0)
            {
                f = 1;
            }
            else
            {
                facRecursivaOut(--n, out f);
                //Necesito hacer 'n+1', porque en el contexto de cuando se vuelve de la recusión, n ya vale 1 menos, por el --.
                f *= n + 1;
            }
        }
    }
}
