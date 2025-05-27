using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string name)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FormClosing_);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            PartnersForm partnersForm = new PartnersForm();
            partnersForm.Show();
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddEditForm editForm = new AddEditForm();
            editForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormClosing_(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            PartnerHistoryForm partnerHistoryForm = new PartnerHistoryForm();
            partnerHistoryForm.Show();
        }
    }
}
