using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Snake.Game;
using Snake.Game.Helpers;

namespace Snake
{
    public class SnakeObj
    {
        public SnakeBody Body { get; set; }
        public int Score { get; set; } = 0;
        public SnakeDirection SnakeDirection { get; set; } = SnakeDirection.Right;

        public SnakeObj()
        {
            Body = new SnakeBody();
        }
        
        public void MoveSnake()
        {
            Body.MoveBody(SnakeDirection);
        }

        public bool IsScore(List<SnakePoints> points)
        {
            bool isScore = false;
            foreach (var snakePoint in points)
            {
                if (CollisionDetector.IsColliding(snakePoint.Location, Body.Head.Location))
                {
                    isScore = true;
                    Score++;
                    Body.AddOneRectOfBody(SnakeDirection);
                    snakePoint.Lives = 0;
                }
            }

            return isScore;
        }

        public void PointGained()
        {
            Score++;
        }

    }


    public class SnakeBodyPart : BasicGameRectangle
    {

        public SnakeBodyPart()
        {
        }

        public static SnakeBodyPart GetDefaultBodyPart()
        {
            var bodyPart = new SnakeBodyPart()
            {
                BasicRectangle = RectangleGenerator.Generate(RectangleTypes.SnakeBody),
                Index = 0,
                Location = new PositionOnBoard(200, 200)
            };

            return bodyPart;
        }
    }
}

    public class PositionOnBoard
    {
        public int PosLeftCanvas { get; set; }
        public int PosTopCanvas { get; set; }

        public PositionOnBoard(int left,int top)
        {
            PosLeftCanvas = left;
            PosTopCanvas = top;
        }
    }

    public enum SnakeDirection
    {
        Up=0,
        Right = 1,
        Down =2,
        Left=3,
    }

