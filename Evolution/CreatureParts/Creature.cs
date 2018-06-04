using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.CreatureParts
{
    public class Creature
    {
        public int Id { get; private set; }

        public IEnumerable<Joint> Joints { get; set; }
        public IEnumerable<Muscle> Muscles { get; set; }

        public bool Alive { get; set; }

        public float Mutability { get; set; }
        public float Distance { get; set; }

        public float CreatureTimer { get; set; }

        public Creature(IEnumerable<Joint> joints, IEnumerable<Muscle> muscles, float distance, bool alive, float creatureTimer, float mutability)
        {
            Joints = joints;
            Muscles = muscles;
            Distance = distance;
            Alive = alive;
            CreatureTimer = creatureTimer;
            Mutability = mutability;
        }

        public Creature(IEnumerable<Joint> joints, IEnumerable<Muscle> muscles)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            Id = Constants.ID;
            Joints = joints;
            Muscles = muscles;
            Distance = 0;
            Alive = true;
            CreatureTimer = random.NextFloat(Constants.MinimumHeartbeat, Constants.MaximumHeartbeat);
            Mutability = Constants.Mutability;
        }
    }
}
