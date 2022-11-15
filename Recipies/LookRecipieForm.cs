using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recipies
{
    public partial class LookRecipieForm : Form
    {
        string name, description;
        byte[] photo;
        public LookRecipieForm(string name, string description, byte[] photo)
        {
            this.name = name;
            this.description = description;
            this.photo = photo;
            InitializeComponent();
        }

        private void LookRecipieForm_Load(object sender, EventArgs e)
        {
            this.nameRecipieLabel.Text = name;
            this.descriptionRecipieLabel.Text = description;
            this.pictureBox1.Image = ConvertBytetoImage(this.photo);
        }
        private string text = "";
        private void button1_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

            PrintDialog printDialog = new PrintDialog();

            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
                printDocument.Print();
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int size = 50;
            IEnumerable<string> s = descriptionRecipieLabel.Text.Split(size);
            text += String.Join(Environment.NewLine, s);

            e.Graphics.DrawImage(pictureBox1.Image, 450, 50);
            e.Graphics.DrawString(nameRecipieLabel.Text, new Font("Arial", 12), Brushes.Black, 10, 50);
            e.Graphics.DrawString(text, new Font("Arial", 12), Brushes.Black, 30, 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "*.doc";
            sfd.Filter = "Doc files|*.doc";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                sfd.FileName.Length > 0)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, true))
                {
                    sw.WriteLine(nameRecipieLabel.Text);
                    sw.WriteLine(descriptionRecipieLabel.Text);
                    sw.Close();
                }
            }
        }

        private Image ConvertBytetoImage(byte[] photo)
        {
            Image newImage;
            using (MemoryStream ms = new MemoryStream(photo, 0, photo.Length))
            {
                ms.Write(photo, 0, photo.Length);
                newImage = Image.FromStream(ms, true);
                return newImage;
            }
        }
    }
    public static class Extensions
    {
        public static IEnumerable<string> Split(this string str, int n)
        {
            if (String.IsNullOrEmpty(str) || n < 1)
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < str.Length; i += n)
            {
                yield return str.Substring(i, Math.Min(n, str.Length - i));
            }
        }
    }
}
