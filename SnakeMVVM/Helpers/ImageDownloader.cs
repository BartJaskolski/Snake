using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SnakeMVVM.ViewModels;

namespace SnakeMVVM.Helpers
{
    public static class ImageDownloader
    {
        public static void  DownloadImages(Action<Bitmap> methodAction)
        {
            System.Drawing.Bitmap imageBitmap = null;
            byte[] imageBytes;
            Task t = Task.Run(() =>
            {
                try
                {
                    using (var webClient = new WebClient())
                    {
                        imageBytes =  webClient.DownloadDataTaskAsync(Urls[new Random().Next(0, 5)]).Result;
                        using (var ms = new MemoryStream(imageBytes))
                        {
                            imageBitmap = new Bitmap(ms);
                        }
                    }
                }
                catch
                {
                }
            });
            t.ContinueWith((a) =>
            {
                methodAction(imageBitmap);
            },TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static  List<string> Urls = new List<string>()
        {
            "https://cdn.pixabay.com/photo/2015/02/28/15/25/snake-653639_960_720.jpg",
            "https://cdn.pixabay.com/photo/2016/08/31/18/19/snake-1634293_960_720.jpg",
            "https://cdn.pixabay.com/photo/2014/11/23/21/22/green-tree-python-543243_960_720.jpg",
            "https://cdn.pixabay.com/photo/2015/10/30/15/04/green-tree-python-1014229_960_720.jpg",
            "https://cdn.pixabay.com/photo/2019/02/06/17/09/snake-3979601_960_720.jpg"
        };
    }
}
