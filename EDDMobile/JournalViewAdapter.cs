using Android.App;
using Android.Views;
using Android.Widget;
using EliteDangerousCore;
using SkiaSharp.Views.Android;
using System.Collections.Generic;

namespace EDDMobile
{
    public class JournalViewAdapter : BaseAdapter<JournalEntry>
    {
        List<JournalEntry> items;
        Activity context;
        public JournalViewAdapter(Activity context, List<JournalEntry> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override JournalEntry this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.custom_view, null);
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.EventTypeStr;
            string info, details;
            item.FillInformation(out info, out details);
            view.FindViewById<TextView>(Resource.Id.Text2).Text = info;
            //TODO: why null??
            if (item.Icon == null)
                return view;

            SkiaSharp.SKImage img = item.Icon;
            
            var canvasvw = view.FindViewById<SKCanvasView>(Resource.Id.Image);
            canvasvw.PaintSurface += (sender, e) => {
                var surface = e.Surface;
                var surfaceSize = e.Info.Size;

                // get the canvas from the view
                var canvas = surface.Canvas;

                // draw the bitmap on the canvas
                canvas.DrawImage(img, 0, 0);

                // draw other stuff
            };
            return view;
        }

    }
}