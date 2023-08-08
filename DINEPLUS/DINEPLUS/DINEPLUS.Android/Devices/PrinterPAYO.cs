using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DINEPLUS.FldrPopup;
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
    public class PrinterPAYO
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
                                e.PrintLine("Receipt"),
                                e.PrintLine(""),
                                e.LeftAlign(), e.PrintLine("Order# : " + "00001"),
                                e.LeftAlign(), e.PrintLine("Date   : " + dateTime.ToString("MM/dd/yyyy")),
                                e.LeftAlign(), e.PrintLine("Time   : " + dateTime.ToString("hh:mm:ss tt")),
                                e.LeftAlign(), e.PrintLine("Waiter : " + usrname),
                                e.LeftAlign(), e.PrintLine(strLine),
                                               e.PrintLine("Items" + "Qty".PadLeft(26)),
                                e.LeftAlign(), e.PrintLine(strLine));
                            await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);

                            //byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes(aa);
                            //await socket.OutputStream.WriteAsync(messageBytes, 0, messageBytes.Length); //old command
                            //foreach (var lv in xamlProdListView.Instance.listPrinting) //new command
                            //{
                            //    var EP = new EPSON();
                            //    var bufferEP = ByteSplicer.Combine(
                            //        EP.RightAlign(), EP.PrintLine(lv.FPDesc + lv.FPQty.ToString("n2").PadLeft(32 - lv.FPDesc.Length))
                            //        );
                            //    await socket.OutputStream.WriteAsync(bufferEP, 0, bufferEP.Length);
                            //}
                            var D = new EPSON();
                            var bufferD = ByteSplicer.Combine(
                            D.LeftAlign(),
                            D.PrintLine("================================")
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