﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kadnet.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Kadnet.Api
{
    public class Client
    {
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly string _params;

        public Client(string token, string urlparam = "", string url = "https://api.kadnet.ru/v2/")
        {
            _token = token;
            _baseUrl = url;
            if (!string.IsNullOrEmpty(urlparam))
                _params = _params.StartsWith("&") ? urlparam : $"&{urlparam}";
            else
                _params = string.Empty;
        }

        /// <summary>
        /// Check cadastral numbers and get minimal information about it as enumerable collection
        /// </summary>
        /// <param name="numbers">one number:"66:41:0402033:2002" or list of numbers:"66:41:0402033:2266;66:41:0402033:2502"</param>
        /// <param name="comment">no comments</param>
        /// <returns>IEnumerable of CadastralObjectInfo</returns>
        public IEnumerable<CadastralObjectInfo> CheckNumbers(string numbers, string comment = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/CheckNumbers?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddParameter("query", numbers);
            if (!string.IsNullOrEmpty(comment)) request.AddParameter("comment", comment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                if ((bool)data["Result"])
                    return JsonConvert.DeserializeObject<IEnumerable<CadastralObjectInfo>>(data["Data"].ToString());
            }
            //TODO: Уточнить тип
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }


        /// <summary>
        /// Async Check cadastral numbers and get minimal information about it as enumerable collection
        /// </summary>
        /// <param name="numbers">one number:"66:41:0402033:2002" or list of numbers:"66:41:0402033:2266;66:41:0402033:2502"</param>
        /// <param name="comment">no comments</param>
        /// <returns>IEnumerable of CadastralObjectInfo</returns>
        public async Task<IEnumerable<CadastralObjectInfo>> CheckNumbersAsyncTask(string numbers, string comment = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/CheckNumbers?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddParameter("query", numbers);
            if (!string.IsNullOrEmpty(comment)) request.AddParameter("comment", comment);
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                if ((bool) data["Result"])
                    return JsonConvert.DeserializeObject<IEnumerable<CadastralObjectInfo>>(data["Data"].ToString());
            }
            //TODO: Уточнить тип
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }

        /// <summary>
        /// Async Check cadastral numbers and get minimal information about it as json
        /// </summary>
        /// <param name="address">one number:"66:41:0402033:2002" or list of numbers:"66:41:0402033:2266;66:41:0402033:2502"</param>
        /// <param name="comment">no comments</param>
        /// <returns>JSON string</returns>
        public async Task<string> CheckNumbersAsJsonAsyncTask(string address, string comment = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/CheckNumbers?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddParameter("query", address);
            if (!string.IsNullOrEmpty(comment)) request.AddParameter("comment", comment);
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }

        /// <summary>
        /// Check cadastral numbers and get minimal information about it as json
        /// </summary>
        /// <param name="address">one number:"66:41:0402033:2002" or list of numbers:"66:41:0402033:2266;66:41:0402033:2502"</param>
        /// <param name="comment">no comments</param>
        /// <returns>JSON string</returns>
        public string CheckNumbersAsJson(string address, string comment = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/CheckNumbers?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddParameter("query", address);
            if (!string.IsNullOrEmpty(comment)) request.AddParameter("comment", comment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }

        /// <summary>
        /// Async Get cadastral object enumerable collection by address
        /// </summary>
        /// <param name="address">Address. Example:"Екатеринбург, ул. Малопрудная 5", "Екатеринбург, ул. Щорса 109, кв. 10"</param>
        /// <param name="comment">no comments</param>
        /// <returns></returns>
        public async Task<IEnumerable<CadastralObjectInfo>> CheckAddressAsyncTask(string address, string comment = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/CheckAddress?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddParameter("query", address);
            if (!string.IsNullOrEmpty(comment)) request.AddParameter("comment", comment);
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                if ((bool) data["Result"])
                    return JsonConvert.DeserializeObject<IEnumerable<CadastralObjectInfo>>(data["Data"].ToString());
            }
            //TODO: Уточнить тип
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }

        /// <summary>
        /// Get cadastral object enumerable collection by address
        /// </summary>
        /// <param name="address">Address. Example:"Екатеринбург, ул. Малопрудная 5", "Екатеринбург, ул. Щорса 109, кв. 10"</param>
        /// <param name="comment">no comments</param>
        /// <returns></returns>
        public IEnumerable<CadastralObjectInfo> CheckAddress(string address, string comment = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/CheckAddress?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddParameter("query", address);
            if (!string.IsNullOrEmpty(comment)) request.AddParameter("comment", comment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                if ((bool)data["Result"])
                    return JsonConvert.DeserializeObject<IEnumerable<CadastralObjectInfo>>(data["Data"].ToString());
            }
            //TODO: Уточнить тип
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }

        /// <summary>
        /// Async Check address and get minimal information about it as json
        /// </summary>
        /// <param name="address">Address. Example:"Екатеринбург, ул. Малопрудная 5", "Екатеринбург, ул. Щорса 109, кв. 10"</param>
        /// <param name="comment">no comments</param>
        /// <returns>JSON string</returns>
        public async Task<string> CheckAddressAsJsonAsyncTask(string address, string comment = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/CheckAddress?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddParameter("query", address);
            if (!string.IsNullOrEmpty(comment)) request.AddParameter("comment", comment);
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }

        /// <summary>
        /// Check address and get minimal information about it as json
        /// </summary>
        /// <param name="address">Address. Example:"Екатеринбург, ул. Малопрудная 5", "Екатеринбург, ул. Щорса 109, кв. 10"</param>
        /// <param name="comment">no comments</param>
        /// <returns>JSON string</returns>
        public string CheckAddressAsJson(string address, string comment = null)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/CheckAddress?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddParameter("query", address);
            if (!string.IsNullOrEmpty(comment)) request.AddParameter("comment", comment);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }

        /// <summary>
        /// Async Create request to Rosreestr. Core functionallity
        /// </summary>
        /// <param name="coi">Pull this object from CheckAddress/CheckNumbers methods</param>
        /// <param name="requestType">RequestType enum. General info about object or Right List of object</param>
        /// <param name="priority">Priority  from 0 to 100. Lazy=0, Greedy=100</param>
        /// <returns>RequestTicket object</returns>
        public async Task<RequestTicket> CreateRequestAsyncTask(CadastralObjectInfo coi, RequestType requestType, int priority = 80)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Create?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            if (coi == null)
                throw new NullReferenceException("Передаваемый объект CadastralObjectInfo не определен. Получить корректный объект можно через методы CheckAddress/CheckNumbers");
            request.AddParameter("Id", coi.Id);
            request.AddParameter("OrderId", coi.OrderId);
            request.AddParameter("Number", coi.Number);
            request.AddParameter("Comment", coi.Comment);
            request.AddParameter("Priority", priority);
            request.AddParameter("RequestType", requestType);
            request.AddParameter("ObjectType", coi.ObjectType);
            request.AddParameter("Region", coi.Region);
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<RequestTicket>(response.Content);
            }
            throw new Exception($"Create request failed. Request Id={coi.Id}. Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Create request to Rosreestr. Core functionallity
        /// </summary>
        /// <param name="coi">Pull this object from CheckAddress/CheckNumbers methods</param>
        /// <param name="requestType">RequestType enum. General info about object or Right List of object</param>
        /// <param name="priority">Priority  from 0 to 100. Lazy=0, Greedy=100</param>
        /// <returns>RequestTicket object</returns>
        public RequestTicket CreateRequest(CadastralObjectInfo coi, RequestType requestType, int priority = 80)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Create?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            if (coi == null)
                throw new NullReferenceException("Передаваемый объект CadastralObjectInfo не определен. Получить корректный объект можно через методы CheckAddress/CheckNumbers");
            request.AddParameter("Id", coi.Id);
            request.AddParameter("OrderId", coi.OrderId);
            request.AddParameter("Number", coi.Number);
            request.AddParameter("Comment", coi.Comment);
            request.AddParameter("Priority", priority);
            request.AddParameter("RequestType", requestType);
            request.AddParameter("ObjectType", coi.ObjectType);
            request.AddParameter("Region", coi.Region);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<RequestTicket>(response.Content);
            }
            throw new Exception($"Create request failed. Request Id={coi.Id}. Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Async Create request to Rosreestr. Core functionallity
        /// </summary>
        /// <param name="coi">Pull this object from CheckAddress/CheckNumbers methods</param>
        /// <param name="requestType">RequestType enum. General info about object or Right List of object</param>
        /// <returns>JSON string</returns>
        public async Task<string> CreateRequestAsJsonAsyncTask(CadastralObjectInfo coi, RequestType requestType)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Create?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            if (coi == null)
                throw new NullReferenceException("Передаваемый объект CadastralObjectInfo не определен. Получить корректный объект можно через методы CheckAddress/CheckNumbers");
            request.AddParameter("Id", coi.Id);
            request.AddParameter("OrderId", coi.OrderId);
            request.AddParameter("Number", coi.Number);
            request.AddParameter("Comment", coi.Comment);
            request.AddParameter("Priority", 80); //TODO: ОТкуда взять?!
            request.AddParameter("RequestType", requestType);
            request.AddParameter("ObjectType", coi.ObjectType); //Пока не актуальна
            request.AddParameter("Region", coi.Region);
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            throw new Exception($"Create request failed. Request Id={coi.Id}. Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Create request to Rosreestr. Core functionallity
        /// </summary>
        /// <param name="coi">Pull this object from CheckAddress/CheckNumbers methods</param>
        /// <param name="requestType">RequestType enum. General info about object or Right List of object</param>
        /// <returns>JSON string</returns>
        public string CreateRequestAsJson(CadastralObjectInfo coi, RequestType requestType)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Create?api-key={apikey}{_params}", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            if (coi == null)
                throw new NullReferenceException("Передаваемый объект CadastralObjectInfo не определен. Получить корректный объект можно через методы CheckAddress/CheckNumbers");
            request.AddParameter("Id", coi.Id);
            request.AddParameter("OrderId", coi.OrderId);
            request.AddParameter("Number", coi.Number);
            request.AddParameter("Comment", coi.Comment);
            request.AddParameter("Priority", 80); //TODO: ОТкуда взять?!
            request.AddParameter("RequestType", requestType);
            request.AddParameter("ObjectType", coi.ObjectType); //Пока не актуальна
            request.AddParameter("Region", coi.Region);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            throw new Exception($"Create request failed. Request Id={coi.Id}. Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Async Get request information by request Id
        /// </summary>
        /// <param name="requestId">request id</param>
        /// <returns></returns>
        public async Task<RequestInfo> GetRequestInfoAsyncTask(Guid requestId)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Info/{requestId}?api-key={apikey}{_params}", Method.GET);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                if ((bool) data["Result"])
                    return JsonConvert.DeserializeObject<RequestInfo>(data["Data"].ToString());
            }
            throw new Exception($"Cannot pull request information. Request Id={requestId}. Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Get request information by request Id
        /// </summary>
        /// <param name="requestId">request id</param>
        /// <returns></returns>
        public RequestInfo GetRequestInfo(Guid requestId)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Info/{requestId}?api-key={apikey}{_params}", Method.GET);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                if ((bool)data["Result"])
                    return JsonConvert.DeserializeObject<RequestInfo>(data["Data"].ToString());
            }
            throw new Exception($"Cannot pull request information. Request Id={requestId}. Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Async Get request information by request Id
        /// </summary>
        /// <param name="requestId">request id</param>
        /// <returns></returns>
        public async Task<string> GetRequestInfoAsJsonAsyncTask(Guid requestId)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Info/{requestId}?api-key={apikey}{_params}", Method.GET);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            throw new Exception($"Cannot pull request information. Request Id={requestId}. Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Get request information by request Id
        /// </summary>
        /// <param name="requestId">request id</param>
        /// <returns></returns>
        public string GetRequestInfoAsJson(Guid requestId)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Info/{requestId}?api-key={apikey}{_params}", Method.GET);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Content;
            throw new Exception($"Cannot pull request information. Request Id={requestId}. Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Async Download result data for request
        /// </summary>
        /// <param name="requestId">RequestId with strong filter by user(api-key)</param>
        /// <param name="format">xml/pdf/html by ResultFormat enum</param>
        /// <returns></returns>
        public async Task<FileResult> GetResultAsyncTask(Guid requestId, ResultFormat format)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Result/{requestId}?api-key={apikey}{_params}", Method.GET);
            request.AddHeader("content-type", "application/octet-stream");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("_params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            request.AddParameter("type", format);
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var fr = new FileResult();
                var name = response.Headers.FirstOrDefault(h => h.Name == "Filename");
                fr.Name = name != null ? Base64Decode(name.Value.ToString()) : $"result.{format}";
                fr.Data = new MemoryStream(response.RawBytes);
                return fr;
            }
            throw new Exception($"Cannot pull request information. Request Id={requestId}. Response status={response.ResponseStatus} Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Download result data for request
        /// </summary>
        /// <param name="requestId">RequestId with strong filter by user(api-key)</param>
        /// <param name="format">xml/pdf/html by ResultFormat enum</param>
        /// <returns></returns>
        public FileResult GetResult(Guid requestId, ResultFormat format)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Result/{requestId}?api-key={apikey}{params}", Method.GET);
            request.AddHeader("content-type", "application/octet-stream");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            request.AddParameter("type", format);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var fr = new FileResult();
                var name = response.Headers.FirstOrDefault(h => h.Name == "Filename");
                fr.Name = name != null ? Base64Decode(name.Value.ToString()) : $"result.{format}";
                fr.Data = new MemoryStream(response.RawBytes);
                return fr;
            }
            throw new Exception($"Cannot pull request information. Request Id={requestId}. Response status={response.ResponseStatus} Send this information to support@kadnet.ru");
        }

        /// <summary>
        /// Async Get history of statuses for request
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HistoryEntry>> GetHistoryAsyncTask(Guid requestId)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/History/{requestId}?api-key={apikey}{params}", Method.GET);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                if ((bool)data["Result"])
                    return JsonConvert.DeserializeObject<IEnumerable<HistoryEntry>>(data["Data"].ToString());
            }
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }
        /// <summary>
        /// Get history of statuses for request
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public IEnumerable<HistoryEntry> GetHistory(Guid requestId)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/History/{requestId}?api-key={apikey}{params}", Method.GET);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                if ((bool)data["Result"])
                    return JsonConvert.DeserializeObject<IEnumerable<HistoryEntry>>(data["Data"].ToString());
            }
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }

        /// <summary>
        /// Async Delete request entry
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsyncTask(Guid requestId)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Delete/{requestId}?api-key={apikey}{params}", Method.GET);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                return (bool)data["Result"];
            }
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }

        /// <summary>
        /// Delete request entry
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public bool Delete(Guid requestId)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/Delete/{requestId}?api-key={apikey}{params}", Method.GET);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JObject.Parse(response.Content);
                return (bool)data["Result"];
            }
            throw new Exception($"Response Error. Response status = {response.StatusCode}");
        }


        /// <summary>
        /// Async Download order data (many request with same orderId) for request
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public async Task<FileResult> GetOrderResultAsyncTask(Guid requestId, ResultFormat format)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/OrderResult/{requestId}?api-key={apikey}{params}", Method.GET);
            request.AddHeader("content-type", "application/octet-stream");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            request.AddParameter("type", format);
            var cancellationToken = new CancellationToken();
            var response = await client.ExecuteTaskAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var fr = new FileResult();
                var name = response.Headers.FirstOrDefault(h => h.Name == "Filename");
                fr.Name = name != null ? Base64Decode(name.Value.ToString()) : $"result.{format}";
                fr.Data = new MemoryStream(response.RawBytes);
                return fr;
            }
            throw new Exception($"Cannot pull request information. Request Id={requestId}. Response status={response.ResponseStatus} Send this information to support@kadnet.ru");
        }
        /// <summary>
        /// Download order data (many request with same orderId) for request
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public FileResult GetOrderResult(Guid requestId, ResultFormat format)
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("Requests/OrderResult/{requestId}?api-key={apikey}{params}", Method.GET);
            request.AddHeader("content-type", "application/octet-stream");
            request.AddUrlSegment("apikey", _token);
            request.AddUrlSegment("params", _params);
            request.AddUrlSegment("requestId", requestId.ToString());
            request.AddParameter("type", format);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var fr = new FileResult();
                var name = response.Headers.FirstOrDefault(h => h.Name == "Filename");
                fr.Name = name != null ? Base64Decode(name.Value.ToString()) : $"result.{format}";
                fr.Data = new MemoryStream(response.RawBytes);
                return fr;
            }
            throw new Exception($"Cannot pull request information. Request Id={requestId}. Response status={response.ResponseStatus} Send this information to support@kadnet.ru");
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
