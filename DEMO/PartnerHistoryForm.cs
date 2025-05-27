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
    public partial class PartnerHistoryForm : Form
    {
        private List<Partner> partners = null;

        public PartnerHistoryForm(List<Partner> list)
        {
            InitializeComponent();
            partners = list;
        }

        public PartnerHistoryForm()
        {
            InitializeComponent();
          
        }

        private void PartnerHistoryForm_Load(object sender, EventArgs e)
        {
            this.partner_ProductsTableAdapter.Fill(this.masterPolDataSet.Partner_products);

            IEnumerable<DataRow> rows = masterPolDataSet.Partner_products.AsEnumerable();

            if (partners != null && partners.Count > 0)
            {
                var partnerNames = partners.Select(p => p.Name).ToList();
                rows = rows.Where(row => partnerNames.Contains(row.Field<string>("Наименование партнера")));
            }

            dataGridView1.Rows.Clear();

            DataTable table = new DataTable();
            table.Columns.Add("Партнёр");
            table.Columns.Add("Продукция");
            table.Columns.Add("Количество");
            table.Columns.Add("Дата продажи");

            foreach (DataRow row in rows)
            {
                string partnerName = row["Наименование партнера"].ToString();
                string productName = row["Продукция"].ToString();
                string quantity = row["Количество продукции"].ToString();

                string date = row.IsNull("Дата продажи")
                    ? "—"
                    : Convert.ToDateTime(row["Дата продажи"]).ToShortDateString();

                table.Rows.Add(partnerName, productName, quantity, date);
            }

            dataGridView1.DataSource = table;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (partners == null)
            {
                Form1 form1 = new Form1();
                form1.Show();
            }
            else
            {
                PartnersForm partnersForm = new PartnersForm();
                partnersForm.Show();
            }
            this.Hide();

        }
    }
}

