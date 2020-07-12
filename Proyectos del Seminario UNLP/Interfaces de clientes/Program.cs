/*1) Codificar las clases e interfaces necesarias para modelar un sistema que trabaja con las siguientes
entidades: Autos, Libros, Películas, Personas y Perros. Las entidades pueden ser alquilables (se
pueden alquilar a una persona y ser devueltas por una persona), vendibles (se pueden vender a una
persona), lavables (se pueden lavar y secar) y reciclables (se pueden reciclar). A continuación se
describen estas relaciones:
● Son Alquilables: Libros y Películas
● Son Vendibles: Autos y Perros
● Son Lavables: Autos
● Son Reciclables: Libros y Autos
● Son atendibles: Personas y Perros
Completar el código de la clase estática Procesador:
 static class Procesador
 {
 public static void Alquilar(IAlquilable x, Persona p) => x.SeAlquilaA(p);
 public static . . .
 . . .
 }
*/
using System;

namespace P7_E3
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
            // Auto auto = new Auto();
            // Libro libro = new Libro();
            // Persona persona = new Persona();
            // Perro perro = new Perro();
            // Pelicula pelicula = new Pelicula();
            // Procesador.Alquilar(pelicula, persona);
            // Procesador.Alquilar(libro, persona);
            // Procesador.Atender(persona);
            // Procesador.Atender(perro);
            // Procesador.Devolver(pelicula, persona);
            // Procesador.Devolver(libro, persona);
            // Procesador.Lavar(auto);
            // Procesador.Reciclar(libro);
            // Procesador.Reciclar(auto);
            // Procesador.Secar(auto);
            // Procesador.Vender(auto, persona);
            // Procesador.Vender(perro, persona);
            // /*Ejercicio 2*/
            // Procesador.Lavar(perro);
            // Procesador.Secar(perro);
            // PeliculaClasica peliculaClasica = new PeliculaClasica();
            // Procesador.Alquilar(peliculaClasica, persona);
            // Procesador.Devolver(peliculaClasica, persona);
            // Procesador.Vender(peliculaClasica, persona);

            /*Ejercicio 3*/
            System.Collections.ArrayList lista = new System.Collections.ArrayList() {
                new Persona(),
                new Auto()
            };
            foreach (IComercial c in lista)
            {
                c.Importa();
            }
            foreach (IImportante i in lista)
            {
                i.Importa();
            }
            (lista[0] as Persona).Importa();
            (lista[1] as Auto).Importa();

        }
        static class Procesador
        {
            //Es una utility class, que implemente métodos que harán actuar a diferentes objetos con las mismas acciones.
            public static void Alquilar(IAlquilable x, Persona p) => x.SeAlquilaA(p);
            public static void Atender(IAtendible x) => x.EsAtendido();
            public static void Devolver(IAlquilable x, Persona p) => x.SeDevuelveA(p);
            public static void Lavar(ILavable x) => x.EsLavado();
            public static void Reciclar(IReciclable x) => x.EsReciclado();
            public static void Secar(ILavable x) => x.EsSecado();
            public static void Vender(IVendible x, Persona p) => x.SeVendeA(p);
        }
    }

    //Las interfaces 'obligan' a las clases que las implementan a tener todos los miembros de dichas interfaces
    interface IAlquilable { public void SeAlquilaA(Persona p); public void SeDevuelveA(Persona p); };
    interface IVendible { public void SeVendeA(Persona p); };
    interface ILavable { public void EsLavado(); public void EsSecado(); };
    interface IReciclable { public void EsReciclado(); };
    interface IAtendible { public void EsAtendido(); };
    /*Interfaces Ejercicio 3*/
    interface IComercial { public void Importa(); };
    interface IImportante { public void Importa(); };

    class Persona : IAtendible, IComercial, IImportante
    {
        public void EsAtendido() => Console.WriteLine("Atendiendo persona");

        //La implementación explícita de métodos de interfaces no llevan public
        void IComercial.Importa() => Console.WriteLine("Persona vendiendo al exterior");
        void IImportante.Importa() => Console.WriteLine("Persona importante");
        public void Importa() => Console.WriteLine("Método Importar() de la clase Persona");
    }

    class Auto : IVendible, ILavable, IReciclable, IComercial, IImportante
    {
        public void SeVendeA(Persona p) => Console.WriteLine("Vendiendo auto a persona");
        public void EsLavado() => Console.WriteLine("Lavando auto");
        public void EsSecado() => Console.WriteLine("Secando auto");
        public void EsReciclado() => Console.WriteLine("Reciclando auto");

        //La implementación explícita de métodos de interfaces no llevan public
        void IComercial.Importa() => Console.WriteLine("Auto que se vende al exterior");
        void IImportante.Importa() => Console.WriteLine("Auto importante");
        public void Importa() => Console.WriteLine("Método Importar() de la clase Auto");

    }

    class Libro : IAlquilable, IReciclable
    {
        public void SeAlquilaA(Persona p) => Console.WriteLine("Alquilado libro a persona");
        public void SeDevuelveA(Persona p) => Console.WriteLine("Libro devuelta por persona");
        public void EsReciclado() => Console.WriteLine("Reciclando libro");
    }

    class Perro : IVendible, IAtendible, ILavable
    {
        public void SeVendeA(Persona p) => Console.WriteLine("Vendiendo perro a persona");
        public void EsAtendido() => Console.WriteLine("Atendiendo perro");

        public void EsLavado() => Console.WriteLine("Lavando perro");
        public void EsSecado() => Console.WriteLine("Secando perro");
    }

    class Pelicula : IAlquilable
    {
        public virtual void SeAlquilaA(Persona p) => Console.WriteLine("Alquilado pelicula a persona");
        public virtual void SeDevuelveA(Persona p) => Console.WriteLine("Pelicula devuelta por persona");
    }

    class PeliculaClasica : Pelicula, IVendible
    {
        //Preguntar si acá va override o que
        public override void SeAlquilaA(Persona p) => Console.WriteLine("Alquilado pelicula clásica a persona");
        public override void SeDevuelveA(Persona p) => Console.WriteLine("Pelicula clásica devuelta por persona");
        public void SeVendeA(Persona p) => Console.WriteLine("Vendiendo película clásica a persona ");
    }
}
