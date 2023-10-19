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
        Task<bool> CheckBlueToothPrinter();
        void testPrint();
        void testRePrint();
        void testPrintSO();
        void testPrintSO1();
        void testRePrintSO();

    }
}
