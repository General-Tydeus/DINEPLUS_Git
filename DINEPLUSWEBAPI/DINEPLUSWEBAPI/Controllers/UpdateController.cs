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
    public class UpdateController : ControllerBase
    {
        public string strIC;

        int varintTableDoor = 0;
        int number = 0;
        int IntRowNum2 = 1;

        private int priIntVoidExist = 2;

        ClsGetAcctVoucher ClsGetAcctVoucher1 = new ClsGetAcctVoucher();

        SqlConnection myconnection;
        SqlCommand mycommand, mycommand2, mycommand3, mycommand4;
        SqlDataReader dr;

        [HttpPut]
        [Route("API/DINEPLUSWEBAPI/UpdateTblStatus")]
        public string VoidVoucher(ModeltblMain1 MdlVoidMain11)
        {
            try
            {
                string SqlStatement = $"UPDATE tblEntryTables SET Status=@_Status, TableDocNum=@_TableDocNum WHERE TableCode = '{MdlVoidMain11.TableCode}'";
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand(SqlStatement, myconnection);
                mycommand.Parameters.Add("_Status", SqlDbType.VarChar).Value = MdlVoidMain11.TableDesc;
                mycommand.Parameters.Add("_TableDocNum", SqlDbType.VarChar).Value = MdlVoidMain11.TableDocNum;
                mycommand.ExecuteNonQuery();
                myconnection.Close();
                return "1";
            }
         
             catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [Route("API/DINEPLUSWEBAPI/UpdateMain1")]
        public string UpdateMain1(ModeltblMain1 ModeltblMain11)
        {
            try
            {
                strIC = new ClsDuplicate().CheckMain1(ModeltblMain11.GUID);
                string SqlStatement = $"UPDATE tblMain1 SET CashReceived=@_CashReceived, CAmount=@_CAmount, Discount=@_Discount, Serve=@_Serve WHERE GUID = '{ModeltblMain11.GUID}'";
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand(SqlStatement, myconnection);
                mycommand.Parameters.Add("_Serve", SqlDbType.Bit).Value = 1;
                mycommand.Parameters.Add("_CashReceived", SqlDbType.Money).Value = ModeltblMain11.CashReceived;
                mycommand.Parameters.Add("_CAmount", SqlDbType.Money).Value = ModeltblMain11.CAmount;
                mycommand.Parameters.Add("_Discount", SqlDbType.Money).Value = ModeltblMain11.Discount;
                mycommand.ExecuteNonQuery();
                if (ModeltblMain11.ModelSubtblMain2 != null)
                {
                   string res = new ClsDuplicate().DeleteMain2(strIC);
                   Task.Delay(100);    
                    if (ModeltblMain11.ModelSubtblMain2.Count > 0)
                    {
                        foreach (var vartblMain2 in ModeltblMain11.ModelSubtblMain2)
                        {
                            string sqlstatement2 = "INSERT INTO tblMain2 (IC, StockNumber,  PIn, POut, UP, Cost, Discount,  OrderTime, RowNum) " +
                                                "Values (@_IC, @_StockNumber, @_PIn, @_POut, @_UP, @_Cost, @_Discount,  @_OrderTime, @_RowNum)";
                            mycommand3 = new SqlCommand(sqlstatement2, myconnection);
                            mycommand3.Parameters.Add("_IC", SqlDbType.VarChar).Value = strIC;
                            mycommand3.Parameters.Add("_StockNumber", SqlDbType.VarChar).Value = vartblMain2.StockNumber;
                            mycommand3.Parameters.Add("_PIn", SqlDbType.Money).Value = vartblMain2.PIn;
                            mycommand3.Parameters.Add("_POut", SqlDbType.Money).Value = vartblMain2.POut;
                            mycommand3.Parameters.Add("_UP", SqlDbType.Money).Value = vartblMain2.UP;
                            mycommand3.Parameters.Add("_Cost", SqlDbType.Money).Value = vartblMain2.Cost;
                            mycommand3.Parameters.Add("_Discount", SqlDbType.Money).Value = vartblMain2.Discount;
                            mycommand3.Parameters.Add("_OrderTime", SqlDbType.VarChar).Value = vartblMain2.OrderTime;
                            mycommand3.Parameters.Add("_RowNum", SqlDbType.Int).Value = IntRowNum2++;
                            //mycommand3.Parameters.Add("_RowNum", SqlDbType.Int).Value = new ClsDuplicate().CheckRowMain2(strIC); // intRowNum++;
                            mycommand3.ExecuteNonQuery();
                        }
                    }
                }
                myconnection.Close();

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        [HttpPost]
        [Route("API/DINEPLUSWEBAPI/ExportMain1")]
        public string ExportMain1(ModeltblMain1 ModeltblMain11)
        {
            try
            {
                //foreach (var ModeltblMain11 in main1)
                //{

                    strIC = new ClsDuplicate().CheckMain1(ModeltblMain11.GUID);
                    if (strIC != "0")// guid exist
                    {
                    string SqlStatement = $"UPDATE tblMain1 SET CashReceived=@_CashReceived, CAmount=@_CAmount, Discount=@_Discount, Serve=@_Serve WHERE GUID = '{ModeltblMain11.GUID}'";
                    myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                    myconnection.Open();
                    mycommand = new SqlCommand(SqlStatement, myconnection);
                    mycommand.Parameters.Add("_Serve", SqlDbType.Bit).Value = ModeltblMain11.Serve;
                    mycommand.Parameters.Add("_CashReceived", SqlDbType.Money).Value = ModeltblMain11.CashReceived;
                    mycommand.Parameters.Add("_CAmount", SqlDbType.Money).Value = ModeltblMain11.CAmount;
                    mycommand.Parameters.Add("_Discount", SqlDbType.Money).Value = ModeltblMain11.Discount;
                    mycommand.ExecuteNonQuery();
                    if (ModeltblMain11.ModelSubtblMain2 != null)
                    {
                        //string res = new ClsDuplicate().DeleteMain2(strIC);
                        //Task.Delay(100);
                        if (ModeltblMain11.ModelSubtblMain2.Count > 0)
                        {
                            foreach (var vartblMain2 in ModeltblMain11.ModelSubtblMain2)
                            {
                                string sqlstatement2 = "INSERT INTO tblMain2 (IC, StockNumber,  PIn, POut, UP, Cost, Discount,  OrderTime, RowNum) " +
                                                    "Values (@_IC, @_StockNumber, @_PIn, @_POut, @_UP, @_Cost, @_Discount,  @_OrderTime, @_RowNum)";
                                mycommand3 = new SqlCommand(sqlstatement2, myconnection);
                                mycommand3.Parameters.Add("_IC", SqlDbType.VarChar).Value = strIC;
                                mycommand3.Parameters.Add("_StockNumber", SqlDbType.VarChar).Value = vartblMain2.StockNumber;
                                mycommand3.Parameters.Add("_PIn", SqlDbType.Money).Value = vartblMain2.PIn;
                                mycommand3.Parameters.Add("_POut", SqlDbType.Money).Value = vartblMain2.POut;
                                mycommand3.Parameters.Add("_UP", SqlDbType.Money).Value = vartblMain2.UP;
                                mycommand3.Parameters.Add("_Cost", SqlDbType.Money).Value = vartblMain2.Cost;
                                mycommand3.Parameters.Add("_Discount", SqlDbType.Money).Value = vartblMain2.Discount;
                                mycommand3.Parameters.Add("_OrderTime", SqlDbType.VarChar).Value = vartblMain2.OrderTime;
                                mycommand3.Parameters.Add("_RowNum", SqlDbType.Int).Value = IntRowNum2++; 
                                //mycommand3.Parameters.Add("_RowNum", SqlDbType.Int).Value = new ClsDuplicate().CheckRowMain2(strIC); // intRowNum++;
                                mycommand3.ExecuteNonQuery();
                            }
                        }
                    }
                    myconnection.Close();
                    return "1";
                    }
                    else
                    {
                        string result = new VoucherController().PostVoucher(ModeltblMain11);
                        return result;
                    }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
    }
}
