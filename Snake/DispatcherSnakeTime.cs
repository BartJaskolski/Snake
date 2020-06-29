using System;
using System.Windows.Threading;

namespace Snake
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
            Timer.Interval = new TimeSpan(0, 0, 0,0,500);
            Timer.Start();
        }
    }
}
