using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEMO
{
    public partial class AddEditForm : Form
    {
        private Partner editingPartner = null;




        public AddEditForm()
        {
            InitializeComponent(false);
            Type.Items.Add("ООО");
            Type.Items.Add("ОАО");
            Type.Items.Add("ЗАО");

            // Визуальные подсказки
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(Name, "Введите наименование партнёра");
            toolTip.SetToolTip(Type, "Выберите тип партнёра");
            toolTip.SetToolTip(Rate, "Введите целое число от 0 и выше");
        }

        public AddEditForm(Partner partner)
        {
            InitializeComponent(true);

            Type.Items.Add("ООО");
            Type.Items.Add("ОАО");
            Type.Items.Add("ЗАО");

            editingPartner = partner;

            // Визуальные подсказки
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(Name, "Введите наименование партнёра");
            toolTip.SetToolTip(Type, "Выберите тип партнёра");
            toolTip.SetToolTip(Rate, "Введите целое число от 0 и выше");

            // Заполнение полей
            Name.Text = partner.Name;
            Type.Text = partner.Type;
            Rate.Text = partner.Rate;
            Address.Text = partner.Address;
            Director.Text = partner.Director;
            Phone.Text = partner.Phone;
            Email.Text = partner.Email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = Name.Text.Trim();
            string type = Type.Text;
            string rateText = Rate.Text.Trim();
            string address = Address.Text.Trim();
            string director = Director.Text.Trim();
            string phone = Phone.Text.Trim();
            string email = Email.Text.Trim();
            string inn = "0000000000"; // Пока заглушка, если не используешь

            if (!int.TryParse(rateText, out int rate) || rate < 0)
            {
                MessageBox.Show("Рейтинг должен быть целым числом от 0 и выше.", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Rate.Focus();
                return;
            }

            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Пожалуйста, заполните обязательные поля: 'Наименование' и 'Тип'.", "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=.;Initial Catalog=MasterPol;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query;

                if (editingPartner == null)
                {
                    // Добавление
                    query = @"INSERT INTO Partners 
                ([Тип партнера], [Наименование партнера], [Директор], [Электронная почта партнера], 
                 [Телефон партнера], [Юридический адрес партнера], [ИНН], [Рейтинг])
                VALUES (@Type, @Name, @Director, @Email, @Phone, @Address, @INN, @Rate)";
                }
                else
                {
                    DialogResult confirm = MessageBox.Show(
                        "Вы действительно хотите обновить данные партнёра?",
                        "Подтверждение обновления",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes)
                        return;

                    query = @"UPDATE Partners SET 
                    [Тип партнера] = @Type,
                    [Директор] = @Director,
                    [Электронная почта партнера] = @Email,
                    [Телефон партнера] = @Phone,
                    [Юридический адрес партнера] = @Address,
                    [ИНН] = @INN,
                    [Рейтинг] = @Rate
                    WHERE [Наименование партнера] = @Name";
                }

                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Director", director);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@INN", inn);
                        cmd.Parameters.AddWithValue("@Rate", rate);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    string action = editingPartner == null ? "добавлен" : "обновлён";
                    MessageBox.Show($"Партнёр успешно {action}.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Ошибка базы данных:\n{ex.Message}\n\nПроверьте соединение и уникальность данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла непредвиденная ошибка:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void AddEditForm_Load(object sender, EventArgs e)
        {

        }
    }
}
