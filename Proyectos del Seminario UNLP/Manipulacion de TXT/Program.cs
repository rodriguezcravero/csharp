using System;
using System.Collections;
using System.IO;

namespace P7_E10
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creo la lista que me va a servir para guardar datos durante toda la ejecución del programa
            ArrayList listaAutos = new ArrayList();

            //Do-While con las opciones del menú, que se repiten después de cada opción elegida. Sale con 5.
            ConsoleKeyInfo tecla;
            do
            {
                //Menú de opciones como función void aparte
                Menu();

                tecla = Console.ReadKey(true);
                Console.WriteLine();
                switch (tecla.KeyChar)
                {
                    case '1': IngresoConsola(listaAutos); break;
                    case '2': CargarDesdeDisco(listaAutos); break;
                    case '3': GuardaEnDisco(listaAutos); break;
                    case '4': ListarPorConsola(listaAutos); break;
                    case '5': Console.WriteLine("\nHa elegido salir del programa.\n"); break;
                    default: Console.WriteLine("\nLa opción ingresada no es válida\n"); break;
                }

            } while (tecla.KeyChar != '5');

            Console.ReadKey();
        }

        static void Menu()
        {
            Console.WriteLine("Menu de opciones");
            Console.WriteLine("================\n");
            Console.WriteLine("\t1. Ingresar autos desde la consola");
            Console.WriteLine("\t2. Cargar lista de autos desde el disco");
            Console.WriteLine("\t3. Guardar lista de autos en el disco");
            Console.WriteLine("\t4. Listar por consola");
            Console.WriteLine("\t5. Salir");
            Console.Write("Ingrese su opción: ");
        }

        static void IngresoConsola(ArrayList listaAutos)
        {
            //Se valida siempre y cuando la marca no sea un string vacío, en ese caso deja de cargar
            //Se usan dos strings (marca y modelo) y luego se concatenan para guardarse como string único en la lista global
            string marca, modelo;
            Console.WriteLine();
            Console.Write("Ingrese una marca: ");
            marca = Console.ReadLine();
            while (marca != "")
            {
                Console.Write("Ingrese el modelo: ");
                modelo = Console.ReadLine();
                string linea = marca + " " + modelo;
                listaAutos.Add(linea);
                Console.Write("Ingrese una marca: ");
                marca = Console.ReadLine();
            }
            Console.WriteLine("\nSe finalizó la carga de datos.\n");
        }

        static void CargarDesdeDisco(ArrayList listaAutos)
        {
            Console.Write("Ingrese el nombre del archivo: ");
            string archivo = Console.ReadLine();
            //Uso un try/catch que valida que se ingrese un path válido de acceso, sino se imprime el error en pantalla y continúa la ejecución del programa
            try
            {
                using (StreamReader txt = new StreamReader(archivo))
                {
                    string linea = "";
                    while (!txt.EndOfStream)
                    {
                        linea = txt.ReadLine();
                        listaAutos.Add(linea);
                    }
                    //Supuestamente así se hace el 'garbage collector'. Pero ver si hay que hacer dispose.
                    txt.Close();
                    //Se imprime si los datos fueron cargados, o bien si el arrchivo estaba vacío.
                    string str = linea != "" ? "\nLos datos han sido leídos.\n" : "\nNo había datos en el archivo\n";
                    Console.WriteLine(str);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en el archivo => " + e.Message);
            }
        }

        static void GuardaEnDisco(ArrayList listaAutos)
        {
            if (listaAutos.Count == 0) Console.WriteLine("No hay datos para escribir");
            else
            {
                //Podría crear una excepción si el tipo de archivo no es válido, como txt, doc, etc.
                Console.Write("Ingrese el nombre del archivo de destino: ");
                string archivo = Console.ReadLine();
                try
                {
                    using (StreamWriter txt = new StreamWriter(archivo))
                    {
                        foreach (string strs in listaAutos)
                        {
                            txt.WriteLine(strs);
                        }
                        //Supuestamente así se hace el 'garbage collector'. Pero ver si hay que hacer dispose.
                        txt.Close();
                        Console.WriteLine("\nLos datos han sido guardados.\n");
                    }
                }
                //El catch funciona si no se ingresa nombre de archivo, pero no valida el tipo de archivo.
                catch (Exception e)
                {
                    Console.WriteLine("Error en la creación del archivo => " + e.Message);
                }
            }
        }

        static void ListarPorConsola(ArrayList listaAutos)
        {
            //Se chequea si la lista está vacía o no.
            if (listaAutos.Count == 0) Console.WriteLine("\nNo hay datos que mostrar\n");
            else
            {
                Console.WriteLine();
                string[] strs;
                foreach (string linea in listaAutos)
                {
                    strs = linea.Split(" ");
                    Console.WriteLine($"Marca: {strs[0]}, Modelo: {strs[1]}");
                }
            }
            Console.WriteLine();
        }
    }
}
