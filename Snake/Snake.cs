using System;
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

        public Snake(Vector vector, Directions startDirection = Directions.South)
        {
            vector.Value = "O";
            Parts = new List<Vector>() { vector };
            LastDirection = startDirection;
            NextMove = Command.Unassigned;
            Speed = new TimeSpan(0, 0, 0, 0, 2000);
        }

        public void Eat(Vector destination)
        {
            Vector newHead = new Vector(destination.X, destination.Y, Head.Value);
            Head.Value = Body;

            Parts.Insert(0, newHead);

            if (Speed.TotalMilliseconds > 200)
            {
                Speed -= new TimeSpan(0, 0, 0, 0, 200);
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

            if (NextMove == Command.Left)
            {
                switch (LastDirection)
                {
                    case Directions.North:
                        destinationDirection = Directions.West;
                        break;
                    case Directions.South:
                        destinationDirection = Directions.East;
                        break;
                    case Directions.East:
                        destinationDirection = Directions.North;
                        break;
                    case Directions.West:
                        destinationDirection = Directions.South;
                        break;
                    default:
                        break;
                }
            }
            else if (NextMove == Command.Right)
            {
                switch (LastDirection)
                {
                    case Directions.North:
                        destinationDirection = Directions.East;
                        break;
                    case Directions.South:
                        destinationDirection = Directions.West;
                        break;
                    case Directions.East:
                        destinationDirection = Directions.South;
                        break;
                    case Directions.West:
                        destinationDirection = Directions.North;
                        break;
                    default:
                        break;
                }
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
    }
}
