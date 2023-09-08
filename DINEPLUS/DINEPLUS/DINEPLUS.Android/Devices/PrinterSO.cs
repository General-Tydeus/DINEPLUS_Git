using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DINEPLUS.FldrSO;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Java.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DINEPLUS.Droid.Devices
{
    class PrinterSO
    {
        public async Task PrintTest(string pln, BluetoothDevice _connectedDevice)
        {
            string totalOrd = PagePaySO.Instance.lblTotals.Text;
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
                                e.PrintLine("Official Receipt"),
                                //e.PrintLine(PagePAYO.Instance.listOrders.Count.ToString()),
                                e.PrintLine(""),
                                e.LeftAlign(), e.PrintLine("Table   : " + $"{PagePrevOrder.Instance.lblTableName.Text}"),
                                e.LeftAlign(), e.PrintLine("Order#  : " + $"{PagePrevOrder.Instance.mdlTables11.TableDocNum}"),
                                e.LeftAlign(), e.PrintLine("Date    : " + dateTime.ToString("MM/dd/yyyy")),
                                e.LeftAlign(), e.PrintLine("Time    : " + dateTime.ToString("hh:mm tt")),
                                e.LeftAlign(), e.PrintLine("Cashier : " + usrname),
                                e.LeftAlign(), e.PrintLine(strLine),
                                              e.PrintLine("Items" + "        Qty" + " Price" + "    Total"),
                                e.LeftAlign(), e.PrintLine(strLine));
                            await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);

                            foreach (var lv in PagePrevOrder.Instance.PreviousOrd) 
                            {
                                double totals = lv.POut * lv.UP;

                                var EP = new EPSON();
                                var bufferEP = ByteSplicer.Combine(
                                    EP.RightAlign(),
                                    EP.PrintLine($"{lv.ProductDesc.PadRight(14)}" +
                                    $"{lv.POut.ToString().PadRight(3)}" +
                                    $"{lv.UP.ToString("n2").PadRight(15 - totals.ToString("n2").Length)}" +
                                    $"{totals.ToString("n2")}")
                                );

                                await socket.OutputStream.WriteAsync(bufferEP, 0, bufferEP.Length);
                            }
                            var D = new EPSON();
                            double CR = Convert.ToDouble(PagePaySO.Instance.txtCR.Text);

                            var bufferD = ByteSplicer.Combine(
                               D.LeftAlign(),
                               D.PrintLine(strLine),
                               D.RightAlign(),
                               D.PrintLine($"{"Discount :"}{PagePaySO.Instance.lblDiscount.Text.PadLeft(22)}"),
                               D.PrintLine($"{"Total :"}{totalOrd.PadLeft(25)}"),
                               D.PrintLine($"{"Cash Received :"}{CR.ToString("n2").PadLeft(17)}"),
                               D.PrintLine($"{"Change :"}{PagePaySO.Instance.lblChange.Text.PadLeft(24)}"),
                               D.PrintLine("================================"),
                               D.CenterAlign(),
                               D.PrintLine(""),
                               D.PrintLine("Powered By : CBytes Computer Programming Services"),
                               D.PrintLine("Tel. No. : (034)703-5016")
                           );


                            await socket.OutputStream.WriteAsync(bufferD, 0, bufferD.Length);

                            socket.OutputStream.WriteByte(0x0A); // br 
                            socket.OutputStream.WriteByte(0x0A);
                            socket.OutputStream.WriteByte(0x0A);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: " + ex.Message);
            }
        }
        
        public async Task PrintTest1(string pln, BluetoothDevice _connectedDevice)
        {
            string totalOrd = PagePrevOrder.Instance.lblTotals.Text;
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
                            var heading = "Bill Slip";
                            var buffer = ByteSplicer.Combine(
                                e.CenterAlign(),
                                e.PrintLine(heading),
                                //e.PrintLine(PagePAYO.Instance.listOrders.Count.ToString()),
                                e.PrintLine(""),
                                e.LeftAlign(), e.PrintLine("Table   : " + $"{PagePrevOrder.Instance.lblTableName.Text}"),
                                e.LeftAlign(), e.PrintLine("Date    : " + dateTime.ToString("MM/dd/yyyy")),
                                e.LeftAlign(), e.PrintLine("Time    : " + dateTime.ToString("hh:mm tt")),
                                e.LeftAlign(), e.PrintLine(strLine),
                                              e.PrintLine("Items" + "       Qty" + " Price" + "    Total"),
                                e.LeftAlign(), e.PrintLine(strLine));
                            await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);

                            foreach (var lv in PagePrevOrder.Instance.PreviousOrd) 
                            {
                                double totals = lv.POut * lv.UP;
                         
                                var EP = new EPSON();
                                var bufferEP = ByteSplicer.Combine(
                                    EP.RightAlign(),
                                    EP.PrintLine($"{lv.ProductDesc.PadRight(14)}" +
                                    $"{lv.POut.ToString().PadRight(3)}" +
                                    $"{lv.UP.ToString("n2").PadRight(15 - totals.ToString("n2").Length)}" +
                                    $"{totals.ToString("n2")}")
                                );

                                await socket.OutputStream.WriteAsync(bufferEP, 0, bufferEP.Length);
                            }

                            var D = new EPSON();
                            var bufferD = ByteSplicer.Combine(
                            D.LeftAlign(),
                            D.PrintLine(strLine),
                            D.RightAlign(),
                            D.PrintLine($"{"Total :"}{totalOrd.PadLeft(25)}"),
                            D.PrintLine("================================"),
                            D.CenterAlign(),
                            D.PrintLine("Powered By : CBytes Computer Programming Services"),
                            D.PrintLine("Tel. No. : (034)703-5016")
                                );

                            await socket.OutputStream.WriteAsync(bufferD, 0, bufferD.Length);

                            socket.OutputStream.WriteByte(0x0A); // br 
                            socket.OutputStream.WriteByte(0x0A);
                            socket.OutputStream.WriteByte(0x0A);
                            socket.OutputStream.WriteByte(0x0A);
                            break;
                    }
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                System.Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}