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
namespace okulProje
{
    public partial class FrmSinavNotlar : Form
    {
        public FrmSinavNotlar()
        {
            InitializeComponent();
        }

        DataSet1TableAdapters.TBLNOTLARTableAdapter ds = new DataSet1TableAdapters.TBLNOTLARTableAdapter();

        private void BtnAra_Click(object sender, EventArgs e)
        {
          dataGridView1.DataSource=  ds.NotListesi(int.Parse(TxtOgrenciID.Text));
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
      
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KCSQPRE\SQLEXPRESS;Initial Catalog=OkulProje;Integrated Security=True");

        private void FrmSinavNotlar_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDERSLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbDers.DisplayMember = "DERSAD"; // gözükecek olan üye
            cmbDers.ValueMember = "DESID";   // Arka plandaki değeri
            cmbDers.DataSource = dt;
            baglanti.Close();
            // SQLDATAADAPTER DA BAĞLANTIYI AÇIP KAPATMAK ŞART DEĞİLDİR.
        }

        int notid;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            notid= int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            TxtOgrenciID.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSinav1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSinav2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSinav3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtProje.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtOrtalama.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }
   
        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            int sinav1, sinav2, sinav3, proje;
            double ortalama;

            //string durum;
            sinav1 = Convert.ToInt16(txtSinav1.Text);
            sinav2 = Convert.ToInt16(txtSinav2.Text);
            sinav3 = Convert.ToInt16(txtSinav3.Text);
            proje = Convert.ToInt16(TxtProje.Text);
            ortalama = (sinav1+sinav2+sinav3+proje)/4;
            txtOrtalama.Text = ortalama.ToString();
            if (ortalama>=50)
            {
                txtDurum.Text = "True";
            }
            else
            {
                txtDurum.Text = "False";
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.NotGuncelle(byte.Parse(cmbDers.SelectedValue.ToString()), int.Parse(TxtOgrenciID.Text), byte.Parse(txtSinav1.Text), byte.Parse(txtSinav2.Text), byte.Parse(txtSinav3.Text), byte.Parse(TxtProje.Text), decimal.Parse(txtOrtalama.Text), bool.Parse(txtDurum.Text),notid);
        }
    }
}
