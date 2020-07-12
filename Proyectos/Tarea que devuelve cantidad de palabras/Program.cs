/*14) Codificar un método asincrónico que utilice el método codificado en el ejercicio anterior y que
devuelva un Task<int> con la cantidad de palabras contenidas en un archivo de texto cuyo nombre
se pasa como parámetro.
*/
using System;
using System.IO;
using System.Threading.Tasks;

namespace P10_E14__Task_que_devuelve_cantidad_de_palabras_
{
    class Program
    {
        static void Main(string[] args)
        {
            //Se invoca a una tarea asincrónica que devolverá como resultado un entero, la cantidad de palabras
            Task<int> t1 = ContarPalabrasAsync("data1.txt");

            //Se realiza una espera sincrónica del resultado para imprimirse en pantalla
            Console.WriteLine($"Cantidad de palabras: {t1.Result}");

            Console.ReadKey();
        }

        static async Task<int> ContarPalabrasAsync(string archivo)
        {
            //El primer método llama a un segundo método asincrónico
            Task<string> tarea = TextoAsync(archivo);
            //Se aguarda el resultado y se devuelve el control a Main
            await tarea;
            //Una vez que tengo el string, lo spliteo y cuento las palabras del arreglo resultante
            string texto = tarea.Result;
            string[] arr = texto.Split(" ");
            return arr.Length;
        }

        static async Task<string> TextoAsync(string archivo)
        {
            //Este segundo método asincrónico llama a la tarea que leer el texto y devuelve un string
            Task<string> tarea = new Task<string>(() => leerTexto(archivo));
            //Se da comienzo a la tarea
            tarea.Start();
            //Se aguarda el resultado y se devuelve el control al método invocador
            await tarea;
            //Una vez se obtiene el string, se lo retorna al método invocador
            return tarea.Result;
        }

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
