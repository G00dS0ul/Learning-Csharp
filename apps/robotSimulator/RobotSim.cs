using System;
using System.Runtime.CompilerServices;

namespace robotSimulator
{
    public class RobotSim
    {
        public Robot Robot { get; private set;  }
        public Environment Env { get; } = new Environment();
        public bool IsRobotPlaced => Robot != null;

        public bool ExitRequest { get; private set; }

        public string HandleInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            input = input.Trim();

            if (input == "Quit" || input == "Q")
            {
                ExitRequest = true;
                return "Exiting Simulator.";
            }

            if (!Env.IsInitialized)
            {
                if (Env.setBoundary(input))
                {
                    return $"Boundary set: {Env.Width} x {Env.Height}. Now enter Robot Initial Position (x,y,direction).";
                }
                return "Invalid boundary. Format: width,height (e.g. 8,8)";
            }

            if (!IsRobotPlaced)
            {
                if (Robot.IntialPlacement(input, out int x, out int y, out Direction facing))
                {
                    if (!Env.IsValidPosition(x, y))
                    {
                        return "Placement out of bounds.";
                    }
                    Robot = new Robot(x, y, facing);
                    return $"Robot placed at ({x}, {y}) facing {facing}. Enter commands";
                }
                return "Invalid Placement. Format: x,y,direction(e.g. 2,3,East).";
            }

            if (input.StartsWith("B ", StringComparison.OrdinalIgnoreCase) || input.StartsWith("BOUNDARY ", StringComparison.OrdinalIgnoreCase))
            {
                var reset = input.Contains(' ') ? input[(input.IndexOf(' ') + 1)..] : "";
                if (Env.setBoundary(reset))
                {
                    Robot = null;
                    return $"Boundary updated to {Env.Width} x {Env.Height}. Replace robot";
                }
                return "Boundary Update Failed. Format: B width,height";
            }

            switch (input.ToUpperInvariant())
            {
                case "F":
                case "MOVE":
                    Robot.MoveForward(Env);
                    return ReportState();
                    break;
                case "L":
                case "LEFT":
                    Robot.TurnLeft();
                    return ReportState();
                    break;
                case "R":
                case "RIGHT":
                    Robot.TurnRight();
                    return ReportState();
                    break;
                case "P":
                case "REPORT":
                    return ReportState();
                    break;
                case "HELP":
                    return Guide();
                default:
                    return "Unknown command enter HELP for Guide";
            }
        }

        private string ReportState()
        {
            return $"X-Postion is: {Robot.XAxis}, Starting Y-Position is: {Robot.YAxis} And Facing: {Robot.Facing}";
        }

        private string Guide()
        {
            return "Commands:\n" + " width,height           -> Set boundary (Step 1)\n" + " x,y,direction          -> Place Robot (Step 2)\n" + " F or MOVE              -> Move Forward\n" + " L / LEFT, R /RIGHT     -> turn Left and Right respectively\n" + " B width,height         -> Reset Boundary (Robot also cleared)\n" + " HELP                   -> Show Guide\n" + " Q or EXIT              -> Quit";

        }

    }
}
