using Android.Bluetooth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Java.Util;
using System.Threading.Tasks;
using Android.App;
using Android.Content;

namespace Blueteeth.Bluetooth
{

    public class Bluetooth
    {
        BluetoothAdapter bta;
        BluetoothSocket _socket;

        public Bluetooth(Activity activity)
        {
            bta = BluetoothAdapter.DefaultAdapter;
            try
            {
                if (bta == null) throw new BluetoothAdapterNotFoundException("No Bluetooth adapter found.");
                if (!bta.IsEnabled) throw new BluetoothAdapterNotEnabledException("Bluetooth adapter is not enabled.");
            }
            catch
            {
                Context context = activity;
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(context);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Atenion");
                alert.SetMessage("You are about to delete a Brushing history permanently, do you want to proceed?");
                alert.SetButton("OK", (c, ev) =>
                {
                });
                alert.SetButton2("CANCEL", (c, ev) => { });
                alert.Show();
            }
        }

        public List<BluetoothDevice> GetPairedDevices()
        {
            List<BluetoothDevice> lst = new List<BluetoothDevice>();
            if (BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled)
            {
                foreach (BluetoothDevice pairedDevice in BluetoothAdapter.DefaultAdapter.BondedDevices)
                {
                    lst.Add(pairedDevice);
                }
            }
            return lst;
        }
        
        public async void Connect()
        {
            BluetoothDevice device = (from bd in bta.BondedDevices where bd.Name == "Ble-Nano" select bd).FirstOrDefault();
            if (device == null) throw new BluetoothDeviceNotFoundException("Named device not found.");
            _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
            await _socket.ConnectAsync().ConfigureAwait(false);
        }
        
        public async Task<string> ReadAsync()
        {
            byte[] buffer = null;
            await _socket.InputStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
            return buffer.ToString();
        } 
        
        public async void WriteAsync(byte[] buffer) 
        {
            await _socket.OutputStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
        }

    }

}