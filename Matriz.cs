using System;
using System.Security.Cryptography.X509Certificates;

namespace Matriz
{
    public class Matrix
    {
        private int numColumnas;
        private int numFilas;
        private readonly int longMax = 10; 
        private int[,]? matrizDatos;
        
        public int[,]? MatrizDatos => matrizDatos;
        public int NumFilas => numFilas;
        public int NumColumnas => numColumnas;

        public Matrix() { matrizDatos = null; }

        public Matrix(int filas, int columnas, int[,] datos)
        {
            numFilas = filas;
            numColumnas = columnas;
            matrizDatos = datos;
        }
        
        public int ObtenerDato(bool aplicarLimite = true)
        {
            int numero = 0;

            while (true)
            {
                Console.Write(aplicarLimite ? $"Ingrese valor (1-{longMax}): " : "Ingrese valor: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out int tempNumero))
                {
                    if (aplicarLimite)
                    {
                        if (tempNumero > 0 && tempNumero <= longMax)
                        {
                            numero = tempNumero;
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"El número debe de estar entre 1 y {longMax}");
                        }
                    }
                    else
                    {
                        numero = tempNumero;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Por favor, ingresa un número entero.");
                }
            }
            return numero;
        }

        public void ObtenerColumnasyFilas(string nombreMatriz)
        {
            Console.WriteLine($"\n--- Dimensiones de Matriz {nombreMatriz} (Máx: {longMax}x{longMax}) ---");
            Console.Write("Ingresa las Filas: ");
            numFilas = ObtenerDato();

            Console.Write("Ingresa las Columnas: ");
            numColumnas = ObtenerDato();
        }

        public bool VerificarSuma(Matrix matrizB)
        {
            return matrizB.numColumnas == numColumnas && matrizB.numFilas == numFilas;
        }

        public bool VerificarMultiplicacion(Matrix matrizB)
        {
            return numColumnas == matrizB.numFilas;
        }

        public void LlenarMatriz(string nombreMatriz)
        {
            int[,] nuevaMatriz = new int[numFilas, numColumnas];

            Console.WriteLine($"\n--- Llenando Matriz {nombreMatriz} ({numFilas}x{numColumnas}) ---");

            for (int i = 0; i < numFilas; i++)
            {
                for (int j = 0; j < numColumnas; j++)
                {
                    Console.Write($"Matriz {nombreMatriz}[{i + 1}][{j + 1}]: ");
                    nuevaMatriz[i, j] = ObtenerDato(false); 
                }
            }

            matrizDatos = nuevaMatriz;
        }

        public void Imprimir(string nombre = "Resultado") {
            if (matrizDatos == null) return;
            
            Console.WriteLine($"\n--- Matriz {nombre} ({numFilas}x{numColumnas}) ---");
            for (int i = 0; i < numFilas; i++) {
                string fila = "[ ";
                for (int j = 0; j < numColumnas; j++) {
                    fila += $"{matrizDatos[i, j]}";
                    if (j < numColumnas - 1)
                    {
                        fila += ", ";
                    }
                }
                fila += " ]";
                Console.WriteLine(fila);
            }
        }

        public Matrix? SumaOResta(Matrix matrizB, char signo = '+')
        {
            if (matrizDatos == null || matrizB.MatrizDatos == null) return null;
            int[,] matrizNueva = new int[numFilas, numColumnas];
            int[,] datosB = matrizB.MatrizDatos;

            for (int i = 0; i < numFilas; i++)
            {
                for (int j = 0; j < numColumnas; j++)
                {
                    if (signo == '+')
                    {
                        matrizNueva[i, j] = matrizDatos[i, j] + datosB[i, j];
                    }
                    else if (signo == '-')
                    {
                        matrizNueva[i, j] = matrizDatos[i, j] - datosB[i, j];
                    }
                }
            }

            return new Matrix(numFilas, numColumnas, matrizNueva);
        }

        public Matrix? Multiplicacion(Matrix B) {
            if (matrizDatos == null || B.MatrizDatos == null) return null;
            
            int[,] matrizResultante = new int[numFilas, B.NumColumnas];

            for (int i = 0; i < numFilas; i++)
            {
                for (int j = 0; j < B.NumColumnas; j++)
                {
                    int sumaProducto = 0; 
                    
                    for (int k = 0; k < numColumnas; k++) 
                    {
                        sumaProducto += matrizDatos[i, k] * B.MatrizDatos[k, j];
                    }
                    
                    matrizResultante[i, j] = sumaProducto;
                }
            }
            return new Matrix(numFilas, B.NumColumnas, matrizResultante);
        }
    }
}
