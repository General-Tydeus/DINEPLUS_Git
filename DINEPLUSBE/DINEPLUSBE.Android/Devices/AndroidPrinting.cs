using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DINEPLUSBE.FldrReports;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DINEPLUSBE.Droid.Devices
{
    public class AndroidPrinting
    {
        public async Task PrintCCSUM(BluetoothDevice connectedDevice)
        {
            double pridblGrossTotal = 0;
            var Listings = PageRptCollectionSum.Instance.ItemInListView;
            var dateTime = DateTime.Now;
            try
            {
                using (BluetoothSocket socket = connectedDevice.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                {
                    await socket.ConnectAsync();
                    string strLine = "--------------------------------";
                    var e = new EPSON();
                    var buffer = ByteSplicer.Combine(
                                      e.SetStyles(PrintStyle.DoubleHeight | PrintStyle.Bold),
                    e.CenterAlign(), e.PrintLine("CBYTES"),

                    e.CenterAlign(), e.PrintLine("COLLECTION SUMMARY"),
                    e.SetStyles(PrintStyle.None),

                    e.PrintLine("********************************"),
                    e.LeftAlign(), e.PrintLine("From   : " + PageRptCollectionSum.Instance.pristrFromDate),
                    e.LeftAlign(), e.PrintLine("To     : " + PageRptCollectionSum.Instance.pristrToDate),

                    e.PrintLine("********************************"),


                    e.LeftAlign(), e.PrintLine("Date Printed : " + dateTime.ToString("MM/dd/yyyy")),
                    e.LeftAlign(), e.PrintLine("Time Printed : " + dateTime.ToString("hh:mm:ss tt")),
                    e.LeftAlign(), e.PrintLine(strLine),

                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine("DESCRIPTION"),
                    e.SetStyles(PrintStyle.None),
                    e.LeftAlign(), e.PrintLine(strLine));
                    await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    int counters = 0;
                    foreach (var varLoop in Listings)
                    {
                        pridblGrossTotal += varLoop.CAmount;
                        counters++;
                        var EP = new EPSON();

                        var bufferEP = ByteSplicer.Combine(
                                                    EP.RightAlign(), EP.PrintLine(varLoop.RefDoc.PadRight(13) + varLoop.TDate.ToString("MM/dd/yyyy").PadLeft(7) + varLoop.CAmount.ToString("N2").PadLeft(9))
                                                    );
                        await socket.OutputStream.WriteAsync(bufferEP, 0, bufferEP.Length);
                    }
                    var D = new EPSON();
                    var bufferD = ByteSplicer.Combine(
                    D.LeftAlign(), D.PrintLine(strLine)
                        );
                    await socket.OutputStream.WriteAsync(bufferD, 0, bufferD.Length);
                    string strlblTotal = "TOTAL    :";
                    string strlblNOT = "Number of Transactions :";


                    var E = new EPSON();

                    var bufferE = ByteSplicer.Combine(
                        E.SetStyles(PrintStyle.Bold),
                        E.LeftAlign(), E.PrintLine(strlblNOT + counters.ToString().PadLeft(8)),
                        E.LeftAlign(), E.PrintLine(strlblTotal.PadRight(12) + pridblGrossTotal.ToString("N2").PadLeft(20)),
                        E.SetStyles(PrintStyle.None),
                        E.LeftAlign(), E.PrintLine("================================"),
                        E.CenterAlign(), E.PrintLine("PoweredBY CBYTES Programming"),
                        E.CenterAlign(), E.PrintLine("Bacolod City, 6100"),
                        E.CenterAlign(), E.PrintLine("Tel# (034) 703 5016"));
                    await socket.OutputStream.WriteAsync(bufferE, 0, bufferE.Length);
                    socket.OutputStream.WriteByte(0x0A);
                    socket.OutputStream.WriteByte(0x0A);
                    socket.OutputStream.WriteByte(0x0A);
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



















        public async Task PrintSalesSUM(BluetoothDevice connectedDevice)
        {
            double pridblGrossTotal = 0;
            var Listings = PageRptSalesProducts.Instance.ItemInListView;
            var dateTime = DateTime.Now;
            try
            {
                using (BluetoothSocket socket = connectedDevice.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                {
                    await socket.ConnectAsync();
                    string strLine = "--------------------------------";
                    var e = new EPSON();
                    var buffer = ByteSplicer.Combine(
                                      e.SetStyles(PrintStyle.DoubleHeight | PrintStyle.Bold),
                    e.CenterAlign(), e.PrintLine("CBYTES"),

                    e.CenterAlign(), e.PrintLine("PRODUCT SALES SUMMARY"),
                    e.SetStyles(PrintStyle.None),

                    e.PrintLine("********************************"),
                    e.LeftAlign(), e.PrintLine("From   : " + PageRptSalesProducts.Instance.pristrFromDate),
                    e.LeftAlign(), e.PrintLine("To     : " + PageRptSalesProducts.Instance.pristrToDate),

                    e.PrintLine("********************************"),


                    e.LeftAlign(), e.PrintLine("Date Printed : " + dateTime.ToString("MM/dd/yyyy")),
                    e.LeftAlign(), e.PrintLine("Time Printed : " + dateTime.ToString("hh:mm:ss tt")),
                    e.LeftAlign(), e.PrintLine(strLine),

                    e.SetStyles(PrintStyle.Bold),
                    e.PrintLine("DESCRIPTION"),
                    e.SetStyles(PrintStyle.None),
                    e.LeftAlign(), e.PrintLine(strLine));
                    await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    foreach (var varLoop in Listings)
                    {
                        pridblGrossTotal += varLoop.TotalSales;
                        var EP = new EPSON();

                        var bufferEP = ByteSplicer.Combine(
                            EP.SetStyles(PrintStyle.Bold),
                            EP.LeftAlign(), EP.PrintLine(varLoop.ProductDesc),
                            EP.SetStyles(PrintStyle.None),
                            EP.RightAlign(), EP.PrintLine(varLoop.TotalQty + varLoop.TotalSales.ToString("n2").PadLeft(20))
                            );
                        await socket.OutputStream.WriteAsync(bufferEP, 0, bufferEP.Length);
                    }
                    var D = new EPSON();
                    var bufferD = ByteSplicer.Combine(
                    D.LeftAlign(), D.PrintLine(strLine)
                        );
                    await socket.OutputStream.WriteAsync(bufferD, 0, bufferD.Length);
                    string strlblTotal = "TOTAL    :";


                    var E = new EPSON();

                    var bufferE = ByteSplicer.Combine(
                        E.SetStyles(PrintStyle.Bold),
                        E.LeftAlign(), E.PrintLine(strlblTotal.PadRight(12) + pridblGrossTotal.ToString("N2").PadLeft(20)),
                        E.SetStyles(PrintStyle.None),
                        E.LeftAlign(), E.PrintLine("================================"),
                        E.CenterAlign(), E.PrintLine("PoweredBY CBYTES Programming"),
                        E.CenterAlign(), E.PrintLine("Bacolod City, 6100"),
                        E.CenterAlign(), E.PrintLine("Tel# (034) 703 5016"));
                    await socket.OutputStream.WriteAsync(bufferE, 0, bufferE.Length);
                    socket.OutputStream.WriteByte(0x0A);
                    socket.OutputStream.WriteByte(0x0A);
                    socket.OutputStream.WriteByte(0x0A);
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