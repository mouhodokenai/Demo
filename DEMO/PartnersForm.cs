using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Data;
using System.Windows.Forms;

using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DEMO
{
    public partial class PartnersForm : Form
    {
        public PartnersForm()
        {
            InitializeComponent();
        }

        private int DisCount(Partner partner)
        {
            try
            {
                this.partner_ProductsTableAdapter.Fill(this.masterPolDataSet.Partner_products);
                int total = 0;

                foreach (DataRow row in masterPolDataSet.Partner_products.Rows)
                {
                    if (Convert.ToString(row["Наименование партнера"]) == partner.Name)
                    {
                        total += Convert.ToInt32(row["Количество продукции"]);
                    }
                }

                if (total >= 300000)
                    return 15;
                else if (total >= 50000)
                    return 10;
                else if (total >= 10000)
                    return 5;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расчёте скидки:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private void PartnersForm_Load(object sender, EventArgs e)
        {
            try
            {
                PartnersList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить список партнёров:\n{ex.Message}", "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PartnersList()
        {
            try
            {
                this.partnersTableAdapter.Fill(this.masterPolDataSet.Partners);
                flowLayoutPanel1.Controls.Clear();

                foreach (DataRow row in masterPolDataSet.Partners.Rows)
                {
                    try
                    {
                        Partner partner = new Partner
                        {
                            Name = row["Наименование партнера"].ToString(),
                            Type = row["Тип партнера"].ToString(),
                            Director = row["Директор"].ToString(),
                            Phone = row["Телефон партнера"].ToString(),
                            Rate = row["Рейтинг"].ToString(),
                            Email = row["Электронная почта партнера"].ToString(),
                            INN = row["ИНН"].ToString(),
                            Address = row["Юридический адрес партнера"].ToString()
                        };

                        var dis = DisCount(partner).ToString();
                        PartnersCard card = new PartnersCard();
                        card.SetData(partner.Type, partner.Name, dis, partner.Rate, partner.Phone, partner.Director);

                        card.Tag = partner;
                        card.DoubleClick += Card_DoubleClick;

                        flowLayoutPanel1.Controls.Add(card);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при создании карточки партнёра:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                }

                if (flowLayoutPanel1.Controls.Count == 0)
                {
                    MessageBox.Show("Список партнёров пуст или не удалось отобразить карточки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке партнёров:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Card_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (sender is PartnersCard card && card.Tag is Partner partner)
                {
                    AddEditForm form = new AddEditForm(partner); // Передаём данные партнёра

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        PartnersList(); // Перезагрузка списка
                        MessageBox.Show("Данные партнёра обновлены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы редактирования:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Собираем выбранных партнёров
                var selectedPartners = flowLayoutPanel1.Controls
                    .OfType<PartnersCard>()
                    .Where(card => card.IsSelected)
                    .Select(card => card.Tag as Partner)
                    .Where(p => p != null)
                    .ToList();

                PartnerHistoryForm historyForm;

                if (selectedPartners.Count == 0)
                {
                    DialogResult result = MessageBox.Show(
                        "Вы не выбрали ни одного партнёра. Показать историю всех партнёров?",
                        "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        historyForm = new PartnerHistoryForm();
                        historyForm.ShowDialog();
                    }
                }
                else
                {
                    historyForm = new PartnerHistoryForm(selectedPartners);
                    historyForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии истории:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}