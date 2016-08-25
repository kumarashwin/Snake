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
        Straight,
        Unassigned
    }

    static class Program
    {
        static void Main(string[] args)
        {
            World world = new World(50, 20);
            Snake snake = new Snake(new Vector(10, 10));

            int count = 0;
            bool eat = true;
            Vector destination;
            

            while (true)
            {
                world.Render(snake);
                snake.ProcessCommands();
                snake.Destination(out destination);
                snake.Act(destination, eat);

                if(count > 4)
                {
                    eat = false;
                }
                else
                {
                    count++;
                }
            }
        }

        private static void ProcessCommands(this Snake snake)
        {
            //Sets future time at which loop to read keys ends and,
            //at which point, the snake performs the action and
            //the Console renders.
            var timeLimit = DateTime.Now.Add(snake.Speed);

            do
            {
                if (snake.NextMove == Command.Unassigned)
                {
                    while (!Console.KeyAvailable)
                    {
                        if (!TimeLeft(timeLimit))
                        {
                            break;
                        }
                    }

                    if (!TimeLeft(timeLimit))
                    {
                        break;
                    }

                    //Check and process keypress
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            snake.NextMove = Command.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            snake.NextMove = Command.Right;
                            break;
                    }
                }

            } while (TimeLeft(timeLimit)); //While amount of time isn't finished

            FlushReadKeyStream();
        }

        private static void FlushReadKeyStream()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        private static bool TimeLeft(DateTime timeLimit)
        {
            return timeLimit > DateTime.Now ? true : false;
        }
    }

    class World
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector[,] Grid { get; set; }

        public World(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            CreateGrid();
        }

        public void Render(Snake snake)
        {
            Console.Clear();
            CreateGrid();
            foreach (var part in snake.Parts)
            {
                Grid[part.X, part.Y] = part;
            }
            Console.WriteLine(this.ToString());
        }

        private void CreateGrid()
        {
            this.Grid = new Vector[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                    {
                        Grid[x, y] = new Vector(x, y, "#");
                    }
                    else
                    {
                        Grid[x, y] = new Vector(x, y);
                    }
                }
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    if(x == Width && y != Height)
                    {
                        result += "\n";
                    }
                    else
                    {
                        result += Grid[x, y].Value;
                    }
                }
            }
            return result;
        }
    }
}
