using DINEPLUSWEBAPI.FldrClass;
using DINEPLUSWEBAPI.FldrModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.Controllers
{
    [ApiController]
    public class VoucherController : ControllerBase
    {
        ClsGetAcctVoucher ClsGetAcctVoucher1 = new ClsGetAcctVoucher();

        int varintTableDoor = 0;
        int number = 0;
        int IntRowNum2 = 1;

        private int priIntVoidExist = 2;
        SqlConnection myconnection;
        SqlCommand mycommand, mycommand2, mycommand3, mycommand4;

        [HttpPost]
        [Route("API/DINEPLUSWEBAPI/Voucher/InsertMain1")]

        public string PostVoucher(ModeltblMain1 ModeltblMain11)
        {

            try
            {
                priIntVoidExist = ClsGetAcctVoucher1.ClsIfVoidExist(ModeltblMain11.Voucher, ModeltblMain11.UserCode, ModeltblMain11.CNCode);

                if (priIntVoidExist == 1)
                {
                    ClsGetAcctVoucher1.ClsDeleteErrorTransaction(ModeltblMain11.Voucher, "1", ModeltblMain11.UserCode, ModeltblMain11.CNCode);
                }
                ClsGetAcctVoucher1.ClsGetTDoor(ModeltblMain11.Voucher);
                varintTableDoor = int.Parse(ClsGetAcctVoucher1.plsTableDoor);
                number = 0;
                while (varintTableDoor == 1 && number <= 20)
                {
                    number = number + 1;
                    Task.Delay(200);
                    varintTableDoor = int.Parse(ClsGetAcctVoucher1.plsTableDoor);
                }
                if (varintTableDoor == 0 && number <= 20)
                {
                    priIntVoidExist = ClsGetAcctVoucher1.ClsIfVoidExist(ModeltblMain11.Voucher, ModeltblMain11.UserCode, ModeltblMain11.CNCode);
                    if (priIntVoidExist == 2)
                    {
                        myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                        myconnection.Open();
                        ClsGetAcctVoucher1.ClsOneTheDoor(ModeltblMain11.Voucher);
                        string SqlStatement = "INSERT INTO tblMain1 (IC, DocNum, Voucher, UserCode, TDate, Reference, ControlNo, Remarks, CashReceived, Serve, CNCode)" +
                                                "Values (@_IC, @_DocNum, @_Voucher, @_UserCode, @_TDate, @_Reference, @_ControlNo, @_Remarks, @_CashReceived, @_Serve, @_CNCode)";
                        mycommand = new SqlCommand(SqlStatement, myconnection);
                        mycommand.Parameters.Add("_IC", SqlDbType.VarChar).Value = ModeltblMain11.Voucher + ModeltblMain11.UserCode + ModeltblMain11.CNCode;
                        mycommand.Parameters.Add("_Voucher", SqlDbType.VarChar).Value = ModeltblMain11.Voucher;
                        mycommand.Parameters.Add("_DocNum", SqlDbType.VarChar).Value = ModeltblMain11.UserCode;
                        mycommand.Parameters.Add("_UserCode", SqlDbType.VarChar).Value = ModeltblMain11.UserCode;
                        mycommand.Parameters.Add("_TDate", SqlDbType.DateTime).Value = ModeltblMain11.TDate;
                        mycommand.Parameters.Add("_Reference", SqlDbType.VarChar).Value = ModeltblMain11.Reference;
                        mycommand.Parameters.Add("_ControlNo", SqlDbType.VarChar).Value = ModeltblMain11.ControlNo;
                        mycommand.Parameters.Add("_Remarks", SqlDbType.VarChar).Value = ModeltblMain11.Remarks;
                        mycommand.Parameters.Add("_CashReceived", SqlDbType.VarChar).Value = ModeltblMain11.CashReceived;
                        mycommand.Parameters.Add("_Serve", SqlDbType.Bit).Value = ModeltblMain11.Serve;
                        mycommand.Parameters.Add("_CNCode", SqlDbType.VarChar).Value = ModeltblMain11.CNCode;

                        mycommand.ExecuteNonQuery();


                        //if (ModeltblMain11.ModelSubtblMain3 != null)
                        //{
                        //    if (ModeltblMain11.ModelSubtblMain3.Count > 0)
                        //    {
                        //        foreach (var vartblMain3 in ModeltblMain11.ModelSubtblMain3)
                        //        {
                        //            string sqlstatement2 = "INSERT INTO tblMain3 (IC, Refer, ActRemarks, Debit, Credit, PA, STCode, SubNameCode, DeptCodeCC, SIT, RowNum, TaxBase)" +
                        //             "Values (@_IC, @_Refer, @_ActRemarks, @_Debit, @_Credit, @_PA, @_STCode, @_SubNameCode, @_DeptCodeCC, @_SIT, @_RowNum, @_TaxBase)";

                        //            mycommand2 = new SqlCommand(sqlstatement2, myconnection);
                        //            mycommand2.Parameters.Add("_IC", SqlDbType.VarChar).Value = ModeltblMain11.Voucher + ModeltblMain11.UserCode + ModeltblMain11.CNCode;
                        //            mycommand2.Parameters.Add("_Refer", SqlDbType.VarChar).Value = vartblMain3.Refer;
                        //            mycommand2.Parameters.Add("_ActRemarks", SqlDbType.VarChar).Value = vartblMain3.ActRemarks;
                        //            mycommand2.Parameters.Add("_Debit", SqlDbType.Money).Value = vartblMain3.Debit;
                        //            mycommand2.Parameters.Add("_Credit", SqlDbType.Money).Value = vartblMain3.Credit;
                        //            mycommand2.Parameters.Add("_PA", SqlDbType.VarChar).Value = vartblMain3.PA;
                        //            mycommand2.Parameters.Add("_SIT", SqlDbType.Bit).Value = vartblMain3.SIT;
                        //            mycommand2.Parameters.Add("_TaxBase", SqlDbType.Bit).Value = vartblMain3.TaxBase;
                        //            mycommand2.Parameters.Add("_STCode", SqlDbType.Char).Value = vartblMain3.STCode;
                        //            mycommand2.Parameters.Add("_SubNameCode", SqlDbType.Char).Value = vartblMain3.SubNameCode;
                        //            mycommand2.Parameters.Add("_DeptCodeCC", SqlDbType.Char).Value = vartblMain3.DeptCodeCC;
                        //            mycommand2.Parameters.Add("_RowNum", SqlDbType.Int).Value = intRowNum++;

                        //            mycommand2.ExecuteNonQuery();
                        //        }
                        //    }
                        //}

                        if (ModeltblMain11.ModelSubtblMain2 != null)
                        {
                            if (ModeltblMain11.ModelSubtblMain2.Count > 0)
                            {
                                foreach (var vartblMain2 in ModeltblMain11.ModelSubtblMain2)
                                {
                                    string sqlstatement2 = "INSERT INTO tblMain2 (IC, StockNumber,  PIn, POut, UP, Cost, Discount, RowNum) " +
                                                        "Values (@_IC, @_StockNumber, @_PIn, @_POut, @_UP, @_Cost, @_Discount, @_RowNum)";
                                    mycommand3 = new SqlCommand(sqlstatement2, myconnection);
                                    mycommand3.Parameters.Add("_IC", SqlDbType.VarChar).Value = ModeltblMain11.Voucher + ModeltblMain11.UserCode + ModeltblMain11.CNCode;
                                    mycommand3.Parameters.Add("_StockNumber", SqlDbType.VarChar).Value = vartblMain2.StockNumber;
                                    mycommand3.Parameters.Add("_PIn", SqlDbType.Money).Value = vartblMain2.PIn;
                                    mycommand3.Parameters.Add("_POut", SqlDbType.Money).Value = vartblMain2.POut;
                                    mycommand3.Parameters.Add("_UP", SqlDbType.Money).Value = vartblMain2.UP;
                                    mycommand3.Parameters.Add("_Cost", SqlDbType.Money).Value = vartblMain2.Cost;
                                    mycommand3.Parameters.Add("_Discount", SqlDbType.Money).Value = vartblMain2.Discount;
                                    mycommand3.Parameters.Add("_RowNum", SqlDbType.Int).Value = IntRowNum2++; // intRowNum++;
                                    mycommand3.ExecuteNonQuery();
                                }
                            }
                        }


                        //string varstrVoucher, string varstrDocNum, string strTransactType, string strUserCode, string strCNCode
                        myconnection.Close();
                        ClsGetAcctVoucher1.ClsFinalize(ModeltblMain11.Voucher, new ClsAutoNumber().VoucherAutoNum(ModeltblMain11.Voucher, ModeltblMain11.CNCode), "1", ModeltblMain11.UserCode, ModeltblMain11.CNCode);
                        ClsGetAcctVoucher1.ClsZeroTheDoor(ModeltblMain11.Voucher);
                        ClsGetAcctVoucher1.ClsDoorMessage(ModeltblMain11.Voucher, "0");//Saved properly
                                                                                       //return new HttpResponseMessage(HttpStatusCode.Created);
                        return "1";
                    }
                    else
                    {
                        ClsGetAcctVoucher1.ClsDeleteErrorTransaction(ModeltblMain11.Voucher, "1", ModeltblMain11.UserCode, ModeltblMain11.CNCode);
                        ClsGetAcctVoucher1.ClsDoorMessage(ModeltblMain11.Voucher, "1");//Transaction not saved
                        return "2";
                    }
                }
                else if (varintTableDoor == 1 && number == 21)
                {
                    ClsGetAcctVoucher1.ClsDoorMessage(ModeltblMain11.Voucher, "2");//Contact your administrator
                    return "3";
                }
                else
                {
                    ClsGetAcctVoucher1.ClsDoorMessage(ModeltblMain11.Voucher, "3");//Transaction not saved
                    return "4";
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
