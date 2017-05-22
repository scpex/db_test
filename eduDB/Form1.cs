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

namespace eduDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet(); // create data set
        string constr = "Data Source = S-PC\\SQLEXPRESS; Initial Catalog = eduDB; User ID = sa; Password = 12345";   // create connection string
        private void Form1_Load(object sender, EventArgs e)
        {
            // show all data on gridview
            string sql = "SELECT Students.std_id, Students.std_name, Students.std_gender, Classes.cl_name, Teachers.tc_name, School.sc_name"+
                         " FROM Students"+
                         " INNER JOIN Classes ON Students.cl_id = Classes.cl_id"+
                         " INNER JOIN Teachers ON Classes.tc_id = Teachers.tc_id"+
                         " INNER JOIN School ON Teachers.sc_id = School.sc_id";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, constr);
            
            da.Fill(dt);
            dataGV.DataSource = dt;
            
        }
    }
}
