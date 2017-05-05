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

namespace db_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet(); // create data set
        string constr = "Data Source = S-PC\\SQLEXPRESS; Initial Catalog = dbtester; User ID = sa; Password = 12345";   // create connection string
        private void Form1_Load(object sender, EventArgs e)
        {
            // show all data on gridview
            string sql = "SELECT * FROM tb_Customer";
            SqlDataAdapter da = new SqlDataAdapter(sql, constr);
            da.Fill(ds, "tb_Customer");
            dataGV.DataSource = ds.Tables["tb_Customer"];

            // show County_name data on combobox
            sql = "SELECT * FROM tb_County";
            da = new SqlDataAdapter(sql, constr);
            da.Fill(ds, "tb_County");
            comboBox1.DisplayMember = "County_name";        // เอาข้อมูลไปแสดงผล
            comboBox1.ValueMember = "County_id";            // เอาข้อมูลไปแอบไว้
            comboBox1.DataSource = ds.Tables["tb_County"];
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // select all datain row when you click each data in gridview and show data selected on textbox feil 

            if (e.RowIndex == -1) { return; }           // เลือกอย่างอื่นที่ไม่ใช้ข้อมูล เช่น หัวของตาราง
            dataGV.Rows[e.RowIndex].Selected = true;    // เมื่อกดตรงข้อมูล จะให้โชว์ข้อมูลทั้งแถว

            //show data selected on textbox feil

            //// sol 1 >> pull data from data table
            //DataRow dr = ds.Tables["tb_Customer"].Rows[e.RowIndex];
            //textBox1.Text = dr["Id"].ToString();
            //textBox2.Text = dr["Name"].ToString();
            //textBox3.Text = dr["Salary"].ToString();
            //comboBox1.SelectedValue = dr["County_id"].ToString();

            // sol 2 >> pull data from data gridview
            DataGridViewRow dgvr = dataGV.Rows[e.RowIndex];
            textBox1.Text = dgvr.Cells[0].Value.ToString();
            textBox2.Text = dgvr.Cells[1].Value.ToString();
            textBox3.Text = dgvr.Cells[2].Value.ToString();
            comboBox1.SelectedValue = dgvr.Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  Insert/Update data
            DataRow[] drs = ds.Tables["tb_Customer"].Select("Id="+ textBox1.Text+"");
            if(drs.Length == 0) 
            {
                // no have data (insert data)
                DataRow dr = ds.Tables["tb_Customer"].NewRow(); // สร้างแถวใหม่
                dr["Id"] = textBox1.Text;                       // ใส่ข้อมูลในแถวใหม่
                dr["Name"] = textBox2.Text;
                dr["Salary"] = textBox3.Text;
                dr["County_id"] = comboBox1.SelectedValue;
                ds.Tables["tb_Customer"].Rows.Add(dr);          // เอาข้อมูลในแถวใหม่ใส่ใน data table
            }
            else
            {
                // have data (update data)
                drs[0]["Name"] = textBox2.Text;
                drs[0]["Salary"] = textBox3.Text;
                drs[0]["County_id"] = comboBox1.SelectedValue;
            }

            dataGV.DataSource = ds.Tables["tb_Customer"];   // update data grid view
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //  Delete data
            DataRow[] drs = ds.Tables["tb_Customer"].Select("Id=" + textBox1.Text + "");
            if(drs.Length == 0) 
            {
                // ไม่พบข้อมูลที่ต้องการลบ ให้แจ้งเตือน โดยใช้ message box
                MessageBox.Show("Data not found!!!");
            }
            else
            {
                //  ถ้าพบข้อมูลที่ต้องการลบ ให้แจ้งเตือน โดยใช้ message box เพื่อยืนยันการลบข้อมูล แล้วทำการลบข้อมูลนั้น
                drs[0].Delete();                            // ลบข้อมูลในแถว แต่แถวยังอยู่

                //  ปรับปรุงฐานข้อมูลตรงนี้เลย เพื่อให้แถวของข้อมูลที่ลบไปนั้นกลับมาสู่สภาพปกติ
                string sql = "SELECT * FROM tb_Customer";
                SqlDataAdapter da = new SqlDataAdapter(sql, constr);
                SqlCommandBuilder comb = new SqlCommandBuilder(da); // ใช้ปรับปรุงฐานข้อมูลที่มีการเปลี่ยนแปลง
                da.Update(ds, "tb_Customer");

                ds.Tables["tb_Customer"].AcceptChanges();   // จะทำให้แถวที่ว่างนั้น หายไป
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //  Save to DB
            string sql = "SELECT * FROM tb_Customer";
            SqlDataAdapter da = new SqlDataAdapter(sql, constr);
            SqlCommandBuilder comb = new SqlCommandBuilder(da); // ใช้ปรับปรุงฐานข้อมูลที่มีการเปลี่ยนแปลง
            da.Update(ds, "tb_Customer");

            MessageBox.Show("Data Saved into Database!!!");
        }
    }
}
