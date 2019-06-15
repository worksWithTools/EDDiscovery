﻿using Android.App;
using Android.Views;
using Android.Widget;
using EliteDangerousCore;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EDDMobile
{
    public class JournalViewAdapter : BaseAdapter<JournalEntry>
    {
        Activity context;
        private ObservableCollection<JournalEntry> items;

        public JournalViewAdapter(Activity context, ObservableCollection<JournalEntry> journalrepo)
            : base()
        {
            this.context = context;
            items = journalrepo;
            items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyDataSetChanged();
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
            view.FindViewById<TextView>(Resource.Id.eventTime).Text = item.EventTimeLocal.ToString("dd/MM/yyyy \nhh:mm:ss");
            string info, details;
            item.FillInformation(out info, out details);
            view.FindViewById<TextView>(Resource.Id.Text2).Text = info;


            SkiaSharp.SKImage img = item.Icon;
            var canvasvw = view.FindViewById<SKCanvasView>(Resource.Id.Image);
            
            //TODO: Something isn't right...
            // The images drawn do not match with the text..
            // possible side effect of reuse?
            canvasvw.PaintSurface += (sender, e) => {
                var surface = e.Surface;
                var pictureFrame = SKRect.Create(0, 0, 96, 96);
                var imageSize = new SKSize(img.Width, img.Height); // eg: 100x200
                var dest = pictureFrame.AspectFit(imageSize); // fit the size inside the rect

                // draw the image
                var paint = new SKPaint
                {
                    FilterQuality = SKFilterQuality.High // high quality scaling
                };
                // get the canvas from the view
                var canvas = surface.Canvas;

                // draw the bitmap on the canvas
                canvas.Clear();
                canvas.DrawImage(img, dest, paint);

                // draw other stuff
            };
            return view;
        }

    }
}