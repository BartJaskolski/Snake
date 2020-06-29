using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using SnakeMVVM.Commands;
using SnakeMVVM.Helpers;
using SnakeMVVM.Models;

namespace SnakeMVVM.ViewModels
{
    public class SnakeViewModel : INotifyPropertyChanged
    {
        public DispatcherSnakeTime SnakeTimer { get; set; }
        public SnakeModel SnakeObj { get; set; }
        public PointModel PointObj { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = (sender,e) => {};
        
        
        private ObservableCollection<IBasicGameRectangle> _allRect;
        public ObservableCollection<IBasicGameRectangle> AllRect
        {
            get
            {
                return _allRect;
            }
            set
            {
                _allRect = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(AllRect)));
            }
        }

        private Bitmap _bitmap;
        public Bitmap ImgWeb 
        {
            get
            {
                return _bitmap;
            }
            set
            {
                _bitmap = value;
                BitmapImage = BitMapConverter.ToBitmapSource(_bitmap);
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ImgWeb)));
            }
        }

        private BitmapSource _bitmapImage;
        public BitmapSource BitmapImage
        {
            get
            {
              return _bitmapImage;
            }
            set
            {
                _bitmapImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(BitmapImage)));
            }
        }

        public ICommand ChangeDirectionCommand { get; set; }

        private void ChangeDirection(object type)
        {
            SnakeObj.Direction = (SnakeDirectionType)int.Parse(type.ToString());
        }

        private bool CanChangeDirection(object type)
        {
            return true;
        }

        public SnakeViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            ChangeDirectionCommand  = new PlayerControlCommnad(ChangeDirection, CanChangeDirection);
            SnakeObj = new SnakeModel(SnakeBodyPart.GetDefaultBodyPart());
            PointObj = new PointModel();

            SnakeTimer = new DispatcherSnakeTime(UpdateBoard);

        }

        private void Run()
        {
            StopGame = IsOutOfBoard() || IsLost();
            UpdateSnake();
            UpdatePoints();
            if(IsSnakeScore())
                ImageDownloader.DownloadImages(UpdateImage);
            RefreshRectangle();
        }

        private bool IsLost()
        {
            foreach (var basicGameRectangle in SnakeObj.SnakeBody.Skip(1))
            {
                if(SnakeObj.Head.Location.Equals(basicGameRectangle.Location))
                    return true;
            }

            return false;
        }

        public bool StopGame { get; set; }

        private bool IsOutOfBoard()
        {
            int PosLeftCanvas = SnakeObj.Head.Location.PosLeftCanvas;
            int PosTopCanvas = SnakeObj.Head.Location.PosTopCanvas;

            return PosLeftCanvas <= 0 ||
                   PosLeftCanvas + 20 >= 400 ||
                   PosTopCanvas <= 0 ||
                   PosTopCanvas + 20 >= 400;
        }

        private bool IsSnakeScore()
        {
            bool isScore = false;
            foreach (var point in PointObj.Points)
            {
                if (CollisionDetector.IsColliding(point.Location, SnakeObj.Head.Location))
                {
                    isScore = true;
                    SnakeObj.Score++;
                    AddOneRectOfBody();
                    point.Lives = 0;
                }
            }

            return isScore;
        }

        private void AddOneRectOfBody()
        {
            SnakeObj.SnakeBody.Add(new SnakeBodyPart()
            {
                BasicRectangle = RectangleGenerator.Generate(RectangleTypes.SnakeBody),
                Index = ++SnakeObj.MaxIndex,
                Location = GetLocationForNewPart(SnakeObj.Direction)
            });
        }

        private PositionOnBoard GetLocationForNewPart(SnakeDirectionType snakeDirection)
        {
            var lastLocation = SnakeObj.Last.Location;
            var newLocation = new PositionOnBoard(lastLocation.PosLeftCanvas, lastLocation.PosTopCanvas);

            switch (snakeDirection)
            {
                case SnakeDirectionType.Up:
                    newLocation.PosTopCanvas += 20;
                    break;
                case SnakeDirectionType.Right:
                    newLocation.PosLeftCanvas -= 20;
                    break;
                case SnakeDirectionType.Down:
                    newLocation.PosTopCanvas -= 20;
                    break;
                case SnakeDirectionType.Left:
                    newLocation.PosLeftCanvas += 20;
                    break;

            }
            return newLocation;
        }

        private void UpdatePoints()
        {
            ObservableCollection<SnakePoints> localPoints = new ObservableCollection<SnakePoints>();

            foreach (var snakePoints in PointObj.Points)
                snakePoints.RemoveLive();

            foreach (var point in PointObj.Points)
            {
                if (!point.ToRemove)
                    localPoints.Add(point);
            }

            PointObj.Points = localPoints;

            if (SnakeTimer.PointToGenerate)
                GeneratePoint();
        }

        private void UpdateSnake()
        {
            for (int i = SnakeObj.SnakeBody.Count - 1; i > 0; i--)
            {
                var nextBodyPart = SnakeObj.SnakeBody[i - 1].Location;
                SnakeObj.SnakeBody[i].Location = new PositionOnBoard(nextBodyPart.PosLeftCanvas, nextBodyPart.PosTopCanvas);
            }

            switch (SnakeObj.Direction)
            {
                case SnakeDirectionType.Up:
                    SnakeObj.Head.Location.PosTopCanvas -= SnakeObj.SnakeSpeed;
                    break;
                case SnakeDirectionType.Right:
                    SnakeObj.Head.Location.PosLeftCanvas += SnakeObj.SnakeSpeed;
                    break;
                case SnakeDirectionType.Down:
                    SnakeObj.Head.Location.PosTopCanvas += SnakeObj.SnakeSpeed;
                    break;
                case SnakeDirectionType.Left:
                    SnakeObj.Head.Location.PosLeftCanvas -= SnakeObj.SnakeSpeed;
                    break;
                default:
                    throw new Exception("Direction Unknown");
            }
        }

        private void UpdateBoard(object sender, EventArgs e)
        {
            if (StopGame)
                SnakeTimer.Timer.Stop();

            SnakeTimer.Ticks++;
            Run();
        }

        private void RefreshRectangle()
        {
            AllRect = new ObservableCollection<IBasicGameRectangle>();

            foreach (var bodyPart in SnakeObj.SnakeBody)
            {
                AllRect.Add(bodyPart);
            }

            foreach (var point in PointObj.Points)
            {
                AllRect.Add(point);
            }
        }

        private void GeneratePoint()
        {
            var snPoint = SnakePointGenerator.Generate();
            PointObj.Points.Add(snPoint);
        }

        public void UpdateImage(Bitmap imageBitmap)
        {
            ImgWeb = imageBitmap;
            //return Task.WhenAll();
        }
    }
}
