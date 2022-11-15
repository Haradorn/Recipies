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
            db.Recipies.Load();
            dataGridView1.DataSource = db.Recipies.Local.ToBindingList();

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
                sPath = @"math-add-icon.jpg";
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
            //var recepies = db.Recipies.ToList();
            //dataGridView1.Columns.Add("Id", "Код");
            //dataGridView1.Columns.Add("Name", "Наименование");
            //dataGridView1.Columns.Add("Description", "Описание");
            //if (recepies.Count == 0)
            //    return;
            //else
            //{
            //    if (recepies.Count == 1)
            //    {
            //        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //        int count = 0;
            //        foreach (Recipie recipie in recepies)
            //        {
            //            dataGridView1.Rows[count].Cells["Id"].Value = recipie.Id;
            //            dataGridView1.Rows[count].Cells["Name"].Value = recipie.Name;
            //            dataGridView1.Rows[count].Cells["Description"].Value = recipie.Description;
            //            count++;
            //        }
            //        this.dataGridView1.Columns["Id"].Visible = false;
            //    }
            //    else
            //    {
            //        dataGridView1.Rows.Add(recepies.Count - 1);
            //        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //        int count = 0;
            //        foreach (Recipie recipie in recepies)
            //        {
            //            dataGridView1.Rows[count].Cells["Id"].Value = recipie.Id;
            //            dataGridView1.Rows[count].Cells["Name"].Value = recipie.Name;
            //            dataGridView1.Rows[count].Cells["Description"].Value = recipie.Description;
            //            count++;
            //        }
            //        this.dataGridView1.Columns["Id"].Visible = false;
            //    }
            //}
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            AddRecipieForm recipieForm = new AddRecipieForm();
            List<HealthGroup> healthGroups = db.HealthGroups.ToList();
            recipieForm.comboBox1.DataSource = healthGroups;
            recipieForm.comboBox1.ValueMember = "Id";
            recipieForm.comboBox1.DisplayMember = "Name";
            DialogResult result = recipieForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                try
                {
                    Recipie recipie = new Recipie();
                    recipie.Name = recipieForm.textBox1.Text;
                    recipie.Description = recipieForm.textBox2.Text;
                    recipie.Photo = ConvertFiletoByte(recipieForm.pictureBox1.ImageLocation);
                    recipie.HealthGroup = (HealthGroup)recipieForm.comboBox1.SelectedItem;
                    db.Recipies.Add(recipie);
                    db.SaveChanges();
                    MessageBox.Show("Новый рецепт добавлен!");

                    //var recipies1 = db.Recipies.ToList();
                    //if (recipies1.Count == 1)
                    //{
                    //    healthGroupDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //    int count = 0;
                    //    foreach (Recipie rc in recipies1)
                    //    {
                    //        healthGroupDataGridView.Rows[count].Cells["Id"].Value = rc.Id;
                    //        healthGroupDataGridView.Rows[count].Cells["Name"].Value = rc.Name;
                    //        count++;
                    //    }
                    //    this.healthGroupDataGridView.Columns["Id"].Visible = false;
                    //}
                    //else
                    //{
                    //    healthGroupDataGridView.Rows.Clear();
                    //    healthGroupDataGridView.Rows.Add(recipies1.Count - 1);
                    //    int count1 = 0;
                    //    foreach (Recipie rc in recipies1)
                    //    {
                    //        healthGroupDataGridView.Rows[count1].Cells["Id"].Value = rc.Id;
                    //        healthGroupDataGridView.Rows[count1].Cells["Name"].Value = rc.Name;
                    //        count1++;
                    //    }
                    //    this.healthGroupDataGridView.Columns["Id"].Visible = false;
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int index = dataGridView1.SelectedRows[0].Index;

                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    Recipie recipie = db.Recipies.Find(id);
                    AddRecipieForm addRecipieForm = new AddRecipieForm();
                    addRecipieForm.textBox1.Text = recipie.Name;
                    addRecipieForm.textBox2.Text = recipie.Description;
                    addRecipieForm.pictureBox1.Image = ConvertBytetoImage(recipie.Photo);
                    List<HealthGroup> healthGroups = db.HealthGroups.ToList();
                    addRecipieForm.comboBox1.DataSource = healthGroups;
                    addRecipieForm.comboBox1.ValueMember = "Id";
                    addRecipieForm.comboBox1.DisplayMember = "Name";
                    if (recipie.HealthGroup != null)
                        addRecipieForm.comboBox1.SelectedValue = recipie.HealthGroup.Id;
                    DialogResult result = addRecipieForm.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                        return;
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Желаете поменять изображение для этого объекта? " +
                            "Если вы не выбрали новое изображение для него и нажали Да, то старое изображение пропадет",
                            "Сменить изображение?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            recipie.Name = addRecipieForm.textBox1.Text;
                            recipie.Description = addRecipieForm.textBox2.Text;
                            recipie.Photo = ConvertFiletoByte(addRecipieForm.pictureBox1.ImageLocation);
                            recipie.HealthGroup = (HealthGroup)addRecipieForm.comboBox1.SelectedItem;
                            db.Entry(recipie).State = EntityState.Modified;
                            db.SaveChanges();
                            MessageBox.Show("Объект обновлён");
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            recipie.Name = addRecipieForm.textBox1.Text;
                            recipie.Description = addRecipieForm.textBox2.Text;
                            recipie.Photo = ConvertFiletoByte(addRecipieForm.pictureBox1.ImageLocation);
                            recipie.HealthGroup = (HealthGroup)addRecipieForm.comboBox1.SelectedItem;
                            db.Entry(recipie).State = EntityState.Modified;
                            db.SaveChanges();
                            MessageBox.Show("Объект обновлён");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    Recipie recipie = db.Recipies.Find(id);
                    db.Recipies.Remove(recipie);
                    db.SaveChanges();
                    MessageBox.Show("Рецепт удалён");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void healthGroupDataGridView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void healthGroupDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (healthGroupDataGridView.SelectedRows.Count > 0)
            {
                int index = healthGroupDataGridView.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(healthGroupDataGridView[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                HealthGroup healthGroup = db.HealthGroups.Find(id);
                healthGroupListBox.DataSource = healthGroup.Recipies.ToList();
                healthGroupListBox.DisplayMember = "Name";
            }
        }

        private void healthGroupListBox_DoubleClick(object sender, EventArgs e)
        {
            Recipie recipie = (Recipie)healthGroupListBox.SelectedItem;
            //MessageBox.Show(recipie.Name);
            Form lookRecipieForm = new LookRecipieForm(recipie.Name, recipie.Description, recipie.Photo);          
            lookRecipieForm.ShowDialog();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                label2.Visible = true;
                Recipie recipie = db.Recipies.Find(id);
                label2.Text = recipie.HealthGroup.Name;
                pictureBox1.Image = ConvertBytetoImage(recipie.Photo);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;

            label2.Visible = true;
            Recipie recipie = db.Recipies.Find(id);
            Form lookRecipieForm = new LookRecipieForm(recipie.Name, recipie.Description, recipie.Photo);
            lookRecipieForm.ShowDialog();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var recipie = db.Recipies.Where(r => EF.Functions.Like(r.Name, String.Format("%" + textBox1.Text + "%"))).ToList();

            dataGridView1.DataSource = recipie;
            dataGridView1.Columns[1].HeaderText = "Наименование";
        }
    }
}
