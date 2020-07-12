using System;

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
            new Persona() {Nombre="Zulema"},
            new Perro() {Nombre="Sultán"},
            new Persona() {Nombre="Claudia"},
            new Persona() {Nombre="Carlos"},
            new Perro() {Nombre="Chopper"},
            };
            lista.Sort(); //debe ordenar por Nombre alfabéticamente
            foreach (INombrable n in lista)
            {
                Console.WriteLine($"{n.Nombre}: {n}");
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

    class Persona : IAtendible, IComercial, IImportante, INombrable, IComparable
    {
        public string Nombre { get; set; }

        public int CompareTo(object obj) => this.Nombre.CompareTo((obj as INombrable).Nombre);

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

        public int CompareTo(object obj) => this.Nombre.CompareTo((obj as INombrable).Nombre);

        public void SeVendeA(Persona p) => Console.WriteLine("Vendiendo perro a persona");
        public void EsAtendido() => Console.WriteLine("Atendiendo perro");

        public void EsLavado() => Console.WriteLine("Lavando perro");
        public void EsSecado() => Console.WriteLine("Secando perro");

        //En este caso debe ser un ToString o implementar una interfaz?
        public override string ToString() => $"{Nombre} es un perro";
    }

}
