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
            string a = zapros.items_selection(textBox1.Text);
            label1.Text = a;
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
        string name = string.Empty;
        string link = string.Empty;
        string price = string.Empty;
        public string items_selection(string item)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://shop-lot.ru/search/?s=%D0%A1%D0%A2%D0%98%D0%A0%D0%90%D0%9B%D0%AC%D0%9D%D0%90%D0%AF+%D0%9C%D0%90%D0%A8%D0%98%D0%9D%D0%90");
            var liNodes = doc.DocumentNode.SelectNodes("//div[@class='prod_info']");
           
            
                HtmlNode name_node = liNodes[0].SelectSingleNode("//span[@itemprop='name']");
                name = name_node.InnerText;
               
            
            return name;
        }
    }
}
