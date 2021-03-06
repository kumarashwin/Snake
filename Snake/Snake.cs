﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Snake
    {
        public Vector Head { get { return Parts.First(); } }
        public string Body { get { return "o"; } }
        public List<Vector> Parts { get; set; }
        public Directions LastDirection { get; set; }
        public Command NextMove { get; set; }
        public TimeSpan Speed { get; set; }
        public static Dictionary<Directions, Vector> DirectionToVector { get; private set; }

        static Snake()
        {
            DirectionToVector = new Dictionary<Directions, Vector>() {
                { Directions.North, new Vector(0, -1) },
                { Directions.South, new Vector(0, 1) },
                { Directions.East, new Vector(1, 0) },
                { Directions.West, new Vector(-1, 0) }};
        }

        public Snake(Vector vector,
                     Directions startDirection = Directions.South,
                     int speed = 800)
        {
            vector.Value = "O";
            Parts = new List<Vector>() { vector };
            LastDirection = startDirection;
            NextMove = Command.Unassigned;
            Speed = new TimeSpan(0, 0, 0, 0, speed);
        }

        public void Eat(Vector destination)
        {
            Vector newHead = new Vector(destination.X, destination.Y, Head.Value);
            Head.Value = Body;

            Parts.Insert(0, newHead);

            if (Speed.TotalMilliseconds > 80)
            {
                int newMilliseconds = (int)Math.Floor(Speed.TotalMilliseconds - (Speed.TotalMilliseconds * 0.15));
                Speed = new TimeSpan(0, 0, 0, 0, newMilliseconds);
            }
        }

        /// <summary>
        /// Takes care of having the rest of the body of the snake
        /// follow its head
        /// </summary>
        /// <param name="destination">Last position of the head before it moved</param>
        public void Propogate(Vector destination)
        {
            Vector currentPosition;
            for (int i = 1; i < Parts.Count; i++)
            {
                currentPosition = Parts[i];
                Parts[i] = destination;
                destination = currentPosition;
            }
        }

        /// <summary>
        /// Reads NextMove;
        /// Sets destionationDirection based on the LastDirection
        /// snake was heading in;
        /// Unassigns NextMove;
        /// Moves the head;
        /// Sets the new LastDirection;
        /// Calls Propogate() to have the rest of the body follow;
        /// </summary>
        public void Destination(out Vector destination)
        {
            Directions destinationDirection = LastDirection;
            switch (NextMove)
            {
                case Command.Left:
                    if(LastDirection == Directions.North || LastDirection == Directions.South)
                    {
                        destinationDirection = Directions.West;
                    }
                    break;
                case Command.Right:
                    if (LastDirection == Directions.North || LastDirection == Directions.South)
                    {
                        destinationDirection = Directions.East;
                    }
                    break;
                case Command.Up:
                    if (LastDirection == Directions.East || LastDirection == Directions.West)
                    {
                        destinationDirection = Directions.North;
                    }
                    break;
                case Command.Down:
                    if (LastDirection == Directions.East || LastDirection == Directions.West)
                    {
                        destinationDirection = Directions.South;
                    }
                    break;
            }

            NextMove = Command.Unassigned;
            LastDirection = destinationDirection;

            destination = new Vector(Head.X, Head.Y);
            destination.Add(DirectionToVector[destinationDirection]);
        }

        public void Act(Vector destination, bool eat = false)
        {
            Vector prevHeadPosition = new Vector(Head.X, Head.Y, Body);

            if (eat)
            {
                Eat(destination);
            }
            else
            {
                Head.X = destination.X;
                Head.Y = destination.Y;
                Propogate(prevHeadPosition);
            }
        }

        public void ProcessCommands()
        {
            //Sets future time at which loop to read keys ends and,
            //at which point, the snake performs the action and
            //the Console renders.
            var timeLimit = DateTime.Now.Add(Speed);

            do
            {
                if (NextMove == Command.Unassigned)
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
                            NextMove = Command.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            NextMove = Command.Right;
                            break;
                        case ConsoleKey.UpArrow:
                            NextMove = Command.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            NextMove = Command.Down;
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
}
