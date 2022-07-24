namespace Recipies
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.информацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuTab = new System.Windows.Forms.TabControl();
            this.menuTab1 = new System.Windows.Forms.TabPage();
            this.countriesRecepiesGroupBox = new System.Windows.Forms.GroupBox();
            this.countryRecepiesList = new System.Windows.Forms.ListBox();
            this.countriesDataGridView = new System.Windows.Forms.DataGridView();
            this.menuTab2 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.mainMenuTab.SuspendLayout();
            this.menuTab1.SuspendLayout();
            this.countriesRecepiesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countriesDataGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.информацияToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // информацияToolStripMenuItem
            // 
            this.информацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.информацияToolStripMenuItem.Name = "информацияToolStripMenuItem";
            this.информацияToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.информацияToolStripMenuItem.Text = "Информация";
            // 
            // mainMenuTab
            // 
            this.mainMenuTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainMenuTab.Controls.Add(this.menuTab1);
            this.mainMenuTab.Controls.Add(this.menuTab2);
            this.mainMenuTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainMenuTab.Location = new System.Drawing.Point(0, 52);
            this.mainMenuTab.Name = "mainMenuTab";
            this.mainMenuTab.SelectedIndex = 0;
            this.mainMenuTab.Size = new System.Drawing.Size(800, 509);
            this.mainMenuTab.TabIndex = 3;
            // 
            // menuTab1
            // 
            this.menuTab1.BackColor = System.Drawing.Color.PowderBlue;
            this.menuTab1.Controls.Add(this.countriesRecepiesGroupBox);
            this.menuTab1.Location = new System.Drawing.Point(4, 25);
            this.menuTab1.Name = "menuTab1";
            this.menuTab1.Padding = new System.Windows.Forms.Padding(3);
            this.menuTab1.Size = new System.Drawing.Size(792, 480);
            this.menuTab1.TabIndex = 0;
            this.menuTab1.Text = "Страны";
            // 
            // countriesRecepiesGroupBox
            // 
            this.countriesRecepiesGroupBox.Controls.Add(this.countryRecepiesList);
            this.countriesRecepiesGroupBox.Controls.Add(this.countriesDataGridView);
            this.countriesRecepiesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.countriesRecepiesGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.countriesRecepiesGroupBox.Location = new System.Drawing.Point(3, 3);
            this.countriesRecepiesGroupBox.Name = "countriesRecepiesGroupBox";
            this.countriesRecepiesGroupBox.Size = new System.Drawing.Size(786, 474);
            this.countriesRecepiesGroupBox.TabIndex = 3;
            this.countriesRecepiesGroupBox.TabStop = false;
            this.countriesRecepiesGroupBox.Text = "Рецепты из разных стран";
            // 
            // countryRecepiesList
            // 
            this.countryRecepiesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.countryRecepiesList.BackColor = System.Drawing.SystemColors.Menu;
            this.countryRecepiesList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.countryRecepiesList.FormattingEnabled = true;
            this.countryRecepiesList.ItemHeight = 20;
            this.countryRecepiesList.Location = new System.Drawing.Point(308, 38);
            this.countryRecepiesList.Name = "countryRecepiesList";
            this.countryRecepiesList.Size = new System.Drawing.Size(461, 402);
            this.countryRecepiesList.TabIndex = 4;
            // 
            // countriesDataGridView
            // 
            this.countriesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.countriesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.countriesDataGridView.Location = new System.Drawing.Point(17, 38);
            this.countriesDataGridView.Name = "countriesDataGridView";
            this.countriesDataGridView.Size = new System.Drawing.Size(262, 402);
            this.countriesDataGridView.TabIndex = 3;
            // 
            // menuTab2
            // 
            this.menuTab2.BackColor = System.Drawing.Color.PowderBlue;
            this.menuTab2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuTab2.Location = new System.Drawing.Point(4, 25);
            this.menuTab2.Name = "menuTab2";
            this.menuTab2.Padding = new System.Windows.Forms.Padding(3);
            this.menuTab2.Size = new System.Drawing.Size(792, 480);
            this.menuTab2.TabIndex = 1;
            this.menuTab2.Text = "Группа здоровья";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Recipies.Properties.Resources.add_1_icon;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Добавить рецепт";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Recipies.Properties.Resources.Pencil_icon;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Изменить рецепт";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::Recipies.Properties.Resources.delete_icon;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Удалить рецепт";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::Recipies.Properties.Resources.print_icon;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Распечатать рецепт";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::Recipies.Properties.Resources.Actions_document_save_icon;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Сохранить рецепт";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Image = global::Recipies.Properties.Resources._11573231501558095979_128;
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Image = global::Recipies.Properties.Resources._20814518601537355605_128;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mainMenuTab);
            this.Controls.Add(this.mainMenuStrip);
            this.ForeColor = System.Drawing.Color.Navy;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник рецептов для здорового образа жизни";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainMenuTab.ResumeLayout(false);
            this.menuTab1.ResumeLayout(false);
            this.countriesRecepiesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.countriesDataGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem информацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.TabControl mainMenuTab;
        private System.Windows.Forms.TabPage menuTab1;
        private System.Windows.Forms.GroupBox countriesRecepiesGroupBox;
        private System.Windows.Forms.ListBox countryRecepiesList;
        private System.Windows.Forms.DataGridView countriesDataGridView;
        private System.Windows.Forms.TabPage menuTab2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
    }
}

