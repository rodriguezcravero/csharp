using System;
using System.Collections;

namespace P7_E4
{
    class Program
    {
        static void Main(string[] args)
        {
            Ejecutar();

            Console.ReadKey();
        }

        public static void Ejecutar()
        {
            System.Collections.ArrayList lista = new System.Collections.ArrayList() {
            new Persona() {Nombre="Ana María"},
            new Perro() {Nombre="Sultán"},
            new Persona() {Nombre="Ana"},
            new Persona() {Nombre="José Carlos"},
            new Perro() {Nombre="Chopper"}
            };
            lista.Sort(new ComparadorLongitudNombre()); //ordena por longitud de Nombre
            foreach (INombrable n in lista)
            {
                Console.WriteLine($"{n.Nombre.Length}: {n.Nombre}");
            }
        }
    }

    //Las interfaces 'obligan' a las clases que las implementan a tener todos los miembros de dichas interfaces
    interface IVendible { public void SeVendeA(Persona p); }
    interface ILavable { public void EsLavado(); public void EsSecado(); }
    interface IAtendible { public void EsAtendido(); }
    /*Interfaces Ejercicio 3*/
    interface IComercial { public void Importa(); }
    interface IImportante { public void Importa(); }
    /*Interfaces Ejercicio 4*/
    interface INombrable { public string Nombre { get; set; } }

    class ComparadorLongitudNombre : IComparer
    {
        //Esta clase implementa IComparer, por ende debe implementar el método Compare, que recibe 2 objetos como parámetros
        //Los casteo como INombrables para usar su propiedad Nombre, y comparo el Length de cada uno.
        public int Compare(object o1, object o2) => (o1 as INombrable).Nombre.Length.CompareTo((o2 as INombrable).Nombre.Length);
    }

    class Persona : IAtendible, IComercial, IImportante, INombrable, IComparable
    {
        public string Nombre { get; set; }

        //CompareTo primero evalua si el objeto con el que comparo es Perro, si es así, devuelvo -1, porque Perro está después que Persona
        //En caso de ser Persona, compara sus nombres.
        public int CompareTo(object obj) => obj is Perro ? -1 : this.Nombre.CompareTo((obj as INombrable).Nombre);

        public void EsAtendido() => Console.WriteLine("Atendiendo persona");

        //La implementación explícita de métodos de interfaces no llevan public
        void IComercial.Importa() => Console.WriteLine("Persona vendiendo al exterior");
        void IImportante.Importa() => Console.WriteLine("Persona importante");
        public void Importa() => Console.WriteLine("Método Importar() de la clase Persona");

        //En este caso debe ser un ToString o implementar una interfaz?
        public override string ToString() => $"{Nombre} es una persona";

    }

    class Perro : IVendible, IAtendible, ILavable, INombrable, IComparable
    {
        public string Nombre { get; set; }

        //CompareTo primero evalua si el objeto con el que comparo es Persona, si es así, devuelvo 1, porque Perro está después que Persona
        //En caso de ser Perro, compara sus nombres.
        public int CompareTo(object obj) => obj is Persona ? 1 : this.Nombre.CompareTo((obj as INombrable).Nombre);

        public void SeVendeA(Persona p) => Console.WriteLine("Vendiendo perro a persona");
        public void EsAtendido() => Console.WriteLine("Atendiendo perro");

        public void EsLavado() => Console.WriteLine("Lavando perro");
        public void EsSecado() => Console.WriteLine("Secando perro");

        //En este caso debe ser un ToString o implementar una interfaz?
        public override string ToString() => $"{Nombre} es un perro";
    }

}

