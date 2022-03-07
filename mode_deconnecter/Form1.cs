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


namespace mode_deconnecter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-INU93R5\SQLEXPRESS01;Initial Catalog=revGeneral;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        BindingSource bs = new BindingSource();
        private void afficher()
        {
            cnx.Open();
            da = new SqlDataAdapter("select * from employe", cnx);
            da.Fill(ds, "employe");
            bs.DataSource = ds.Tables["employe"];
            dataGridView1.DataSource = bs;
            textBox1.DataBindings.Add("Text",bs,"id");
            textBox2.DataBindings.Add("Text", bs, "nom");
            textBox3.DataBindings.Add("Text", bs, "prenom");
            textBox4.DataBindings.Add("Text", bs, "age");
            textBox5.DataBindings.Add("Text", bs, "salaire");
            comboBox1.DataBindings.Add("SelectedValue", bs, "code");

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            afficher();
            SqlDataAdapter da1 = new SqlDataAdapter("select * from entreprise", cnx);
             da1.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "code";
        }

        private void ADD_Click(object sender, EventArgs e)
        {
            //DataRow l = ds.Tables["employe"].NewRow();
            //l[0] =textBox1.Text;
            //l[1] =textBox2.Text;
            //l[2] =textBox3.Text;
            //l[3] =textBox4.Text;
            //l[4] =textBox5.Text;
            //l[5] =comboBox1.SelectedValue;
            //ds.Tables["employe"].Rows.Add(l);
            //MessageBox.Show("bien");
            // deuxeime methode 
            ds.Tables["employe"].Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, comboBox1.SelectedValue) ;
            MessageBox.Show("bien");
        }

        private void SEARCH_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < ds.Tables["employe"].Rows.Count; i++)
            {
                if(textBox1.Text== ds.Tables["employe"].Rows[i][0].ToString())
                {
                    MessageBox.Show("se employes se trouve dans la liste");
                    textBox2.Text= ds.Tables["employe"].Rows[i][1].ToString();
                    textBox3.Text= ds.Tables["employe"].Rows[i][2].ToString();
                    textBox4.Text= ds.Tables["employe"].Rows[i][3].ToString();
                    textBox5.Text= ds.Tables["employe"].Rows[i][4].ToString();
                    comboBox1.SelectedValue = ds.Tables["employe"].Rows[i][5].ToString();
                    break;
                }
            }
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ds.Tables["employe"].Rows.Count; i++)
            {
                if (textBox1.Text == ds.Tables["employe"].Rows[i][0].ToString())
                {
                    MessageBox.Show("voulez vous vraiment supprimer l'employe -->" + textBox1.Text);
                    ds.Tables["employe"].Rows[i].Delete();


                }
            }
        }

        private void UPDATE_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < ds.Tables["employe"].Rows.Count; i++)
            {
                if (textBox1.Text == ds.Tables["employe"].Rows[i][0].ToString())
                {
                     ds.Tables["employe"].Rows[i][1]=textBox2.Text;
                     ds.Tables["employe"].Rows[i][2]=textBox3.Text;
                     ds.Tables["employe"].Rows[i][3]=textBox4.Text;
                    ds.Tables["employe"].Rows[i][4] = textBox5.Text;
                    ds.Tables["employe"].Rows[i][5] = comboBox1.SelectedValue;
                    MessageBox.Show("bien modifier");
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DataSet s = new DataSet();
            SqlDataAdapter d;
            d = new SqlDataAdapter("select nom,prenom from employe,entreprise where entreprise.code = employe.code and entreprise.name ='"+comboBox1.Text+"' ", cnx);
            d.Fill(s, "employe");
            dataGridView1.DataSource = s.Tables["employe"];
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }
    }
}
