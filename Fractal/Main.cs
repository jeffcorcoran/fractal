using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Fractal
{
    public partial class Main : Form
    {
        double _xZoom = 0.0d;
        double _yZoom = 0.0d;

        double _baseXZoom = 4;
        double _baseYZoom = 4;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            _xZoom = (_baseXZoom / Convert.ToDouble(pbMain.Width));
            _yZoom = (_baseYZoom / Convert.ToDouble(pbMain.Height));

            Bitmap pic = Mandelbrot(2, 2);
            pbMain.Image = pic;

            //pbMain.MouseWheel += PbMain_MouseWheel;
        }

        private void PbMain_MouseWheel(object sender, MouseEventArgs e)
        {
            _xZoom += 1d;
            _yZoom += 1d;
            Bitmap pic = Mandelbrot(2, 2);
            pbMain.Image = pic;
        }

        private Bitmap Mandelbrot(double Xmin, double Ymin)
        {
            Bitmap pic = new Bitmap(pbMain.Width, pbMain.Height);

            double zx = 0;
            double zy = 0;
            double cx = 0;
            double cy = 0;
            double tempzx = 0;

            int loopgo = 0;

            for (int x = 0; x < pic.Width; x++)
            {
                cx = (_xZoom * x) - Math.Abs(Xmin);
                for (int y = 0; y < pic.Height; y++)
                {
                    zx = 0;
                    zy = 0;
                    cy = (_yZoom * y) - Math.Abs(Ymin);
                    loopgo = 0;

                    while (zx * zx + zy * zy <= 4 && loopgo < 1000)
                    {
                        loopgo++;
                        tempzx = zx;
                        zx = (zx * zx) - (zy * zy) + cx;
                        zy = (2 * tempzx * zy) + cy;
                    }

                    if (loopgo != 1000)
                    {
                        pic.SetPixel(x, y, Color.FromArgb(loopgo % 128 * 2, loopgo % 32 * 7, loopgo % 16 * 14));
                    }
                    else
                    {
                        pic.SetPixel(x, y, Color.Black);
                    }
                }
            }

            return pic;
        }

        private void PbMain_MouseEnter(object sender, EventArgs e)
        {
            pbMain.Focus();
        }
    }
}
