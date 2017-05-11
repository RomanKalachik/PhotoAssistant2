using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core.Model {
    public class DmColorLabel : ISupportId {
        public static string RedString { get { return "Red"; } }
        public static string OrangeString { get { return "Orange"; } }
        public static string YellowString { get { return "Yellow"; } }
        public static string GreenString { get { return "Green"; } }
        public static string BlueString { get { return "Blue"; } }
        public static string CyanString { get { return "Cyan"; } }
        public static string PinkString { get { return "Pink"; } }
        public static string PurpleString { get { return "Purple"; } }
        public static string NoneString { get { return "None"; } }

        public static Color Red { get { return Color.Red; } }
        public static Color Orange { get { return Color.Orange; } }
        public static Color Yellow { get { return Color.Yellow; } }
        public static Color Green { get { return Color.Green; } }
        public static Color Blue { get { return Color.Blue; } }
        public static Color Cyan { get { return Color.Cyan; } }
        public static Color Pink { get { return Color.Pink; } }
        public static Color Purple { get { return Color.Purple; } }

        public DmColorLabel() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Int32 ColorValue { get; set; }
        [NotMapped]
        public Color Color {
            get { return Color.FromArgb(ColorValue); }
            set { ColorValue = value.ToArgb(); }
        }

        public override bool Equals(object obj) {
            DmColorLabel model = obj as DmColorLabel;
            return model != null && model.Color == Color;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override string ToString() {
            return Text;
        }
    }
}
