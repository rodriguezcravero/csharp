/*8) Dada la siguiente definición de clase:
 class ListaDePersonas
 {
 private Hashtable ht = new Hashtable();
 public void Agregar(Persona p)
 {
 ht[p.DNI] = p;
 }
 }
Completarla agregando dos indizadores de sólo lectura
Un índice entero que permite acceder a las personas de la lista por número de documento. Por ejemplo p=lista[30456345] devuelve el objeto Persona que tiene DNI=30456345 o null en caso
que no exista en la lista. Un índice de tipo char que devuelve un arreglo de strings con todos los nombres de las personas de la lista que comienzan con el carácter índice. Por ejemplo arreglo=lista['L'] devuelve todos los nombres de las personas que comienzan con L.*/
using System;
using System.Collections;


namespace P5_E8
{
    class Program
    {
        static void Main(string[] args)
        {
            ListaDePersonas lista = new ListaDePersonas();
            Persona Nacho = new Persona("Nacho", 'm', 111, new DateTime(1985, 03, 11));
            Persona Maria = new Persona("Maria", 'f', 222, new DateTime(1990, 06, 11));
            Persona Elsa = new Persona("Elsa", 'f', 333, new DateTime(1999, 03, 01));
            Persona Noelia = new Persona("Noelia", 'f', 444, new DateTime(2002, 05, 21));
            Persona Martin = new Persona("Martin", 'm', 555, new DateTime(1980, 12, 05));
            lista.Agregar(Nacho);
            lista.Agregar(Maria);
            lista.Agregar(Elsa);
            lista.Agregar(Noelia);
            lista.Agregar(Martin);

            Persona p1 = lista[111];
            Persona p2 = lista[11];
            Persona p3 = lista[444];

            Console.WriteLine(p1 is null); //False
            Console.WriteLine(p2 is null); //True
            Console.WriteLine(p3 is null); //False

            string[] arreglo = lista['U'];

            if (arreglo != null) foreach (string item in arreglo) Console.Write(item + " - ");



            Console.ReadKey();
        }
    }
    class ListaDePersonas
    {
        private Hashtable ht = new Hashtable();

        public void Agregar(Persona p)
        {
            //El indizador devuelve la persona si existe el dni, y si no devueolve null
            ht[p.DNI] = p;
        }

        public Persona this[int i]
        {
            get => (Persona)ht[i];
        }

        public string[] this[char c]
        {
            get
            {
                //Como no puedo saber cuántas personas tienen nombre con esa inicial, creo primero una lista dinámica, donde voy agregando cada persona que encuentro.
                ArrayList listaStr = new ArrayList();
                foreach (DictionaryEntry p in ht)
                {
                    Persona aux = (Persona)p.Value;
                    if (aux.Nombre[0] == c) listaStr.Add(aux.Nombre);
                }

                //La consigna pide devolver un arreglo de strings. Cuando finalizo de crear mi ArrayList, ahora sí puedo crear un string[]
                string[] arrStr = new string[listaStr.Count];

                for (int i = 0; i < listaStr.Count; i++) arrStr[i] = (string)listaStr[i];

                return arrStr;
            }
        }

    }
}
