using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaintDotNet;

namespace PhotoAssistant.UI.View {
    public partial class HistogramControl : UserControl {
        public HistogramControl() {
            DoubleBuffered = true;
            InitializeComponent();
        }
        public HistogramRgb HistogramRgb { get; set; }
        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255,71,71,71)), new Rectangle(0,Height - 100,256, 100));
            DrawChannels(e);

        }
        Color hRedColor = Color.FromArgb(255, 231, 66, 66);
        Color hGreenColor = Color.FromArgb(255, 77, 185, 77);
        Color hBlueColor = Color.FromArgb(255, 48, 96, 184);
        Color hGRColor = Color.FromArgb(255, 232, 221, 71);
        Color hGBColor = Color.FromArgb(255, 27, 207, 207);
        Color hRBColor = Color.FromArgb(255, 243, 34, 209);
        Color hRGBColor = Color.FromArgb(255, 174, 174, 174);


        private void DrawChannels(PaintEventArgs e) {
            Point center = new Point(Width / 2, Height);
            for(int i = 0; i < 256; i++) {
                int max = (int)HistogramRgb.GetMax();
                int normValue = 100;
                int r = (int)(normValue* HistogramRgb.HistogramValues[2][i]/max);
                int g = (int)(normValue * HistogramRgb.HistogramValues[1][i] / max);
                int b = (int)(normValue * HistogramRgb.HistogramValues[0][i] / max);
                int grayLevel = 0;
                int twoColorMixLevel = 0;
                if(r <= g && r <= b) {
                    grayLevel = r;
                    if(g > b) {
                        twoColorMixLevel = b;
                        e.Graphics.DrawLine(new Pen(hGreenColor), new Point(i, Height - twoColorMixLevel), new Point(i, Height - g));
                    } else {
                        twoColorMixLevel = g;
                        e.Graphics.DrawLine(new Pen(hBlueColor), new Point(i, Height - twoColorMixLevel), new Point(i, Height - b));
                    }
                    e.Graphics.DrawLine(new Pen(hGBColor), new Point(i, Height - grayLevel), new Point(i, Height - twoColorMixLevel));
                }
                if(b <= g && b <= r) {
                    grayLevel = b;
                    if(g > r) {
                        twoColorMixLevel = r;
                        e.Graphics.DrawLine(new Pen(hGreenColor), new Point(i, Height - twoColorMixLevel), new Point(i, Height - g));
                    } else {
                        twoColorMixLevel = g;
                        e.Graphics.DrawLine(new Pen(hRedColor), new Point(i, Height - twoColorMixLevel), new Point(i, Height - r));
                    }
                    e.Graphics.DrawLine(new Pen(hGRColor), new Point(i, Height - grayLevel), new Point(i, Height - twoColorMixLevel));
                }
                if(g <= b && g <= r) {
                    grayLevel = g;
                    if(b > r) {
                        twoColorMixLevel = r;
                        e.Graphics.DrawLine(new Pen(hBlueColor), new Point(i, Height - twoColorMixLevel), new Point(i, Height - b));
                    } else {
                        twoColorMixLevel = b;
                        e.Graphics.DrawLine(new Pen(hRedColor), new Point(i, Height - twoColorMixLevel), new Point(i, Height - r));
                    }
                    e.Graphics.DrawLine(new Pen(hRBColor), new Point(i, Height - grayLevel), new Point(i, Height - twoColorMixLevel));
                }
                e.Graphics.DrawLine(new Pen(hRGBColor), new Point(i, Height), new Point(i, Height - grayLevel));
            }
        }
    }
}
