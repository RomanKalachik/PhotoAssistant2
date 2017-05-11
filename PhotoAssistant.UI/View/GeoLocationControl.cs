using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraMap;
using PhotoAssistant.Core.Model;

namespace PhotoAssistant.UI.View {
    public partial class GeoLocationControl : UserControl {
        private static readonly object okClick = new object();
        private static readonly object cancelClick = new object();

        public GeoLocationControl() {
            InitializeComponent();
        }

        DmFile file;
        public DmFile File {
            get { return file; }
            set {
                if(File == value)
                    return;
                file = value;
                OnFileChanged();
            }
        }

        GeoPoint geoPoint;
        public GeoPoint GeoPoint {
            get { return geoPoint; }
            set {
                if(GeoPoint == value)
                    return;
                geoPoint = value;
                OnGeoPointChanged();
            }
        }

        private void OnGeoPointChanged() {
            this.teLattitude.EditValue = GeoPoint.ToString();
        }

        protected bool IsInitializing { get; set; }
        private void OnFileChanged() {
            if(File == null)
                return;
            IsInitializing = true;
            try {
                if(File.Latitude != DmFile.InvalidGeoLocation && File.Longitude != DmFile.InvalidGeoLocation)
                    GeoPoint = new GeoPoint(File.Latitude, File.Longitude);
                else
                    GeoPoint = null;
                if(GeoPoint == null)
                    this.teLattitude.EditValue = null;
                else
                    this.teLattitude.EditValue = GeoPoint.ToString();
                this.teCountry.EditValue = File.Country;
                this.teState.EditValue = File.State;
                this.teCity.EditValue = File.City;
                this.teLocation.EditValue = File.Location;
            } finally {
                IsInitializing = false;
            }
        }

        public Size CalcBestSize() {
            this.layoutControl1.LayoutChanged();
            return this.layoutControl1.Root.MinSize;
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            RaiseCancelClick();
        }

        void RaiseCancelClick() {
            EventHandler handler = Events[cancelClick] as EventHandler;
            if(handler != null)
                handler(this, EventArgs.Empty);
        }

        void RaiseOkClick() {
            EventHandler handler = Events[okClick] as EventHandler;
            if(handler != null)
                handler(this, EventArgs.Empty);
        }

        public event EventHandler CancelClick {
            add { Events.AddHandler(cancelClick, value); }
            remove { Events.RemoveHandler(cancelClick, value); }
        }

        public event EventHandler OkClick {
            add { Events.AddHandler(okClick, value); }
            remove { Events.RemoveHandler(cancelClick, value); }
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            ApplyProperties();
            RaiseOkClick();
        }

        private void ApplyProperties() {
            if(File == null)
                return;
            if(GeoPoint == null) {
                File.Latitude = DmFile.InvalidGeoLocation;
                File.Longitude = DmFile.InvalidGeoLocation;
            }
            File.Country = this.teCountry.Text.Trim();
            File.State = this.teState.Text.Trim();
            File.City = this.teCity.Text.Trim();
            File.Location = this.teLocation.Text.Trim();
        }
    }
}
