using System.Linq.Expressions;
using System.Numerics;

namespace Primer_Obligatorio_2022
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            int [,] quinielas = new int[3, 20];
            int option = 0;
            while (option != 5)
            {
                option = Menu();
                switch (option)
                {
                    case 1:
                        {
                            AgregarSorteo(quinielas);
                            Console.Clear();
                            break;
                        }
                    case 2:
                        {
                            ModificarPremioExistente(quinielas);
                            break;
                        }
                    case 3:
                        {
                            MostrarUnicoSorteo(quinielas);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                                
                        }
                    case 4:
                        {
                            MostrarSorteos(quinielas);
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                }
            }
            Console.WriteLine("Gracias por usar nuestra aplicación...");
            Console.ReadLine();
        }

        static int Dias()
        {
            DateTime dia = DateTime.Today;
            int mes = dia.Month;
            int anio = dia.Year;
            return DateTime.DaysInMonth(anio, mes);
        }

        static int Menu()
        {

            int option = 0;
            bool error = true;
            while (error)
            {
       
                Console.Write("\n\t\t** Binvenido al menú **\n");

                Console.WriteLine("\n\n 1- Agregar sorteo");
                Console.WriteLine("\n 2- Modificar un número de un sorteo");
                Console.WriteLine("\n 3- Mostrar un sorteo");
                Console.WriteLine("\n 4- Mostrar todos los sorteos");
                Console.WriteLine("\n 5- Salir");
    
                try
                {
                    Console.Write("\n Ingrese la opción deseada: ");
                    option = Convert.ToInt32(Console.ReadLine());
                    error = false;

                }
                catch
                {
                    Console.WriteLine("\n\n El dato ingresado no es correcto, presione enter para continuar...");
                    Console.ReadLine();
                    Console.Clear();

                }
            }
            Console.Clear();
            return option;
            
        }

        static void AgregarSorteo(int[,] matriz)
        {


            bool fechaNoAgregada = true;
            int diaAgregar = 0;
            while (fechaNoAgregada)
            {
                Console.WriteLine("\n\t **  Agregar sorteo  **\n");
                try
                {
                    Console.Write("Ingrese para que día desea agregar un sorteo: ");
                    diaAgregar = Convert.ToInt32(Console.ReadLine());
                    if (diaAgregar > matriz.GetLength(0))
                    {
                        Console.WriteLine("El dia ingresado no existe para este mes");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        bool fechaExistente = FechaIngresada(matriz, diaAgregar);
                        if (!fechaExistente)
                            fechaNoAgregada = false;
                        else
                            Console.Write("Ya se ha ingresado el sorteo para esta fecha\n");
                    }

                    for (int j = 0; j < matriz.GetLength(1); j++)
                    {
                        try
                        {
                            Console.Write("Ingrese numero para el " + (j + 1) + "° premio: ");
                            int premio = Convert.ToInt32(Console.ReadLine());
                            bool duplicado = PremioDuplicado(premio, diaAgregar, matriz);

                            if (!(premio < 999 && premio > 0))
                            {
                                Console.WriteLine("\n\nEl premio ingresado no puede ser menor que 0 ni mayor a 999");
                                j--;
                            }
                            else if (duplicado)
                            {
                                Console.WriteLine("\nEl premio ingresado ya existe para este día\n");
                                j--;
                            }
                            else
                            {
                                matriz[diaAgregar - 1, j] = premio;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("\nLo ingresado no tiene formato númerico");
                            Console.ReadLine();
                            j--;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Lo ingresado no tiene formato númerico");
                    Console.ReadLine();
                    Console.Clear();
                }


            }


            //for (int j = 0; j < matriz.GetLength(1); j++)
            //{
            //    Console.Write("Ingrese numero para el " + (j + 1) + "° premio: ");
            //    int premio = Convert.ToInt32(Console.ReadLine());
            //    bool duplicado = PremioDuplicado(premio, diaAgregar, matriz);

            //    if (!(premio < 999 && premio > 0))
            //    {
            //        Console.WriteLine("\n\nEl premio ingresado no puede ser menor que 0 ni mayor a 999");
            //        j--;
            //    }
            //    else if(duplicado)
            //    {
            //        Console.WriteLine("\nEl premio ingresado ya existe para este día\n");
            //        j--;
            //    }
            //    else
            //    {
            //        matriz[diaAgregar - 1, j] = premio;
            //    }
            //}
        }

        static bool PremioDuplicado(int premio, int dia, int[,] matriz)
        {
            int resultado = 0;
            for (int i = 0; i < matriz.GetLength(1); i++)
            {
                if(premio == matriz[dia-1, i])
                {
                    resultado++;
                }
            }

            if (resultado != 0)
            {
                return true;
            }
            else
                return false;
        }

        static bool FechaIngresada(int[,] matriz, int dia)
        {
            int resultado = 0;

            for (int i = 0; i < matriz.GetLength(1); i++)
            {
                if (matriz[dia - 1, i] == 0)
                    resultado++;
            }

            if (resultado == 0)
                return true;
            else
                return false;          
        }

        static void ModificarPremioExistente(int[,] matriz)
        {
           
            try
            {
                Console.Write("Ingrese que día desea modificar: ");
                int dia = Convert.ToInt32(Console.ReadLine());
                bool existeYaSorteo = FechaIngresada(matriz, dia);
                if (existeYaSorteo)
                {
                    Console.Write("Ingrese que posicion desea modificar: ");
                    int posicion = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\n\nIngrese el nuevo número del premio: ");
                    int premio = Convert.ToInt32(Console.ReadLine());

                    matriz[dia-1,posicion-1] = premio;
                }
                else
                {
                    Console.WriteLine("Aun no se ha agregado el sorteo para este día"); 
                }
            }
            catch 
            {
                Console.Write("\nLo ingresado no tiene formato númerico");
            }
        }

        static void MostrarSorteos(int[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                Console.Write("Día " + (i + 1) + ": ");
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void MostrarUnicoSorteo(int[,] matriz)
        {
            Console.Write("Ingrese la fecha del sorteo que desea mostrar: ");
            try
            {
                int dia = Convert.ToInt32(Console.ReadLine());
                bool diaValido = FechaIngresada(matriz, dia);
                if (diaValido)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = i; j < matriz.GetLength(1);)
                        {
                            Console.Write("Posición " + (j + 1) + ": " + matriz[dia-1, j] + "\t\t");
                            j += 5; 
                        }
                        Console.WriteLine();
                    }   
                }
                else
                {
                    Console.Write("Aun no se ha agregado el sorteo para este día");
                }
            }
            catch 
            {

                
            }

        }
    }
}
