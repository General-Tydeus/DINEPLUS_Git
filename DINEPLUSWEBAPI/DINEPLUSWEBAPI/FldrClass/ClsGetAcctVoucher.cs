using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DINEPLUSWEBAPI.FldrClass
{
    public class ClsGetAcctVoucher
    {
        public string plsTableDoor, plsVoidIC;
        SqlConnection myconnection;
        SqlDataReader dr;
        SqlCommand mycommand;


        public void ClsGetTDoor(string varstrVoucher)
        {
            try
            {
                string varstrVouc = varstrVoucher.Trim();
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                mycommand = new SqlCommand("Select TableDoor FROM tblTDoor WHERE Voucher='" + varstrVoucher + "'", myconnection);
                dr = mycommand.ExecuteReader();
                while (dr.Read())
                {
                    plsTableDoor = dr["TableDoor"].ToString();
                }
                dr.Close();
                myconnection.Close();
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                //dr.Close();
                myconnection.Close();
            }
        }

        public void clsGetDeleteTre(string varstrVoucher)
        {
            try
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();
                string sqldelete1 = "DELETE FROM tblMain3 WHERE IC='RET'";
                string sqldelete2 = "DELETE FROM tblMain1 WHERE IC='RET'";

                mycommand = new SqlCommand(sqldelete1, myconnection);
                int n1 = mycommand.ExecuteNonQuery();
                mycommand = new SqlCommand(sqldelete2, myconnection);
                int n2 = mycommand.ExecuteNonQuery();
                //myconnection.Close();
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                //dr.Close();
                myconnection.Close();
            }
        }

        public void ClsOneTheDoor(string varstrVoucher)
        {
            try
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                string sqlstatement;
                sqlstatement = "UPDATE tblTDoor SET TableDoor=@_TableDoor WHERE Voucher ='" + varstrVoucher + "'";
                mycommand = new SqlCommand(sqlstatement, myconnection);
                mycommand.Parameters.Add("_TableDoor", SqlDbType.Int).Value = 1;
                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                //dr.Close();
                myconnection.Close();
            }
        }

        public void ClsZeroTheDoor(string varstrVoucher)
        {
            try
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                string sqlstatement;
                sqlstatement = "UPDATE tblTDoor SET TableDoor=@_TableDoor WHERE Voucher ='" + varstrVoucher + "'";
                mycommand = new SqlCommand(sqlstatement, myconnection);
                mycommand.Parameters.Add("_TableDoor", SqlDbType.Int).Value = 0;
                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                //dr.Close();
                myconnection.Close();
            }
        }

        public void ClsDoorMessage(string varstrVoucher, string varstrMsgCode)
        {
            try
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                string sqlstatement;
                sqlstatement = "UPDATE tblTDoor SET DoorMsg=@_DoorMsg WHERE Voucher ='" + varstrVoucher + "'";
                mycommand = new SqlCommand(sqlstatement, myconnection);
                mycommand.Parameters.Add("_DoorMsg", SqlDbType.VarChar).Value = varstrMsgCode;
                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                //dr.Close();
                myconnection.Close();
            }
        }

        public void ClsDeleteErrorTransaction(string varstrVoucher, string strTransactType, string strUserCode, string strCNCode)
        {
            if (strTransactType == "1")
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                SqlCommand mycommand = new SqlCommand("usp_DelVoid", myconnection);
                mycommand.CommandType = CommandType.StoredProcedure;

                mycommand.Parameters.Add("@ParamVoucher", SqlDbType.VarChar).Value = varstrVoucher;
                mycommand.Parameters.Add("@ParamUserCode", SqlDbType.VarChar).Value = strUserCode;
                mycommand.Parameters.Add("@ParamCNCode", SqlDbType.VarChar).Value = strCNCode;

                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            else if (strTransactType == "2")
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                SqlCommand mycommand = new SqlCommand("usp_DelVoidPO", myconnection);
                mycommand.CommandType = CommandType.StoredProcedure;

                mycommand.Parameters.Add("@ParamVoucher", SqlDbType.VarChar).Value = varstrVoucher;
                mycommand.Parameters.Add("@ParamUserCode", SqlDbType.VarChar).Value = strUserCode;
                mycommand.Parameters.Add("@ParamCNCode", SqlDbType.VarChar).Value = strCNCode;
                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }

            else if (strTransactType == "3")
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                SqlCommand mycommand = new SqlCommand("usp_DelVoidSO", myconnection);
                mycommand.CommandType = CommandType.StoredProcedure;

                mycommand.Parameters.Add("@ParamVoucher", SqlDbType.VarChar).Value = varstrVoucher;
                mycommand.Parameters.Add("@ParamUserCode", SqlDbType.VarChar).Value = strUserCode;
                mycommand.Parameters.Add("@ParamCNCode", SqlDbType.VarChar).Value = strCNCode;

                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }

            else if (strTransactType == "4")
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                SqlCommand mycommand = new SqlCommand("usp_DelVoidPCV", myconnection);
                mycommand.CommandType = CommandType.StoredProcedure;

                mycommand.Parameters.Add("@ParamVoucher", SqlDbType.VarChar).Value = varstrVoucher;
                mycommand.Parameters.Add("@ParamUserCode", SqlDbType.VarChar).Value = strUserCode;
                mycommand.Parameters.Add("@ParamCNCode", SqlDbType.VarChar).Value = strCNCode;

                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            else if (strTransactType == "5")
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                SqlCommand mycommand = new SqlCommand("usp_DelVoidOS", myconnection);
                mycommand.CommandType = CommandType.StoredProcedure;
                mycommand.Parameters.Add("@ParamVoucher", SqlDbType.VarChar).Value = varstrVoucher;
                mycommand.Parameters.Add("@ParamUserCode", SqlDbType.VarChar).Value = strUserCode;
                mycommand.Parameters.Add("@ParamCNCode", SqlDbType.VarChar).Value = strCNCode;
                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            else if (strTransactType == "6")
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                SqlCommand mycommand = new SqlCommand("usp_DelVoidAI", myconnection);
                mycommand.CommandType = CommandType.StoredProcedure;
                mycommand.Parameters.Add("@ParamVoucher", SqlDbType.VarChar).Value = varstrVoucher;
                mycommand.Parameters.Add("@ParamUserCode", SqlDbType.VarChar).Value = strUserCode;
                mycommand.Parameters.Add("@ParamCNCode", SqlDbType.VarChar).Value = strCNCode;
                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }
            else if (strTransactType == "7")
            {
                myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                myconnection.Open();

                SqlCommand mycommand = new SqlCommand("usp_DelVoidClaim", myconnection);
                mycommand.CommandType = CommandType.StoredProcedure;
                mycommand.Parameters.Add("@ParamVoucher", SqlDbType.VarChar).Value = varstrVoucher;
                mycommand.Parameters.Add("@ParamUserCode", SqlDbType.VarChar).Value = strUserCode;
                mycommand.Parameters.Add("@ParamCNCode", SqlDbType.VarChar).Value = strCNCode;
                int n1 = mycommand.ExecuteNonQuery();
                myconnection.Close();
            }



        }
        public void ClsFinalize(string varstrVoucher, string varstrDocNum, string strTransactType, string strUserCode, string strCNCode)
        {
            try
            {
                if (strTransactType == "1")
                {
                    myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                    myconnection.Open();
                    string sqlstatement;
                    sqlstatement = "UPDATE ViewOnlineEditNumber SET IC=@_IC, DocNum=@_DocNum, Void=@_Void WHERE Voucher ='" + varstrVoucher + "' AND DocNum='" + strUserCode + "' AND CNCode='" + strCNCode + "'";
                    mycommand = new SqlCommand(sqlstatement, myconnection);
                    mycommand.Parameters.Add("_IC", SqlDbType.VarChar).Value = varstrVoucher + varstrDocNum + strCNCode;
                    mycommand.Parameters.Add("_DocNum", SqlDbType.VarChar).Value = varstrDocNum;
                    mycommand.Parameters.Add("_Void", SqlDbType.Bit).Value = 0;
                    int n1 = mycommand.ExecuteNonQuery();
                    myconnection.Close();
                }
                else if (strTransactType == "2")
                {
                    myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                    myconnection.Open();
                    string sqlstatement;
                    sqlstatement = "UPDATE ViewOnlineEditNumberPO SET IC=@_IC, DocNum=@_DocNum, Void=@_Void WHERE Voucher ='" + varstrVoucher + "' AND DocNum='" + strUserCode + "' AND CNCode='" + strCNCode + "'";
                    mycommand = new SqlCommand(sqlstatement, myconnection);
                    mycommand.Parameters.Add("_IC", SqlDbType.VarChar).Value = varstrVoucher + varstrDocNum + strCNCode;
                    mycommand.Parameters.Add("_DocNum", SqlDbType.VarChar).Value = varstrDocNum;
                    mycommand.Parameters.Add("_Void", SqlDbType.Bit).Value = 0;
                    int n1 = mycommand.ExecuteNonQuery();
                    myconnection.Close();
                }
                else if (strTransactType == "3")
                {
                    myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                    myconnection.Open();
                    string sqlstatement;
                    sqlstatement = "UPDATE ViewOnlineEditNumberSO SET IC=@_IC, DocNum=@_DocNum, Void=@_Void WHERE Voucher ='" + varstrVoucher + "' AND DocNum='" + strUserCode + "' AND CNCode='" + strCNCode + "'";
                    mycommand = new SqlCommand(sqlstatement, myconnection);
                    mycommand.Parameters.Add("_IC", SqlDbType.VarChar).Value = varstrVoucher + varstrDocNum + strCNCode;
                    mycommand.Parameters.Add("_DocNum", SqlDbType.VarChar).Value = varstrDocNum;
                    mycommand.Parameters.Add("_Void", SqlDbType.Bit).Value = 0;
                    int n1 = mycommand.ExecuteNonQuery();
                    myconnection.Close();
                }

                else if (strTransactType == "4")
                {
                    myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                    myconnection.Open();
                    string sqlstatement;
                    sqlstatement = "UPDATE ViewOnlineEditNumberPCV SET IC=@_IC, DocNum=@_DocNum, Void=@_Void WHERE Voucher ='" + varstrVoucher + "' AND DocNum='" + strUserCode + "' AND CNCode='" + strCNCode + "'";
                    mycommand = new SqlCommand(sqlstatement, myconnection);
                    mycommand.Parameters.Add("_IC", SqlDbType.VarChar).Value = varstrVoucher + varstrDocNum + strCNCode;
                    mycommand.Parameters.Add("_DocNum", SqlDbType.VarChar).Value = varstrDocNum;
                    mycommand.Parameters.Add("_Void", SqlDbType.Bit).Value = 0;
                    int n1 = mycommand.ExecuteNonQuery();
                    myconnection.Close();
                }
                else if (strTransactType == "5")
                {
                    myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                    myconnection.Open();
                    string sqlstatement;
                    sqlstatement = "UPDATE ViewOnlineEditNumberOS SET IC=@_IC, DocNum=@_DocNum, Void=@_Void WHERE Voucher ='" + varstrVoucher + "' AND DocNum='" + strUserCode + "' AND CNCode='" + strCNCode + "'";
                    mycommand = new SqlCommand(sqlstatement, myconnection);
                    mycommand.Parameters.Add("_IC", SqlDbType.VarChar).Value = varstrVoucher + varstrDocNum + strCNCode;
                    mycommand.Parameters.Add("_DocNum", SqlDbType.VarChar).Value = varstrDocNum;
                    mycommand.Parameters.Add("_Void", SqlDbType.Bit).Value = 0;
                    int n1 = mycommand.ExecuteNonQuery();
                    myconnection.Close();
                }
                else if (strTransactType == "6")
                {
                    myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                    myconnection.Open();
                    string sqlstatement;
                    sqlstatement = "UPDATE ViewOnlineEditNumberAI SET IC=@_IC, DocNum=@_DocNum, Void=@_Void WHERE Voucher ='" + varstrVoucher + "' AND DocNum='" + strUserCode + "' AND CNCode='" + strCNCode + "'";
                    mycommand = new SqlCommand(sqlstatement, myconnection);
                    mycommand.Parameters.Add("_IC", SqlDbType.VarChar).Value = varstrVoucher + varstrDocNum + strCNCode;
                    mycommand.Parameters.Add("_DocNum", SqlDbType.VarChar).Value = varstrDocNum;
                    mycommand.Parameters.Add("_Void", SqlDbType.Bit).Value = 0;
                    int n1 = mycommand.ExecuteNonQuery();
                    myconnection.Close();
                }
                else if (strTransactType == "7")
                {
                    myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
                    myconnection.Open();

                    string sqlstatement;
                    sqlstatement = "UPDATE ViewOnlineEditNumberClaim SET IC=@_IC, DocNum=@_DocNum, Void=@_Void WHERE Voucher ='" + varstrVoucher + "' AND DocNum='" + strUserCode + "' AND CNCode='" + strCNCode + "'";
                    mycommand = new SqlCommand(sqlstatement, myconnection);
                    mycommand.Parameters.Add("_IC", SqlDbType.VarChar).Value = varstrVoucher + varstrDocNum + strCNCode;
                    mycommand.Parameters.Add("_DocNum", SqlDbType.VarChar).Value = varstrDocNum;
                    mycommand.Parameters.Add("_Void", SqlDbType.Bit).Value = 0;
                    int n1 = mycommand.ExecuteNonQuery();
                    myconnection.Close();
                }

            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                //dr.Close();
                myconnection.Close();
            }
        }
        public int ClsIfVoidExist(string varstrVoucher, string strUserCode, string strCNCode)
        {
            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
            myconnection.Open();

            //string CheckNoTransact = string.Format("SELECT Count(*) FROM tblMain1 WHERE IC='" + varstrVoucher + "'+'" + strUserCode + "'+'" + strCNCode + "'");
            string CheckNoTransact = string.Format($"SELECT Count(*) FROM tblMain1 WHERE IC='{varstrVoucher}{strUserCode}{strCNCode}'");

            SqlCommand com = new SqlCommand(CheckNoTransact, myconnection);
            int CountData = int.Parse(com.ExecuteScalar().ToString());
            myconnection.Close();

            if (CountData > 0)
            {
                return 1; // Exists
            }
            else
            {
                return 2;// doesnt exist
            }


        }


        //public void ClsGetVoidRef(string varstrVoucher, string strTransactType, string strUserCode)
        //{
        //    try
        //    {
        //        ClsDefaultBranch ClsDefaultBranch1 = new ClsDefaultBranch(strUserCode);
        //        if (strTransactType == "1")
        //        {
        //            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
        //            myconnection.Open();
        //            mycommand = new SqlCommand("Select IC FROM tblMain1 WHERE IC='" + varstrVoucher + "'+'" + strUserCode + "'+'" + (ClsDefaultBranch1.plsvardb) + "'", myconnection);
        //            dr = mycommand.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                plsVoidIC = dr["IC"].ToString();
        //            }
        //            dr.Close();
        //            myconnection.Close();
        //        }
        //        else if (strTransactType == "2")
        //        {
        //            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
        //            myconnection.Open();
        //            mycommand = new SqlCommand("Select IC FROM tblPO1 WHERE IC='" + varstrVoucher + "'+'" + strUserCode + "'+'" + (ClsDefaultBranch1.plsvardb) + "'", myconnection);
        //            dr = mycommand.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                plsVoidIC = dr["IC"].ToString();
        //            }
        //            dr.Close();
        //            myconnection.Close();
        //        }

        //        else if (strTransactType == "3")
        //        {
        //            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
        //            myconnection.Open();
        //            mycommand = new SqlCommand("Select IC FROM tblMain6 WHERE IC='" + varstrVoucher + "'+'" + strUserCode + "'+'" + (ClsDefaultBranch1.plsvardb) + "'", myconnection);
        //            dr = mycommand.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                plsVoidIC = dr["IC"].ToString();
        //            }
        //            dr.Close();
        //            myconnection.Close();
        //        }

        //        else if (strTransactType == "4")
        //        {
        //            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
        //            myconnection.Open();
        //            mycommand = new SqlCommand("Select IC FROM tblPCV1 WHERE IC='" + varstrVoucher + "'+'" + strUserCode + "'+'" + (ClsDefaultBranch1.plsvardb) + "'", myconnection);
        //            dr = mycommand.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                plsVoidIC = dr["IC"].ToString();
        //            }
        //            dr.Close();
        //            myconnection.Close();
        //        }

        //        else if (strTransactType == "5")
        //        {
        //            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
        //            myconnection.Open();
        //            mycommand = new SqlCommand("Select IC FROM tblOS1 WHERE IC='" + varstrVoucher + "'+'" + strUserCode + "'+'" + (ClsDefaultBranch1.plsvardb) + "'", myconnection);
        //            dr = mycommand.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                plsVoidIC = dr["IC"].ToString();
        //            }
        //            dr.Close();
        //            myconnection.Close();
        //        }
        //        else if (strTransactType == "6")
        //        {
        //            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
        //            myconnection.Open();
        //            mycommand = new SqlCommand("Select IC FROM tblmain4 WHERE IC='" + varstrVoucher + "'+'" + strUserCode + "'+'" + (ClsDefaultBranch1.plsvardb) + "'", myconnection);
        //            dr = mycommand.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                plsVoidIC = dr["IC"].ToString();
        //            }
        //            dr.Close();
        //            myconnection.Close();
        //        }
        //        else if (strTransactType == "7")
        //        {
        //            myconnection = new SqlConnection(new ClsGetConnection().PlsConnect());
        //            myconnection.Open();
        //            mycommand = new SqlCommand("Select IC FROM tblClaim1 WHERE IC='" + varstrVoucher + "'+'" + strUserCode + "'+'" + (ClsDefaultBranch1.plsvardb) + "'", myconnection);
        //            dr = mycommand.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                plsVoidIC = dr["IC"].ToString();
        //            }
        //            dr.Close();
        //            myconnection.Close();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        //dr.Close();
        //        myconnection.Close();
        //    }
        //}

    }
}
