namespace Juego_del_ahorcado
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
             Ejercicio 16: Juego del Ahorcado

             Crear un juego simple del ahorcado en C#. Aquí tienes una descripción general de cómo puedes abordarlo:

             Elige una lista de palabras o frases para que el jugador adivine. 
             Puedes almacenar estas palabras o frases en un arreglo o en una lista.

             Selecciona una palabra o frase aleatoria de la lista para que el jugador intente adivinar.

             Muestra al jugador una serie de guiones bajos que representen las letras de la palabra 
             o frase a adivinar. Por ejemplo, "_ _ _ _ _" para una palabra de cinco letras.

             Permite que el jugador ingrese letras una a una para adivinar la palabra. Lleva un 
             registro de las letras que ha adivinado correctamente y muestra las letras adivinadas 
             en la palabra o frase, reemplazando los guiones bajos correspondientes. Por ejemplo, 
             si el jugador adivina "a", podría mostrar "_ _ _ a _" si "a" está en la palabra.

             Limita el número de intentos del jugador. Si el jugador adivina una letra incorrecta,
             reduce el número de intentos disponibles. Si el número de intentos llega a cero, el juego 
             termina y se revela la palabra correcta.

             Si el jugador adivina la palabra completa antes de quedarse sin intentos, muestra un mensaje 
             de victoria. De lo contrario, muestra un mensaje de derrota.
             */


            //Variables
            bool victory = false;
            int attempts = 10;
            string PalabraMagica;


            //Creamos arreglo de palabras
            string[] strings = { "elefante", "desododante", "godzilla", "termotanque", "toy story" };


            //Minusculizamos cada palabra
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = strings[i].ToLower();
            }


            //Metodo para elegir palabra aleatoria
            PalabraMagica = ChooseWord(strings);


            //Creamos arreglo de guiones segun el tamaño de la palabra aleatoria
            string[] guiones = CreateScripts(PalabraMagica);

            //Declaramos un lista para llevar un registro de las letras ingresadas.
            List<char> letrasIngresadas = new List<char>();


            //Que comience el juego.. xD
            while (attempts > 0)
            {
              
                //Primero verificamos comparamos el array con la palabra
                victory = WordStatus(guiones, PalabraMagica);

                //en caso de que victory sea true se saltea todo el proceso, sino sigue su curso
                if (victory) break;
                else
                {
                  
                    //Mostramos el estado actual de la palabra
                    ShowProgress(guiones);


                    //Pedimos una letra con get valid string
                    char letra = GetValidLetter("Digite una letra: ");


                    //Verificamos si la letra ya se ha ingresado
                    if (!letrasIngresadas.Contains(letra))
                    {
                        //Agregamos la letra a la lista de letras ingresadas
                        letrasIngresadas.Add(letra);
                    }
                    else
                    {
                        //Imprimimos mensaje 
                        Console.WriteLine($"Ya ingresaste la letra '{letra}'. Ingrese una letra diferente.");
                        Console.WriteLine();
                        continue; //Volvemos al inicio del bucle 
                    }


                    //Checkeamos si la letra esta en la palabra magica
                    CheckLetter(letra, ref guiones, ref attempts, PalabraMagica);
                }
            }


            //Mostramos el mensaje de derrota o victoria segun victory
            LoadingSeconds(3);
            if (victory) Console.WriteLine($"ENHORABUENA! has descubierto la palabra! Ganaste!, la palabra es: {PalabraMagica}");
            else Console.WriteLine($"Game over, la palabra es {PalabraMagica}, suerte la proxima!");


            Console.ReadKey();
        }


        //Metodo para dar tiempo durante la eleccion de palabras xd
        static void LoadingSeconds(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }



        //Metodo para elegir palabra aleatoriamente
        static string ChooseWord(string[] strings)
        {

            //Creamos objeto random
            Random random = new Random();

            //Retornamos un una palabra especifica segun el numero indice que retorna random
            return strings[random.Next(strings.Length)];
        }



        //Metodo para inicializar el arreglo de guiones
        static string[] CreateScripts(string palabra)
        {

            //nuevo arreglo
            string[] strings = new string[palabra.Length];

            //recorremos
            for (int i = 0; i < palabra.Length; i++)
            {
                if (palabra[i] != ' ')
                {
                    strings[i] = "_ ";
                }
                else
                {
                    strings[i] = " ";
                }
            }


            return strings;
        }



        //Metodo para mostrar progreso
        static void ShowProgress(string[] arregloGuiones)
        {

            Console.Write("Estado actual: ");
            foreach (string s in arregloGuiones)
            {
                Console.Write(s);
            }
            Console.WriteLine();
        }


        //Metodo para analizar la letra ingresada
        static void CheckLetter(char letra, ref string[] strings, ref int intentos, string palabraVerdadera)
        {

            //Si la letra esta en la palabra entonces recorremos el array 
            if (palabraVerdadera.Contains(letra))
            {
                for (int i = 0; i <= palabraVerdadera.Length - 1; i++)
                {
                    //Reemplazamos el guion bajo por la letra
                    if (palabraVerdadera[i] == letra) strings[i] = letra.ToString();
                }
                Console.WriteLine("Correcto!");
                Console.WriteLine();
            }
            //La letra no esta en la palabra
            else
            {
                intentos--;
                Console.WriteLine($"Error, {letra} no esta en la palabra.");
                Console.WriteLine($"Te quedan {intentos} intentos.");
                Console.WriteLine();

            }
        }


        //Metodo para comprobar el estado de la palabra
        static bool WordStatus(string[] guiones, string palabraObjetivo)
        {

            //Comparamos cada letra en el arreglo de guiones con la palabra objetivo
            for (int i = 0; i < guiones.Length; i++)
            {
                if (guiones[i] != palabraObjetivo[i].ToString() && guiones[i] != " ")
                {
                    return false;
                }
            }


            return true;
        }


        //Metodo auxiliar para validar letra
        public static char GetValidLetter(string message)
        {

            //Variable a usar
            char letter;

            do
            {
                Console.Write(message);
                string input = Console.ReadLine();


                //por si se digita mas de un char, o es null
                if (input.Length == 1 && char.IsLetter(input[0]) && !string.IsNullOrWhiteSpace(input))
                {

                    //Minusculizamos
                    letter = char.ToLower(input[0]);
                    break;

                }
                else Console.WriteLine("Error: Debes ingresar una única letra válida.");

            } while (true);

            return letter;
        }

    }
}