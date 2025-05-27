using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO
{
    internal class MaterType
    {
        public int CalculateMaterialFromDb(
            int productTypeId,
            int materialTypeId,
            int quantity,
            double param1,
            double param2)
        {
            if (quantity <= 0 || param1 <= 0 || param2 <= 0)
                return -1;

            double? productCoefficient = null;
            double? wastePercent = null;

            try
            {
                using (SqlConnection connection = new SqlConnection("YourConnectionStringHere"))
                {
                    connection.Open();

                    // Получаем коэффициент продукции
                    using (SqlCommand cmd = new SqlCommand("SELECT Coefficient FROM ProductTypes WHERE ID = @id", connection))
                    {
                        cmd.Parameters.AddWithValue("@id", productTypeId);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                            productCoefficient = Convert.ToDouble(result);
                    }

                    // Получаем процент брака материала
                    using (SqlCommand cmd = new SqlCommand("SELECT WastePercent FROM MaterialTypes WHERE ID = @id", connection))
                    {
                        cmd.Parameters.AddWithValue("@id", materialTypeId);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                            wastePercent = Convert.ToDouble(result);
                    }
                }

                // Проверка на существование значений
                if (productCoefficient == null || wastePercent == null)
                    return -1;

                double materialPerUnit = param1 * param2 * productCoefficient.Value;
                double totalMaterial = materialPerUnit * quantity * (1 + wastePercent.Value);

                return (int)Math.Ceiling(totalMaterial);
            }
            catch (Exception)
            {
                return -1; // При ошибке запроса
            }
        }

    }
}
