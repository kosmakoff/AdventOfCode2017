using System;
using System.Text.RegularExpressions;

namespace Day20
{
    internal class PointData
    {
        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        public Vector Acceleration { get; set; }

        public PointData(Vector position, Vector velocity, Vector acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }

        private static readonly Regex PointDataRegex = new Regex(@"^p=<(?<px>-?\d+),(?<py>-?\d+),(?<pz>-?\d+)>, v=<(?<vx>-?\d+),(?<vy>-?\d+),(?<vz>-?\d+)>, a=<(?<ax>-?\d+),(?<ay>-?\d+),(?<az>-?\d+)>$");

        public static PointData Parse(string pointDataString)
        {
            var match = PointDataRegex.Match(pointDataString);
            if (!match.Success)
                throw new FormatException();

            var p = new Vector(int.Parse(match.Groups["px"].Value), int.Parse(match.Groups["py"].Value), int.Parse(match.Groups["pz"].Value));
            var v = new Vector(int.Parse(match.Groups["vx"].Value), int.Parse(match.Groups["vy"].Value), int.Parse(match.Groups["vz"].Value));
            var a = new Vector(int.Parse(match.Groups["ax"].Value), int.Parse(match.Groups["ay"].Value), int.Parse(match.Groups["az"].Value));

            return new PointData(p, v, a);
        }
    }
}
