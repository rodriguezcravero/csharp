using System;
using System.Collections;

namespace P5_E3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cuenta c1 = new Cuenta();
            // c1.Depositar(100).Depositar(50).Extraer(120).Extraer(50);
            // Cuenta c2 = new Cuenta();
            // c2.Depositar(200).Depositar(800);
            // new Cuenta().Depositar(20).Extraer(20);
            // c2.Extraer(1000).Extraer(1);
            // Console.WriteLine("\nDETALLE");
            // Cuenta.ImprimirDetalle();

            // new Cuenta();
            // new Cuenta();
            // ArrayList cuentas = Cuenta.GetCuentas();
            // //se recuperó la lista de cuentas creadas
            // (cuentas[0] as Cuenta).Depositar(50);
            // // se depositó 50 en la primera cuenta de la lista devuelta
            // cuentas.RemoveAt(0);
            // Console.WriteLine(cuentas.Count);
            // // se borró un elemento de la lista devuelta
            // // pero la clase Cuenta sigue manteniendo todos
            // // los datos "La cuenta id: 1 tiene 50 de saldo"
            // cuentas = Cuenta.GetCuentas();
            // Console.WriteLine(cuentas.Count);
            // // se recupera nuevamente la lista de cuentas
            // (cuentas[0] as Cuenta).Extraer(30);
            // // se extrae 25 de la cuenta id: 1 que tenía 50 de saldo

            new Cuenta();
            new Cuenta();
            ArrayList cuentas = Cuenta.Cuentas;
            //se recuperó la lista de cuentas creadas
            (cuentas[0] as Cuenta).Depositar(50);
            // se depositó 50 en la primera cuenta de la lista devuelta
            cuentas.RemoveAt(0);
            Console.WriteLine(cuentas.Count);
            // se borró un elemento de la lista devuelta
            // pero la clase Cuenta sigue manteniendo todos
            // los datos "La cuenta id: 1 tiene 50 de saldo"
            cuentas = Cuenta.Cuentas;
            Console.WriteLine(cuentas.Count);
            // se recupera nuevamente la lista de cuentas
            (cuentas[0] as Cuenta).Extraer(30);
            // se extrae 25 de la cuenta id: 1 que tenía 50 de saldo


            Console.ReadKey();
        }
    }

    class Cuenta
    {
        private int _saldo;
        private int _id;
        private static int s_id;
        private static int s_cant_cuentas;
        private static int s_cant_depositos;
        private static int s_cant_extracciones;
        private static int s_total_depositado;
        private static int s_total_extraido;
        private static int s_cant_extracciones_denegadas;
        private static int s_saldo_total;
        private static ArrayList s_arreglo = new ArrayList();
        //Esta es la propiedad que reemplaza a GetCuentas;
        public static ArrayList Cuentas
        {
            get
            {
                ArrayList arr = new ArrayList();
                foreach (Cuenta c in s_arreglo) arr.Add(c);
                return arr;
            }
        }

        public Cuenta()
        {
            //Por cada cuenta creada se incrementa el id, se asigna el id, se aumenta la cantidad de cuentas, y se agrega al ArrayList de cuentas;
            s_id++;
            this._id = s_id;
            s_cant_cuentas++;
            s_arreglo.Add(this);
            Console.WriteLine("Se creó la cuenta Id={0}", _id);
        }

        // public static ArrayList GetCuentas()
        // {
        //     ArrayList arr = new ArrayList();
        //     foreach (Cuenta c in s_arreglo)
        //     {
        //         arr.Add(c);
        //     }
        //     return arr;
        // }

        public Cuenta Depositar(int monto)
        {
            _saldo += monto;
            s_cant_depositos++;
            s_total_depositado += monto;
            s_saldo_total += monto;
            Console.WriteLine($"Se depositó {monto} en la cuenta {this._id} (Saldo= {this._saldo})");
            return this;
        }
        public Cuenta Extraer(int monto)
        {
            if (monto > this._saldo)
            {
                Console.WriteLine("Operación denegada - Saldo insuficiente");
                s_cant_extracciones_denegadas++;
                return this;
            }
            else
            {
                _saldo -= monto;
                s_cant_extracciones++;
                s_total_extraido += monto;
                s_saldo_total -= monto;
                Console.WriteLine($"Se extrajo {monto} en la cuenta {this._id} (Saldo= {this._saldo})");
                return this;
            }
        }

        public static void ImprimirDetalle()
        {
            Console.WriteLine($"CUENTAS CREADAS: {s_cant_cuentas}");
            Console.WriteLine($"DEPOSITOS:\t {s_cant_depositos} \t - Total depositado: {s_total_depositado}");
            Console.WriteLine($"EXTRACCIONES:\t {s_cant_extracciones} \t - Total depositado: {s_total_extraido}");
            Console.WriteLine($"\t\t\t - Saldo: \t {s_saldo_total}");
            Console.WriteLine($"* Se denegaron {s_cant_extracciones_denegadas} extracciones por falta de fondos");

        }
    }

}
