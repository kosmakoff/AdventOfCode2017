using System;

namespace Day24
{
    internal class Link
    {
        public Guid Guid { get; }

        private Link(int leftSide, int rightSide, Guid guid)
        {
            Guid = guid;
            LeftSide = leftSide;
            RightSide = rightSide;
        }

        public Link(int leftSide, int rightSide)
            : this(leftSide, rightSide, Guid.NewGuid())
        {
        }

        public int LeftSide { get; }
        public int RightSide { get; }

        public static Link Parse(string link)
        {
            var parts = link.Split('/');

            return new Link(int.Parse(parts[0]), int.Parse(parts[1]));
        }

        public Link Rotate(int side)
        {
            return LeftSide == side ? this : new Link(RightSide, LeftSide, Guid);
        }

        public override string ToString()
        {
            return $"{LeftSide}/{RightSide}";
        }
    }
}
