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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DINEPLUS.Droid.Devices
{
    class PrinterSO
    {
        private double pridblGrossTotal = 0;
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

                            string usrname = Preferences.Get("prefUserName", "NA");
                            string strLine = "--------------------------------";
                            var e = new EPSON();
                            var buffer = ByteSplicer.Combine(
                                e.CenterAlign(),
                                e.PrintLine("Official Receipt"),
                                //e.PrintLine(PagePAYO.Instance.listOrders.Count.ToString()),
                                e.PrintLine(""),
                                e.LeftAlign(), e.PrintLine("Order# : " + $"CS{PagePrevOrder.Instance.mdlTables11.TableDocNum}"),
                                e.LeftAlign(), e.PrintLine("Date   : " + dateTime.ToString("MM/dd/yyyy")),
                                e.LeftAlign(), e.PrintLine("Time   : " + dateTime.ToString("hh:mm:ss tt")),
                                e.LeftAlign(), e.PrintLine("Cashier : " + usrname),
                                e.LeftAlign(), e.PrintLine(strLine),
                                              e.PrintLine("Time" + "Items" + "Qty".PadLeft(17) + "  Price".PadLeft(5)),
                                e.LeftAlign(), e.PrintLine(strLine));
                            await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);

                            foreach (var lv in PagePrevOrder.Instance.listItems) //new command
                            {
                                //var EP = new EPSON();
                                //var bufferEP = ByteSplicer.Combine(
                                //EP.LeftAlign(), EP.PrintLine(lv.ProductDesc + lv.SellingPrice.ToString("n2").PadLeft(10 - lv.ProductDesc.Length) + lv.Qty.ToString("").PadLeft(10 - lv.ProductDesc.Length))
                                //    );
                                //await socket.OutputStream.WriteAsync(bufferEP, 0, bufferEP.Length);

                                var EP = new EPSON();
                                var bufferEP = ByteSplicer.Combine(
                                        EP.LeftAlign(),
                                          EP.PrintLine($"{lv.OrderTime} {lv.ProductDesc} {lv.POut.ToString("").PadLeft(20 - lv.ProductDesc.Length)} {lv.UP.ToString("n2").PadLeft(8 + lv.POut.ToString("").Length)}")
                                          );

                                await socket.OutputStream.WriteAsync(bufferEP, 0, bufferEP.Length);

                            }
                            var D = new EPSON();
                            double CR = Convert.ToDouble(PagePaySO.Instance.txtCR.Text);

                            var bufferD = ByteSplicer.Combine(
                            D.LeftAlign(),
                            D.PrintLine(strLine),
                            D.PrintLine("Total : " + PagePaySO.Instance.lblTotals.Text.PadLeft(30 - (PagePaySO.Instance.lblTotals.Text.Length))),
                            D.PrintLine("Cash Recieved : " + CR.ToString("n2").PadLeft(18 - (CR.ToString().Length))),
                            D.PrintLine("Change : " + PagePaySO.Instance.lblChange.Text.PadLeft(27 - (PagePaySO.Instance.lblChange.Text).Length)),
                            D.PrintLine("================================"),
                            D.PrintLine("Powered By : CBytes")
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
                throw ex;
            }
        }
    }
}