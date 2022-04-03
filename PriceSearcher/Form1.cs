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
           string addr = System.Net.WebUtility.UrlEncode(textBox1.Text);
            webBrowser1.Navigate($"https://shop-lot.ru/search/?s={addr}");
         
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           var doc1 = webBrowser1.Document.Body.OuterHtml;
           using (StreamWriter w = new StreamWriter("D:\\1.html", false, Encoding.GetEncoding(1251)))
            {
                w.Write(doc1);
            }
           Parcer zapros = new Parcer();
           zapros.items_selection(textBox1.Text);
           label1.Text = zapros.names[0] + "    " + zapros.prices[0];
           label2.Text = zapros.names[1] + "    " + zapros.prices[1];
           label3.Text = zapros.names[2] + "    " + zapros.prices[2];

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Parcer
    {
        public List<string> prices = new List<string>();
        public List<string> names = new List<string>();
        public List<string> links = new List<string>();
        public void items_selection(string item)
        {
            HtmlWeb web = new HtmlWeb();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load("D:\\1.html");

            var liNodes = doc.DocumentNode.SelectNodes("//div[@class='prod_info']");
            var list_q = liNodes.ToList();
            foreach (var ll in list_q)
            {
                string qq = ll.InnerHtml;
                var d = new HtmlAgilityPack.HtmlDocument();
                d.LoadHtml(qq);
                HtmlNode name_nod = d.DocumentNode.SelectSingleNode("//span[@itemprop='name']");
                string name = name_nod.InnerText;
                names.Add(name);
            }
            foreach (var ll in list_q)
            {
                string qq = ll.InnerHtml;
                var d = new HtmlAgilityPack.HtmlDocument();
                d.LoadHtml(qq);
                HtmlNode price_nod = d.DocumentNode.SelectSingleNode("//span[@class='price_num']");
                string price = price_nod.InnerText;
                prices.Add(price);
            }
           

        }
    }
}
