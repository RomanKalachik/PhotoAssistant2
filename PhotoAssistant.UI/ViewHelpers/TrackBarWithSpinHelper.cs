
using DevExpress.XtraEditors;
using PhotoAssistant.UI.View.EditingControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.UI.ViewHelpers {
    //public class TrackBarWithSpinHelper {
    //    public TrackBarWithSpinHelper(ColorTrackBarControl trackBar, ScrollableSpinEdit spinEdit) {
    //        TrackBar = trackBar;
    //        SpinEdit = spinEdit;
    //        TrackBar.ValueChanged += TrackBar_ValueChanged;
    //        SpinEdit.ValueChanged += SpinEdit_ValueChanged;
    //    }

    //    protected bool SuppressValueChanged { get; set; }
    //    private void SpinEdit_ValueChanged(object sender, EventArgs e) {
    //        if(SuppressValueChanged)
    //            return;
    //        SuppressValueChanged = true;
    //        try {
    //            TrackBar.Values[0] = (int)(Convert.ToDouble(SpinEdit.Value) / Delta);
    //            TrackBar.Invalidate();
    //            TrackBar.Update();
    //            RaiseValueChanged();
    //        }
    //        finally {
    //            SuppressValueChanged = false;
    //        }
    //    }

    //    private void RaiseValueChanged() {
    //        if(ValueChanged != null)
    //            ValueChanged(this, EventArgs.Empty);
    //    }

    //    private void TrackBar_ValueChanged(object sender, EventArgs e) {
    //        if(SuppressValueChanged)
    //            return;
    //        SuppressValueChanged = true;
    //        try {
    //            SpinEdit.Value = new decimal(TrackBar.Values[0] * Delta);
    //            RaiseValueChanged();
    //        }
    //        finally {
    //            SuppressValueChanged = false;
    //        }
    //    }

    //    public float Value {
    //        get { return (float)Convert.ToDouble(SpinEdit.Value); }
    //        set {
    //            if(Value == value)
    //                return;
    //            SpinEdit.Value = new decimal(value);
    //        }
    //    }

    //    float delta = 1.0f;
    //    public float Delta {
    //        get { return delta; }
    //        set {
    //            if(Delta == value)
    //                return;
    //            delta = value;
    //            OnDeltaChanged();
    //        }
    //    }

    //    protected virtual void UpdateProperties() {
    //        float multiplier = Delta >= 1.0f ? 1.0f : 1.0f / Delta;
    //        SpinEdit.Properties.MaxValue = new decimal(Maximum);
    //        TrackBar.Properties.Maximum = (int)(Maximum * multiplier);
    //        SpinEdit.Properties.MinValue = new decimal(Minimum);
    //        TrackBar.Properties.Minimum = (int)(Minimum * multiplier);
    //    }

    //    protected virtual void OnDeltaChanged() {
    //        UpdateProperties();
    //    }

    //    float maximum = 10.0f;
    //    public float Maximum {
    //        get { return maximum; }
    //        set {
    //            if(Maximum == value)
    //                return;
    //            maximum = value;
    //            OnMaximumChanged();
    //        }
    //    }

    //    protected virtual void OnMaximumChanged() {
    //        UpdateProperties();
    //    }

    //    protected virtual void OnMinimumChanged() {
    //        UpdateProperties();
    //    }

    //    float minimum = 0.0f;
    //    public float Minimum {
    //        get { return minimum; }
    //        set {
    //            if(Minimum == value)
    //                return;
    //            minimum = value;
    //            OnMinimumChanged();
    //        }
    //    }

    //    public ColorTrackBarControl TrackBar { get; private set; }
    //    public ScrollableSpinEdit SpinEdit { get; private set; }

    //    public event EventHandler ValueChanged;
        
    //}
}
