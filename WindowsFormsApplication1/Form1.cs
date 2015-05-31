using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Spatial;
using System.Windows.Forms;
using WhatIsThat.WhatIsThatServiceClient;

namespace WhatIsThat
{
    public partial class Form1 : Form
    {
        private Point _startPointRectangle = new Point(0,0);
        private Point _endPointRectangle = new Point(0, 0);
        private readonly ConcurrentQueue<Rectangle> _completedRectangles = new ConcurrentQueue<Rectangle>();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            string filename;
            var validData = GetFilename(out filename, e);
            if (validData)
            {
                ImageInterface.SourceImage = Image.FromFile(filename);
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private bool GetFilename(out string filename, DragEventArgs e)
        {
            filename = String.Empty;
            if ((e.AllowedEffect & DragDropEffects.Copy) != DragDropEffects.Copy) return false;
            var data = e.Data.GetData("FileName") as Array;
            if (data == null) return false;
            if ((data.Length != 1) || (!(data.GetValue(0) is String))) return false;
            filename = ((string[]) data)[0];
            var extension = Path.GetExtension(filename);
            if (extension == null) return false;
            var ext = extension.ToLower();
            return (ext == ".jpg") || (ext == ".png");
        }

        private Rectangle GetDrawnRectangleScaledForImage(Rectangle rectangle)
        {
            var image = ImageInterface.BackgroundImage;
            var xScaling = Convert.ToDouble(image.Size.Width) / Convert.ToDouble(ImageInterface.Size.Width);
            var yScaling = Convert.ToDouble(image.Size.Height) / Convert.ToDouble(ImageInterface.Size.Height);
            var scaledRectangle = new Rectangle(
                Convert.ToInt32(xScaling * rectangle.X),
                Convert.ToInt32(yScaling * rectangle.Y),
                Convert.ToInt32(xScaling * rectangle.Width),
                Convert.ToInt32(yScaling * rectangle.Height));
            return scaledRectangle;
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            var image = ImageInterface.SourceImage;
            var whatIsThatClient = new WhatIsThatClient();
            if (image == null) return;
            var listViewItems = TagsListView.Items;
            listViewItems.Clear();
            AnswerLabel.Text = "";
            answerPicture.Image = null;
            Refresh();
            var coordinates = GeographyPoint.Create(Convert.ToDouble(latitudeInput.Value), Convert.ToDouble(longitudeInput.Value));

            var cropAreas = new List<Rectangle>();

            while (!_completedRectangles.IsEmpty)
            {
                Rectangle rectangle;
                _completedRectangles.TryDequeue(out rectangle);
                if (rectangle.Width <= 0 || rectangle.Height <= 0) continue;
                var cropArea = GetDrawnRectangleScaledForImage(rectangle);
                cropAreas.Add(cropArea);
            }

            if (cropAreas.Count == 0)
            {
                var defaultRectangle = new Rectangle(0, 0, ImageInterface.Width, ImageInterface.Height);
                cropAreas.Add(GetDrawnRectangleScaledForImage(defaultRectangle));
            }


            var identities = whatIsThatClient.GetMostLikelyIdentities(image, coordinates, cropAreas.Last(), true);

            if (identities.SpeciesCandidates.Count > 0)
            {
                var firstIdentity = identities.SpeciesCandidates[0];
                AnswerLabel.Text = Capitalise(firstIdentity.CommonName);

                var text = Capitalise(firstIdentity.CommonName) + " (" + firstIdentity.Confidence.ToString(".###") +")";
                listViewItems.Add(new ListViewItem(text));
            }
        }

        private static string Capitalise(String str) {
            if (String.IsNullOrEmpty(str))
                return String.Empty;
            return Char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

        private void geoContextModeOffRB_CheckedChanged(object sender, EventArgs e)
        {
            latitudeInput.Enabled = geoContextModeOnRB.Checked;
            longitudeInput.Enabled = geoContextModeOnRB.Checked;
        }

        private void ImageInterface_MouseDown(object sender, MouseEventArgs e)
        {
            _startPointRectangle.X = e.X;
            _startPointRectangle.Y = e.Y;

            _endPointRectangle.X = e.X;
            _endPointRectangle.Y = e.Y;
        }

        private void ImageInterface_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _endPointRectangle.X = e.X;
            _endPointRectangle.Y = e.Y;
            var rectangle = GenerateMostRecentlyDrawnRectangle();
            ImageInterface.AddRectangleToDraw(GetDrawnRectangleScaledForImage(rectangle));
            ImageInterface.Invalidate();
        }

        private Rectangle GenerateMostRecentlyDrawnRectangle()
        {
            var startX = _startPointRectangle.X < _endPointRectangle.X ? _startPointRectangle.X : _endPointRectangle.X;
            var startY = _startPointRectangle.Y < _endPointRectangle.Y ? _startPointRectangle.Y : _endPointRectangle.Y;

            var endX = _startPointRectangle.X < _endPointRectangle.X ? _endPointRectangle.X : _startPointRectangle.X;
            var endY = _startPointRectangle.Y < _endPointRectangle.Y ? _endPointRectangle.Y : _startPointRectangle.Y;

            var topLeft = new Point(startX, startY);
            var width = Math.Abs(endX - startX);
            var height = Math.Abs(endY - startY);
            var size = new Size(width, height);
            return new Rectangle(topLeft, size);
        }

        private void ImageInterface_MouseUp(object sender, MouseEventArgs e)
        {
            var rectangle = GenerateMostRecentlyDrawnRectangle();
            _completedRectangles.Enqueue(rectangle);
        }
    }
}