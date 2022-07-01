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
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KCSQPRE\SQLEXPRESS;Initial Catalog=OkulProje;Integrated Security=True");

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        

        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListe();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLKULUPLER",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbKulup.DisplayMember = "KULUPAD"; // gözükecek olan üye
            cmbKulup.ValueMember = "KULUPID";   // Arka plandaki değeri
            cmbKulup.DataSource = dt;
            baglanti.Close();
            // SQLDATAADAPTER DA BAĞLANTIYI AÇIP KAPATMAK ŞART DEĞİLDİR.
        }
       
        string cinsiyet = "";

        private void BtnEkle_Click(object sender, EventArgs e)
        {
          
            ds.OgrenciEkle(TxtOgrenciAd.Text,txtOgrenciSoyad.Text,byte.Parse(cmbKulup.SelectedValue.ToString()),cinsiyet);
            MessageBox.Show("Öğrenci Ekleme Yapıldı");
           
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListe();
        }

        private void cmbKulup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TxtOgrenciID.Text = cmbKulup.SelectedValue.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(Convert.ToInt32(TxtOgrenciID.Text));
            MessageBox.Show("Öğrenci Silme İşlemi Tamamlandı");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            TxtOgrenciID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtOgrenciAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtOgrenciSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbKulup.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()=="KIZ")
            {
                rdbtnkiz.Checked = true;
                rdbtnErkek.Checked = false;
            }
            if (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() == "ERKEK")
            {
                rdbtnkiz.Checked = false;
                rdbtnErkek.Checked = true;
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.OgrenciGuncelle(TxtOgrenciAd.Text, txtOgrenciSoyad.Text, byte.Parse(cmbKulup.SelectedValue.ToString()), cinsiyet, int.Parse(TxtOgrenciID.Text));
            MessageBox.Show("Öğrenci Güncelleme İşlemi Tamamlandı");
        }

        private void rdbtnkiz_CheckedChanged(object sender, EventArgs e)
        {

            if (rdbtnkiz.Checked == true)
            {
                cinsiyet = "KIZ";
            }
       
        }

        private void rdbtnErkek_CheckedChanged(object sender, EventArgs e)
        {

           
            if (rdbtnErkek.Checked == true)
            {
                cinsiyet = "ERKEK";
            }
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource= ds.OgrenciGetir(txtAra.Text);
        }
    }
}
