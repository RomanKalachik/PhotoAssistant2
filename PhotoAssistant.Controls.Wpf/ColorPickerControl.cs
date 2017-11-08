using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
namespace PhotoAssistant.Controls.Wpf {
    public class ColorPickerControl : ItemsControl {
        static ColorPickerControl() => DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerControl), new FrameworkPropertyMetadata(typeof(ColorPickerControl)));
        public static int GetRowCount(DependencyObject obj) => (int)obj.GetValue(RowCountProperty);
        public static void SetRowCount(DependencyObject obj, int value) => obj.SetValue(RowCountProperty, value);
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.RegisterAttached("RowCount", typeof(int), typeof(ColorPickerControl), new FrameworkPropertyMetadata(5, FrameworkPropertyMetadataOptions.Inherits, (d, e) => OnRowCountChanged(d, e)));
        static void OnRowCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if(d is ColorPickerControl) {
                ((ColorPickerControl)d).UpdateCells();
                return;
            }
            ColorPickerGrid grid = d as ColorPickerGrid;
            if(grid == null || grid.RowDefinitions.Count == (int)e.NewValue) {
                return;
            }

            int count = (int)e.NewValue;
            if(grid.RowDefinitions.Count < count) {
                for(int i = grid.RowDefinitions.Count; i < count; i++) {
                    grid.RowDefinitions.Add(new RowDefinition());
                }
            } else {
                for(int i = grid.RowDefinitions.Count; i > count; i--) {
                    grid.RowDefinitions.RemoveAt(grid.RowDefinitions.Count - 1);
                }
            }
        }
        static void OnColumnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if(d is ColorPickerControl) {
                ((ColorPickerControl)d).UpdateCells();
                return;
            }
            ColorPickerGrid grid = d as ColorPickerGrid;
            if(grid == null || grid.ColumnDefinitions.Count == (int)e.NewValue) {
                return;
            }

            int count = (int)e.NewValue;
            if(grid.ColumnDefinitions.Count < count) {
                for(int i = grid.ColumnDefinitions.Count; i < count; i++) {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }
            } else {
                for(int i = grid.RowDefinitions.Count; i > count; i--) {
                    grid.ColumnDefinitions.RemoveAt(grid.ColumnDefinitions.Count - 1);
                }
            }
        }
        void UpdateCells() {
            colorCells = null;
            ItemsSource = ColorCells;
        }
        List<ColorCellInfo> colorCells;
        public List<ColorCellInfo> ColorCells {
            get {
                if(colorCells == null) {
                    colorCells = CreateColorCells();
                }

                return colorCells;
            }
        }
        List<ColorCellInfo> CreateColorCells() {
            List<ColorCellInfo> res = new List<ColorCellInfo>();
            for(int i = 0; i < RowCount * ColumnCount; i++) {
                res.Add(new ColorCellInfo());
            }

            return res;
        }
        public static int GetColumnCount(DependencyObject obj) => (int)obj.GetValue(ColumnCountProperty);
        public static void SetColumnCount(DependencyObject obj, int value) => obj.SetValue(ColumnCountProperty, value);
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.RegisterAttached("ColumnCount", typeof(int), typeof(ColorPickerControl), new FrameworkPropertyMetadata(5, FrameworkPropertyMetadataOptions.Inherits, (d, e) => OnColumnCountChanged(d, e)));
        public int RowCount {
            get => GetRowCount(this);
            set => SetRowCount(this, value);
        }
        public int ColumnCount {
            get => GetColumnCount(this);
            set => SetColumnCount(this, value);
        }
        public double CellSize {
            get => (double)GetValue(CellSizeProperty);
            set => SetValue(CellSizeProperty, value);
        }
        public Color Color {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorPickerControl), new PropertyMetadata(Colors.Black, new PropertyChangedCallback((d, e) => ((ColorPickerControl)d).OnColorChanged(e))));
        protected virtual void OnColorChanged(DependencyPropertyChangedEventArgs e) {
            RedChannelText = $"{Color.R / 255.0:P1}";
            GreenChannelText = $"{Color.R / 255.0:P1}";
            BlueChannelText = $"{Color.R / 255.0:P1}";
        }
        public string RedChannelText {
            get => (string)GetValue(RedChannelTextProperty);
            set => SetValue(RedChannelTextProperty, value);
        }
        public static readonly DependencyProperty RedChannelTextProperty =
            DependencyProperty.Register("RedChannelText", typeof(string), typeof(ColorPickerControl), new PropertyMetadata(string.Empty));
        public string GreenChannelText {
            get => (string)GetValue(GreenChannelTextProperty);
            set => SetValue(GreenChannelTextProperty, value);
        }
        public static readonly DependencyProperty GreenChannelTextProperty =
            DependencyProperty.Register("GreenChannelText", typeof(string), typeof(ColorPickerControl), new PropertyMetadata(string.Empty));
        public string BlueChannelText {
            get => (string)GetValue(BlueChannelTextProperty);
            set => SetValue(BlueChannelTextProperty, value);
        }
        public static readonly DependencyProperty BlueChannelTextProperty =
            DependencyProperty.Register("BlueChannelText", typeof(string), typeof(ColorPickerControl), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty CellSizeProperty =
            DependencyProperty.Register("CellSize", typeof(double), typeof(ColorPickerControl), new PropertyMetadata(20.0));
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            ((FrameworkElement)element).Width = CellSize;
            ((FrameworkElement)element).Height = CellSize;
        }
    }
    public class ColorPickerColorControl : Control {
        static ColorPickerColorControl() => DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerColorControl), new FrameworkPropertyMetadata(typeof(ColorPickerColorControl)));
        public Color Color {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorPickerColorControl), new PropertyMetadata(Colors.Black, (d, e) => ((ColorPickerColorControl)d).OnColorChanged(e)));
        void OnColorChanged(DependencyPropertyChangedEventArgs e) => ActualColor = Color;
        void Storyboard_Completed(object sender, EventArgs e) {
            Storyboard.Completed -= Storyboard_Completed;
            Storyboard = null;
        }
        protected Storyboard Storyboard {
            get; set;
        }
        public Color ActualColor {
            get => (Color)GetValue(ActualColorProperty);
            set => SetValue(ActualColorProperty, value);
        }
        public static readonly DependencyProperty ActualColorProperty =
            DependencyProperty.Register("ActualColor", typeof(Color), typeof(ColorPickerColorControl), new PropertyMetadata(Colors.Black));
    }
    public class ColorCellInfo : DependencyObject {
        public Color Color {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorCellInfo), new PropertyMetadata(Colors.Black));
    }
    public class ColorPickerGrid : Grid {
        public ColorPickerGrid() {
        }
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved) {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            int index = InternalChildren.IndexOf((UIElement)visualAdded);
            Grid.SetRow((UIElement)visualAdded, index / ColorPickerControl.GetColumnCount(this));
            Grid.SetColumn((UIElement)visualAdded, index % ColorPickerControl.GetColumnCount(this));
        }
    }
}
