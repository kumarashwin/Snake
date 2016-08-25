using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class World
    {
        private Random _random;
        private List<int> _notEmptyX;
        private List<int> _notEmptyY;
        private int _gridSize;

        public int Width { get; set; }
        public int Height { get; set; }
        public Vector Food { get; set; }
        public Vector[,] Grid { get; set; }

        public World(int width, int height)
        {
            _notEmptyX = new List<int>();
            _notEmptyY = new List<int>();
            _random = new Random();

            //(Width * Height) - Border
            _gridSize = (width * height) - (width * 2) + (height * 2) - 8;

            this.Width = width;
            this.Height = height;
            CreateGrid();
        }


        //TODO: Fix Victory condition
        //== crashes due to something in SpawnFood/Render
        public bool Victory()
        {
            int nonEmptySpaces = _notEmptyX.Count * _notEmptyY.Count;
            return nonEmptySpaces == _gridSize - 1;
        }

        public void SpawnFood()
        {
            int x, y;

            do
            {
                x = _random.Next(1, Width - 1);
            } while (_notEmptyX.Contains(x));

            do
            {
                y = _random.Next(1, Height - 1);
            } while (_notEmptyX.Contains(x));

            Food = new Vector(x, y, "+");
        }

        public void Render(Snake snake, bool food)
        {
            Console.Clear();
            CreateGrid();

            _notEmptyX.Clear();
            _notEmptyY.Clear();

            foreach (var part in snake.Parts)
            {
                Grid[part.X, part.Y] = part;
                _notEmptyX.Add(part.X);
                _notEmptyY.Add(part.Y);
            }

            if (food)
            {
                SpawnFood();
            }

            Grid[Food.X, Food.Y].Value = "+";

            Console.WriteLine(this.ToString());
            Console.Write("\n    Controls: Use direction keys to change direction");
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


            //Temporary: Rudimentary spacer
            for (int i = 0; i < 3; i++)
            {
                result += "\n";
            }

            for (int y = 0; y < Height; y++)
            {

                //Temporary: Rudimentary spacer
                for (int i = 0; i < 16; i++)
                {
                    result += " ";
                }

                for (int x = 0; x <= Width; x++)
                {
                    if (x == Width && y != Height)
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

        internal void CheckDestination(Vector destination, out bool collision, out bool eat)
        {
            var value = Grid[destination.X, destination.Y].Value;

            eat = false;
            collision = false;

            switch (value)
            {
                case "+":
                    eat = true;
                    break;
                case "#":
                    collision = true;
                    break;
                case "o":
                    collision = true;
                    break;
            }
        }
    }
}
