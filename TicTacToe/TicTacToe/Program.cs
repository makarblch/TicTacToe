using System.ComponentModel.Design;
using TicTacToeLib;

class Program
{
    public static void Beginning()
    {
        Console.WriteLine("\nHello! Welcome to the TicTacToe game developed by the " +
                          "cutiest boy MK. \nThere are 2 types of this game. You can " +
                          "play with your friend or with your computer.\n\n" +
                          "1. To choose the first one press 1.\n" +
                          "2. To choose the second one press 2");
    }

   
    public static void Main(string[] args)
    {
        do
        {
            Beginning();
            
            // Выбор режима игры.
            if (Console.ReadKey().Key == ConsoleKey.D1)
            {
                Field myField = new Field();
                Console.WriteLine(myField);
                
                int i = 1;
                
                try
                {
                    while (i <= 9)
                    {
                        // Выполнение хода.
                        if (i % 2 != 0)
                        {
                            myField.Game("X", ref i);
                        }
                        else
                        {
                            myField.Game("0", ref i);
                        }
                
                        // Проверка на победу.
                        char meow = myField.Check();
                
                        if (meow != ' ')
                        {
                            Console.WriteLine(myField);
                            Console.WriteLine($"{meow} won");
                            break;
                        }
                
                        i++;
                        Console.WriteLine(myField);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Data);
                }
                
                if (myField.Check() == ' ') Console.WriteLine("Draw");
                Console.WriteLine("This is the end of the game. Press Enter to continue\n");
            }
            else
            {
                Field myField = new Field();
                Console.WriteLine(myField);
                
                Random fMove = new Random();
                
                // Человек играет за Х.
                if (fMove.Next(2) == 0)
                {
                    Console.WriteLine("You're gonna play Х");
                    Console.WriteLine("X starts\n");
                    int i = 1;
                
                    try
                    {
                        while (i <= 9)
                        {
                            // Выполнение хода.
                            if (i % 2 != 0)
                            {
                                myField.Game("X", ref i);
                            }
                            else
                            {
                                if (i == 2)
                                {
                                    myField.FirstMove("0");
                                }
                                else
                                {
                                    myField.ComputerMove("0");
                                }
                            }
                
                            Console.WriteLine(myField);
                            Console.WriteLine("________");
                
                            // Проверка.
                            char meow = myField.Check();
                
                            if (meow != ' ')
                            {
                                Console.WriteLine(myField);
                                Console.WriteLine($"{meow} won");
                                break;
                            }
                
                            i++;
                
                        }
                    }
                    catch (Exception e)
                    { 
                        Console.WriteLine(e.Message);
                    }
                
                    if (myField.Check() == ' ') Console.WriteLine("Draw");
                    Console.WriteLine("This is the end of the game. Press Enter to continue\n");
                }
                else
                {
                    // Человек играет за 0.
                    Console.WriteLine("You're gonna play 0");
                    Console.WriteLine("X starts.\n");
                
                    int i = 1;
                
                    try
                    {
                        while (i <= 9)
                        {
                            if (i % 2 == 0)
                            {
                               
                                myField.Game("0", ref i);
                            }
                            else
                            {
                                if (i == 1)
                                {
                                    myField.FirstMove("X");
                                }
                                else
                                {
                                    myField.ComputerMove("X");
                                }
                            }
                
                            Console.WriteLine(myField);
                            Console.WriteLine("________");
                
                            char meow = myField.Check();
                
                            if (meow != ' ')
                            {
                                Console.WriteLine(myField);
                                Console.WriteLine($"{meow} won");
                                break;
                            }
                
                            i++;
                
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                
                    if (myField.Check() == ' ') Console.WriteLine("Draw");
                    Console.WriteLine("This is the end of the game. Press Enter to continue\n");
                }
            }
        } while (Console.ReadKey().Key == ConsoleKey.Enter);
        Console.WriteLine("Кончили. Спасибо за игру!");
    }
}