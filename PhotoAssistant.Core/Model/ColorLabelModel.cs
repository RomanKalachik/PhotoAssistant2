using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
namespace PhotoAssistant.Core.Model {
    public class DmColorLabel : ISupportId {
        public static string RedString => "Red";
        public static string OrangeString => "Orange";
        public static string YellowString => "Yellow";
        public static string GreenString => "Green";
        public static string BlueString => "Blue";
        public static string CyanString => "Cyan";
        public static string PinkString => "Pink";
        public static string PurpleString => "Purple";
        public static string NoneString => "None";
        public static Color Red => Color.Red;
        public static Color Orange => Color.Orange;
        public static Color Yellow => Color.Yellow;
        public static Color Green => Color.Green;
        public static Color Blue => Color.Blue;
        public static Color Cyan => Color.Cyan;
        public static Color Pink => Color.Pink;
        public static Color Purple => Color.Purple;
        public DmColorLabel() => Id = Guid.NewGuid();

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id {
            get; set;
        }
        public string Text {
            get; set;
        }
        public int ColorValue {
            get; set;
        }
        [NotMapped]
        public Color Color {
            get => Color.FromArgb(ColorValue);
            set => ColorValue = value.ToArgb();
        }
        public override bool Equals(object obj) {
            DmColorLabel model = obj as DmColorLabel;
            return model != null && model.Color == Color;
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => Text;
    }
}
