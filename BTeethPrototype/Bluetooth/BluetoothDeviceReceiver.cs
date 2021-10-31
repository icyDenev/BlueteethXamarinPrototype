using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BTeethPrototype.Bluetooth
{
    class BluetoothDeviceReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;

            if (action == BluetoothDevice.ActionFound)
            {
                // Get device
                BluetoothDevice newDevice = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

                
            }
        }
    }
}