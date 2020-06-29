using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Snake.Game.Helpers;
using Drawing =System.Drawing;

namespace Snake.Game
{
    /// <summary>
    /// SnakeEngine 
    /// </summary>
    public class SnakeEngine //: ISnakeEngine
    {
        public SnakeBoard Board { get; set; }
        public SnakeObj SnakeObj { get; set; }
        public bool PointToGenerate { get; set; }
        public List<SnakePoints> SnPoints { get; set; }
        public SnakePointGenerator SnakePointGenerator { get; set; }

        public SnakeEngine(SnakeBoard snakeBoard)
        {
            Board = snakeBoard;
            SnakeObj = new SnakeObj();
            SnPoints = new List<SnakePoints>();
            SnakePointGenerator = new SnakePointGenerator();
        }

        public SnakeBoard Run(SnakeBoard snakeBoard)
        {
            Board = snakeBoard;
            PointToGenerate = snakeBoard.SnakeTimer.PointToGenerate;

            UpdateBoard();

            UpdateSnake();
            UpdatePointsOnBoard();

            return Board;
        }

        private void UpdateBoard()
        {
            int PosLeftCanvas = SnakeObj.Body.Head.Location.PosLeftCanvas;
            int PosTopCanvas = SnakeObj.Body.Head.Location.PosTopCanvas;

            if (PosLeftCanvas <= 0 || 
                PosLeftCanvas +20 >= Board.Board.ActualWidth  ||
                PosTopCanvas <= 0 ||
                PosTopCanvas + 20 >= Board.Board.ActualHeight )
            {
                Board.StopGame = true;
                var lab = new Label()
                    {Content = $"GameOver Score :{SnakeObj.Score}" };
                lab.Background = Brushes.White;
                Board.Board.Children.Add(lab);
                SetPosition(lab, new PositionOnBoard(200,200));
            }
        }

        private void UpdateSnake()
        {
            SnakeObj.MoveSnake();
            if (SnakeObj.IsScore(SnPoints))
            {
                DownloadImages("https://cdn.pixabay.com/photo/2015/02/28/15/25/snake-653639_960_720.jpg", "xd55");
                DownloadImages("https://cdn.pixabay.com/photo/2015/02/28/15/25/snake-653639_960_720.jpg", "xd1");
                DownloadImages("https://cdn.pixabay.com/photo/2015/02/28/15/25/snake-653639_960_720.jpg", "xd2");
                DownloadImages("https://cdn.pixabay.com/photo/2015/02/28/15/25/snake-653639_960_720.jpg", "xd3");
            }
            AddChildren();
        }

            async Task<Drawing.Bitmap> DownloadImages(string url,string name)
            {
                Drawing.Bitmap imageBitmap = null;
                try
                {
                    using (var webClient = new WebClient())
                    {
                        var imageBytes = await webClient.DownloadDataTaskAsync(url);

                    MemoryStream ms = new MemoryStream(imageBytes);
                    Drawing.Image i = Drawing.Image.FromStream(ms);

                    i.Save(@"C:\snakeImg\" + name  + ".png" );
                }
                }
                catch
                {
                    //Silence is gold.
                }
                return imageBitmap;
            }

        public void ChangeSnakeDirection(SnakeDirection direction)
        {
            switch (SnakeObj.SnakeDirection)
            {
                case SnakeDirection.Right:
                     if(direction != SnakeDirection.Left)  SnakeObj.SnakeDirection = direction;
                     break;
                case SnakeDirection.Down:
                    if (direction != SnakeDirection.Up) SnakeObj.SnakeDirection = direction;
                    break;
                case SnakeDirection.Left:
                    if (direction != SnakeDirection.Right) SnakeObj.SnakeDirection = direction;
                    break;
                case SnakeDirection.Up:
                    if (direction != SnakeDirection.Down) SnakeObj.SnakeDirection = direction;
                    break;
            }
        }

        private void UpdatePointsOnBoard()
        {
            foreach (var snakePoints in SnPoints)
            {
                snakePoints.RemoveLive();

                if (snakePoints.ToRemove)
                {
                    Board.Board.Children.Remove(snakePoints.BasicRectangle);
                }
            }

            SnPoints.RemoveAll(x => x.ToRemove);

            if (PointToGenerate)
                GeneratePoint();
        }

        private void GeneratePoint()
        {
            var snPoint = SnakePointGenerator.Generate();
            SnPoints.Add(snPoint);
            Board.Board.Children.Add(snPoint.BasicRectangle);
            SetPosition(snPoint.BasicRectangle, snPoint.Location);
        }

        private void SetPosition(UIElement rect, PositionOnBoard location)
        {
            Canvas.SetLeft(rect, location.PosLeftCanvas);
            Canvas.SetTop(rect, location.PosTopCanvas);
        }

        private void AddChildren()
        {
            foreach (var rect in SnakeObj.Body.SnakeBodyParts)
            {
                Board.Board.Children.Remove(rect.BasicRectangle);
                Board.Board.Children.Add(rect.BasicRectangle);
                SetPosition(rect.BasicRectangle, rect.Location);
            }
        }
    }

    public abstract class BasicGameRectangle : IBasicGameRectangle
    {
        public Rectangle BasicRectangle { get; set; }
        public PositionOnBoard Location { get; set; }
        public int Index { get; set; }
    }

    public interface IBasicGameRectangle
    {
        Rectangle BasicRectangle { get; set; }
        PositionOnBoard Location { get; set; }
        int Index { get; set; }
    }

    public class SnakePoints : BasicGameRectangle
    {
        public int Lives = 10;
        public bool ToRemove => Lives <= 0;

        public SnakePoints(Rectangle rect, PositionOnBoard location)
        {
            BasicRectangle = rect;
            Location = location;
        }

        public void RemoveLive() => Lives--;
    }

    public enum RectangleTypes
    {
        Point,
        SnakeBody
    }

public interface ISnakeEngine
    {
        void MoveSnake();
        bool CheckIfLost();
        void GainPoint();
        void DrawSnake();
        Canvas UpdatesCanvas();
    }
}
