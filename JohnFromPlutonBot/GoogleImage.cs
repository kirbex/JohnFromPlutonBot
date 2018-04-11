using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JohnFromPlutonBot
{
    public static class GoogleImage
    {
        private static List<string> _topics = new List<string>
        {
            "dog",
            "cat",
            "собака",
            "кошка",
            "Илья",
            "Гульназ",
            "Юра",
            "Айрат",
            "Густав",
            "сиськи",
            "audi",
            "плутон",
            "пришельцы",
            "инопланетянин",
            "нло",
            "пейзаж",
            "стиральный порошок",
            "татнефть",
            "танеко",
            "автоналив",
            "автоцистерна",
            "мезим",
            "катастрофа",
            "послезавтра",
            "Света",
            "канитель",
            "c#",
            "visual studio",
            "котики",
            "цветы",
            "рыбка",
            "утка",
            "селезень",
            "батон",
            "снежная королева",
            "кенгуру",
            "обама",
            "чебурашка",
            "сиськи",
            "анимэ",
            "трамп",
            "дональд",
            "c#",
            ".net",
            "goldstein",
            "Казань",
            "Ижевск",
            "Москва",
            "Санкт-петербург",
            "инкапсуляция",
            "наследование",
            "ковбой",
            "крэк",
            "марихуана",
            "робокоп",
            "супермен",
            "крекер",
            "алкаш",
            "трезвое радио",
            "возрождение нравственности",
            "общее дело",
            "жданов",
            "концепция общественной безопасности",
            "достаточно общая теория управления",
            "алкоголь зло",
            "сигареты зло",
            "иван чай",
            "здоровый образ жизни",
            "демотиватор",
            "демотиватор о жизни",
            "демотиватор о программировании",
            "демотиватор о женщинах",
            "демотиватор о мужчинах",
            "демотиватор о водке",
            "демотиватор о медведе",
            "медведь",
            "путин краб",
            "коррупционер",
            "нельзя просто так взять",
            "нельзя просто так взять",
            "нельзя просто так взять",
            "demotivational poster"
        };

        public static Stream GetGoogleRandomImage(out string topic)
        {
            string html = GetHtmlCode(out string selectedTopic);
            topic = selectedTopic;
            List<string> urls = GetUrls(html);
            var rnd = new Random();
            int randomUrl = rnd.Next(0, urls.Count);

            string luckyUrl = urls[randomUrl];
            byte[] image = GetImage(luckyUrl);
            return new MemoryStream(image);
        }

        public static Stream GetGoogleRandomImage()
        {
                string html = GetHtmlCode();
                List<string> urls = GetUrls(html);
                var rnd = new Random();
                int randomUrl = rnd.Next(0, urls.Count);

                string luckyUrl = urls[randomUrl];
                byte[] image = GetImage(luckyUrl);
                return new MemoryStream(image);
        }

        public static Stream GetGoogleRandomImage(string topic)
        {
            string html = GetHtmlCode(topic);
            List<string> urls = GetUrls(html);
            var rnd = new Random();
            int randomUrl = rnd.Next(0, urls.Count);

            string luckyUrl = urls[randomUrl];
            byte[] image = GetImage(luckyUrl);
            return new MemoryStream(image);
        }

        private static string GetHtmlCode(string topic)
        {
            string url = "https://www.google.com/search?q=" + topic + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;
        }

        private static string GetHtmlCode(out string selectedTopic)
        {
            var rnd = new Random();

            int topic = rnd.Next(0, _topics.Count);

            selectedTopic = _topics[topic];
            string url = "https://www.google.com/search?q=" + selectedTopic + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;
        }

        private static string GetHtmlCode()
        {
            var rnd = new Random();

            int topic = rnd.Next(0, _topics.Count);

            string url = "https://www.google.com/search?q=" + _topics[topic] + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;
        }

        private static List<string> GetUrls(string html)
        {
            var urls = new List<string>();

            int ndx = html.IndexOf("\"ou\"", StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
                ndx++;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);
                ndx = html.IndexOf("\"ou\"", ndx2, StringComparison.Ordinal);
            }
            return urls;
        }

        private static byte[] GetImage(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return null;
                using (var sr = new BinaryReader(dataStream))
                {
                    byte[] bytes = sr.ReadBytes(100000000);

                    return bytes;
                }
            }

            return null;
        }
    }
}
