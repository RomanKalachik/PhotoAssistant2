using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DevExpress.Xpf.Core;

namespace PhotoAssistant.Controls.Wpf {

    public class PageTransferControlEx : ContentControl {
        public static readonly RoutedEvent ActualContentChangedEvent;

        static PageTransferControlEx() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PageTransferControlEx), new FrameworkPropertyMetadata(typeof(PageTransferControlEx)));
            ActualContentChangedEvent = EventManager.RegisterRoutedEvent("ActualContentChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(PageTransferControlEx));
        }

        ContentPresenter ContentPresenter { get; set; }
        FrameworkElement RootObject { get; set; }
        ContentPresenter PrevContentPresenter { get; set; }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            ContentPresenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");
            RootObject = (FrameworkElement)GetTemplateChild("PART_RootObject");
            PrevContentPresenter = (ContentPresenter)GetTemplateChild("PART_PrevContentPresenter");
        }

        public object ActualContent {
            get { return (object)GetValue(ActualContentProperty); }
            set { SetValue(ActualContentProperty, value); }
        }

        public static readonly DependencyProperty ActualContentProperty =
            DependencyProperty.Register("ActualContent", typeof(object), typeof(PageTransferControlEx), new PropertyMetadata(null));



        public object PrevContent {
            get { return (object)GetValue(PrevContentProperty); }
            set { SetValue(PrevContentProperty, value); }
        }

        public static readonly DependencyProperty PrevContentProperty =
            DependencyProperty.Register("PrevContent", typeof(object), typeof(PageTransferControlEx), new PropertyMetadata(null));



        public event RoutedEventHandler ActualContentChanged {
            add { this.AddHandler(ActualContentChangedEvent, value); }
            remove { this.RemoveHandler(ActualContentChangedEvent, value); }
        }

        protected override void OnContentChanged(object oldContent, object newContent) {
            base.OnContentChanged(oldContent, newContent);

            if(oldContent == null) {
                ActualContent = Content;
                return;
            }

            PrevContent = oldContent;
            Dispatcher.BeginInvoke(new Action(() => {
                PrevContentPresenter.Opacity = 1.0;
                ActualContent = Content;
                RunAnimation();
            }));
        }

        private void RunAnimation() {
            if(IsAnimated) {
                LastStoryboard.Stop();
                LastStoryboard.Completed -= st_Completed;
            }
            Storyboard st = new Storyboard();
            DoubleAnimation opacity = new DoubleAnimation() { From = 1.0, To = 0.0, FillBehavior = System.Windows.Media.Animation.FillBehavior.Stop, BeginTime = TimeSpan.FromMilliseconds(500), Duration = TimeSpan.FromMilliseconds(200) };
            Storyboard.SetTarget(opacity, PrevContentPresenter);
            Storyboard.SetTargetProperty(opacity, new PropertyPath(FrameworkElement.OpacityProperty));
            DoubleAnimation x = new DoubleAnimation() { From = 0.0, To = -100.0, BeginTime = TimeSpan.FromMilliseconds(0), Duration = TimeSpan.FromMilliseconds(700) };
            DoubleAnimation y = new DoubleAnimation() { From = 0.0, To = 30.0, BeginTime = TimeSpan.FromMilliseconds(0), Duration = TimeSpan.FromMilliseconds(700) };
            x.EasingFunction = new BackEase() { Amplitude = 1.0, EasingMode = EasingMode.EaseIn };
            y.EasingFunction = new BackEase() { Amplitude = 1.0, EasingMode = EasingMode.EaseIn };
            RegisterName("PART_TranslateTransform", PrevContentPresenter.RenderTransform);
            Storyboard.SetTargetName(x, "PART_TranslateTransform");
            Storyboard.SetTargetProperty(x, new PropertyPath(TranslateTransform.XProperty));
            Storyboard.SetTargetName(y, "PART_TranslateTransform");
            Storyboard.SetTargetProperty(y, new PropertyPath(TranslateTransform.YProperty));
            st.Children.Add(opacity);
            st.Children.Add(x);
            st.Children.Add(y);
            st.Completed += st_Completed;
            IsAnimated = true;
            LastStoryboard = st;
            st.Begin(this);
        }

        bool IsAnimated { get; set; }
        Storyboard LastStoryboard { get; set; }
        void st_Completed(object sender, EventArgs e) {
            IsAnimated = false;
            PrevContentPresenter.Opacity = 0.0;
            LastStoryboard.Completed -= st_Completed;
        }
    }
}
