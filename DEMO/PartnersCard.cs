using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEMO
{
    public class PartnersCard : UserControl
    {
        private Label Director;
        private Label phoneNumber;
        private Label rate;
        private Label Discount;
        private CheckBox checkBox1;
        private Label TypeAndName;

        public bool IsSelected => checkBox1.Checked;
        public PartnersCard()
        {
            InitializeComponent(); 
        }

        private void InitializeComponent()
        {
            this.TypeAndName = new System.Windows.Forms.Label();
            this.Director = new System.Windows.Forms.Label();
            this.phoneNumber = new System.Windows.Forms.Label();
            this.rate = new System.Windows.Forms.Label();
            this.Discount = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TypeAndName
            // 
            this.TypeAndName.AutoSize = true;
            this.TypeAndName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TypeAndName.Location = new System.Drawing.Point(20, 17);
            this.TypeAndName.Name = "TypeAndName";
            this.TypeAndName.Size = new System.Drawing.Size(291, 28);
            this.TypeAndName.TabIndex = 0;
            this.TypeAndName.Text = "Тип | Наименование партнера";
            this.TypeAndName.Click += new System.EventHandler(this.label1_Click);
            // 
            // Director
            // 
            this.Director.AutoSize = true;
            this.Director.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Director.Location = new System.Drawing.Point(23, 45);
            this.Director.Name = "Director";
            this.Director.Size = new System.Drawing.Size(86, 23);
            this.Director.TabIndex = 1;
            this.Director.Text = "Директор";
            // 
            // phoneNumber
            // 
            this.phoneNumber.AutoSize = true;
            this.phoneNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.phoneNumber.Location = new System.Drawing.Point(21, 68);
            this.phoneNumber.Name = "phoneNumber";
            this.phoneNumber.Size = new System.Drawing.Size(90, 23);
            this.phoneNumber.TabIndex = 2;
            this.phoneNumber.Text = "+8 987654";
            // 
            // rate
            // 
            this.rate.AutoSize = true;
            this.rate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rate.Location = new System.Drawing.Point(22, 90);
            this.rate.Name = "rate";
            this.rate.Size = new System.Drawing.Size(100, 23);
            this.rate.TabIndex = 3;
            this.rate.Text = "Рейтинг: 10";
            // 
            // Discount
            // 
            this.Discount.AutoSize = true;
            this.Discount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Discount.Location = new System.Drawing.Point(559, 17);
            this.Discount.Name = "Discount";
            this.Discount.Size = new System.Drawing.Size(50, 28);
            this.Discount.TabIndex = 4;
            this.Discount.Text = "10%";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(509, 94);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(167, 20);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "История реализации";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // PartnersCard
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Discount);
            this.Controls.Add(this.rate);
            this.Controls.Add(this.phoneNumber);
            this.Controls.Add(this.Director);
            this.Controls.Add(this.TypeAndName);
            this.Name = "PartnersCard";
            this.Size = new System.Drawing.Size(700, 135);
            this.Load += new System.EventHandler(this.PartnersCard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void SetData(string type, string name, string discount, string rat, string phone, string dir)
        {
            TypeAndName.Text = $"{type} | {name}";
            rate.Text = $"Рейтинг: {rat}";
            Director.Text = $"{dir}";
            phoneNumber.Text = $"{phone}";
            Discount.Text = $"{discount}%";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PartnersCard_Load(object sender, EventArgs e)
        {

        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
