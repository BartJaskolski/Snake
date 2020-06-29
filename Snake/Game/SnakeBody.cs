using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Game.Helpers;

namespace Snake.Game
{

    public class SnakeBody
    {
        public List<SnakeBodyPart> SnakeBodyParts { get; set; }

        public SnakeBodyPart Head => SnakeBodyParts.First(x => x.Index == 0);
        public SnakeBodyPart Last => SnakeBodyParts.OrderByDescending(x => x.Index).First();
        public int SnakeSpeed { get; set; } = 20;

        public int MaxIndex
        {
            get
            {
                return SnakeBodyParts.Max(x => x.Index);
            }
            set { }
        }

        public SnakeBody()
        {
            SnakeBodyParts = new List<SnakeBodyPart>
            {
                SnakeBodyPart.GetDefaultBodyPart()
            };
        }

        public void AddOneRectOfBody(SnakeDirection snakeDirection)
        {
            SnakeBodyParts.Add(new SnakeBodyPart()
            {
                BasicRectangle = RectangleGenerator.Generate(RectangleTypes.SnakeBody),
                Index = ++MaxIndex,
                Location = GetLocationForNewPart(snakeDirection)
            });
        }

        private PositionOnBoard GetLocationForNewPart(SnakeDirection snakeDirection)
        {
            var location = Last.Location;
            var newLocation = new PositionOnBoard(location.PosLeftCanvas, location.PosTopCanvas);

            switch (snakeDirection)
            {
                case SnakeDirection.Up:
                    newLocation.PosTopCanvas += 22;
                    break;
                case SnakeDirection.Right:
                    newLocation.PosLeftCanvas -= 22;
                    break;
                case SnakeDirection.Down:
                    newLocation.PosTopCanvas -= 22;
                    break;
                case SnakeDirection.Left:
                    newLocation.PosLeftCanvas += 22;
                    break;

            }
            return newLocation;
        }

        public void MoveBody(SnakeDirection snakeDirection)
        {
            for (int i = SnakeBodyParts.Count - 1; i > 0; i--)
            {
                var nextBodyPart = SnakeBodyParts[i - 1].Location;
                SnakeBodyParts[i].Location = new PositionOnBoard(nextBodyPart.PosLeftCanvas, nextBodyPart.PosTopCanvas);
            }

            switch (snakeDirection)
            {
                case SnakeDirection.Up:
                    Head.Location.PosTopCanvas -= SnakeSpeed;
                    break;
                case SnakeDirection.Right:
                    Head.Location.PosLeftCanvas += SnakeSpeed;
                    break;
                case SnakeDirection.Down:
                    Head.Location.PosTopCanvas += SnakeSpeed;
                    break;
                case SnakeDirection.Left:
                    Head.Location.PosLeftCanvas -= SnakeSpeed;
                    break;
                default:
                    throw new Exception("Direction Unknown");
            }
        }
    }
}
