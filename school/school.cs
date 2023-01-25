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
    enum rowState
    {
        Exist,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class school : Form
    {
        Database db = new Database();
        int selectedRow;
        int selectedRecordId;

        public school()
        {
            InitializeComponent();
        }

        private void createColumns()
        {
            dataGridView1.Columns.Add("Id", "id");
            dataGridView1.Columns.Add("Number", "number");
            dataGridView1.Columns.Add("Floor", "floor");
            dataGridView1.Columns.Add("Capacity", "capacity");
            dataGridView1.Columns.Add("Equip", "equip");
            dataGridView1.Columns.Add("Spec", "spec");


        }
        private void refreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string query = $"SELECT *,(SELECT ISNULL(STRING_AGG(EquipName,','),':(') FROM EquipLinks INNER JOIN Equip on EquipLinks.EquipId = Equip.Id WHERE EquipLinks.CabId = School.Id),(SELECT ISNULL(STRING_AGG(SpecName,','),':(') FROM SpecLinks INNER JOIN Spec on SpecLinks.SpecId = Spec.Id WHERE SpecLinks.CabId = School.Id) FROM School ";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dgv.Rows.Add(dr.GetInt32(0), dr.GetInt32(1), dr.GetInt32(2), dr.GetInt32(3),dr.GetString(4),dr.GetString(5));
            }
            dr.Close();
        }
//        private void updateSpecList(ComboBox cb)
//        {
//            cb.Items.Clear();
//            string query = $"SELECT * FROM Spec";
//            SqlCommand cmd = new SqlCommand(query, db.getConn());
//            db.openConn();
//            SqlDataReader dr = cmd.ExecuteReader();
//            while (dr.Read())
//            {

////                cb.Items.Add(dr.GetInt32(0),dr.GetString(1));
//            }
//            dr.Close();
//        }


        private void school_Load(object sender, EventArgs e)
        {
            createColumns();
            refreshDataGrid(dataGridView1);
            drawEquipBoxes();
            drawPurposeBoxes();
        }
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
                //box.Parent = equipGroupBox;
                box.Name = $"equipBox_{dr.GetInt32(0)}";
                box.Tag = dr.GetInt32(0);
                box.Text = dr.GetString(1);
                box.Click += new EventHandler(checkEquip);
                box.AutoSize = true;
                box.Location = new Point(10, 20 + (i++ * 20));
                MessageBox.Show(equipGroupBox.Top.ToString());
                MessageBox.Show(equipGroupBox.Left.ToString());
                equipGroupBox.Controls.Add(box);
            }
            dr.Close();

        }

        private void drawPurposeBoxes()
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
                box.Click += new EventHandler(checkSpec);
                box.AutoSize = true;
                box.Location = new Point(10, 20 + (i++ * 20));
                specGroupBox.Controls.Add(box);
            }
            dr.Close();
        }
        private void checkSpec(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            
            if (box.Checked == true) {
                string query = $"SELECT COUNT(*) FROM SpecLinks WHERE CabId =  '{selectedRecordId}' AND SpecId = '{box.Tag.ToString()}'";
                MessageBox.Show(query);
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
                string query = $"DELETE FROM SpecLinks where CabId = '{selectedRecordId}' AND SpecId = {box.Tag.ToString()}";
                SqlCommand cmd = new SqlCommand(query, db.getConn());
                db.openConn();
                cmd.ExecuteNonQuery();
            }
            refreshDataGrid(dataGridView1);
        }
        private void checkEquip(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if (box.Checked == true)
            {
                MessageBox.Show($"lalala {box.Tag.ToString()} ");
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
                string query = $"DELETE FROM EquipLinks where CabId = '{selectedRecordId}' AND EquipId = {box.Tag.ToString()}";
                SqlCommand cmd = new SqlCommand(query, db.getConn());
                db.openConn();
                cmd.ExecuteNonQuery();
            }
            refreshDataGrid(dataGridView1);
        }

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
                updateSpecBoxesById(id.ToString());
                updateEquipBoxesById(id.ToString());
            }
        }
        private void updateSpecBoxesById(string k)
        {
            string query = $"SELECT SpecId FROM SpecLinks where CabId = {k}";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            SqlDataReader dr = cmd.ExecuteReader();
            foreach (CheckBox box in specGroupBox.Controls.OfType<CheckBox>())
            {
                box.Checked = false;
            }
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
        private void updateEquipBoxesById(string k)
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

        private void addButton_Click(object sender, EventArgs e)
        {
            string query = $"INSERT INTO School (Number, Floor, Capacity) values ('{cabUpDown.Value.ToString()}','{floorUpDown.Value.ToString()}','{capacityUpDown.Value.ToString()}')";
            SqlCommand cmd = new SqlCommand(query, db.getConn());
            db.openConn();
            cmd.ExecuteNonQuery();
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
    }
}
