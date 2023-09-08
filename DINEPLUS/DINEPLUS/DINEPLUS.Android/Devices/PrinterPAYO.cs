using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrPopup;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Java.IO;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DINEPLUS.Droid.Devices
{
    public class PrinterPAYO
    {
        public async Task PrintTest(string pln, BluetoothDevice _connectedDevice)
        {
            string totalOrd = PagePayOrder.Instance.lblTotals.Text;
            if (totalOrd.StartsWith("₱"))
            {
                totalOrd = totalOrd.Substring(1);
            }
            var dateTime = DateTime.Now;
            try
            {
                using (BluetoothSocket socket = _connectedDevice.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                {
                    await socket.ConnectAsync(); 
                    switch (pln)
                    {
                        case "plain":
                            string usrname = Preferences.Get("prefUserName", "NA");
                            string strLine = "--------------------------------";
                            var e = new EPSON();
                            var buffer = ByteSplicer.Combine(
                                e.CenterAlign(),
                                e.PrintLine(""),
                                e.PrintLine("Receipt"),
                                e.PrintLine(""),
                                e.LeftAlign(), e.PrintLine("Order#  : " + $"CS{PagePayOrder.Instance.lblDocNum.Text}"),
                                e.LeftAlign(), e.PrintLine("Date    : " + dateTime.ToString("MM/dd/yyyy")),
                                e.LeftAlign(), e.PrintLine("Time    : " + dateTime.ToString("hh:mm:ss tt")),
                                e.LeftAlign(), e.PrintLine("Cashier : " + usrname),
                                e.LeftAlign(), e.PrintLine(strLine),
                                e.PrintLine("Items" + "        Qty" + " Price" + "    Total"),
                                e.LeftAlign(), e.PrintLine(strLine));

                            await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);

                            foreach (var lv in PagePAYO.Instance.listOrders)
                            {
                                double totals = lv.Qty * lv.SellingPrice;

                                var EP = new EPSON();
                                var bufferEP = ByteSplicer.Combine(
                                    EP.RightAlign(),
                                    EP.PrintLine($"{lv.ProductDesc.PadRight(14)}" +
                                    $"{lv.Qty.ToString().PadRight(3)}" +
                                    $"{lv.SellingPrice.ToString("n2").PadRight(15- totals.ToString("n2").Length)}" +
                                    $"{totals.ToString("n2")}")
                                );

                                await socket.OutputStream.WriteAsync(bufferEP, 0, bufferEP.Length);
                            }

                            var D = new EPSON();
                            double CR = Convert.ToDouble(PagePayOrder.Instance.txtCR.Text);

                            var bufferD = ByteSplicer.Combine(
                                D.LeftAlign(),
                                D.PrintLine(strLine),
                                D.RightAlign(),
                                D.PrintLine($"{"Discount :"}{PagePayOrder.Instance.lblDiscount.Text.PadLeft(22)}"),
                                D.PrintLine($"{"Total :"}{totalOrd.PadLeft(25)}"),
                                D.PrintLine($"{"Cash Received :"}{CR.ToString("n2").PadLeft(17)}"),
                                D.PrintLine($"{"Change :"}{PagePayOrder.Instance.lblChange.Text.PadLeft(24)}"),
                                D.PrintLine("================================"),
                                D.CenterAlign(),
                                D.PrintLine("Powered By : CBytes Computer Programming Services"),
                                D.PrintLine("Tel. No. : (034)703-5016")
                            );
                           

                            await socket.OutputStream.WriteAsync(bufferD, 0, bufferD.Length);

                            socket.OutputStream.WriteByte(0x0A); 
                            socket.OutputStream.WriteByte(0x0A); 
                            socket.OutputStream.WriteByte(0x0A); 
                            break;
                    }
                }
            }
            catch (IOException ex)
            {
                System.Console.WriteLine("IOException: " + ex.Message);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: " + ex.Message);
            }
        }

    }
}