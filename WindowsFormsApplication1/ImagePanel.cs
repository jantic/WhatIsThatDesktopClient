using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Windows.Forms;

namespace WhatIsThat
{
    public sealed class ImagePanel : Panel
    {

        private Image _sourceImage;
        private readonly ConcurrentQueue<Rectangle> _rectanglesToDraw = new ConcurrentQueue<Rectangle>();

        public ImagePanel()
        {
            BackgroundImageLayout = ImageLayout.Zoom;
            _sourceImage = new Bitmap(1,1);
            BackgroundImage = _sourceImage;
            DoubleBuffered = true;
        }

        public void AddRectangleToDraw(Rectangle rectangle)
        {
            _rectanglesToDraw.Enqueue(rectangle);
        }

        public Image SourceImage
        {
            get
            {
                return (Image)_sourceImage.Clone();
            }
            set
            {
                _sourceImage = (Image)value.Clone();
                BackgroundImage = _sourceImage;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
   
            Rectangle rectangle;
            while (_rectanglesToDraw.TryDequeue(out rectangle))
            {
                var image = (Image)_sourceImage.Clone();
                //For consistent appearance
                var penWidth = Convert.ToInt32(2*BackgroundImage.Width/1000);

                using (var pen = new Pen(Color.FromArgb(255, 255, 0, 0), penWidth))
                {
                   
                    using (var graphics = Graphics.FromImage(image))
                    {
                        graphics.DrawRectangle(pen, rectangle);
                        BackgroundImage = image;
                    }    
                }
            }
        }
    }
}