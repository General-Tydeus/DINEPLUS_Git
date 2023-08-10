using Android.Bluetooth;
using DINEPLUSBE.Droid.Devices;
using DINEPLUSBE.fldrInterface;
using DINEPLUSBE.FldrModel;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(PrinterService))]
namespace DINEPLUSBE.Droid.Devices
{
    public class PrinterService : IBlueToothPrinterService
    {
        private BluetoothDevice _connectedDevice;
        public PrinterService()
        {
        }

        public List<DeviceInfo> GetAvailableDevices()
        {
            if (BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled)
            {
                List<DeviceInfo> result = new List<DeviceInfo>();
                foreach (var pairedDevice in BluetoothAdapter.DefaultAdapter.BondedDevices)
                {
                    result.Add(new DeviceInfo
                    {
                        Title = pairedDevice.Name,
                        MacAddress = pairedDevice.Address
                    });
                }
                return result;
            }
            return null;
        }
        public DeviceInfo GetCurrentDevice()
        {
            if (_connectedDevice != null)
            {
                return new DeviceInfo
                {
                    Title = _connectedDevice.Name,
                    MacAddress = _connectedDevice.Address
                };
            }
            return null;
        }
        public bool SetCurrentDevice(string printerName)
        {
            if (BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled)
            {
                foreach (var pairedDevice in BluetoothAdapter.DefaultAdapter.BondedDevices)
                {
                    if (pairedDevice.Name == printerName)
                    {
                        _connectedDevice = pairedDevice;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool boolBluetoothOn(string strPrinterName)
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            if (adapter == null)
            {
                return false; //Bluetooth is off
            }
            else if (!adapter.IsEnabled)
            {
                return false; //Bluetooth is off
            }
            else
            {
                BluetoothDevice device1 = (from bd in adapter.BondedDevices where bd.Name == strPrinterName select bd).FirstOrDefault();
                if (device1 == null)
                {
                    return false; //Device is off
                }
                else
                {
                    return true;//OK to print
                }
            }
        }


        public async Task<bool> CheckBlueToothPrinter()
        {
            try
            {
                using (BluetoothSocket socket = _connectedDevice.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                {
                    await socket.ConnectAsync();

                    socket.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }




        public async void PrintCSUM()
        {
            AndroidPrinting PrinterTrans1 = new AndroidPrinting();
            if (_connectedDevice != null)
            {
                await PrinterTrans1.PrintCCSUM(_connectedDevice);
            }
            else
            {
                throw new Exception("No selected device.");
            }
        }


        public async void PrintSalesSUM()
        {
            AndroidPrinting PrinterTrans1 = new AndroidPrinting();
            if (_connectedDevice != null)
            {
                await PrinterTrans1.PrintSalesSUM(_connectedDevice);
            }
            else
            {
                throw new Exception("No selected device.");
            }
        }
    }
}