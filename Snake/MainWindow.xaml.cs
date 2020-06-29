using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Snake.Game;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SnakeBoard Board { get; set; }
        private SnakeEngine SnakeEngine { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitCustomBoard();
            InitPlayerControl();
            StartGame();
        }

        private void InitCustomBoard()
        {
            Board = new SnakeBoard(SetTimer(), SnakeBoard);
        }

        private void InitPlayerControl()
        {
            this.KeyDown += new KeyEventHandler(OnButtonPress);
        }

        private void OnButtonPress(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    SnakeEngine.ChangeSnakeDirection(SnakeDirection.Down);
                    break;
                case Key.Up:
                    SnakeEngine.ChangeSnakeDirection(SnakeDirection.Up);
                    break;
                case Key.Left:
                    SnakeEngine.ChangeSnakeDirection(SnakeDirection.Left);
                    break;
                case Key.Right:
                    SnakeEngine.ChangeSnakeDirection(SnakeDirection.Right);
                    break;
            }
        }

        private DispatcherSnakeTime SetTimer() => new DispatcherSnakeTime(timer_Tick);

        private void StartGame()
        {
            SnakeEngine = new SnakeEngine(Board);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(Board.StopGame)
                Board.SnakeTimer.Timer.Stop();
            Board.SnakeTimer.Ticks++;

            Board = SnakeEngine.Run(Board);
        }
    }

    public class SnakeBoard
    {
        public Canvas Board { get; set; }
        public DispatcherSnakeTime SnakeTimer { get; set; }
        public bool StopGame { get; set; }

        public SnakeBoard(DispatcherSnakeTime timer, Canvas board )
        {
            Board = board;
            SnakeTimer = timer;
        }
    }
}
