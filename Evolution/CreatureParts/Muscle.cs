using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Evolution.Timing;

namespace Evolution.CreatureParts
{
    public class Muscle
    {
        public int Id { get; private set; }

        public int Period { get; set; }
        public float ContractTime { get; set; }
        public float ContractLength { get; set; }

        public float ExtendTime { get; set; }
        public float ExtendLength { get; set; }

        public float ThruPeriod { get; set; }
        public bool Contracted { get; set; }
        public float Rigidity { get; set; }

        public Tuple<Joint, Joint> Joints { get; set; }

        public Muscle(int period, Tuple<Joint, Joint> joints, float contractTime, float extendTime, float contractLength, float extendLength, bool contracted, float rigidity)
        {
            Id = Constants.ID;
            Period = period;
            Joints = joints;
            ContractTime = contractTime;
            ExtendTime = extendTime;
            ContractLength = contractLength;
            ExtendLength = extendLength;
            Contracted = contracted;
            Rigidity = rigidity;
        }

        public Muscle(Tuple<Joint, Joint> joints)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            var lengths = new[] { random.NextFloat(Constants.MinimumMuscleLength, Constants.MaximumMuscleLength), random.NextFloat(Constants.MinimumMuscleLength, Constants.MaximumMuscleLength) };
            var contractTime = random.NextFloat(Constants.MinimumMuscleContractionTime, Constants.MaximumMuscleContractionTime);
            var extendTime = random.NextFloat(Constants.MinimumMuscleExtensionTime, Constants.MaximumMuscleExtensionTime);

            Id = Constants.ID;
            Period = random.Next(1, 4);
            Joints = joints;
            ContractTime = contractTime;
            ExtendTime = extendTime;
            ContractLength = lengths.Min();
            ExtendLength = lengths.Max();
            Contracted = ContractTime <= ExtendTime;
            Rigidity = random.NextFloat(Constants.MinimumMuscleRigidity, Constants.MaximumMuscleRigidity);
        }

        public void ApplyForce(Time time, float creatureTimer)
        {
            float distance = Joints.Item1.Position.DistanceFrom(Joints.Item2.Position);
            float angle = Joints.Item1.Position.Angle(Joints.Item2.Position);

            var target = Contracted ? ContractLength : ExtendLength;

            float force = (float)Math.Min(Math.Max(1 - (distance / target), -0.4), 0.4) * Constants.ForceMultiplier;

            Joints.Item1.Direction = new Vector(Joints.Item1.Direction.X + (Math.Cos(angle) * force * Rigidity / Joints.Item1.Diameter), Joints.Item1.Direction.Y + ((float)Math.Sin(angle) * force * Rigidity / Joints.Item1.Diameter));
            Joints.Item2.Direction = new Vector(Joints.Item2.Direction.X - (Math.Cos(angle) * force * Rigidity / Joints.Item2.Diameter), Joints.Item2.Direction.Y - ((float)Math.Sin(angle) * force * Rigidity / Joints.Item2.Diameter));

            ThruPeriod = (time.ElapsedMilliseconds / creatureTimer) / Period;
            if ((ThruPeriod <= ExtendTime && ExtendTime <= ContractTime) ||
               (ContractTime <= ThruPeriod && ThruPeriod <= ExtendTime) ||
               (ExtendTime <= ContractTime && ContractTime <= ThruPeriod))
            {
                Contracted = true;
            }
            else
            {
                Contracted = false;
            }
        }
    }
}
