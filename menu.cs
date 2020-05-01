using System;
using System.Collections.Generic;

namespace Autobus
{
    class Menu
    {
        List<int> mainMenuOptions = new List<int>(new int[] { 1, 2, 3, 4, 5, 9 });
        List<char> rutas = new List<char>(new char[] { 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Ñ', 'Z', 'X', 'C', 'V', 'B', 'N', 'M' });

        private const int MAIN_MENU_EXIT_OPTION = 9;
        int paseDeAutobuses = 0;

        string vecesTransitada = "";
        Dictionary<char, int> countByChar = new Dictionary<char, int>();

        private List<Autobuss> elements = new List<Autobuss>();


        private void DisplayWelcomeMessage()
        {
            System.Console.WriteLine("Bienvenid@, por favor elija una opción\n");
        }

        private void DisplayMainMenuOptions()
        {
            System.Console.WriteLine("1) Ingreso de camión");
            System.Console.WriteLine("2) Salida de camión");
            System.Console.WriteLine("3) Exportar / Imprimir camiones");
            System.Console.WriteLine("4) Ver estadísticas");
            System.Console.WriteLine("5) Borrar información");
            System.Console.WriteLine();
            System.Console.WriteLine("9) Salir");
        }
        private void DisplayByeMessage()
        {
            System.Console.WriteLine("Adiós");
        }

        private int RequestOption(List<int> validOptions)
        {
            int userInputAsInt = 0;
            bool isUserInputValid = false;

            //Mientras no haya una respuesta válida
            while (!isUserInputValid)
            {
                System.Console.WriteLine("Selecciona una opción:");
                string userInput = System.Console.ReadLine();


                try
                {
                    userInputAsInt = Convert.ToInt32(userInput);
                    isUserInputValid = validOptions.Contains(userInputAsInt);
                }
                catch (System.Exception)
                {
                    isUserInputValid = false;
                }


                if (!isUserInputValid)
                {
                    System.Console.WriteLine("La opción seleccionada no es válida.");
                }
            }

            return userInputAsInt;
        }



        private char RutaValida(List<char> okOptions)
        {
            char userInputAsChar = ' ';
            bool isUserInputValid = false;

            //Mientras no haya una respuesta válida
            while (!isUserInputValid)
            {
                System.Console.WriteLine("Inserta una ruta");
                string seleccion = System.Console.ReadLine();


                try
                {
                    userInputAsChar = Convert.ToChar(seleccion);
                    isUserInputValid = okOptions.Contains(userInputAsChar);
                }
                catch (System.Exception)
                {
                    isUserInputValid = false;
                }


                if (!isUserInputValid)
                {
                    System.Console.WriteLine("Ruta invalida o inexistente");
                }
            }
            vecesTransitada = vecesTransitada + userInputAsChar;
            return userInputAsChar;
        }


        public void IngresoDeCamion()
        {
            System.Console.WriteLine("Inserte el nombre del conductor");
            string nombreIngresar = Convert.ToString(Console.ReadLine());
            foreach (var caracter in nombreIngresar)
            {
                if (caracter == ' ')
                {
                    System.Console.WriteLine("Solo nombre porfavor");
                    return;
                }
            }


            char rutaIngresar = RutaValida(rutas);


            elements.Add(new Autobuss(nombreIngresar, rutaIngresar));
            paseDeAutobuses = paseDeAutobuses + 1;
            System.Console.WriteLine($"Ingresado {nombreIngresar} con la ruta {rutaIngresar}\n");
        }

        public void SalidaDeCamion()
        {
            if (elements.Count <= 0)
            {
                System.Console.WriteLine("No hay camiones para salir");
            }
            else
            {
                System.Console.WriteLine($"Ha salido el conductor {elements[0].Nombre()} por la ruta {elements[0].Ruta()}\n");
                elements.RemoveAt(0);

            }
        }

        public void ExportarImprimir()
        {
            if (elements.Count <= 0)
            {
                System.Console.WriteLine("No hay Autobuses en la estación");
            }
            else
            {
                System.Console.WriteLine("Autobuses dentro de la estación:\n");
                for (int i = elements.Count - 1; i >= 0; i--)
                {

                    System.Console.WriteLine($"Autobus #{i + 1}: {elements[i].Nombre()} por la ruta {elements[i].Ruta()}\n");
                }
            }
        }

        public void Estadisticas()
        {
            System.Console.WriteLine($"Ingreso total de Autobuses : {paseDeAutobuses} ");

            foreach (var caracter in vecesTransitada)
            {
                if (countByChar.ContainsKey(caracter))
                {
                    countByChar.TryGetValue(caracter, out int currentCount);

                    int nextCount = currentCount + 1;

                    countByChar.Remove(caracter);
                    countByChar.Add(caracter, nextCount);
                }
                else
                {
                    countByChar.Add(caracter, 1);
                }
            }

            System.Console.WriteLine("Veces Transitada :");
            foreach (var item in countByChar)
            {
                System.Console.WriteLine($"{item.Key} -> {item.Value}");
            }

            foreach (var item in countByChar.Keys)
            {
                countByChar.Remove(item);
            }

        }

        public void Display()
        {
            int selectedOption = 0;

            DisplayWelcomeMessage();

            while (selectedOption != MAIN_MENU_EXIT_OPTION)
            {
                DisplayMainMenuOptions();

                selectedOption = RequestOption(mainMenuOptions);

                switch (selectedOption)
                {
                    case 1:
                        IngresoDeCamion();
                        break;

                    case 2:
                        SalidaDeCamion();
                        break;

                    case 3:
                        ExportarImprimir();
                        break;

                    case 4:
                        Estadisticas();
                        break;
                }
            }

            DisplayByeMessage();
        }
    }
}
//TO DO: -Borrar info y Alerta