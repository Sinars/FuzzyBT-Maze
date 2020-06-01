using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    class Keyword
    {
        public const string MoveForward = "Move forward";

        public const string GoForward = "Go Forward";

        public const string GoLeft = "Go Left";

        public const string GoRight = "Go Right";

        public const string TurnLeft = "Turn Left";

        public const string TurnRight = "Turn Right";

        public const string StopMoving = "Stop Moving";

        public const string Stop = "Stop";

        public const string StopRunning = "Stop running";

        public const string Pull = "Pull the lever";

        public const string GoToLever = "Go to lever";

        public const string TurnAround = "Turn around";

        public const string GoToChest = "Go to chest";

        public const string OpenChest = "Open the chest";

        public const string StepBack = "Step back";

        public const string Kick = "Kick";

        public const string Run = "Run";

        public static readonly string[] Keys = new string[] { MoveForward,
            TurnLeft,
            GoLeft,
            TurnRight,
            GoRight,
            StopMoving,
            GoForward,
            TurnAround,
            GoToLever,
            Pull,
            StepBack,
            Kick,
            Run,
            Stop,
            OpenChest,
            GoToChest,
            StopRunning
        };

        public static readonly List<String> ForwardKeys = new List<string> {GoForward, GoToLever, MoveForward};
    }
}
