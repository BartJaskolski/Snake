using System;
using System.Windows.Media;
using System.Windows.Shapes;
using SnakeMVVM.Models;

namespace SnakeMVVM.Helpers
{
    public static class RectangleGenerator
    {
        public static Rectangle Generate(RectangleTypes type)
        {
            switch (type)
            {
                case RectangleTypes.Point:
                    return new Rectangle()
                    {
                        Fill = new SolidColorBrush(Colors.White),
                        Width = 20,
                        Height = 20,
                    };
                case RectangleTypes.SnakeBody:
                    return new Rectangle()
                    {
                        Fill = new SolidColorBrush(Colors.Red),
                        Width = 20,
                        Height = 20,
                    };
                default:
                    throw new Exception("Unknow RectangleType");
            }
        }
    }
}
