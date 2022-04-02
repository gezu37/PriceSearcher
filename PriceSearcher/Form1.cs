using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
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
            Parcer zapros = new Parcer();
            zapros.items_selection(textBox1.Text);
            label1.Text = zapros.names[0] + "    " + zapros.prices[0];
            label2.Text = zapros.names[1] + "    " + zapros.prices[1];
            label3.Text = zapros.names[2] + "    " + zapros.prices[2];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
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
            
            //HtmlWeb web = new HtmlWeb();
            // HtmlAgilityPack.HtmlDocument doc = web.Load("https://shop-lot.ru/search/?s=%D0%A1%D0%A2%D0%98%D0%A0%D0%90%D0%9B%D0%AC%D0%9D%D0%90%D0%AF+%D0%9C%D0%90%D0%A8%D0%98%D0%9D%D0%90");
            HtmlWeb web = new HtmlWeb();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load("d:\\1.html");

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
