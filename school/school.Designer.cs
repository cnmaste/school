namespace school
{
    partial class school
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cabinetLabel = new System.Windows.Forms.Label();
            this.floorLabel = new System.Windows.Forms.Label();
            this.capacityLabel = new System.Windows.Forms.Label();
            this.cabUpDown = new System.Windows.Forms.NumericUpDown();
            this.floorUpDown = new System.Windows.Forms.NumericUpDown();
            this.capacityUpDown = new System.Windows.Forms.NumericUpDown();
            this.equipGroupBox = new System.Windows.Forms.GroupBox();
            this.specGroupBox = new System.Windows.Forms.GroupBox();
            this.addButton = new System.Windows.Forms.Button();
            this.rmButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cabUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.capacityUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 239);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(840, 202);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // cabinetLabel
            // 
            this.cabinetLabel.AutoSize = true;
            this.cabinetLabel.Location = new System.Drawing.Point(91, 28);
            this.cabinetLabel.Name = "cabinetLabel";
            this.cabinetLabel.Size = new System.Drawing.Size(63, 13);
            this.cabinetLabel.TabIndex = 10;
            this.cabinetLabel.Text = "Кабинет №";
            // 
            // floorLabel
            // 
            this.floorLabel.AutoSize = true;
            this.floorLabel.Location = new System.Drawing.Point(121, 69);
            this.floorLabel.Name = "floorLabel";
            this.floorLabel.Size = new System.Drawing.Size(33, 13);
            this.floorLabel.TabIndex = 11;
            this.floorLabel.Text = "Этаж";
            // 
            // capacityLabel
            // 
            this.capacityLabel.AutoSize = true;
            this.capacityLabel.Location = new System.Drawing.Point(57, 112);
            this.capacityLabel.Name = "capacityLabel";
            this.capacityLabel.Size = new System.Drawing.Size(97, 13);
            this.capacityLabel.TabIndex = 12;
            this.capacityLabel.Text = "Вместительность";
            // 
            // cabUpDown
            // 
            this.cabUpDown.Location = new System.Drawing.Point(169, 22);
            this.cabUpDown.Name = "cabUpDown";
            this.cabUpDown.Size = new System.Drawing.Size(120, 20);
            this.cabUpDown.TabIndex = 13;
            // 
            // floorUpDown
            // 
            this.floorUpDown.Location = new System.Drawing.Point(169, 67);
            this.floorUpDown.Name = "floorUpDown";
            this.floorUpDown.Size = new System.Drawing.Size(120, 20);
            this.floorUpDown.TabIndex = 14;
            // 
            // capacityUpDown
            // 
            this.capacityUpDown.Location = new System.Drawing.Point(169, 110);
            this.capacityUpDown.Name = "capacityUpDown";
            this.capacityUpDown.Size = new System.Drawing.Size(120, 20);
            this.capacityUpDown.TabIndex = 15;
            // 
            // equipGroupBox
            // 
            this.equipGroupBox.Location = new System.Drawing.Point(451, 22);
            this.equipGroupBox.Name = "equipGroupBox";
            this.equipGroupBox.Size = new System.Drawing.Size(200, 207);
            this.equipGroupBox.TabIndex = 16;
            this.equipGroupBox.TabStop = false;
            this.equipGroupBox.Text = "Оборудование";
            // 
            // specGroupBox
            // 
            this.specGroupBox.Location = new System.Drawing.Point(657, 28);
            this.specGroupBox.Name = "specGroupBox";
            this.specGroupBox.Size = new System.Drawing.Size(200, 201);
            this.specGroupBox.TabIndex = 17;
            this.specGroupBox.TabStop = false;
            this.specGroupBox.Text = "Назначение";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(324, 35);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 18;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // rmButton
            // 
            this.rmButton.Location = new System.Drawing.Point(324, 92);
            this.rmButton.Name = "rmButton";
            this.rmButton.Size = new System.Drawing.Size(75, 23);
            this.rmButton.TabIndex = 19;
            this.rmButton.Text = "Delete";
            this.rmButton.UseVisualStyleBackColor = true;
            this.rmButton.Click += new System.EventHandler(this.rmButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(254, 184);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 20;
            // 
            // school
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 452);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.rmButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.specGroupBox);
            this.Controls.Add(this.equipGroupBox);
            this.Controls.Add(this.capacityUpDown);
            this.Controls.Add(this.floorUpDown);
            this.Controls.Add(this.cabUpDown);
            this.Controls.Add(this.capacityLabel);
            this.Controls.Add(this.floorLabel);
            this.Controls.Add(this.cabinetLabel);
            this.Controls.Add(this.dataGridView1);
            this.Name = "school";
            this.Text = "School";
            this.Load += new System.EventHandler(this.school_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cabUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floorUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.capacityUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label cabinetLabel;
        private System.Windows.Forms.Label floorLabel;
        private System.Windows.Forms.Label capacityLabel;
        private System.Windows.Forms.NumericUpDown cabUpDown;
        private System.Windows.Forms.NumericUpDown floorUpDown;
        private System.Windows.Forms.NumericUpDown capacityUpDown;
        private System.Windows.Forms.GroupBox equipGroupBox;
        private System.Windows.Forms.GroupBox specGroupBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button rmButton;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

