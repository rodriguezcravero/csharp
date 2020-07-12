using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace P9_E10
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Declaración de variables*/
            string texto1, texto2;
            SortedSet<string> set1 = new SortedSet<string>();
            SortedSet<string> set2 = new SortedSet<string>();
            SortedSet<string> setFinal = new SortedSet<string>();
            List<string> listaPalabras = new List<string>();

            // Console.Write("Ingrese el nombre de un archivo: ");
            // string archivo2 = Console.ReadLine();
            // Console.Write("Ingrese el nombre de un archivo: ");
            // string archivo1 = Console.ReadLine();

            //Para mayor rapidez, hardcodeo los nombres de ambos archivos.
            string archivo1 = "data.txt";
            string archivo2 = "data2.txt";

            //Guardo los textos de cada archivo.
            texto1 = textoArchivo(archivo1);
            texto2 = textoArchivo(archivo2);

            //Llamo a un método que lee estos textos y devuelve un set de palabras únicas.
            set1 = textoASet(texto1);
            set2 = textoASet(texto2);

            //Finalmente, sólo guardo las palabras repetidas en un solo SortedSet.
            setFinal = set1;
            setFinal.IntersectWith(set2);

            //Luego, obtengo una lista de strings, solamente con las ocurrencias de ambos textos.
            foreach (string palabra in setFinal)
            {
                listaPalabras.Add(palabra);
            }

            //Creo una lista de objetos PalabraPosiciones, e invoco al método ConvertAll, que lleva la lista de palabras recién creada.
            List<PalabraPosiciones> data = recorrer(listaPalabras, texto1, texto2);

            Imprimir(data);

            Console.ReadKey();
        }

        //El método estático ConvertAll recibe la lista de palabras únicas generadas entre ambos archivos.
        public static List<PalabraPosiciones> recorrer(List<string> listaPalabras, params string[] textos)
        {
            //A la lista de palabras que recibo por parámetro, invoco su método ConvertAll, que, por cada palabra, me devuelve un objeto PalabraPosiciones
            //Cada uno de esos objetos, tendrá una palabra y una lista de listas de enteros, vacía, que luego se llenará con las ocurrencias.
            List<PalabraPosiciones> listaPP = listaPalabras.ConvertAll<PalabraPosiciones>(Convertir);

            //Empiezo a recorrer uno por uno, estas instancias de PalabraPosiciones que acabo de crear
            foreach (PalabraPosiciones palPos in listaPP)
            {
                //Ahora voy a recorrer N veces, segun la cantidad de archivos de texto que haya leido en el Main (en este caso, 2)
                foreach (string texto in textos)
                {
                    //Creo una lista de enteros. Voy a tener N lista de enteros, que insertaran cada posicion de la ocurrencia
                    List<int> listaOcurrencias = new List<int>();
                    //Busco la primera posición de la palabra de mi lista PalabraPosiciones
                    int pos = texto.IndexOf(palPos.Palabra);

                    //Pos será igual a -1 cuando ya no encuentre ocurrencias
                    while (pos != -1)
                    {
                        listaOcurrencias.Add(pos);
                        //Busco de nuevo con pos + 1, así va avanzando en el string
                        pos = texto.IndexOf(palPos.Palabra, pos + 1);
                    }
                    //Guardo de a una, cada lista de enteros con las ocurrencias de las palabras
                    palPos.Posiciones.Add(listaOcurrencias);
                }
            }

            return listaPP;
        }

        public static PalabraPosiciones Convertir(string palabra)
        {
            List<List<int>> listaDeListasInt = new List<List<int>>();
            PalabraPosiciones pp = new PalabraPosiciones(palabra, listaDeListasInt);
            return pp;
        }

        public static SortedSet<string> textoASet(string texto)
        {
            SortedSet<string> set = new SortedSet<string>();
            //Hago un split con la expresión regular, y guardo cada una de las palabras en un arreglo de strings.
            string[] palabras = Regex.Split(texto, @"\W+");
            //Luego hago un foreach donde por cada palabra recién leída, invoco al método Add de SortedSet, donde solo se guardará una ocurrencia.
            foreach (string palabra in palabras) set.Add(palabra);
            //Retorno el set
            return set;
        }

        public static string textoArchivo(string archivo)
        {
            string textoAux = "";
            //Hago un Try/Catch del StreamReader. En caso de no encontrar el archivo, el programa sigue y hace una validación con la variable texto.
            try
            {
                using (StreamReader st1 = new StreamReader(archivo))
                {
                    while (!st1.EndOfStream) textoAux = st1.ReadToEnd();
                    st1.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
            }
            return textoAux;
        }

        public static void Imprimir(List<PalabraPosiciones> data)
        {
            //Este foreach recorre cada uno de los objetos finales:
            //1) PalabraPosiciones cuenta con 2 propiedades, la primera, es cada polabra de la lista anterior.
            //2) La segunda propiedad, en este ejemplo, cuenta con una lista de 2 listas de enteros.
            //3) Por ende, por cada elemento de PalabraPosiciones, debo pararme en la 1º lista y hacer un foreach, y luego en la 2º
            foreach (PalabraPosiciones item in data)
            {
                Console.WriteLine($"Palabra: {item.Palabra}");
                Console.Write($"\t|--Posiciones en Texto1:-->");
                foreach (int pos in item.Posiciones[0]) Console.Write(pos + " ");
                Console.Write($"\n\t|--Posiciones en Texto2:-->");
                foreach (int pos in item.Posiciones[1]) Console.Write(pos + " ");
                Console.WriteLine(); Console.WriteLine();
            }
        }
    }

    //Cada objeto de PalabraPosiciones guardará una palabra, y además una lista de listas, esta segunda será una colección de enteros.
    class PalabraPosiciones
    {
        public string Palabra { private set; get; }
        public List<List<int>> Posiciones { private set; get; } = new List<List<int>>();

        public PalabraPosiciones(string p, List<List<int>> l)
        {
            Palabra = p;
            Posiciones = l;
        }
    }

}

