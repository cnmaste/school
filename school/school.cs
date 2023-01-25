using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace school
{
    public partial class school : Form
    {
        // основной экземпляр класса БД. Через него едет большинство запросов
        Database db = new Database();
        // вспомогательный экземпляр. Через него едут те запросы, когда основной - занят
        Database db2 = new Database();
        // номер выбранной строки в datagridview
        int selectedRow;
        // айдишник записи (в базе) выбранной строки в datagridview
        int selectedRecordId;

        public school()
        {
            InitializeComponent();
        }
        // функция добавления столбцов в datagridview, выполняется один раз при запуске
        private void createColumns()
        {
            dataGridView1.Columns.Add("Id", "id");
            dataGridView1.Columns.Add("Number", "number");
            dataGridView1.Columns.Add("Floor", "floor");
            dataGridView1.Columns.Add("Capacity", "capacity");
            dataGridView1.Columns.Add("Equip", "equip");
            dataGridView1.Columns.Add("Spec", "spec");


        }
        // заполнение datagridview из базы. выполняется селект, каждая строчка добавляется в цикле while.
        private void refreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string query = $"SELECT * FROM School ";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            // executereader - используется когда нужно читать ответ в виде таблицы
            SqlDataReader dr = cmd.ExecuteReader();
            // читаем ответ построчно, почти как с файлом
            while (dr.Read())
            {
                int id = dr.GetInt32(0);
                // запишем айдишник, и вызовем две функции: первая вернет список через запятую оборудования, вторая - специализаций. Обход отсутствия STRING_AGG
                // 0, 1, 2 и тп - номера столбцов в ответе. getInt, getString выбирается в зависимости от типа поля в базе (int, varchar итп)
                dgv.Rows.Add(id, dr.GetInt32(1), dr.GetInt32(2), dr.GetInt32(3),getEquipStringById(id),getSpecStringById(id));
            }
            dr.Close();
        }
        // функция, которая вернет список спецификаций через запятую по айдишнику кабинета (например: "физика, химия")
        private string getSpecStringById(int id)
        {
            // создадим список типа List, по очереди добавим туда оборудование из таблицы, преобразуем список в массив, вернем строку типа ["физика","химия"] -> "физика, химия"
            string query = $"SELECT Spec.SpecName FROM SpecLinks left join Spec on SpecLinks.SpecId = Spec.Id WHERE SpecLinks.CabId = '{id}'";
            List<string> specsList = new List<string>();
            SqlCommand cmd = new SqlCommand(query, db2.getConn());
            db2.openConn();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr.GetString(0);
                specsList.Add(name);
            }
            dr.Close();
            String[] str = specsList.ToArray();
            return string.Join(", ", str);

        }
        // функция, которая вернет список оборудования через запятую по айдишнику кабинета (например: "проектор, монитор")
        private string getEquipStringById(int id)
        {
            // алгоритм 1 в 1 что и выше, только для оборудования
            string query = $"SELECT Equip.EquipName FROM EquipLinks left join Equip on EquipLinks.EquipId = Equip.Id WHERE EquipLinks.CabId = '{id}'";
            List<string> equipsList = new List<string>();
            SqlCommand cmd = new SqlCommand(query, db2.getConn());
            db2.openConn();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr.GetString(0);
                equipsList.Add(name);
            }
            dr.Close();
            String[] str = equipsList.ToArray();
            return string.Join(", ", str);
        }

        private void school_Load(object sender, EventArgs e)
        {
            // при показе основной формы - просто запустим основные функции: нарисовать столбцы, загрузить из базы, нарисовать чекбоксы и пр
            createColumns();
            refreshDataGrid(dataGridView1);
            drawEquipBoxes();
            drawSpecBoxes();
        }
        // функция, которая динамически нарисует чекбоксы с оборудованием из того, что в базе написано. Автоматически присвоит их в equipGroupBox
        private void drawEquipBoxes()
        {
            string query = $"SELECT * FROM Equip";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            SqlDataReader dr = cmd.ExecuteReader();
            CheckBox box;
            int i = 0;
            while (dr.Read())
            {
                box = new CheckBox();
                box.Name = $"equipBox_{dr.GetInt32(0)}";
                box.Tag = dr.GetInt32(0); // в теге будем хранить айди оборудования
                box.Text = dr.GetString(1); // текст - название оборудования
                // перенесли управление на кнопку "применить"
                //                box.Click += new EventHandler(checkEquip);
                box.AutoSize = true;
                box.Location = new Point(10, 20 + (i++ * 20));
                equipGroupBox.Controls.Add(box);
            }
            dr.Close();

        }

        // функция, которая динамически нарисует чекбоксы со спецификацией из того, что в базе написано. Автоматически присвоит их в equipGroupBox
        private void drawSpecBoxes()
        {
            string query = $"SELECT * FROM Spec";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            SqlDataReader dr = cmd.ExecuteReader();
            CheckBox box;
            int i = 0;
            while (dr.Read())
            {
                box = new CheckBox();
                //box.Parent = specGroupBox;
                box.Name = $"specBox_{dr.GetInt32(0)}";
                box.Tag = dr.GetInt32(0); 
                box.Text = dr.GetString(1);
                // перенесли управление на кнопку "применить"
                // box.Click += new EventHandler(checkSpec);
                box.AutoSize = true;
                box.Location = new Point(10, 20 + (i++ * 20));
                specGroupBox.Controls.Add(box);
            }
            dr.Close();
        }
        private void checkSpec(object sender, EventArgs e)
        {
            // вся логика вынесена на кнопку "применить"
        }
        private void checkEquip(object sender, EventArgs e)
        {
            // вся логика вынесена на кнопку "применить"
        }
        // когда кликнем на любой ячейке dgv - заполним updown-ы текущими значениями, + заполним чекбоксы со спецификацией и оборудованием
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >=0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                int id, cab, floor, capacity;
                if (Int32.TryParse(row.Cells[0].Value.ToString(), out id))
                {
                    selectedRecordId = id;
                }
                if (Int32.TryParse(row.Cells[1].Value.ToString(), out cab))
                {
                    cabUpDown.Value = cab;
                }
                if (Int32.TryParse(row.Cells[2].Value.ToString(), out floor))
                {
                    floorUpDown.Value = floor;
                }
                if (Int32.TryParse(row.Cells[3].Value.ToString(), out capacity))
                {
                    capacityUpDown.Value = capacity;
                }
                updateSpecBoxesById(id);
                updateEquipBoxesById(id);
            }
        }
        // функция, которая расставит флажки в чекбоксах cо специализацией на основании айдишника кабинета
        private void updateSpecBoxesById(int k)
        {
            string query = $"SELECT SpecId FROM SpecLinks where CabId = '{k}'";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            SqlDataReader dr = cmd.ExecuteReader();
            // сперва снимем все галки, что б расставить по-новой
            foreach (CheckBox box in specGroupBox.Controls.OfType<CheckBox>())
            {
                box.Checked = false;
            }
            // пройдемся по каждой строке в базе, внутри - пройдемся по чекбоксам, если айди совпал - поставим галку
            while (dr.Read())
            {
                foreach (CheckBox box in specGroupBox.Controls.OfType<CheckBox>())
                {
                    if (box.Tag.ToString() == dr.GetInt32(0).ToString())
                    {
                        box.Checked=true;
                    }
                }
            }
            dr.Close();

        }
        // то же самое, только для оборудования
        private void updateEquipBoxesById(int k)
        {
            string query = $"SELECT EquipId FROM EquipLinks where CabId = {k}";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            SqlDataReader dr = cmd.ExecuteReader();
            foreach (CheckBox box in equipGroupBox.Controls.OfType<CheckBox>())
            {
                box.Checked = false;
            }
            while (dr.Read())
            {
                foreach (CheckBox box in equipGroupBox.Controls.OfType<CheckBox>())
                {
                    if (box.Tag.ToString() == dr.GetInt32(0).ToString())
                    {
                        box.Checked = true;
                    }
                }
            }
            dr.Close();

        }
        // функция для добавления
        private void addButton_Click(object sender, EventArgs e)
        {
            //                                                               || вот этот прикол использовали. при выполнении запроса вернется айдишник.
            string query = $"INSERT INTO School (Number, Floor, Capacity) OUTPUT INSERTED.ID VALUES ('{cabUpDown.Value.ToString()}','{floorUpDown.Value.ToString()}','{capacityUpDown.Value.ToString()}')";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            // сперва вставим запись в таблицу с кабинетами
            int modified = (int)cmd.ExecuteScalar();
            // после инсерта - получим айдишник вставленной записи
            // пробежимся по чекбоксам со спецификациями, если что-то стоит - сделаем соответствующие записи
            foreach (CheckBox box in specGroupBox.Controls.OfType<CheckBox>())
            {
                if (box.Checked == true)
                {
                    string insertQuery = $"INSERT INTO SpecLinks (CabId, SpecId) values ('{modified}', '{box.Tag.ToString()}')";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, db.getConn());
                    insertCmd.ExecuteNonQuery();
                }
            }
            // пробежимся по чекбоксам с оборудованием, если что-то стоит - сделаем соответствующие записи
            foreach (CheckBox box in equipGroupBox.Controls.OfType<CheckBox>())
            {
                if (box.Checked == true)
                {
                    string insertQuery = $"INSERT INTO EquipLinks (CabId, EquipId) values ('{modified}', '{box.Tag.ToString()}')";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, db.getConn());
                    insertCmd.ExecuteNonQuery();
                }
            }

            refreshDataGrid(dataGridView1);

        }

        private void rmButton_Click(object sender, EventArgs e)
        {
            string query = $"DELETE FROM School where id = '{selectedRecordId}'";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            cmd.ExecuteNonQuery();
            refreshDataGrid(dataGridView1);

        }
        // по клику на кнопке "применить оборудование" - если стоит галка, сперва кинем селект чтоб узнать, имеются ли записи уже. Если нет - выполним insert.
        // если галка снята - выполним delete.
        private void applyEquipBtn_Click(object sender, EventArgs e)
        {
            foreach (CheckBox box in equipGroupBox.Controls.OfType<CheckBox>())
            {
                if (box.Checked == true)
                {
                    string query = $"SELECT COUNT(*) FROM EquipLinks WHERE CabId =  '{selectedRecordId}' AND EquipId = '{box.Tag.ToString()}'";
                    SqlCommand cmd = new SqlCommand(query, db.getConn());
                    db.openConn();
                    Int32 count = (Int32)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string insertQuery = $"INSERT INTO EquipLinks (CabId, EquipId) values ('{selectedRecordId}', '{box.Tag.ToString()}')";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, db.getConn());
                        insertCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string query = $"DELETE FROM EquipLinks where CabId = '{selectedRecordId}' AND EquipId = '{box.Tag.ToString()}'";
                    SqlCommand cmd = new SqlCommand(query, db.getConn());
                    db.openConn();
                    cmd.ExecuteNonQuery();
                }
            }
            refreshDataGrid(dataGridView1);
        }

        private void applySpecBtn_Click(object sender, EventArgs e)
        {
            foreach (CheckBox box in specGroupBox.Controls.OfType<CheckBox>())
            {
                if (box.Checked == true)
                {
                    string query = $"SELECT COUNT(*) FROM SpecLinks WHERE CabId =  '{selectedRecordId}' AND SpecId = '{box.Tag.ToString()}'";
                    SqlCommand cmd = new SqlCommand(query, db.getConn());
                    db.openConn();
                    Int32 count = (Int32)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string insertQuery = $"INSERT INTO SpecLinks (CabId, SpecId) values ('{selectedRecordId}', '{box.Tag.ToString()}')";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, db.getConn());
                        insertCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string query = $"DELETE FROM SpecLinks where CabId = '{selectedRecordId}' AND SpecId = '{box.Tag.ToString()}'";
                    SqlCommand cmd = new SqlCommand(query, db.getConn());
                    db.openConn();
                    cmd.ExecuteNonQuery();
                }
            }
            refreshDataGrid(dataGridView1);
        }
    }
}
