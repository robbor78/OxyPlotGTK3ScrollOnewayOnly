using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxyPlotGTK3ScrollOnewayOnly
{
    class Program
    {
        static void Main(string[] args)
        {

            global::Gtk.Application.Init();
            var w = new MainWindow();

            w.Show();
            global::Gtk.Application.Run();
        }
    }

    public class MainWindow : global::Gtk.Window
    {

        public MainWindow() : base(global::Gtk.WindowType.Toplevel)
        {
            //Build();

            var plotView = new CustomPlotView();
            this.Add(plotView);
            plotView.ShowAll();

            var myModel = new OxyPlot.PlotModel { Title = "Example 1" };
            myModel.Series.Add(new OxyPlot.Series.FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            plotView.Model = myModel;
        }

        protected void OnDeleteEvent(object sender, global::Gtk.DeleteEventArgs a)
        {
            global::Gtk.Application.Quit();
            a.RetVal = true;
        }


    }

    public class CustomPlotView : OxyPlot.GtkSharp.PlotView
    {
        protected override bool OnScrollEvent(Gdk.EventScroll e)
        {
            e.Direction = DetermineScrollDirection(e);
            return base.OnScrollEvent(e);
        }

        private Gdk.ScrollDirection DetermineScrollDirection(Gdk.EventScroll e)
        {
            var nativeScrollEvent = (GdkNativeScrollStruct)System.Runtime.InteropServices.Marshal.PtrToStructure(e.Handle, typeof(GdkNativeScrollStruct));
            return nativeScrollEvent.delta_y > 0 ? Gdk.ScrollDirection.Down : Gdk.ScrollDirection.Up;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        struct GdkNativeScrollStruct
        {
            private Gdk.EventType type;
            private IntPtr window;
            private sbyte send_event;
            public uint time;
            public double x;
            public double y;
            public uint state;
            public Gdk.ScrollDirection direction;
            public IntPtr device;
            public double x_root;
            public double y_root;
            public double delta_x;
            public double delta_y;
        }
    }

}
