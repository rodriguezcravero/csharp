/*15) Codificar un método asincrónico que utilice el método codificado en el ejercicio anterior y que devuelva un Task<int[]> con la cantidad de palabras contenidas en cada uno de los archivos de texto cuyos nombres se pasan como parámetro en un string[]. Este método debe invocar varias veces al método definido en el ejercicio anterior lo que generará varias tareas que deben esperarse sincrónicamente. Para esperar varias tareas de manera asincrónica se puede usar Task.WenAll(...) que crea una tarea que finalizará cuando se hayan completado todas las tareas proporcionadas, por lo tanto se puede usar await Task.WenAll(...).*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace P10_E15__Task_que_devuelve_int___palabras_
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] archivos = new string[6] { "data1.txt", "data2.txt", "data3.txt", "data4.txt", "data5.txt", "data6.txt" };

            Task<int[]> tarea = Punto15Async(archivos);

            foreach (int cantidad in tarea.Result)
            {
                Console.WriteLine(cantidad);
            }
            Console.ReadKey();
        }

        //Método asincrónico que devuelve un arreglo de enteros, es decir, la cantidad de palabras de cada archivo.
        static async Task<int[]> Punto15Async(string[] archivos)
        {
            //Creo un arreglo de enteros con el tamaño N de la cantidad de archivos recibidos por parámetro
            int[] resultado = new int[archivos.Length];
            //Hago una lista de tareas
            List<Task<int>> listaTareas = new List<Task<int>>();
            //Por cada archivo, voy a invocar a un método asincrónico, que será guardado en la lista de tareas.
            for (int i = 0; i < archivos.Length; i++)
            {
                Task<int> t = ContarPalabrasAsync(archivos[i]);
                listaTareas.Add(t);
            }

            //Aguardo las tareas
            await Task.WhenAll(listaTareas);

            //Guardo los resultados en mi arreglo de enteros
            for (int i = 0; i < archivos.Length; i++)
            {
                resultado[i] = listaTareas[i].Result;
            }
            return resultado;

        }

        static async Task<int> ContarPalabrasAsync(string archivo)
        {
            //Este método es llamado por cada archivo, que a su vez invocará a un tercer método asincrónico
            Task<string> tarea = TextoAsync(archivo);
            //Se aguarda el resultado y se devuelve el control al método invocador
            await tarea;
            //Una vez que tengo el string, lo spliteo y cuento las palabras del arreglo resultante
            string[] arr = tarea.Result.Split(" ");
            //Devuelvo la cantidad de palabras
            return arr.Length;
        }

        static async Task<string> TextoAsync(string archivo)
        {
            //Este método asincrónico llama a la tarea que leer el texto y devuelve un string
            Task<string> tarea = new Task<string>(() => leerTexto(archivo));
            //Se da comienzo a la tarea
            tarea.Start();
            //Se aguarda el resultado y se devuelve el control al método invocador
            await tarea;
            //Una vez se obtiene el string, se lo retorna al método invocador
            return tarea.Result;
        }

        //Método que lee los textos de cada archivo
        static string leerTexto(string archivo)
        {
            string texto = null;
            try
            {
                using (StreamReader txt = new StreamReader(archivo))
                {
                    while (!txt.EndOfStream)
                    {
                        texto = txt.ReadToEnd();
                    }
                    txt.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en el archivo => " + e.Message);
            }
            return texto;
        }
    }
}

