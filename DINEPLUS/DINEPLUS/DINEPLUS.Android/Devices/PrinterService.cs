using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using DINEPLUS.Droid.Devices;
using DINEPLUS.Interfaces;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(PrinterService))]

namespace DINEPLUS.Droid.Devices
{
    public class PrinterService : IBlueToothPrinterService
    {
        private BluetoothDevice _connectedDevice;

        public PrinterService()
        {
        }

        public List<FldrModel.DeviceInfo> GetAvailableDevices()//nes
        {
            if (BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled)
            {
                List<FldrModel.DeviceInfo> result = new List<FldrModel.DeviceInfo>();
                foreach (var pairedDevice in BluetoothAdapter.DefaultAdapter.BondedDevices)
                {
                    result.Add(new FldrModel.DeviceInfo
                    {
                        Title = pairedDevice.Name,
                        MacAddress = pairedDevice.Address
                    });
                }
                return result;
            }
            return null;
        }
        public FldrModel.DeviceInfo GetCurrentDevice()//
        {
            if (_connectedDevice != null)
            {
                return new FldrModel.DeviceInfo
                {
                    Title = _connectedDevice.Name,
                    MacAddress = _connectedDevice.Address
                };
            }
            return null;
        }
        public bool SetCurrentDevice(string printerName)//
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

        public bool boolBluetoothOn(string strPrinterName)//
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

        public async void testPrint()
        {
            PrinterPAYO PrinterPAYO1 = new PrinterPAYO();
            if (_connectedDevice != null)
            {
                await PrinterPAYO1.PrintTest("plain", _connectedDevice);
            }
            else
            {
                throw new Exception("No selected device.");
            }
        }
        public async void testPrintSO()
        {
            PrinterSO PrinterSO1 = new PrinterSO();
            if (_connectedDevice != null)
            {
                await PrinterSO1.PrintTest("plain", _connectedDevice);
            }
            else
            {
                throw new Exception("No selected device.");
            }
        }
    }
}