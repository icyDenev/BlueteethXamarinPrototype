using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BTeethPrototype.Models;
using Android.Support.V7.App;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace BTeethPrototype.Adapters
{
    class DataAdapter : RecyclerView.Adapter
    {
        public event EventHandler<DataAdapterClickEventArgs> ItemClick;
        public event EventHandler<DataAdapterClickEventArgs> ItemLongClick;
        List<BrushingHistory> BHsList;
        IRedirectable parentact;
        Context ctx;

        public DataAdapter(List<BrushingHistory> data, Activity act)
        {
            BHsList = data;
            ctx = act;
            parentact = act as IRedirectable;
        }

        private string getColorFromProgress(double pr)
        {
            double r, g, b = 0;
            r = 1.0; g = 0;
            if (pr <= 0.5) g = pr / 0.5;
            else { g = 1; r = (1 - pr) / 0.5; }
            r *= 150; r += 50;
            g *= 150; g += 50;
            string str = "#" + Convert.ToInt32(Math.Round(r)).ToString("X2") + Convert.ToInt32(Math.Round(g)).ToString("X2") + Convert.ToInt32(Math.Round(b)).ToString("X2");
            return str;
        }

        public override int ItemCount => BHsList.Count;

        void OnClick(DataAdapterClickEventArgs args)
        {
            parentact.Redirect(BHsList[args.Position].Id);
        }

        void OnLongClick(DataAdapterClickEventArgs args)
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(ctx);
            Android.App.AlertDialog alert = dialog.Create();
            alert.SetTitle("Atenion");
            alert.SetMessage("You are about to delete a Brushing history permanently, do you want to proceed?");
            alert.SetButton("OK", (c, ev) =>
            {
                BHsList.Remove(BHsList[args.Position]);
                NotifyItemRemoved(args.Position);
                // TODO :  remove the entry from database
            });
            alert.SetButton2("CANCEL", (c, ev) => { });
            alert.Show();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var BH = BHsList[position];

            var holder = viewHolder as DataAdapterViewHolder;
            holder.DateText.Text = "" + BH.StartTime.Day + "." + (BH.StartTime.Month < 10 ? "0" : "") + BH.StartTime.Month + "." + BH.StartTime.Year + " г.";
            holder.DateText1.Text = "" + BH.StartTime.Hour + ":" + (BH.StartTime.Minute < 10 ? "0" : "") + BH.StartTime.Minute + " ч.";
            holder.TimeText.Text = "" + (BH.EndTime - BH.StartTime).Minutes + ":" + ((BH.EndTime - BH.StartTime).Seconds < 10 ? "0" : "") + (BH.EndTime - BH.StartTime).Seconds;
            holder.ProgressText.Text = "" + BH.Progress + "%";
            var drawable = holder.ProgressText.Background.Current as GradientDrawable;
            drawable.SetColor(Color.ParseColor(getColorFromProgress(0.01 * BH.Progress)));
            holder.MovementsText.Text = "" + BH.getMovements();
            drawable = holder.MovementsText.Background.Current as GradientDrawable;
            drawable.SetColor(Color.ParseColor(getColorFromProgress(0.01 * BH.getMovements())));

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;

            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.brushing_histories_row, parent, false);
            var vh = new DataAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }
    }

    public class DataAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView DateText { get; set; }
        public TextView DateText1 { get; set; }
        public TextView TimeText { get; set; }
        public TextView ProgressText { get; set; }
        public TextView MovementsText { get; set; }

        public DataAdapterViewHolder(View itemView, Action<DataAdapterClickEventArgs> clickListener, Action<DataAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            DateText = (TextView)itemView.FindViewById(Resource.Id.bh_layout_date1);
            DateText1 = (TextView)itemView.FindViewById(Resource.Id.bh_layout_date2);
            TimeText = (TextView)itemView.FindViewById(Resource.Id.bh_layout_time);
            ProgressText = (TextView)itemView.FindViewById(Resource.Id.bh_layout_pokr);
            MovementsText = (TextView)itemView.FindViewById(Resource.Id.bh_layout_dvij);

            itemView.Click += (sender, e) => clickListener(new DataAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new DataAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }

    }

    public class DataAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}