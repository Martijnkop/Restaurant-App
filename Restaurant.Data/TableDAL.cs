using MySql.Data.MySqlClient;
using Restaurant.Interface.table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public class TableDAL : ITableContainerDAL, ITableDAL
    {
        public bool Add(int tableNumber)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string tryExist =
                    $"SELECT COUNT(tablenumber) FROM restauranttable WHERE tablenumber='{tableNumber}'";
                MySqlCommand cmd = new(tryExist, conn.Connection);
                Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count >= 1)
                {
                    conn.Close();
                    return false;
                }

                string add =
                    "INSERT INTO restauranttable (" +
                        "tablenumber," +
                        "tableStatus" +
                    ") values(" +
                        $"'{tableNumber}'," +
                        $"'0'" +
                    ");";
                conn.RunCommand(add);

                conn.Close();

                return true;
            }

            return false;
        }

        public TableDTO FindByTableNumber(int tableNumber)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                string get =
                    $"SELECT * FROM restauranttable WHERE tablenumber = '{tableNumber}';";

                MySqlCommand cmd = new(get, conn.Connection);

                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        int tableNum = reader.GetInt32(1);
                        int tableStatus = reader.GetInt32(2);

                        return new TableDTO { Id = id, TableNumber = tableNumber, Status = (TableStatus)tableStatus };
                    }
                }
            }

            return new TableDTO();
        }

        public List<TableDTO> GetAll()
        {
            DBConnection conn = new();

            List<TableDTO> tables = new();

            if (conn.Open())
            {
                string getAll =
                    "SELECT * FROM restauranttable ORDER BY tablenumber;";

                MySqlCommand cmd = new(getAll, conn.Connection);

                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int id = int.Parse(reader.GetString(0));
                        int tableNumber = reader.GetInt32(1);
                        int tableStatus = reader.GetInt32(2);

                        tables.Add(new TableDTO { Id = id, TableNumber = tableNumber, Status = (TableStatus)tableStatus });
                    }
                }

                conn.Close();
            }

            return tables;
        }

        public bool Update(int oldTableNumber, int tableNumber, int status)
        {
            DBConnection conn = new();

            if (conn.Open())
            {
                if (oldTableNumber != tableNumber)
                {
                    string tryExist =
                        $"SELECT COUNT(tablenumber) FROM restauranttable WHERE tablenumber='{tableNumber}'";
                    MySqlCommand command = new(tryExist, conn.Connection);
                    Int32 count = Convert.ToInt32(command.ExecuteScalar());
                    if (count >= 1)
                    {
                        conn.Close();
                        return false;
                    }
                }

                string cmd = 
                    $"UPDATE restauranttable SET TABLENUMBER='{tableNumber}', TABLESTATUS='{status}' WHERE TABLENUMBER='{oldTableNumber}';";
                conn.RunCommand(cmd);
                conn.Close();
                return true;
            }

            return false;
        }
    }
}
