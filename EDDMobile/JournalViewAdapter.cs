using Android.App;
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
            var canvasvw = view.FindViewById<ImageView>(Resource.Id.Image);
            
            canvasvw.SetImageBitmap(img.ToBitmap());            
            return view;
        }

    }
}