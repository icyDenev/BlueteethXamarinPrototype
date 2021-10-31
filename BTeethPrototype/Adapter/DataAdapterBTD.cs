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
using Android.Bluetooth;

namespace BTeethPrototype.Adapters
{
    class DataAdapterBTD : RecyclerView.Adapter
    {
        public event EventHandler<DataAdapterClickEventArgs> ItemClick;
        public event EventHandler<DataAdapterClickEventArgs> ItemLongClick;
        List<BluetoothDevice> BHsList;

        public DataAdapterBTD(List<BluetoothDevice> data)
        {
            BHsList = data;
        }

        public override int ItemCount => BHsList.Count;

        void OnClick(DataAdapterClickEventArgs args)
        {

        }

        void OnLongClick(DataAdapterClickEventArgs args)
        {
            
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var BH = BHsList[position];

            var holder = viewHolder as DataAdapterViewHolderBTD;
            holder.DeviceName.Text = BH.Name;

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;

            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.bluetooth_devices_row, parent, false);
            var vh = new DataAdapterViewHolderBTD(itemView, OnClick, OnLongClick);
            return vh;
        }
    }

    public class DataAdapterViewHolderBTD : RecyclerView.ViewHolder
    {
        public TextView DeviceName { get; set; }

        public DataAdapterViewHolderBTD(View itemView, Action<DataAdapterClickEventArgs> clickListener, Action<DataAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            DeviceName = (TextView)itemView.FindViewById(Resource.Id.bd_layout_devname);

            itemView.Click += (sender, e) => clickListener(new DataAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new DataAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }

    }
}