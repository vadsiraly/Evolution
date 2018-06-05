using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Evolution.CreatureParts
{
    public class Joint
    {
        public int Id { get; private set; }

        public Point Position { get; set; }
        public Vector Direction { get; set; }
        public float Diameter { get; set; }
        public float Friction { get; set; }

        public IEnumerable<Muscle> Muscles { get; set; }

        public Joint(Point p, Vector direction, float mass, float friction)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            Id = Constants.ID;
            Position = p;
            Direction = direction;
            Diameter = mass;
            Friction = friction;
        }

        public Joint(Point p)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            Id = Constants.ID;
            Position = p;
            Direction = new Vector(0, 0);
            Diameter = random.NextFloat(Constants.MinimumJointSize, Constants.MaximumJointSize);
            Friction = random.NextFloat(Constants.MinimumJointFriction, Constants.MaximumJointFriction);
        }

        public void ApplyForce()
        {
            Direction = new Vector(Direction.X * Constants.AirFriction, ( Direction.Y * Constants.AirFriction ) - Constants.Gravity);
            Position = new Point(Position.X + Direction.X, Position.Y + Direction.Y);

            var heightFromGround = (Position.Y + Diameter / 2);

            if (heightFromGround <= Constants.GroundOffset)
            {
                Position = new Point (Position.X + (Direction.X * Friction), (-Diameter / 2) + Constants.GroundOffset);
                Direction = new Vector(Direction.X, 0);

                if(Direction.X > 0)
                {
                    Direction = new Vector(Direction.X - (Friction * (Constants.GroundOffset - heightFromGround) * Constants.Friction), Direction.Y);
                    if(Direction.X < 0)
                    {
                        Direction = new Vector(0, Direction.Y);
                    }
                }
                else
                {
                    Direction = new Vector(Direction.X + (Friction * (Constants.GroundOffset - heightFromGround) * Constants.Friction), Direction.Y);
                    if (Direction.X > 0)
                    {
                        Direction = new Vector(0, Direction.Y);
                    }
                }
            }
        }
    }
}
