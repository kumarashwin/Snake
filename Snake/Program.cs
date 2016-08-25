using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    public enum Directions
    {
        North,
        South,
        East,
        West
    }

    public enum Command
    {
        Left,
        Right,
        Up,
        Down,
        Straight,
        Unassigned
    }

    static class Program
    {

        //TODO: Fix Victory condition
        static void Main(string[] args)
        {
            bool repeat;
            do
            {
                RunGame(out repeat);
            } while (repeat);
        }

        private static void RunGame(out bool repeat)
        {
            World world = new World(20, 10);
            Snake snake = new Snake(new Vector(world.Width / 2, world.Height / 2), speed: 800);
            Vector destination;
            bool eat = true;
            bool collision = false;
            bool victory = false;

            while (true)
            {
                world.Render(snake, eat);
                snake.ProcessCommands();
                snake.Destination(out destination);
                world.CheckDestination(destination, out collision, out eat);
                
                if (collision)
                {
                    break;
                }

                if (world.Victory())
                {
                    victory = true;
                    break;
                }

                snake.Act(destination, eat);
            }         

            string result = "\n\n\n\n          ";
            
            if (victory)
            {
                result = result + "Holy shit, you won!\n" + result.Remove(0, 8) + "Press 'q' to exit. Press 'r' to retry: ";
            }
            else
            {
                result = result + "You collided with something!\n" + result.Remove(0, 8) + "Press 'q' to exit. Press 'r' to retry: ";
            }          

            ConsoleKey key = new ConsoleKey();
            int dots = 0;
            do
            {
                while(dots > 0)
                {
                    Thread.Sleep(400);
                    Console.Write(" .");
                    dots--;
                }
                dots = 4;

                Console.Clear();
                Console.Write(result);

                key = Console.ReadKey(true).Key;
            } while (!(key == ConsoleKey.Q || key == ConsoleKey.R));

            if (key == ConsoleKey.R)
            {
                repeat = true;
            }
            else
            {
                repeat = false;
            }
        }
    }
}
