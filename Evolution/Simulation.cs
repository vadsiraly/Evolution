using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolution.CreatureParts;
using Evolution.Timing;

namespace Evolution
{
    public class Simulation
    {
        public void Run()
        {

        }

        public void Simulate(Creature creature, Time time)
        {
            foreach(var muscle in creature.Muscles)
            {
                muscle.ApplyForce(time, creature.CreatureTimer);
            }

            foreach (var joint in creature.Joints)
            {
                joint.ApplyForce();
            }
        }
    }
}
