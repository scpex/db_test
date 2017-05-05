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

        DataSet ds = new DataSet();
        string constr = "Data Source = .; Initial Catalog = dbtester; User ID = sa; Password = 12345";
        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tb_Customer";
            SqlDataAdapter da = new SqlDataAdapter(sql, constr);
            da.Fill(ds, "Customer");
            dataGV.DataSource = ds.Tables["Customer"];
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
