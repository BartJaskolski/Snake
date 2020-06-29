using System.Windows.Shapes;

namespace SnakeMVVM.Models
{
    public abstract class BasicGameRectangle : IBasicGameRectangle
    {
        public Rectangle BasicRectangle { get; set; }
        public PositionOnBoard Location { get; set; }
        public RectangleTypes RectType { get; set; }
        public int Index { get; set; }
    }

    public interface IBasicGameRectangle
    {
        Rectangle BasicRectangle { get; set; }
        PositionOnBoard Location { get; set; }
        RectangleTypes RectType { get; set; }
        int Index { get; set; }
    }
}
