using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.Skins;
using DevExpress.XtraEditors;
using System.Drawing;

using PhotoAssistant.Core.Model;
using PhotoAssistant.UI.ViewHelpers;

namespace PhotoAssistant.UI{ 
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.SetCompatibleTextRenderingDefault(false);
            ((DevExpress.LookAndFeel.Design.UserLookAndFeelDefault)DevExpress.LookAndFeel.Design.UserLookAndFeelDefault.Default).LoadSettings(() => { });
            DevExpress.Utils.BrowserEmulationHelper.DisableBrowserEmulation(System.Reflection.Assembly.GetEntryAssembly().GetName().Name);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            CustomSkinHelper.UpdateSkin();
            Application.EnableVisualStyles();
            Application.Run(new MainForm(new DmModel()));
        }
    }
}
