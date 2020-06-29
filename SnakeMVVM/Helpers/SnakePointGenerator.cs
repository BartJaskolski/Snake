using System;
using SnakeMVVM.Models;

namespace SnakeMVVM.Helpers
{
    public static class SnakePointGenerator 
    {
        public static Random rand { get; set; } = new Random();

        public static SnakePoints Generate()
        {
            return 
                new SnakePoints(RectangleGenerator.Generate(RectangleTypes.Point),
                    new PositionOnBoard(rand.Next(1, 20) * 20, rand.Next(1, 20) * 20));
        }
    }
}
