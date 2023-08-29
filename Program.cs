using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace lab1EDII_CésarSilva
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int opcion;
                string ruta;
                Console.WriteLine("Ingrese opcion a realizar");
                Console.WriteLine("1. Operaciones a travez de CSV");
                Console.WriteLine("2. Buscar un dato");
                Console.WriteLine("3. Salir");
                opcion = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Inserte ruta absoluta del archivo");
                        ruta = Console.ReadLine();
                        string[] archivo = File.ReadAllLines(ruta);
                        string[,] jason = new string[1000, 1000];
                        foreach (var linea in archivo)
                        {
                            var valores = linea.Split(';');
                            string operacion = valores[0];
                            Console.WriteLine(valores[0]);
                            Console.WriteLine(valores[1]);
                            Console.ReadLine();

                        }
                        break;
                    case 2:
                        Console.WriteLine("Que desea buscar");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Adios");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Opcion invalida");
                        break;
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Inserte una opcion valida");
                Console.ReadLine();
            }
        }
    }
}
