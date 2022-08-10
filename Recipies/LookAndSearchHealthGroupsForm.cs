using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Recipies
{
    public partial class LookAndSearchHealthGroupsForm : Form
    {
        ApplicationContext db;
        public LookAndSearchHealthGroupsForm()
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.UseSqlite(connectionString).Options;
            db = new ApplicationContext(options);
            InitializeComponent();
            List<HealthGroup> healthGroup = db.HealthGroups.ToList();
            this.listBox1.DataSource = healthGroup;
            this.listBox1.ValueMember = "Id";
            this.listBox1.DisplayMember = "Name";
        }
        //методы конвертирования файла изображения в поток байтов и наоборот
        //----------------------------------------------------------------------------------------------------------
        private byte[] ConvertFiletoByte(string sPath)
        {
            byte[] data = null;
            if (sPath == null)
            {
                sPath = @"C:\Users\Amadeus\Documents\Visual Studio 2019\Projects\CargoBase\empty.jpg";
                FileInfo fInfo = new FileInfo(sPath);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                data = br.ReadBytes((int)numBytes);
                return data;
            }
            else
            {
                FileInfo fInfo = new FileInfo(sPath);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                data = br.ReadBytes((int)numBytes);
                return data;
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

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            HealthGroup healthGroup = (HealthGroup)this.listBox1.SelectedItem;
            this.textBox1.Text = healthGroup.Name;
            this.textBox2.Text = healthGroup.Description;
            this.pictureBox1.Image = ConvertBytetoImage(healthGroup.Photo);
        }

        private void LookAndSearchHealthGroupsForm_Load(object sender, EventArgs e)
        {
            HealthGroup healthGroup = (HealthGroup)this.listBox1.SelectedItem;
            this.textBox1.Text = healthGroup.Name;
            this.textBox2.Text = healthGroup.Description;
            this.pictureBox1.Image = ConvertBytetoImage(healthGroup.Photo);
        }
        //-----------------------------------------------------------------------------------------------------------

    }
}
