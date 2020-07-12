/*8) Crear un programa para gestionar empleados en una empresa. Los empleados deben tener las
propiedades públicas de sólo lectura Nombre, DNI, FechaDeIngreso, SalarioBase y Salario. Los
valores de estas propiedades (a excepción de Salario que es una propiedad calculada) deben
establecerse por medio de un constructor adecuado.
Existen dos tipos de empleados: Administrativo y Vendedor. No se podrán crear objetos de la
clase padre Empleado, pero sí de sus clases hijas (Administrativo y Vendedor). Aparte de las
propiedades de solo lectura mencionadas, el administrativo tiene otra propiedad pública de
lectura/escritura llamada Premio y el vendedor tiene otra propiedad pública de lectura/escritura
llamada Comision.
La propiedad de solo lectura Salario, se calcula como el salario base más la comisión o el premio
según corresponda.
Las clases tendrán además un método público llamado AumentarSalario() que tendrá una
implementación distinta en cada clase. En el caso del administrativo se incrementará el salario base
en un 1% por cada año de antigüedad que posea en la empresa, en el caso del vendedor se
incrementará el salario base en un 5% si su antigüedad es inferior a 10 años o en un 10% en caso
contrario.*/
using System;

namespace P6_E8
{
    class Program
    {
        static void Main(string[] args)
        {

            Empleado[] empleados = new Empleado[] {
            new Administrativo("Ana", 20000000, DateTime.Parse("26/4/2018"), 10000) {Premio=1000},
            new Vendedor("Diego", 30000000, DateTime.Parse("2/4/2010"), 10000) {Comision=2000},
            new Vendedor("Luis", 33333333, DateTime.Parse("30/12/2011"), 10000) {Comision=2000}
            };
            foreach (Empleado e in empleados)
            {
                Console.WriteLine(e);
                e.AumentarSalario();
                Console.WriteLine(e);
            }



            Console.ReadKey();
        }
    }

    abstract class Empleado
    {
        public string Nombre { get; }
        public int DNI { get; }
        public DateTime FechaDeIngreso { get; }
        //En la consigna se aclara que SalarioBase debe tener un set(protegido en este caso), porque lo utilizarán las derivadas
        public int SalarioBase { get; protected set; }
        public abstract int Salario { get; }

        //Como las propiedades de Empleado son de sólo lectura, sólo pueden ser asignadas en este constructor
        public Empleado(string nombre, int dni, DateTime fechaIngreso, int salarioBase)
        {
            this.Nombre = nombre;
            this.DNI = dni;
            this.FechaDeIngreso = fechaIngreso;
            this.SalarioBase = salarioBase;
        }

        public abstract void AumentarSalario();

        public override string ToString() => $"Nombre: {Nombre}, DNI: {DNI}, Antigüedad: {Antiguedad} \nSalario base: {SalarioBase}, Salario: {Salario}\n-------------";

        /***DECLARACION EXTRA: Propiedad Antiguedad común a todos los derivados***/
        public int Antiguedad
        {
            get
            {
                int años = DateTime.Now.Year - FechaDeIngreso.Year;
                if (DateTime.Now.AddYears(-años) < FechaDeIngreso.Date) años--;
                return años;
            }
        }
        //La antigüedad se calcula con los años, restando el actual con el de ingreso
        //Luego se restan los años a la fecha actual, digamos 10/05/2020 - 8 = 10/05/2012 y se compara con la fecha de ingreso
        //Si el dia y mes actuales son menores, se resta un año.
    }

    class Administrativo : Empleado
    {
        public int Premio { get; set; }
        //Se debe utilizar override en los miembros estáticos heredados
        public override int Salario => this.SalarioBase + this.Premio;

        public Administrativo(string nombre, int dni, DateTime fechaIngreso, int salarioBase)
        : base(nombre, dni, fechaIngreso, salarioBase) { }

        //AumentatSalario suma al base más la parte entera del base por el 0.0N que den los años, por ej, 10000 * 0.05 = 500;
        public override void AumentarSalario() => SalarioBase += ((Antiguedad * SalarioBase) / 100);

        //También podría haber hecho un ToString en la base y llamarlo desde aquí?
        public override string ToString() => $"Administrativo " + base.ToString();

    }

    class Vendedor : Empleado
    {
        public int Comision { get; set; }
        public override int Salario => this.SalarioBase + this.Comision;

        public Vendedor(string nombre, int dni, DateTime fechaIngreso, int salarioBase)
        : base(nombre, dni, fechaIngreso, salarioBase) { }

        public override void AumentarSalario() => SalarioBase += Antiguedad < 10 ? ((5 * SalarioBase) / 100) : ((10 * SalarioBase) / 100);

        public override string ToString() => $"Vendedor " + base.ToString();

    }
}