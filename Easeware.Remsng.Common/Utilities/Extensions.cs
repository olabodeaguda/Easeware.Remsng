using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Easeware.Remsng.Common.Utilities
{
    public static class Extensions
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetFileName(this string url)
        {
            Uri uri = new Uri(url);
            return System.IO.Path.GetFileName(uri.LocalPath);
        }

        public static UrlFileModel GetAttachment(this string url, string fileTitle)
        {
            WebClient client = new WebClient();
            try
            {
                MemoryStream ms = new MemoryStream(client.DownloadData(url));
                if (ms == null)
                {
                    return null;
                }

                return new UrlFileModel()
                {
                    contenType = client.ResponseHeaders["Content-Type"],
                    fileStream = ms,
                    fileName = string.IsNullOrEmpty(fileTitle) ? url.GetFileName() ?? $"attachment {DateTime.Now.ToString("HH:mm:ss")}" : fileTitle
                };
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                client.Dispose();
            }
        }

        public static DateTime? ToDate(this string date)
        {
            string[] sts = date.Split(new char[] { '-' });
            if (sts.Length != 3 && date.Length < 8)
            {
                return null;
            }
            int dd = 0;
            int mm = 0;
            int yyyy = 0;
            if (!int.TryParse(sts[0], out dd))
            {
                return null;
            }
            if (!int.TryParse(sts[1], out mm))
            {
                return null;
            }
            if (!int.TryParse(sts[2], out yyyy))
            {
                return null;
            }

            return new DateTime(yyyy, mm, dd);
        }

        public static string ToHexString(this string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string FromHexString(this string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes);
        }

        public static bool ValidateLicense(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            string[] dd = value.Split(new char[] { '-' });
            if (dd.Length != 3 && dd[0] != dd[2].FromHexString())
            {
                return false;
            }
            long ticks;
            if (!long.TryParse(dd[1], out ticks))
            {
                return false;
            }
            DateTime dateTime = new DateTime(ticks);
            if (DateTime.Now.CompareTo(dateTime) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
