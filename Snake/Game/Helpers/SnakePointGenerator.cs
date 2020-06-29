using System;
using System.Collections.Generic;

namespace Snake.Game.Helpers
{
    public class SnakePointGenerator : BasicGameRectangle
    {
        public Random rand { get; set; }

        public SnakePointGenerator()
        {
            rand = new Random();
        }
        public SnakePoints Generate()
        {
            BasicRectangle = RectangleGenerator.Generate(RectangleTypes.Point);
            Location = new PositionOnBoard(rand.Next(1, 20) *20, rand.Next(1, 20)*20);
            return new SnakePoints(BasicRectangle, Location);
        }
    }
}
