using System;
using SnakeMVVM.Models;

namespace SnakeMVVM.Helpers
{
    public static class CollisionDetector
    {
        public static bool IsColliding(PositionOnBoard first, PositionOnBoard second)
        {
            return CollisionX(first.PosLeftCanvas, second.PosLeftCanvas)
                   && CollisionY(first.PosTopCanvas, second.PosTopCanvas);
        }

        private static bool CollisionY(int firstPosTopCanvas, int secondPosTopCanvas)
        {
            if (Math.Abs(firstPosTopCanvas - secondPosTopCanvas) < 3)
                return true;
            return false;
        }

        private static bool CollisionX(int firstPosLeftCanvas, int secondPosLeftCanvas)
        {
            if (Math.Abs(firstPosLeftCanvas - secondPosLeftCanvas) < 3)
                return true;
            return false;
        }
    }
}
