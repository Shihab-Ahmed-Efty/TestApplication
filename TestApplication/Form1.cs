using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TestApplication
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }
        
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string ConnectionString = @"ConnectionStringForTheDatabase";
            string sql = string.Format("insert into Users (UserName,Password,EmailAddress,UpdatedDate) Values ('{0}','{1}','{2}','{3}')",txtUserName.Text,txtPassword.Text,txtEmailAddress.Text, System.DateTime.Now.ToString());

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand(sql, conn);

            sqlCmd.Connection.Open();
            int rowsAffected = sqlCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Saved Successfully!!");
                LoadUserGridData();
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
            sqlCmd.Connection.Close();


        }

        private void HomeForm_Load(object sender, System.EventArgs e)
        {
            LoadUserGridData();
        }

        void LoadUserGridData()
        {
            string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mou\\Desktop\\OOP2\\TestDb.mdf;Integrated Security=True;Connect Timeout=30";
            string sql = "select UserName as 'User Name',  EmailAddress as 'Email Address' from Users";

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand(sql, conn);

            DataTable dt = new DataTable();

            sqlCmd.Connection.Open();
            //sqlCmd.ExecuteNonQuery();
            dt.Load(sqlCmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            sqlCmd.Connection.Close();
        }
    }
}
