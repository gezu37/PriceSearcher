using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.IO;
using System.Web;


namespace PriceSearcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           string addr = System.Net.WebUtility.UrlEncode(textBox1.Text);   //по нажатию кнопки преобразуем текст в урл код и подставляем в ссылку магазина, открываем ее вэлементе webbrowser 
            webBrowser1.Navigate($"https://shop-lot.ru/search/?s={addr}");
         
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)  //в этом методе после успешной загрузки веб страницы сохраняем ее исходный код и записываем в файл
        {
            string way = "D:\\1.html";
           var doc1 = webBrowser1.Document.Body.OuterHtml;
           using (StreamWriter w = new StreamWriter(way, false, Encoding.GetEncoding(1251)))
            {
                w.Write(doc1);
            }
           Parcer zapros = new Parcer(way); //вызываем наш парсер
           zapros.items_selection(int.Parse(textBox2.Text), int.Parse(textBox3.Text));
           label1.Text = zapros.names1[0] + "    " + zapros.prices1[0] + " руб."; //вывод полученных данных
           label2.Text = zapros.names1[1] + "    " + zapros.prices1[1] + " руб.";
           label3.Text = zapros.names1[2] + "    " + zapros.prices1[2] + " руб.";
           label4.Text = zapros.names1[3] + "    " + zapros.prices1[3] + " руб.";
           linkLabel1.Text = "https://shop-lot.ru" + zapros.links1[0];
           linkLabel2.Text = "https://shop-lot.ru" + zapros.links1[1];
           linkLabel3.Text = "https://shop-lot.ru" + zapros.links1[2];
           linkLabel4.Text = "https://shop-lot.ru" + zapros.links1[3];

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start($"{linkLabel1.Text}");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start($"{linkLabel2.Text}");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start($"{linkLabel3.Text}");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start($"{linkLabel4.Text}");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }

    public class Parcer
    {
        private List<string> prices = new List<string>();
        private List<string> names = new List<string>();
        private List<string> links = new List<string>();
        public List <string> links1
        {
            get 
            { 
                return links; 
            }
            set
            {
                links = value;
            }
        }
        public List<string> names1
        {
            get
            {
                return names;
            }
            set
            {
                names = value;
            }
        }
        public List<string> prices1
        {
            get
            {
                return prices;
            }
            set
            {
                prices = value;
            }
        }
        private string way;

        public string way1
        { 
          get { return way; }
          set { way = value; }
        }
        public Parcer (string way)
        {
            this.way = way;
        }
        public void items_selection(int a, int b)
        {
            HtmlWeb web = new HtmlWeb();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(way); // открываем ранее созданный файл исходного кода страницы

            var liNodes = doc.DocumentNode.SelectNodes("//div[@class='prod_info']"); //отбираем все ноды с информацией о продуктах на странице в отдельный   лист
            var list_q = liNodes.ToList();
            foreach (var ll in list_q) //во втором цены
            {
                string qq = ll.InnerHtml;
                var d = new HtmlAgilityPack.HtmlDocument();
                d.LoadHtml(qq);
                HtmlNode price_nod = d.DocumentNode.SelectSingleNode("//span[@class='price_num']");
                string price = price_nod.InnerText;
                price = price.Replace(" ", string.Empty);
                if (int.Parse(price) >= a & int.Parse(price) <= b)
                    prices.Add(price);
            }
            foreach (var ll in list_q) //проходимся по листу вытаскивая из каждого нода с информацией о продукте необходимый текст, в первом цикле названия
            {
                string qq = ll.InnerHtml;
                var d = new HtmlAgilityPack.HtmlDocument();
                d.LoadHtml(qq);
                HtmlNode name_nod = d.DocumentNode.SelectSingleNode("//span[@itemprop='name']");
                string name = name_nod.InnerText;
                HtmlNode price_nod = d.DocumentNode.SelectSingleNode("//span[@class='price_num']");
                string price = price_nod.InnerText;
                price = price.Replace(" ", string.Empty);
                if (int.Parse(price) >= a & int.Parse(price) <= b)
                    names.Add(name);
            }
           
            foreach (var ll in list_q) //ссылки
            {
                string qq = ll.InnerHtml;
                var d = new HtmlAgilityPack.HtmlDocument();
                d.LoadHtml(qq);
                HtmlNode link_nod = d.DocumentNode.SelectSingleNode("//a[@href]");
                HtmlAttribute att = link_nod.Attributes["href"];
                string link = att.Value;
                HtmlNode price_nod = d.DocumentNode.SelectSingleNode("//span[@class='price_num']");
                string price = price_nod.InnerText;
                price = price.Replace(" ", string.Empty);
                if (int.Parse(price) >= a & int.Parse(price) <= b)
                    links.Add(link);
            }
        }
         public int price_sum(int a, int b)
        {
            int sum = a + b;
            return sum;
        }
        public int price_sum(int a, int b, int c)
        {
            int sum = a + b + c;
            return sum;
        }
        public int price_dif(int a, int b)
        {
            int dif = a - b;
            return dif;
        }
        public int price_dif(int a, int b, int c)
        {
            int dif = a - b - c;
            return dif;
        }
        public virtual string writeSearch()
        {
            return "Неверный формат ввода";
        }
    }
   public class Searcher: Parcer
    {
      public Searcher(string way)
        :base(way)
        {
         this.way1 = way;
        }
        public override string writeSearch()
        {
            return "Введите значение";
        }
    }
}
