using System;
using System.Collections;
class Persona
{
    public string Nombre { get; set; }
    public char Sexo { get; set; }
    public int DNI { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int Edad { get; }

    public Persona(string nombre, char sexo, int dni, DateTime nacimiento)
    {
        this.Nombre = nombre;
        this.Sexo = sexo;
        this.DNI = dni;
        this.FechaNacimiento = nacimiento;
        this.Edad = CalcularEdad();
    }

    public int CalcularEdad()
    {
        //Hago 2 DateTime, y les resto sus años.
        //Luego, si el dia de hoy es menor al del nacimiento, le resto 1 año.
        DateTime fechaHoy = DateTime.Now;
        DateTime nacimiento = this.FechaNacimiento;
        int edad = fechaHoy.Year - nacimiento.Year;
        if (nacimiento.Date > fechaHoy.AddYears(-edad)) edad--;
        return edad;
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
            //No puedo usar el 'ternary operator' porque no hay forma de retornar sin valor.
            if (i == 0) this.Nombre = (string)value;
            else if (i == 1) this.Sexo = (char)value;
            else if (i == 2) this.DNI = (int)value;
            else if (i == 3) this.FechaNacimiento = (DateTime)value;
            else return;
        }
    }

    public void Imprimir()
    {
        Console.WriteLine($"Nombre: {Nombre}, DNI: {DNI}, Edad: {Edad}, nacido el {FechaNacimiento.ToString("dd/MM/yyyy")}");
    }

}