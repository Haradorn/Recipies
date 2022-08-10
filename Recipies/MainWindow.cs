using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Recipies;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Recipies
{
    public partial class MainWindow : Form
    {
        ApplicationContext db;
        public MainWindow()
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
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        //-----------------------------------------------------------------------------------------------------------
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutApplication aboutApplication = new AboutApplication();
            aboutApplication.ShowDialog();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            db.Database.EnsureCreated();
            bool isAvalaible = db.Database.CanConnect();
            if (!isAvalaible) MessageBox.Show("База данных не доступна!");
            var healthGroups = db.HealthGroups.ToList();
            healthGroupDataGridView.Columns.Add("Id", "Код");
            healthGroupDataGridView.Columns.Add("Name", "Наименование");
            if (healthGroups.Count == 0)
                return;
            else
            {
                if (healthGroups.Count == 1)
                {
                    healthGroupDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    int count = 0;
                    foreach (HealthGroup hg in healthGroups)
                    {
                        healthGroupDataGridView.Rows[count].Cells["Id"].Value = hg.Id;
                        healthGroupDataGridView.Rows[count].Cells["Name"].Value = hg.Name;
                        count++;
                    }
                    this.healthGroupDataGridView.Columns["Id"].Visible = false;
                }
                else
                {
                    healthGroupDataGridView.Rows.Add(healthGroups.Count - 1);
                    healthGroupDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    int count = 0;
                    foreach (HealthGroup hg in healthGroups)
                    {
                        healthGroupDataGridView.Rows[count].Cells["Id"].Value = hg.Id;
                        healthGroupDataGridView.Rows[count].Cells["Name"].Value = hg.Name;
                        count++;
                    }
                    this.healthGroupDataGridView.Columns["Id"].Visible = false;
                }
            }

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddHealthGroupForm addHealthGroupForm = new AddHealthGroupForm();
            DialogResult result = addHealthGroupForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                try
                {
                    HealthGroup healthGroup = new HealthGroup();
                    healthGroup.Name = addHealthGroupForm.textBox1.Text;
                    healthGroup.Description = addHealthGroupForm.textBox2.Text;
                    healthGroup.Photo = ConvertFiletoByte(addHealthGroupForm.pictureBox1.ImageLocation);
                    db.HealthGroups.Add(healthGroup);
                    db.SaveChanges();
                    MessageBox.Show("Новая запись добавлена");
                    var healthGroups1 = db.HealthGroups.ToList();

                    if (healthGroups1.Count == 1)
                    {
                        healthGroupDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        int count = 0;
                        foreach (HealthGroup hg in healthGroups1)
                        {
                            healthGroupDataGridView.Rows[count].Cells["Id"].Value = hg.Id;
                            healthGroupDataGridView.Rows[count].Cells["Name"].Value = hg.Name;
                            count++;
                        }
                        this.healthGroupDataGridView.Columns["Id"].Visible = false;
                    }
                    else
                    {
                        healthGroupDataGridView.Rows.Clear();
                        healthGroupDataGridView.Rows.Add(healthGroups1.Count - 1);
                        int count1 = 0;
                        foreach (HealthGroup hg in healthGroups1)
                        {
                            healthGroupDataGridView.Rows[count1].Cells["Id"].Value = hg.Id;
                            healthGroupDataGridView.Rows[count1].Cells["Name"].Value = hg.Name;
                            count1++;
                        }
                        this.healthGroupDataGridView.Columns["Id"].Visible = false;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LookAndSearchHealthGroupsForm lookAndSearchHealthGroupsForm = new LookAndSearchHealthGroupsForm();
            lookAndSearchHealthGroupsForm.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (healthGroupDataGridView.SelectedRows.Count > 0)
                {
                    int index = healthGroupDataGridView.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(healthGroupDataGridView[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    HealthGroup healthGroup = db.HealthGroups.Find(id);
                    AddHealthGroupForm addHealthGroupForm = new AddHealthGroupForm();
                    addHealthGroupForm.textBox1.Text = healthGroup.Name;
                    addHealthGroupForm.textBox2.Text = healthGroup.Description;
                    List<HealthGroup> healthGroups = db.HealthGroups.ToList();
                    DialogResult result = addHealthGroupForm.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                        return;
                    if (healthGroup != null)
                    {
                        healthGroup.Name = addHealthGroupForm.textBox1.Text;
                        healthGroup.Description = addHealthGroupForm.textBox2.Text;
                        db.SaveChanges();

                        var healthGroups1 = db.HealthGroups.ToList();
                        int count = 0;
                        foreach (HealthGroup hg in healthGroups1)
                        {
                            healthGroupDataGridView.Rows[count].Cells["Id"].Value = hg.Id;
                            healthGroupDataGridView.Rows[count].Cells["Name"].Value = hg.Name;
                            count++;
                        }
                        this.healthGroupDataGridView.Columns["Id"].Visible = false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Нет данных для редактирования!");
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (healthGroupDataGridView.SelectedRows.Count > 0)
            {
                int index = healthGroupDataGridView.SelectedRows[0].Index;
                int id = 0;
                try
                {
                    bool converted = Int32.TryParse(healthGroupDataGridView[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    HealthGroup healthGroup = db.HealthGroups.Find(id);
                    db.HealthGroups.Remove(healthGroup);
                    db.SaveChanges();
                    MessageBox.Show("Группа здоровья удалена");
                }
                catch(Exception)
                {
                    MessageBox.Show("В таблице 'Группы здоровья' не осталось записей!");
                }             

                var healthGroups = db.HealthGroups.ToList();
                healthGroupDataGridView.Rows.Clear();


                if (healthGroups.Count == 1)
                {
                    healthGroupDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    int count = 0;
                    foreach (HealthGroup hg in healthGroups)
                    {
                        healthGroupDataGridView.Rows[count].Cells["Id"].Value = hg.Id;
                        healthGroupDataGridView.Rows[count].Cells["Name"].Value = hg.Name;
                        count++;
                    }
                    this.healthGroupDataGridView.Columns["Id"].Visible = false;
                }
                else if (healthGroups.Count == 0)
                {
                    return;
                }
                else
                {
                    healthGroupDataGridView.Rows.Add(healthGroups.Count - 1);
                    healthGroupDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    int count = 0;
                    foreach (HealthGroup hg in healthGroups)
                    {
                        healthGroupDataGridView.Rows[count].Cells["Id"].Value = hg.Id;
                        healthGroupDataGridView.Rows[count].Cells["Name"].Value = hg.Name;
                        count++;
                    }
                    this.healthGroupDataGridView.Columns["Id"].Visible = false;
                }
            }
        }
    }
}
