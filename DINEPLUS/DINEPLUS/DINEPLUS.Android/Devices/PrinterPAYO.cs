using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DINEPLUS.FldrExport;
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
          
            var dateTime = DateTime.Now;
            try
            {
                using (BluetoothSocket socket = _connectedDevice.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                {
                    await socket.ConnectAsync(); 
                    switch (pln)
                    {
                        case "plain":
                            string totalOrd = PagePayOrder.Instance.lblTotals.Text;
                            if (totalOrd.StartsWith("₱"))
                            {
                                totalOrd = totalOrd.Substring(1);
                            }
                            string usrname = Preferences.Get("prefUserName", "NA");
                            string strLine = "--------------------------------";
                            var e = new EPSON();
                            var buffer = ByteSplicer.Combine(
                                e.CenterAlign(),
                                e.PrintLine(""),
                                e.PrintLine("Acknowledgement Receipt"),
                                e.PrintLine(""),
                                e.LeftAlign(), e.PrintLine("Order#  : " + $"{PagePayOrder.Instance.lblDocNum.Text}"),
                                e.LeftAlign(), e.PrintLine("Date    : " + dateTime.ToString("MM/dd/yyyy")),
                                e.LeftAlign(), e.PrintLine("Time    : " + dateTime.ToString("hh:mm:ss tt")),
                                e.LeftAlign(), e.PrintLine("Cashier : " + usrname),
                                e.LeftAlign(), e.PrintLine(strLine),
                                e.PrintLine("Items" + "     Qty" + "   Price" + "    Total"),
                                e.LeftAlign(), e.PrintLine(strLine));

                            await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);

                            foreach (var lv in PagePAYO.Instance.listOrders)
                            {
                                double totals = lv.Qty * lv.SellingPrice;

                                var EP = new EPSON();
                                var bufferEP = ByteSplicer.Combine(
                                    EP.LeftAlign(),
                                    EP.PrintLine($"{lv.ProductDesc.PadRight(14)}"),
                                    EP.RightAlign(),
                                    EP.PrintLine($"{lv.Qty.ToString("n2"),5}{lv.SellingPrice.ToString("n2"),7}{totals.ToString("n2"),10}")
                                );

                                //double totals = lv.Qty * lv.SellingPrice;
                                ////-totals.ToString("n2").Length)
                                //var EP = new EPSON();
                                //var bufferEP = ByteSplicer.Combine(
                                //    EP.RightAlign(),
                                //    EP.PrintLine($"{lv.ProductDesc.PadRight(13 + lv.Qty.ToString("n1").Length)}{lv.Qty.ToString("n1").PadRight(10- lv.SellingPrice.ToString("n2").Length)}{lv.SellingPrice.ToString("n2").PadRight(6)}{totals.ToString("n2")}")
                                //);

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
                                D.PrintLine("Powered By : CBytes Computer"),
                                D.PrintLine("Programming Services"),
                                D.PrintLine("Tel. No. : (034)703-5016"),
                                D.PrintLine(""),
                                D.PrintLine("THIS IS NOT AN OFFICIAL RECEIPT")
                            );
                           

                            await socket.OutputStream.WriteAsync(bufferD, 0, bufferD.Length);

                            socket.OutputStream.WriteByte(0x0A); 
                            socket.OutputStream.WriteByte(0x0A); 
                            socket.OutputStream.WriteByte(0x0A);

                            socket.OutputStream.Close();
                            socket.Close();
                            break;

                        case "reprint":
                            //string total = PagePayOrder.Instance.lblTotals.Text;
                            //if (totalOrd.StartsWith("₱"))
                            //{
                            //    totalOrd = totalOrd.Substring(1);
                            //}
                            string user = Preferences.Get("prefUserName", "NA");
                            string oneline = "--------------------------------";
                            var ee = new EPSON();
                            var buffer1 = ByteSplicer.Combine(
                                ee.CenterAlign(),
                                ee.PrintLine(""),
                                ee.PrintLine("---REPRINT---"),
                                ee.PrintLine("Acknowledgement Receipt"),
                                ee.PrintLine(""),
                                ee.LeftAlign(), ee.PrintLine("Order#  : " + $"{PageRP.Instance.lblDocnum.Text}"),
                                ee.LeftAlign(), ee.PrintLine("Date    : " + $"{PageRP.Instance.lblDate.Text}"),
                                ee.LeftAlign(), ee.PrintLine("Time    : " + $"{PageRP.Instance.lblOrderTime.Text}"),
                                ee.LeftAlign(), ee.PrintLine("Cashier : " + $"{PageRP.Instance.lblCashier.Text}"),
                                ee.LeftAlign(), ee.PrintLine(oneline),
                                ee.PrintLine("Items" + "     Qty" + "   Price" + "    Total"),
                                ee.LeftAlign(), ee.PrintLine(oneline));

                            await socket.OutputStream.WriteAsync(buffer1, 0, buffer1.Length);

                            foreach (var lv in PageRP.Instance.data)
                            {
                                double totals = lv.POut * lv.UP;

                                var EP = new EPSON();
                                var bufferEP = ByteSplicer.Combine(
                                    EP.LeftAlign(),
                                    EP.PrintLine($"{lv.ProductDesc.PadRight(14)}"),
                                    EP.RightAlign(),
                                    EP.PrintLine($"{lv.POut.ToString("n2"),5}{lv.UP.ToString("n2"),7}{totals.ToString("n2"),10}")
                                );

                                await socket.OutputStream.WriteAsync(bufferEP, 0, bufferEP.Length);
                            }

                            var DD = new EPSON();

                            var bufferDD = ByteSplicer.Combine(
                                DD.LeftAlign(),
                                DD.PrintLine(oneline),
                                DD.RightAlign(),
                                DD.PrintLine($"{"Discount :"}{PageRP.Instance.lblDisc.Text.PadLeft(22)}"),
                                DD.PrintLine($"{"Total :"}{PageRP.Instance.lblTotals.Text.PadLeft(25)}"),
                                DD.PrintLine($"{"Cash Received :"}{PageRP.Instance.lblCR.Text.PadLeft(17)}"),
                                DD.PrintLine($"{"Change :"}{PageRP.Instance.lblChange.Text.PadLeft(24)}"),
                                DD.PrintLine("================================"),
                                DD.CenterAlign(),
                                DD.PrintLine("Powered By : CBytes Computer"),
                                DD.PrintLine("Programming Services"),
                                DD.PrintLine("Tel. No. : (034)703-5016"),
                                DD.PrintLine(""),
                                DD.PrintLine("THIS IS NOT AN OFFICIAL RECEIPT")
                            );


                            await socket.OutputStream.WriteAsync(bufferDD, 0, bufferDD.Length);

                            socket.OutputStream.WriteByte(0x0A);
                            socket.OutputStream.WriteByte(0x0A);
                            socket.OutputStream.WriteByte(0x0A);

                            socket.OutputStream.Close();
                            socket.Close();
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