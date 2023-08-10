using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DINEPLUS.Interfaces
{
    public interface IBlueToothPrinterService
    {
        FldrModel.DeviceInfo GetCurrentDevice();
        List<FldrModel.DeviceInfo> GetAvailableDevices();
        bool SetCurrentDevice(string printerName);
        bool boolBluetoothOn(string PrinterName);
        void testPrint();
        void testPrintSO();
        Task<bool> CheckBlueToothPrinter();

    }
}
