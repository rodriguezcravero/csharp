/*8) Implementar la clase Matriz que se utilizará para trabajar con matrices matemáticas.
Implementar los dos constructores y todos los métodos que se detallan a continuación:
public Matriz(int filas, int columnas)
public Matriz(double[,] matriz)
public void SetElemento(int fila, int columna, double elemento)
public double GetElemento(int fila, int columna)
public void imprimir()
public void imprimir(string formatString)
public double[] GetFila(int fila)
public double[] GetColumna(int columna)
public double[] GetDiagonalPrincipal()
public double[] GetDiagonalSecundaria()
public double[][] getArregloDeArreglo()
public void sumarle(Matriz m)
public void restarle(Matriz m)
public void multiplicarPor(Matriz m)
*/
using System;
using System.Collections;

namespace P4_E8
{
    class Program
    {
        static void Main(string[] args)
        {
            Matriz A = new Matriz(2, 3);
            for (int i = 0; i < 6; i++) A.SetElemento(i / 3, i % 3, (i + 1) / 3.0);
            Console.WriteLine("Impresion de la matriz A");
            A.Imprimir("0.000");

            double[,] aux = new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matriz B = new Matriz(aux);
            Console.WriteLine("\nImpresión de la matriz B");
            B.Imprimir();

            Console.WriteLine("\nAcceso al elemento B[1,2]= {0}", B.GetElemento(1, 2));

            Console.Write("\nfila 1 de A: ");
            foreach (double d in A.GetFila(1)) Console.Write("{0:0.0} ", d);

            Console.Write("\n\nColumna 0 de B: ");
            foreach (double d in B.GetColumna(0)) Console.Write("{0} ", d);

            Console.Write("\n\nDiagonal principal de B: ");
            foreach (double d in B.GetDiagonalPrincipal()) Console.Write("{0} ", d);

            Console.Write("\n\nDiagonal secundaria de B: ");
            foreach (double d in B.GetDiagonalSecundaria()) Console.Write("{0} ", d);

            A.multiplicarPor(B);
            Console.WriteLine("\n\nA multiplicada por B");
            A.Imprimir();

            Console.ReadKey();
        }
    }

    class Matriz
    {
        double[,] matriz;
        int cantFilas;
        int cantColumnas;

        //2 constructores: uno recibe dos enteros para filas y columnas, el otro recibe una matriz.
        public Matriz(int filas, int columnas)
        {
            this.matriz = new double[filas, columnas];
            this.cantFilas = filas;
            this.cantColumnas = columnas;
        }

        //En este constructor, el numero de cantidad de filas y columnas, lo obtengo con el método GetLength.
        public Matriz(double[,] matriz)
        {
            this.matriz = matriz;
            this.cantFilas = matriz.GetLength(0);
            this.cantColumnas = matriz.GetLength(1);
        }

        public int GetCantFilas() => cantFilas;

        public int GetCantColumnas() => cantColumnas;

        public void SetElemento(int fila, int columna, double elemento) => this.matriz[fila, columna] = elemento;

        public double GetElemento(int fila, int columna) => this.matriz[fila, columna];

        public void Imprimir()
        {
            for (int i = 0; i < cantFilas; i++)
            {
                for (int j = 0; j < cantColumnas; j++) Console.Write($"{matriz[i, j]} ");
                Console.WriteLine();
            }
        }

        //Método Imprimir sobrecargado, cuando recibe un string con el formato, en el for anidado se hace un nuevo string, con el dato numérico de la matriz y el formato, y se imprimir en pantalla con la cantidad de decimales indicados.
        public void Imprimir(string formatString)
        {
            for (int i = 0; i < cantFilas; i++)
            {
                for (int j = 0; j < cantColumnas; j++)
                {
                    string datoFormateado = string.Format(this.matriz[i, j].ToString(formatString));
                    Console.Write($"{datoFormateado} ");
                }
                Console.WriteLine();
            }
        }

        public double[] GetFila(int fila)
        {
            double[] arr = new double[cantColumnas];

            //Supongamos que quiero toda la fila 2.
            //1) Hago un for de 0 a N, siendo N el Length de columnas;
            //2) Obtengo la pos de esa columna, fila Nº = argumento.
            for (int j = 0; j < cantColumnas; j++) arr[j] = this.matriz[fila, j];

            return arr;
        }

        public double[] GetColumna(int columna)
        {
            double[] arr = new double[cantFilas];

            //Lo mismo que en GetFila.
            for (int j = 0; j < cantFilas; j++) arr[j] = this.matriz[j, columna];

            return arr;
        }

