namespace SnakeMVVM.Models
{
    public class PositionOnBoard
    {
        public int PosLeftCanvas { get; set; }
        public int PosTopCanvas { get; set; }

        public PositionOnBoard(int left, int top)
        {
            PosLeftCanvas = left;
            PosTopCanvas = top;
        }

        public override bool Equals(object obj)
        {
            var loc = (PositionOnBoard) obj;

            return loc.PosTopCanvas == this.PosTopCanvas && loc.PosLeftCanvas == this.PosLeftCanvas;
        }
    }
}
