/*9) Dado el método static int Sumatoria(int n) 

Completar el siguiente código para invocar asincrónicamente el método Sumatoria e imprimir el
entero que devuelve cada tarea
Salida por consola 1
3
6
10
15
21
28
36
45
55
a) Resolverlo utilizando un constructor de la clase Task
b) Resolverlo utilizando el método StartNew de una instancia de TaskFactory*/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P10_E9__Sumatoria_con_Task_y_TaskFactory_
{
    class Program
    {
        static void Main(string[] args)
        {
            ejecutarA();
            ejecutarB();

            Console.ReadKey();

        }

        static public void ejecutarA()
        {
            //Creo una lista de tareas que devuelven un entero
            List<Task<int>> tareas = new List<Task<int>>();
            for (int n = 1; n <= 10; n++)
            {
                //De 1 a 10, agrego la tarea que realizará la sumatoria de 1 a 10
                tareas.Add(new Task<int>((o) => Sumatoria((int)o), n));
                //Les indico que se inicien asincrónicamente
                tareas[n - 1].Start();
            }

            Console.WriteLine("**Se ejecuta A**");
            //Por más que las tareas se ejecuten asincrónicamente, los resultados se mostrarán en orden
            foreach (Task<int> t in tareas) Console.WriteLine(t.Result);
        }

        static public void ejecutarB()
        {
            List<Task<int>> tareas = new List<Task<int>>();
            for (int n = 1; n <= 10; n++)
            {
                //Idem a A, pero el método estático de la clase Task permite iniciar en una sola línea cada tarea
                tareas.Add(Task.Factory.StartNew((o) => Sumatoria((int)o), n));
            }

            Console.WriteLine(); Console.WriteLine("**Se ejecuta B**");
            foreach (Task<int> t in tareas) Console.WriteLine(t.Result);
        }

        static int Sumatoria(int n)
        {
            int suma = 0;
            for (int i = 1; i <= n; i++)
            {
                suma += i;
            }
            return suma;
        }
    }
}
