using System.Drawing;
using System.Windows.Forms;

namespace lab4
{
    public class CoordinatePlane
    {
        private PictureBox _pictureBox;
        private Bitmap? _bitmap;
        private Graphics? _graphics;
        private int _centerX;
        private int _centerY;
        private const int Scale = 20;

        public CoordinatePlane(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            InitializePlane();
        }

        private void InitializePlane()
        {
            _bitmap = new Bitmap(_pictureBox.Width, _pictureBox.Height);
            _graphics = Graphics.FromImage(_bitmap);
            _centerX = _pictureBox.Width / 2;
            _centerY = _pictureBox.Height / 2;

            DrawGrid();
        }

        public void DrawGrid()
        {
            if (_graphics == null || _bitmap == null) return;

            _graphics.Clear(Color.White);

            // Малювання осей
            using Pen axisPen = new Pen(Color.Black, 2);
            _graphics.DrawLine(axisPen, 0, _centerY, _pictureBox.Width, _centerY);
            _graphics.DrawLine(axisPen, _centerX, 0, _centerX, _pictureBox.Height);

            // Малювання сітки
            using Pen gridPen = new Pen(Color.LightGray, 1);

            // Вертикальні лінії
            for (int x = _centerX; x < _pictureBox.Width; x += Scale)
            {
                _graphics.DrawLine(gridPen, x, 0, x, _pictureBox.Height);
            }
            for (int x = _centerX; x > 0; x -= Scale)
            {
                _graphics.DrawLine(gridPen, x, 0, x, _pictureBox.Height);
            }

            // Горизонтальні лінії
            for (int y = _centerY; y < _pictureBox.Height; y += Scale)
            {
                _graphics.DrawLine(gridPen, 0, y, _pictureBox.Width, y);
            }
            for (int y = _centerY; y > 0; y -= Scale)
            {
                _graphics.DrawLine(gridPen, 0, y, _pictureBox.Width, y);
            }

            // Підписи осей
            using Font font = new Font("Arial", 8);
            using Brush brush = new SolidBrush(Color.Black);

            // Вісь X
            _graphics.DrawString("X", font, brush, _pictureBox.Width - 15, _centerY - 15);
            for (int i = 1; i <= 8; i++)
            {
                int xPos = _centerX + i * Scale;
                _graphics.DrawString(i.ToString(), font, brush, xPos - 5, _centerY + 5);
                _graphics.DrawString((-i).ToString(), font, brush, _centerX - i * Scale - 5, _centerY + 5);
            }

            // Вісь Y
            _graphics.DrawString("Y", font, brush, _centerX + 5, 5);
            for (int i = 1; i <= 8; i++)
            {
                int yPos = _centerY - i * Scale;
                _graphics.DrawString(i.ToString(), font, brush, _centerX + 5, yPos - 5);
                _graphics.DrawString((-i).ToString(), font, brush, _centerX + 5, _centerY + i * Scale - 5);
            }

            _pictureBox.Image = _bitmap;
        }

        public void DrawPoint(Point point, int quadrant)
        {
            if (_graphics == null || _bitmap == null) return;

            // Перетворення координат
            int screenX = _centerX + (int)(point.X * Scale);
            int screenY = _centerY - (int)(point.Y * Scale);

            // Визначення кольору
            Color pointColor = quadrant switch
            {
                1 => Color.Red,
                2 => Color.Blue,
                3 => Color.Green,
                4 => Color.Orange,
                _ => Color.Black
            };

            // Малювання точки
            using (Brush brush = new SolidBrush(pointColor))
            {
                _graphics.FillEllipse(brush, screenX - 5, screenY - 5, 10, 10);
            }

            // Підпис точки
            using (Font font = new Font("Arial", 8))
            using (Brush textBrush = new SolidBrush(pointColor))
            {
                string pointLabel = $"({point.X}, {point.Y})";
                _graphics.DrawString(pointLabel, font, textBrush, screenX + 5, screenY - 15);
            }

            _pictureBox.Image = _bitmap;
        }

        public void ClearPoints()
        {
            DrawGrid();
        }
    }
}