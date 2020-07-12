using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P10_E8__Task_Sumatoria_
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creo una lista de Tareas
            List<Task> tareas = new List<Task>();

            for (int a = 1; a <= 3; a++)
            {
                for (int b = a + 2; b <= a + 4; b++)
                {
                    int[] arr = new int[2] { a, b };
                    //A la lista le voy a agregar una lambda que invoca a Sumatoria con un arreglo de enteros,
                    //el que asigno dentro de las llaves.
                    tareas.Add(Task.Factory.StartNew((o) =>
                    {
                        int[] a = (int[])o;
                        Sumatoria(a[0], a[1]);
                    }, arr));
                }
            }

            Task.WaitAll(tareas.ToArray());

            Console.ReadKey();
        }

        static void Sumatoria(int a, int b)
        {
            int sumatoria = 0;
            for (int i = a; i <= b; i++)
            {
                sumatoria += i;
            }
            Console.WriteLine($"suma desde {a} hasta {b} = {sumatoria}");
        }
    }
}

//También funciona si hago esta asignacion
//int A = a; int B = b;
//tareas.Add(Task.Factory.StartNew(() => Sumatoria(A, B)));