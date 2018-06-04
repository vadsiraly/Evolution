using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ApplyForce()
        {
            throw new NotImplementedException();
        }
    }
}
