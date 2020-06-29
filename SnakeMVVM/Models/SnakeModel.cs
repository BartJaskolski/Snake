using System.Collections.ObjectModel;
using System.Linq;

namespace SnakeMVVM.Models
{
    public class SnakeModel
    { 
        public ObservableCollection<IBasicGameRectangle> SnakeBody { get; set; }
        public SnakeDirectionType Direction { get; set; }
        public IBasicGameRectangle Head => SnakeBody.First(x => x.Index == 0);
        public IBasicGameRectangle Last => SnakeBody.OrderByDescending(x => x.Index).First();
        public int SnakeSpeed { get; set; }
        public int Score { get; set; }

        public int MaxIndex
        {
            get
            {
                return SnakeBody.Max(x => x.Index);
            }
            set { }
        }

        public SnakeModel(IBasicGameRectangle deafultSnake)
        {
            SnakeBody = new ObservableCollection<IBasicGameRectangle> {deafultSnake};
            Direction = SnakeDirectionType.Right;
            SnakeSpeed = 20;
        }
    }
}
