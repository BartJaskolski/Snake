using System;

namespace Snake.Game.Helpers
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
            if(Math.Abs(firstPosTopCanvas - secondPosTopCanvas)<15)
                return true;
            return false;
        }

        private static bool CollisionX(int firstPosLeftCanvas, int secondPosLeftCanvas)
        {
            if (Math.Abs(firstPosLeftCanvas - secondPosLeftCanvas) < 15)
                return true;
            return false;
        }
    }
}
