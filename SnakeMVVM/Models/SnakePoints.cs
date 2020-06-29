using System.Windows.Shapes;
using SnakeMVVM.Helpers;

namespace SnakeMVVM.Models
{
    public class SnakePoints : BasicGameRectangle
    {
        public int Lives = 20;
        
        public bool ToRemove => Lives <= 0;

        public SnakePoints(Rectangle rect, PositionOnBoard location)
        {
            BasicRectangle = rect;
            Location = location;
        }

        public void RemoveLive() => Lives--;
    }

    public class SnakeBodyPart : BasicGameRectangle
    {

        public SnakeBodyPart()
        {
        }

        public static IBasicGameRectangle GetDefaultBodyPart()
        {
            var bodyPart = new SnakeBodyPart()
            {
                RectType = RectangleTypes.SnakeBody,
                BasicRectangle = RectangleGenerator.Generate(RectangleTypes.SnakeBody),
                Index = 0,
                Location = new PositionOnBoard(200, 200)
            };

            return bodyPart;
        }
    }
}
