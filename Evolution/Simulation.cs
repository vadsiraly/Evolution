using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolution.CreatureParts;

namespace Evolution
{
    public class Simulation
    {
        public void Run()
        {

        }

        public void Simulate(Creature creature)
        {
            foreach(var muscle in creature.Muscles)
            {
                muscle.ApplyForce();
            }

            foreach (var joint in creature.Joints)
            {
                joint.ApplyForce();
            }
        }
    }
}
