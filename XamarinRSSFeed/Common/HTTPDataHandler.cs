using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using Java.IO;

namespace XamarinRSSFeed.Common
{
    public class HTTPDataHandler
    {
        static string stream = null;

        public String GetHTTPData(string urlString)
        {
            try
            {

                URL url = new URL(urlString);
                using (var urlConnection = (HttpURLConnection)url.OpenConnection())
                {
                    if(urlConnection.ResponseCode == HttpStatus.Ok)
                    {
                        BufferedReader reader = new BufferedReader(new InputStreamReader(urlConnection.InputStream));
                        StringBuilder sb = new StringBuilder();
                        String line;
                        while ((line = reader.ReadLine()) != null)
                            sb.Append(line);
                        stream = sb.ToString();
                        urlConnection.Disconnect();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return stream;
        }
    }
}