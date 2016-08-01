using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;

namespace DTSigninRemind.Services
{
    public class HttpHelper
    {
        ManualResetEvent allDone = new ManualResetEvent(false);
        public string GetJson(string url, Dictionary<string, string> headers = null)
        {
            string _headers = string.Empty;
            string _strResult = null;
            allDone.Reset();
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    _headers = _headers + item.Key + "=" + item.Value + "&";
                }
                _headers = _headers.Substring(0, _headers.Length - 1);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (headers == null ? "" : "?") + _headers);
            request.Method = "GET";
            request.BeginGetResponse(a =>
            {
                try
                {
                    var reponse = request.EndGetResponse(a);
                    using (StreamReader reader = new StreamReader(reponse.GetResponseStream(), System.Text.Encoding.UTF8))
                    {
                        _strResult = reader.ReadToEnd();
                    }
                    allDone.Set();
                }
                catch (Exception)
                {
                    _strResult = null;
                    allDone.Set();
                    //throw;
                }
            }, null);
            allDone.WaitOne();
            _headers = string.Empty;
            return _strResult;
        }

        public string GetJson(string url,Encoding encode=null, Dictionary<string, string> headers = null)
        {
            string _headers = string.Empty;
            string _strResult = null;
            allDone.Reset();
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    _headers = _headers + item.Key + "=" + item.Value + "&";
                }
                _headers = _headers.Substring(0, _headers.Length - 1);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (headers == null ? "" : "?") + _headers);
            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded;charset=GBK";
            request.BeginGetResponse(a =>
            {
                try
                {
                    var reponse = request.EndGetResponse(a);
                    using (StreamReader reader = new StreamReader(reponse.GetResponseStream(), Encoding.GetEncoding("GBK")))
                    {
                        _strResult = reader.ReadToEnd();
                    }
                    allDone.Set();
                }
                catch (Exception)
                {
                    _strResult = null;
                    allDone.Set();
                    //throw;
                }
            }, null);
            allDone.WaitOne();
            _headers = string.Empty;
            return _strResult;
        }

        public string PostJson(string url, string data, Dictionary<string, string> headers = null)
        {
            string _headers = string.Empty;
            string _strResult = null;
            byte[] byteData = Encoding.UTF8.GetBytes(data);
            allDone.Reset();
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    _headers = _headers + item.Key + "=" + item.Value + "&";
                }
                _headers = _headers.Substring(0, _headers.Length - 1);
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + (headers == null ? "" : "?") + _headers);

            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.BeginGetRequestStream((result) =>
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                    using (Stream requestStream = request.EndGetRequestStream(result))
                    {
                        requestStream.Write(byteData, 0, byteData.Length);
                    }
                    request.BeginGetResponse(a =>
                    {
                        try
                        {
                            var reponse = request.EndGetResponse(a);
                            using (StreamReader reader = new StreamReader(reponse.GetResponseStream(), System.Text.Encoding.UTF8))
                            {
                                _strResult = reader.ReadToEnd();
                            }
                            allDone.Set();
                        }
                        catch (Exception)
                        {
                            _strResult = null;
                            allDone.Set();
                            //throw;
                        }
                    }, null);
                }
                catch (Exception)
                {
                    _strResult = null;
                    allDone.Set();
                }
            }, httpWebRequest);


            allDone.WaitOne();
            _headers = string.Empty;
            return _strResult;


        }


        /// </summary>
        public string PostFile(string uri, Dictionary<string, string> headers, string filename, string path)
        {
            string _headers = string.Empty;
            string _responseString = string.Empty;
            allDone.Reset();
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    _headers = _headers + item.Key + "=" + item.Value + "&";
                }
                _headers = _headers.Substring(0, _headers.Length - 1);
            }
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri + (headers == null ? "" : "?") + _headers);

            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            httpWebRequest.Method = "POST";
            httpWebRequest.BeginGetRequestStream((result) =>
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                    using (Stream requestStream = request.EndGetRequestStream(result))
                    {
                        WriteMultipartForm(requestStream, boundary, filename, path);
                    }
                    request.BeginGetResponse(a =>
                    {
                        try
                        {
                            var response = request.EndGetResponse(a);
                            var responseStream = response.GetResponseStream();
                            using (var sr = new StreamReader(responseStream))
                            {
                                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                                {
                                    _responseString = streamReader.ReadToEnd();
                                    //responseString is depend upon your web service.

                                    allDone.Set();
                                }
                            }
                        }
                        catch (Exception)
                        {
                            _responseString = null;
                            allDone.Set();
                        }
                    }, null);
                }
                catch (Exception)
                {
                    _responseString = null;
                    allDone.Set();
                }
            }, httpWebRequest);
            allDone.WaitOne();
            _headers = string.Empty;
            return _responseString;
        }

        /// <summary>
        /// Writes multi part HTTP POST request. Author : Farhan Ghumra
        /// </summary>
        private void WriteMultipartForm(Stream s, string boundary, string filename, string path)
        {
            /// The first boundary
            byte[] boundarybytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
            /// the last boundary.
            byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "–-\r\n");
            /// the form data, properly formatted

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] bArr = new byte[fs.Length];
                fs.Read(bArr, 0, bArr.Length);

                WriteToStream(s, boundarybytes);

                StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"media\";filename=\"{0}\"\r\nfilelength=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n", filename, fs.Length));
                byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
                WriteToStream(s, postHeaderBytes);
                /// Write the file data to the stream.
                fs.Close();
                WriteToStream(s, bArr);
                WriteToStream(s, trailer);
            }




        }

        /// <summary>
        /// Writes string to stream. Author : Farhan Ghumra
        /// </summary>
        private void WriteToStream(Stream s, string txt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(txt);
            s.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Writes byte array to stream. Author : Farhan Ghumra
        /// </summary>
        private void WriteToStream(Stream s, byte[] bytes)
        {
            s.Write(bytes, 0, bytes.Length);
        }



    }
}
