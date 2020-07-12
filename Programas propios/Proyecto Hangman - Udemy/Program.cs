using System;

namespace Proyecto_Hangman___Udemy
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Variables*/
            Random random = new Random(); //Para acceder a un numero random, primero hay que instanciar un objeto de tipo Random.
            int intentos = 8; //Designa la cantidad de intentos como variable global
            bool ganaste = false; //Booleano global que sólo se vuelve true cuando se adivina toda la palabra
            char letra; //Variable que alojará la letra ingresada cada vuelta. SE PODRIA MEJORAR HACIENDO VALIDACION DE SOLO LETRAS
            string repetidas = ""; //Para solamente usar tipos básicos de datos, guardo las letras repetidas en un string. SE PODRIA MEJORAR USANDO UNA COLECCION DINAMICA
            string[] palabras = new string[] { "palabra", "zodiaco", "carpincho", "abecedario", "murcielago", "espada", "computadora", "oceano", "hueste", "vecindario" }; //Palabras que serán seleccionadas de manera random. Podria haber N palabras.

            string palabraSeleccionada = palabras[random.Next(palabras.Length)]; //Se elige una palabra del array de palabras.
            string palabraOculta = ""; //String que será del mismo largo de la palabra seleccionada pero con asteriscos.

            for (int i = 0; i < palabraSeleccionada.Length; i++) palabraOculta += '*'; //Lleno el string de asteriscos, que será mostrado en pantalla.

            /*Comienza el juego*/
            Console.WriteLine("**JUEGO DE AHORCADO**\n");
            while (intentos > 0 && !ganaste) //El juego puede terminar porque se terminaron los intentos (perdió) o se adivinó la palabra (ganó);
            {
                Console.WriteLine($"Tiene {intentos} intentos. La palabra a adivinar es: {palabraOculta}"); // Para imprimir los asteriscos (array de caracteres) uso una funcion con un for que los imprime. Si se adivinan letras, se muestran en lugar de los asteriscos.
                Console.Write("Ingrese una letra: ");
                letra = char.Parse(Console.ReadLine()); //Opcion para leer un caracter. Se podría hacer de mejores formas para validar que sólo se ingresen letras.
                if (letraValida(ref repetidas, letra)) //Lo primer que hago es chequear que se ingrese una letra válida, es decir, una letra que no se haya ingresado previamente.
                {
                    if (comprobarLetra(palabraSeleccionada, letra)) //Si la letra es nueva, se chequea que coincida con alguna de la palabra a adivinar
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Muy bien! ");
                        actualizarAsteriscos(palabraSeleccionada, letra, ref palabraOculta, out ganaste); //Funcion que cambia cada asterisco por ocurrencia de la letra
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Esa letra no está en la palabra");
                        intentos--; //Si la letra es nueva pero no coincide, se pierde un intento.
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Ya ingresaste esa letra previamente. Perdiste un intento.");
                    intentos--; //Si la letra ya fue ingresada antes, se pierde un intento.
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (ganaste) Console.WriteLine($"Ganaste!! La palabra era {palabraSeleccionada}");
            else Console.WriteLine($"Se acabaron los intentos y perdiste, la palabra era {palabraSeleccionada}");

            Console.ReadKey();

        }

        public static bool letraValida(ref string repetidas, char letra)
        {
            //se recorre el string que aloja las letras repetidas. En tal caso, se retorna falso. Sino, se agrega al string.
            for (int i = 0; i < repetidas.Length; i++) if (repetidas[i] == letra) return false;
            repetidas += letra;
            return true;
        }

        public static bool comprobarLetra(string palabra, char letra)
        {
            foreach (char l in palabra) if (l == letra) return true;
            return false;
        }

        public static void actualizarAsteriscos(string palabraSeleccionada, char letra, ref string palabraOculta, out bool ganaste)
        {
            //Primero se recorre cada letra de la palabra para saber la posición del match. Luego se actualizan los asteriscos por la letra en esa posición.
            for (int i = 0; i < palabraSeleccionada.Length; i++)
            {
                if (palabraSeleccionada[i] == letra)
                {
                    palabraOculta = palabraOculta.Remove(i, 1);
                    palabraOculta = palabraOculta.Insert(i, letra.ToString());
                }
            }
            //Si ya no quedan asteriscos, el jugador ganó.
            ganaste = palabraOculta.Contains('*') ? false : true;
        }
    }
}
