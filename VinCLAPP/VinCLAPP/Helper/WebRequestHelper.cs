using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace VinCLAPP.Helper
{
    public class WebRequestHelper
    {
        public WebRequest GetRequest(string method, string contentType, string endPoint, string content)
        {
            var request = this.GetRequest(method, contentType, endPoint);
            var dataArray = Encoding.UTF8.GetBytes(content.ToString());
            request.ContentLength = dataArray.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(dataArray, 0, dataArray.Length);
            requestStream.Flush();
            requestStream.Close();

            return request;
        }

        public WebRequest GetRequest(string method, string contentType, string endPoint)
        {
            var request = WebRequest.Create(endPoint);
            //request.ProtocolVersion = System.Net.HttpVersion.Version10;
            //request.KeepAlive = false;
            request.Method = method;
            request.ContentType = contentType;

            return request;
        }


        public Stream GetResponseStream(WebResponse response)
        {
            return response.GetResponseStream();
        }

        public StreamReader GetResponseReader(WebResponse response)
        {
            return new StreamReader(GetResponseStream(response));
        }

        public string UnPackResponse(WebResponse response)
        {
            return GetResponseReader(response).ReadToEnd();
        }
    }
}
