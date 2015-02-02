using System.Runtime.InteropServices;

namespace TimeTracker.Timer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
        public override string ToString()
        {
            return string.Format("{0},{1}", X, Y);
        }

        public static bool operator == (POINT a, POINT b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(POINT a, POINT b)
        {
            return a.X != b.X || a.Y != b.Y;
        }
    }
}
