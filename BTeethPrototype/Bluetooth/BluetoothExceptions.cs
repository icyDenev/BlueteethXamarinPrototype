using System;
using System.Collections.Generic;
using System.Text;

namespace Blueteeth.Bluetooth
{

    public class BluetoothAdapterNotFoundException : Exception
    {
        public BluetoothAdapterNotFoundException() { }

        public BluetoothAdapterNotFoundException(string message) : base(message) { }

        public BluetoothAdapterNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class BluetoothAdapterNotEnabledException : Exception
    {
        public BluetoothAdapterNotEnabledException() { }

        public BluetoothAdapterNotEnabledException(string message) : base(message) { }

        public BluetoothAdapterNotEnabledException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class BluetoothDeviceNotFoundException : Exception
    {
        public BluetoothDeviceNotFoundException() { }

        public BluetoothDeviceNotFoundException(string message) : base(message) { }

        public BluetoothDeviceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

}
