/*Utilizar la clase Queue (cola) para implementar un programa que realice el cifrado de un texto
aplicando la técnica de clave repetitiva. La técnica de clave repetitiva consiste en desplazar un
carácter una cantidad constante de acuerdo a la lista de valores que se encuentra en la clave. Por
ejemplo: para la siguiente tabla de caracteres:
A B C D E F G H I J K L M N Ñ O P Q R S T U V W X Y Z sp
1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28
Si la clave es {5,3,9,7} y el mensaje a cifrar “HOLA MUNDO”
Se cifra de la siguiente manera:
H O L A sp M U N D O  Mensaje original
8 16 12 1 28 13 22 14 4 16  Código sin cifrar
5 3 9 7 5 3 9 7 5 3  Clave repetida
13 19 21 8 5 16 3 21 9 19  Código cifrado
M R T H E O C T I R  Mensaje cifrado
*/

using System;
using System.Collections;

namespace P3_E12
{
    class Program
    {
        static void Main(string[] args)
        {
            //En vez de usar un arreglo, uso un string con un * adelante para que comience a buscar desde el 1. Podría mejorarse para que funcione con minúsculas y mayúsculas.
            string abecedario = "*ABCDEFGHIJKLMNÑOPQRSTUVWXYZ ";
            string mensaje = "HOLA MUNDO";
            //Guardo el nuevo mensaje en otro string, y le asigno un mensaje vacío, así puedo hacer un append en el for.
            Queue cola = new Queue();
            cola.Enqueue(5); cola.Enqueue(3); cola.Enqueue(9); cola.Enqueue(7);

            string mensajeCifrado = cifrar(mensaje, cola, abecedario);
            Console.WriteLine($"Mensaje a cifrar: {mensaje} / Resultado del cifrado: {mensajeCifrado}");

            //Como la cola puede haber quedado desordenada, vuelvo a chequear que quede en el orden usado. Otra opción era crear una cola nueva.
            resetearCola(cola);

            string mensajeDescifrado = descifrar(mensajeCifrado, cola, abecedario);
            Console.WriteLine($"Mensaje a descifrar: {mensajeCifrado} / Resultado del descifrado: {mensajeDescifrado}");

            Console.ReadKey();
        }

        static string cifrar(string mensaje, Queue cola, string abecedario)
        {
            string mensajeCifrado = ""; char letraMensaje; int numeroCola;
            for (int i = 0; i < mensaje.Length; i++)
            {
                letraMensaje = mensaje[i];
                numeroCola = (int)cola.Dequeue();
                //Obtengo la nueva letra con un operador condicional. Si la suma de la posicion de la letra en abecedario más la clave de la cola me da más de 28, le resto 28.
                mensajeCifrado += abecedario[abecedario.IndexOf(letraMensaje) + numeroCola < 28 ? abecedario.IndexOf(letraMensaje) + numeroCola : (abecedario.IndexOf(letraMensaje) + numeroCola) - 28];
                //Vuelvo a encolar el número que saqué.
                cola.Enqueue(numeroCola);
            }
            return mensajeCifrado;
        }

        static string descifrar(string mensaje, Queue cola, string abecedario)
        {
            string mensajeDescifrado = ""; char letraMensaje; int numeroCola;
            for (int i = 0; i < mensaje.Length; i++)
            {
                letraMensaje = mensaje[i];
                numeroCola = (int)cola.Dequeue();
                //Obtengo la nueva letra con un operador condicional. Si la suma de la posicion de la letra en abecedario más la clave de la cola me da más de 28, le resto 28.
                mensajeDescifrado += abecedario[abecedario.IndexOf(letraMensaje) - numeroCola > 0 ? abecedario.IndexOf(letraMensaje) - numeroCola : (abecedario.IndexOf(letraMensaje) - numeroCola) + 28];
                //Vuelvo a encolar el número que saqué.
                cola.Enqueue(numeroCola);
            }
            return mensajeDescifrado;
        }

        static void resetearCola(Queue cola)
        {
            int aux = (int)cola.Dequeue();
            while (aux != 7)
            {
                cola.Enqueue(aux);
                aux = (int)cola.Dequeue();
            }
            cola.Enqueue(aux);
        }
    }
}
