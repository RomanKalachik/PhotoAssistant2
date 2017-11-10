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
using DevExpress.Utils.Drawing;

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
            using(GraphicsCache cache = new GraphicsCache(e)) {
                Point center = new Point(Width / 2, Height);
                for(int i = 0; i < 256; i++) {
                    int max = (int)HistogramRgb.GetMax();
                    int normValue = 100;
                    int r = (int)(normValue * HistogramRgb.HistogramValues[2][i] / max);
                    int g = (int)(normValue * HistogramRgb.HistogramValues[1][i] / max);
                    int b = (int)(normValue * HistogramRgb.HistogramValues[0][i] / max);
                    int grayLevel = 0;
                    int twoColorMixLevel = 0;
                    if(r <= g && r <= b) {
                        grayLevel = r;
                        if(g > b) {
                            twoColorMixLevel = b;
                            cache.DrawLine(new Point(i, Height - twoColorMixLevel), new Point(i, Height - g), hGreenColor, 1);
                        }
                        else {
                            twoColorMixLevel = g;
                            cache.DrawLine(new Point(i, Height - twoColorMixLevel), new Point(i, Height - b), hBlueColor, 1);
                        }
                        cache.DrawLine(new Point(i, Height - grayLevel), new Point(i, Height - twoColorMixLevel), hGBColor, 1);
                    }
                    if(b <= g && b <= r) {
                        grayLevel = b;
                        if(g > r) {
                            twoColorMixLevel = r;
                            cache.DrawLine(new Point(i, Height - twoColorMixLevel), new Point(i, Height - g), hGreenColor , 1);
                        }
                        else {
                            twoColorMixLevel = g;
                            cache.DrawLine(new Point(i, Height - twoColorMixLevel), new Point(i, Height - r), hRedColor, 1);
                        }
                        cache.DrawLine(new Point(i, Height - grayLevel), new Point(i, Height - twoColorMixLevel), hGRColor, 1);
                    }
                    if(g <= b && g <= r) {
                        grayLevel = g;
                        if(b > r) {
                            twoColorMixLevel = r;
                            cache.DrawLine(new Point(i, Height - twoColorMixLevel), new Point(i, Height - b), hBlueColor, 1);
                        }
                        else {
                            twoColorMixLevel = b;
                            cache.DrawLine(new Point(i, Height - twoColorMixLevel), new Point(i, Height - r), hRedColor, 1);
                        }
                        cache.DrawLine(new Point(i, Height - grayLevel), new Point(i, Height - twoColorMixLevel), hRBColor, 1);
                    }
                    cache.DrawLine(new Point(i, Height), new Point(i, Height - grayLevel), hRGBColor, 1);
                }
            }
        }
    }
}
