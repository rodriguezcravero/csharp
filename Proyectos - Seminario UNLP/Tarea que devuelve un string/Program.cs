/*13) Codificar un método asincrónico que devuelva un Task<string> con el contenido de un archivo
de texto cuyo nombre se pasa como parámetro.
*/
using System;
using System.IO;
using System.Threading.Tasks;

namespace P10_E13__Task_que_devuelve_string_
{
    class Program
    {
        static void Main(string[] args)
        {
            //Invocación del método asincrónico
            Task<string> t1 = TextoAsync("data1.txt");

            //Se imprime en pantalla el string resultado. Podría validar en caso de null
            Console.WriteLine(t1.Result);

            Console.ReadKey();
        }

        static async Task<string> TextoAsync(string archivo)
        {
            //El método asincrónico invoca a su vez a una tarea, que se encarga de leer el texto
            Task<string> tarea = new Task<string>(() => leerTexto(archivo));
            //Se inicia la tarea
            tarea.Start();
            //Se devuelve el control a Main
            await tarea;
            //Mientras que el constructor devuelve una tarea por nosotros, nuestro programa devuelve el resultado del tipo de la tarea
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
