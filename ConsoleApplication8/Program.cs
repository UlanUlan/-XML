using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ModuleXML
{
    public class HabrNews
    {
        public string title { get; set; }
        public string link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<HabrNews> habrNews = new List<HabrNews>();

            XmlDocument doc = new XmlDocument();
            doc.Load("https://habrahabr.ru/rss/interesting/");

            var rootElement = doc.DocumentElement;

            foreach (XmlNode item in rootElement.ChildNodes)
            {
                Console.WriteLine(item.Name);
                foreach (XmlNode ch in item.ChildNodes)
                {
                    Console.WriteLine(ch.Name);
                    if (ch.Name == "item")
                    {
                        HabrNews hNews = new HabrNews();

                        foreach (XmlNode news in ch.ChildNodes)
                        {
                            if (news.Name == "title")
                            {
                                hNews.title = news.InnerText;
                            }
                            else if (news.Name == "link")
                            {
                                hNews.link = news.InnerText;
                            }
                            else if(news.Name == "description")
                            {
                                hNews.Description = news.InnerText;
                            }
                            else if(news.Name == "pubDate")
                            {
                                hNews.PubDate = DateTime.Parse(news.InnerText);
                            }
                            habrNews.Add(hNews);

                            Console.WriteLine("--->" + news.Name);
                        }
                    }
                }
            }



            foreach (var item in habrNews)
            {
                Console.WriteLine(item.title);
                Console.WriteLine(item.link);
                Console.WriteLine("");
            }
            var test = "";

            string s = "";
            foreach (var item in habrNews)
            {
                s += item.title + " " +item.link +" "+ item.Description +""+ item.PubDate;
            }

            string patht = Path.Combine("text.txt");


            FileInfo fi = new FileInfo(patht);
            FileStream file = fi.Create();
            StreamWriter writer = new StreamWriter(s);
            writer.Close();
            file.Close();

        }
    }
}
