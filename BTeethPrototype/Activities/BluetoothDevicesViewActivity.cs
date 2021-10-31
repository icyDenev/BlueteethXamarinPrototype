using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BTeethPrototype.Adapters;
using BTeethPrototype.Bluetooth;
using static Android.Views.View;

namespace BTeethPrototype.Activities
{
    [Activity(Label = "BluetoothDevicesViewActivity")]
    public class BluetoothDevicesViewActivity : AppCompatActivity, IOnClickListener
    {
        BluetoothDeviceReceiver bluetoothDeviceReceiver;
        RecyclerView rv;
        List<BluetoothDevice> lst;
        Blueteeth.Bluetooth.Bluetooth bt;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.bluetooth_devices);
            bluetoothDeviceReceiver = new BluetoothDeviceReceiver();
            RegisterReceiver(bluetoothDeviceReceiver, new IntentFilter(BluetoothDevice.ActionFound));
            FindViewById<Button>(Resource.Id.back_button).SetOnClickListener(this);
            rv = FindViewById<RecyclerView>(Resource.Id.bluetoothDevicesRecyclerView);
            bt = new Blueteeth.Bluetooth.Bluetooth(this);
            lst = bt.GetPairedDevices();

            rv.SetLayoutManager(new LinearLayoutManager(rv.Context));
            DataAdapterBTD adapter = new DataAdapterBTD(lst);
            rv.SetAdapter(adapter);

        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.back_button)
            {
                StartActivity(new Intent(this, typeof(DashBoard)));
                UnregisterReceiver(bluetoothDeviceReceiver);
                Finish();
            }
        }

    }
}