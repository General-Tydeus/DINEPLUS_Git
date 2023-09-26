using DINEPLUSBE.FldrModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DINEPLUSBE.FldrClass
{
    class ClsFrancisDuplicate
    {
        private string pristrIPAddress = new ClsGetIPAddress().GetIPAddress();
        public async Task<string> CheckDuplicateProduct(string strValueName)
        {
            DuplicateData DuplicateData1 = new DuplicateData()
            {
                FldValueFieldName = strValueName,
                
            };
            var jsonDuplicate = JsonConvert.SerializeObject(DuplicateData1);
            var contentDuplicate = new StringContent(jsonDuplicate, Encoding.UTF8, "application/json");
            HttpClient clientDuplicate = new HttpClient();
            var resultDuplicate = await clientDuplicate.PostAsync($"{pristrIPAddress}/API/WEBAPI/Duplicate/CheckDuplicateProduct", contentDuplicate);

            if (resultDuplicate.IsSuccessStatusCode)
            {
                return "1"; //Duplicate
            }
            else
            {
                return "2"; //No Duplicate
            }
        }

        public async Task<string> CheckDuplicateCategory(string strValueName)
        {
            DuplicateData DuplicateData1 = new DuplicateData()
            {
                FldValueFieldName = strValueName,

            };
            var jsonDuplicate = JsonConvert.SerializeObject(DuplicateData1);
            var contentDuplicate = new StringContent(jsonDuplicate, Encoding.UTF8, "application/json");
            HttpClient clientDuplicate = new HttpClient();
            var resultDuplicate = await clientDuplicate.PostAsync($"{pristrIPAddress}/API/WEBAPI/Duplicate/CheckDuplicateCategory", contentDuplicate);

            if (resultDuplicate.IsSuccessStatusCode)
            {
                return "1"; //Duplicate
            }
            else
            {
                return "2"; //No Duplicate
            }
        }


        public async Task<string> CheckDuplicateTable(string strValueName)
        {
            DuplicateData DuplicateData1 = new DuplicateData()
            {
                FldValueFieldName = strValueName,

            };
            var jsonDuplicate = JsonConvert.SerializeObject(DuplicateData1);
            var contentDuplicate = new StringContent(jsonDuplicate, Encoding.UTF8, "application/json");
            HttpClient clientDuplicate = new HttpClient();
            var resultDuplicate = await clientDuplicate.PostAsync($"{pristrIPAddress}/API/WEBAPI/Duplicate/CheckDuplicateTable", contentDuplicate);

            if (resultDuplicate.IsSuccessStatusCode)
            {
                return "1"; //Duplicate
            }
            else
            {
                return "2"; //No Duplicate
            }
        }

        public async Task<string> CheckDuplicateName(string strValueName)
        {
            DuplicateData DuplicateData1 = new DuplicateData()
            {
                FldValueFieldName = strValueName,

            };
            var jsonDuplicate = JsonConvert.SerializeObject(DuplicateData1);
            var contentDuplicate = new StringContent(jsonDuplicate, Encoding.UTF8, "application/json");
            HttpClient clientDuplicate = new HttpClient();
            var resultDuplicate = await clientDuplicate.PostAsync($"{pristrIPAddress}/API/WEBAPI/Duplicate/CheckDuplicateName", contentDuplicate);

            if (resultDuplicate.IsSuccessStatusCode)
            {
                return "1"; //Duplicate
            }
            else
            {
                return "2"; //No Duplicate
            }
        }

        public async Task<string> CheckDuplicateUserName(string strValueName)
        {
            DuplicateData DuplicateData1 = new DuplicateData()
            {
                FldValueFieldName = strValueName,

            };
            var jsonDuplicate = JsonConvert.SerializeObject(DuplicateData1);
            var contentDuplicate = new StringContent(jsonDuplicate, Encoding.UTF8, "application/json");
            HttpClient clientDuplicate = new HttpClient();
            var resultDuplicate = await clientDuplicate.PostAsync($"{pristrIPAddress}/API/WEBAPI/Duplicate/CheckDuplicateUserName", contentDuplicate);

            if (resultDuplicate.IsSuccessStatusCode)
            {
                return "1"; //Duplicate
            }
            else
            {
                return "2"; //No Duplicate
            }
        }

    }
}
