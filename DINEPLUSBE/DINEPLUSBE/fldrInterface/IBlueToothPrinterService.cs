using DINEPLUSBE.FldrModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DINEPLUSBE.fldrInterface
{
    public interface IBlueToothPrinterService
    {
        DeviceInfo GetCurrentDevice();
        List<DeviceInfo> GetAvailableDevices();
        bool SetCurrentDevice(string printerName);
        bool boolBluetoothOn(string PrinterName);
        Task<bool> CheckBlueToothPrinter();
        void PrintCSUM();
        void PrintSalesSUM();
    }
}
