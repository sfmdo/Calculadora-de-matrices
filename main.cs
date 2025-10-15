using System;
using Matriz;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("===============================");
        Console.WriteLine("  CALCULADORA DE OPERACIONES ");
        Console.WriteLine("       CON MATRICES (10x10)");
        Console.WriteLine("===============================");
        
        bool continuar = true;
        while (continuar)
        {
            int operacion;
            Matrix matrizA = new Matrix();
            Matrix matrizB = new Matrix();
            bool esSumaRestaValida = false;
            bool esMultiplicacionValida = false;

            Console.WriteLine("\n--- Menú Principal ---");
            Console.WriteLine("1. Suma o Resta");
            Console.WriteLine("2. Multiplicación");
            Console.WriteLine("3. Ambas (Suma/Resta y Multiplicación)");
            Console.WriteLine("4. Salir");
            Console.Write("Opción: ");

            if (!int.TryParse(Console.ReadLine(), out operacion) || operacion < 1 || operacion > 4)
            {
                Console.WriteLine("\n[ERROR] Opción no válida. Por favor, ingrese un número del 1 al 4.");
                continue;
            }
            
            if (operacion == 4)
            {
                continuar = false;
                Console.WriteLine("\nGracias por usar la calculadora. ¡Hasta luego!");
                break;
            }

            bool dimensionesValidas = false;
            while (!dimensionesValidas)
            {
                matrizA.ObtenerColumnasyFilas("A"); 
                matrizB.ObtenerColumnasyFilas("B"); 
                
                esSumaRestaValida = matrizA.VerificarSuma(matrizB);
                esMultiplicacionValida = matrizA.VerificarMultiplicacion(matrizB);

                dimensionesValidas = true;

                if ((operacion == 1 || operacion == 3) && !esSumaRestaValida)
                {
                    Console.WriteLine("\n[ADVERTENCIA] La Suma/Resta requiere que ambas matrices sean de la misma dimensión (Filas de A = Filas de B, Columnas de A = Columnas de B).");
                    dimensionesValidas = false;
                }

                if ((operacion == 2 || operacion == 3) && !esMultiplicacionValida)
                {
                    Console.WriteLine("\n[ADVERTENCIA] La Multiplicación requiere que las Columnas de A sean iguales a las Filas de B.");
                    dimensionesValidas = false;
                }
                
                if (!dimensionesValidas)
                {
                    Console.WriteLine("\nDimensiones incompatibles para la operación seleccionada. Vuelva a ingresar las dimensiones.");
                }
            }
            
            Console.WriteLine("\n--- Ingrese los valores de las matrices ---");
            matrizA.LlenarMatriz("A");
            matrizB.LlenarMatriz("B");

            matrizA.Imprimir("A");
            matrizB.Imprimir("B");
            
            if (operacion == 1 || (operacion == 3 && esSumaRestaValida))
            {
                char signo;
                do 
                {
                    Console.Write("\n¿Desea Sumar (+) o Restar (-) las matrices? (Escriba + o -): ");
                    signo = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    if (signo != '+' && signo != '-')
                    {
                        Console.WriteLine("[ERROR] Carácter no válido. Por favor, ingrese '+' para Sumar o '-' para Restar.");
                    }
                } while (signo != '+' && signo != '-');
                Matrix? matrizResultado = matrizA.SumaOResta(matrizB, signo);
                if (matrizResultado != null)
                {
                    matrizResultado.Imprimir($"Resultado A {signo} B");
                }
            }

            if (operacion == 2 || (operacion == 3 && esMultiplicacionValida))
            {
                Matrix? matrizResultadoMultiplicacion = matrizA.Multiplicacion(matrizB);
                if (matrizResultadoMultiplicacion != null)
                {
                    matrizResultadoMultiplicacion.Imprimir("Resultado A * B");
                }
            }

            Console.WriteLine("\n\nPresione Enter para volver al menú...");
            Console.ReadLine();
        }
    }
}
