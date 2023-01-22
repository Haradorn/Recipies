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
        int counter = 0; // сквозной номер строки в массиве строк, которые выводятся
        int curPage; // текущая страница
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
            this.rTB_DescriptionRecepie.Text = description;
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
            IEnumerable<string> s = description.Split(size);
            text += String.Join(Environment.NewLine, s);
            //320х250 картинка очень маленькая, а 640х500 очень большая
            e.Graphics.DrawImage(pictureBox1.Image, 450, 50);
            e.Graphics.DrawString(nameRecipieLabel.Text, new Font("Arial", 14), Brushes.Black, 10, 50);
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
                    sw.WriteLine(rTB_DescriptionRecepie.Text);
                    sw.Close();
                }
            }
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            counter = 0;
            curPage = 1;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Создать шрифт myFont
            Font myFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);

            string curLine; // текущая выводимая строка

            // Отступы внутри страницы
            float leftMargin = e.MarginBounds.Left; // отступы слева в документе
            float topMargin = e.MarginBounds.Top; // отступы сверху в документе
            float yPos = 0; // текущая позиция Y для вывода строки

            int nPages; // количество страниц
            int nLines; // максимально-возможное количество строк на странице
            int i; // номер текущей строки для вывода на странице

            // Вычислить максимально возможное количество строк на странице
            nLines = (int)(e.MarginBounds.Height / myFont.GetHeight(e.Graphics));

            // Вычислить количество страниц для печати
            nPages = (rTB_DescriptionRecepie.Lines.Length - 1) / nLines + 1;

            // Цикл печати/вывода одной страницы
            i = 0;
            while ((i < nLines) && (counter < rTB_DescriptionRecepie.Lines.Length))
            {
                // Взять строку для вывода из richTextBox1
                curLine = rTB_DescriptionRecepie.Lines[counter];

                // Вычислить текущую позицию по оси Y
                yPos = topMargin + i * myFont.GetHeight(e.Graphics);
                e.Graphics.DrawImage(pictureBox1.Image, 450, 50);
                // Вывести строку в документ
                e.Graphics.DrawString(curLine, myFont, Brushes.Blue,
                  leftMargin, yPos, new StringFormat());

                counter++;
                i++;
            }

            // Если весь текст не помещается на 1 страницу, то
            // нужно добавить дополнительную страницу для печати
            e.HasMorePages = false;

            if (curPage < nPages)
            {
                curPage++;
                e.HasMorePages = true;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
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
