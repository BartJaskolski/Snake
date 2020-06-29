using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SnakeMVVM.Models
{
    public class DispatcherSnakeTime
    {
        public DispatcherTimer Timer { get; set; }
        public bool PointToGenerate => Ticks % 5 == 0;
        public int Ticks { get; set; }

        public DispatcherSnakeTime(EventHandler timerTick)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += timerTick;
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            Timer.Start();
        }
    }
}
