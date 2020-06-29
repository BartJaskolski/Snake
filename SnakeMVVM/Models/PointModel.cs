using System.Collections.ObjectModel;

namespace SnakeMVVM.Models
{
    public class PointModel
    {
        public ObservableCollection<SnakePoints> Points { get; set; }

        public PointModel()
        {
            Points = new ObservableCollection<SnakePoints>();
        }
    }
}
