using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace P9_E8
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creo las variables del diccionario ordenado, el texto que voy a leer del archivo, y el nombre que pido por consola.
            SortedDictionary<string, int> diccionario = new SortedDictionary<string, int>();
            string texto = "";
            Console.Write("Ingrese el nombre de un archivo: ");
            string archivo = Console.ReadLine();

            //Hago un Try/Catch del StreamReader. En caso de no encontrar el archivo, el programa sigue y hace una validación con la variable texto.
            try
            {
                using (StreamReader st = new StreamReader(archivo))
                {
                    while (!st.EndOfStream) texto = st.ReadToEnd();
                    st.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
            }

            //Probé de hacer un arreglo de caracteres y utilizarlo con el método Split, pero es mejor usar la expresión regular.
            string[] palabras = Regex.Split(texto, @"\W+");

            //Valido que la variable texto tenga contenido (sólo si el archivo era válido y contenía textp).
            //Si no, se imprime un mensaje.
            if (texto != "")
            {
                //Por cada palabra en el arreglo, se comprueba lo siguiente:
                //Si la palabra (como clave) existe en el diccionario, se suma uno al valor;
                //Si no, se agrega la palabra como key, y el valor de 1 como value.
                foreach (string palabra in palabras)
                {
                    if (diccionario.ContainsKey(palabra)) diccionario[palabra]++;
                    else diccionario.Add(palabra, 1);
                }

                foreach (KeyValuePair<string, int> item in diccionario) Console.WriteLine($"{item.Key}: {item.Value}");
            }
            else Console.WriteLine("No se leyeron datos.");

            Console.ReadKey();
        }
    }
}
//Ejemplo de cómo se podría haber usado el arreglo de caracteres con el Split:
// char[] arr = new char[] { ',', ',', '.', '(', ')', ':', ' ' };
// string[] strs = texto.Split(arr);