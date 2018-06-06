using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Evolution
{
    public static class Constants
    {
        private static int _ID = 0;
        public static int ID {
            get
            {
                return ++_ID;
            }
        }

        public static Point WindowSize = new Point(1600, 800);

        public static float GroundHeight = 0.3f;
        public static float GroundOffset = - (float)WindowSize.Y * (0.5f - GroundHeight);
        public static float AirFriction = 0.95f;
        public static float Mutability = 1f;
        public static float Gravity = 0.5f;
        public static float Friction = 0.4f;
        public static float ForceMultiplier = 50f;

        public static float MinimumJointFriction = 0f;
        public static float MaximumJointFriction = 1f;
        public static float MinimumJointSize = 40f;
        public static float MaximumJointSize = 40f;

        public static float MinimumMuscleRigidity = 2f;
        public static float MaximumMuscleRigidity = 8f;

        public static int MinimumMusclePeriod = 1;
        public static int MaximumMusclePeriod = 3;

        public static float MinimumMuscleContractionTime = 0f;
        public static float MaximumMuscleContractionTime = 1f;

        public static float MinimumMuscleExtensionTime = 0f;
        public static float MaximumMuscleExtensionTime = 1f;

        public static float MinimumMuscleLength = 150f;
        public static float MaximumMuscleLength = 700f;

        public static float MinimumHeartbeat = 40f;
        public static float MaximumHeartbeat = 80f;
    }
}