        public double[] GetDiagonalPrincipal()
        {
            double[] vacio = null;

            //Hago un try/catch con una función propia de GetDiagonal, porque puede ser que la matriz no sea compatible con tener una diagonal principal
            try
            {
                return funcion(this.matriz);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return vacio;
            }

            static double[] funcion(double[,] matriz)
            {
                int filas = matriz.GetLength(0);
                int columnas = matriz.GetLength(1);

                if (filas != columnas) throw new ArgumentException("\n La matriz no es cuadrada \n");

                double[] diagonalPrincipal = new double[filas];

                for (int i = 0; i < filas; i++) diagonalPrincipal[i] = matriz[i, i];

                return diagonalPrincipal;
            }
        }

        public double[] GetDiagonalSecundaria()
        {
            double[] vacio = null;
            //Lo mismo que en la diagonal principal.
            try
            {
                return funcion2(this.matriz);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return vacio;
            }

            static double[] funcion2(double[,] matriz)
            {
                int filas = matriz.GetLength(0);
                int columnas = matriz.GetLength(1);

                if (filas != columnas) throw new ArgumentException("\n La matriz no es cuadrada \n");

                double[] diagonalSecundaria = new double[columnas];
                int pos = 0;

                //Si se tratase de una matriz 3x3, por ejemplo, este for hace lo siguiente:
                //1) Iguala i a la cantidad de columnas, en este caso 3 (pero al ser zero based, debe ser 2);
                //2) Pos es una variable inicializada en 0;
                //3) Obtengo 0-2, 1-1, 2,0;
                for (int i = columnas - 1; i >= 0; i--) diagonalSecundaria[pos] = matriz[pos++, i];

                return diagonalSecundaria;
            }
        }

        public double[][] GetArregloDeArreglo()
        {
            double[][] arr = new double[cantFilas][];

            for (int i = 0; i < cantFilas; i++)
            {
                //Por cada nueva iteración, agrego un nuevo arreglo a mi primer arreglo.
                arr[i] = new double[cantColumnas];

                for (int j = 0; j < cantColumnas; j++)
                {
                    arr[i][j] = this.matriz[i, j];
                }
            }

            return arr;
        }

        public void sumarle(Matriz m)
        {
            int filasA = cantFilas;
            int columnasA = cantColumnas;
            int filasB = m.GetCantFilas();
            int columnasB = m.GetCantColumnas();

            //En los casos de Suma-Resta-Multiplicacion, chequeo que filas y columnas sean compatibles.
            if ((filasA != filasB) || (columnasA != columnasB)) Console.WriteLine("Matrices incompatibles de sumar.");
            else
            {
                for (int i = 0; i < filasA; i++)
                {
                    for (int j = 0; j < columnasA; j++)
                    {
                        this.matriz[i, j] = this.matriz[i, j] + m.GetElemento(i, j);
                    }
                }
            }
        }

        public void restarle(Matriz m)
        {
            int filasA = cantFilas;
            int columnasA = cantColumnas;
            int filasB = m.GetCantFilas();
            int columnasB = m.GetCantColumnas();


            if ((filasA != filasB) && (columnasA != columnasB))
            {
                Console.WriteLine("Matrices incompatibles de restar.");
            }
            else
            {
                for (int i = 0; i < filasA; i++)
                {
                    for (int j = 0; j < columnasA; j++)
                    {
                        this.matriz[i, j] = this.matriz[i, j] - m.GetElemento(i, j);
                    }
                }
            }
        }

        public void multiplicarPor(Matriz m)
        {
            int filasA = cantFilas;
            int columnasA = cantColumnas;
            int filasB = m.GetCantFilas();
            int columnasB = m.GetCantColumnas();

            if (columnasA != filasB)
            {
                Console.WriteLine("La matrices no son compatibles para multiplicar.");
            }

            //Al multiplicarse 2 matrices, la nueva tiene que tener las filas de la primera y las columnas de la segunda;
            double[,] matrizRespuesta = new double[filasA, columnasB];

            //Se harán 3 iteraciones:
            //1) La cantidad de filas que tenga la primer matriz, supongamos 3;
            //2) Por cada fila de la primera matriz, se itera la cantidad de columnas de la segunda, supongamos 2;
            //3) Por cada una de las 6 iteraciones, se van a multiplicar la cantidad de veces, cuantas columnas tenga la primer matriz (mismo numero que filas de la segunda)
            //4) Se acumulan estas multilplicaciones, en este ejemplo:
            //5) 1º iteracion: (0-0*0-0) + (0-1*1-0) + (0-2*2-0)
            //6) 2º iteracion: (1-0*-0-0) + (1-1*1-0) + (1-2*2-0) y así...
            for (int i = 0; i < filasA; i++)
            {
                for (int j = 0; j < columnasB; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < columnasA; k++)
                    {
                        temp += this.matriz[i, k] * m.GetElemento(k, j);
                    }
                    matrizRespuesta[i, j] = temp;
                }
            }
            this.matriz = matrizRespuesta;
        }
    }
}
