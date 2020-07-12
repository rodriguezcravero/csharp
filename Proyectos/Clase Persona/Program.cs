/*7) Definir la clase Persona con las siguientes propiedades de lectura y escritura: Nombre de tipo
string, Sexo de tipo char, DNI de tipo int, y FechaNacimiento de tipo DateTime. Además
definir una propiedad de sólo lectura (calculada) Edad de tipo int. Definir un indizador de
lectura/escritura que permita acceder a las propiedades a través de un índice entero. Así, si p es un
objeto Persona, con p[0] se accede al nombre, p[1] al sexo p[2] al DNI, p[3] a la fecha de
nacimiento y p[4] a la edad. En caso de asignar p[4] simplemente el valor es descartado. Observar
que el tipo del indizador debe ser capaz almacenar valores de tipo int, char, DateTime y string.
*/
using System;
using System.Collections;

namespace P5_E7
{
    class Program
    {
        static void Main(string[] args)
        {
            Persona pepe = new Persona("Pepe", 'm', 30123321, new DateTime(1985, 09, 11));
            Console.WriteLine($"Nombre: {pepe[0]}");
            pepe[0] = "Pedro";
            Console.WriteLine($"Nombre: {pepe[0]}");

            pepe.Imprimir();

            Console.ReadKey();
        }
    }

    class Persona
    {
        public string Nombre { get; set; }
        public char Sexo { get; set; }
        public int DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad
        {
            get
            {
                //Calculo una cantidad de años, entre la fecha actual y la de nacimiento.
                //Luego, si el dia de hoy es menor al del nacimiento, le resto 1 año.
                int edad = DateTime.Now.Year - FechaNacimiento.Year;
                if (FechaNacimiento.Date > DateTime.Now.AddYears(-edad)) edad--;
                return edad;
            }
        }

        public Persona(string nombre, char sexo, int dni, DateTime nacimiento)
        {
            this.Nombre = nombre;
            this.Sexo = sexo;
            this.DNI = dni;
            this.FechaNacimiento = nacimiento;
        }

        public object this[int i]
        {
            get
            {
                //Puedo usar el 'ternary operator' si casteo el null
                return i == 0 ? Nombre : i == 1 ? Sexo : i == 2 ? DNI : i == 3 ? FechaNacimiento : i == 4 ? Edad : (object)null;
            }
            set
            {
                //No puedo usar el 'ternary operator' porque no tengo un else final.
                if (i == 0) this.Nombre = (string)value;
                else if (i == 1) this.Sexo = (char)value;
                else if (i == 2) this.DNI = (int)value;
                else if (i == 3) this.FechaNacimiento = (DateTime)value;
            }
        }

        public void Imprimir()
        {
            Console.WriteLine($"Nombre: {Nombre}, DNI: {DNI}, Edad: {Edad}, nacido el {FechaNacimiento.ToString("dd/MM/yyyy")}");
        }

    }
}
