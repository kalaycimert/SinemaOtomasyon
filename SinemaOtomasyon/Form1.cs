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

namespace SinemaOtomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sil();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=SinemaVT;Integrated Security=True");
        public void Listele()
        {
            baglanti.Open();
            string sorgu = "Select * from Sinema";
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }
        public void ekle()
        {
            baglanti.Open();
            string sorgu = "insert into Sinema(filmAdi, salonNo, seansSaat, koltukNo, musteriAdi, odemeDurum,filmTur,menuBoyut,toplamFiyat)values(@p1, @p2, @p3, @p4, @p5, @p6,@p7,@p8,@p9)";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox2.Text);
            komut.Parameters.AddWithValue("@p3", textBox3.Text);
            komut.Parameters.AddWithValue("@p4", textBox4.Text);
            komut.Parameters.AddWithValue("@p5", textBox5.Text);
            komut.Parameters.AddWithValue("@p6", comboBox1.Text);
            komut.Parameters.AddWithValue("@p7", comboBox2.Text);
            komut.Parameters.AddWithValue("@p8", comboBox4.Text);
            komut.Parameters.AddWithValue("@p9", label12.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Eklendi");
            Listele();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Kayıt eklensin mi ?","Sinema Otomasyonu",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                ekle();
                DialogResult ceva;
                ceva = MessageBox.Show("Hizmeti beğendiyseniz evet veya hayır basarmsınız  ?", "Sinema Otomasyonu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ceva == DialogResult.Yes)
                {
                    MessageBox.Show("rica ediriz sizin için çalışıyoruz");
                }
                else
                {
                    MessageBox.Show("Tatsız bir durum yaşatımız için Özür dileriz");
                }
            }
            else
            {
                MessageBox.Show("ekleme işlemi iptal edildi");
            }
        }
        public void sil()
        {
            baglanti.Open();
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            string sorgu = "delete from Sinema where id=@p1";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@p1", id);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi");
            Listele();
        }
        public void guncelle()
        {
            baglanti.Open();

            int id = Convert.ToInt32
            (dataGridView1.CurrentRow.Cells[0].Value);
            string sorgu = "update Sinema set filmAdi = @p1, salonNo = @p2,seansSaat = @p3,koltukNo = @p4,musteriAdi = @p5,odemeDurum = @p6 where id = @p7";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox2.Text);
            komut.Parameters.AddWithValue("@p3", textBox3.Text);
            komut.Parameters.AddWithValue("@p4", textBox4.Text);
            komut.Parameters.AddWithValue("@p5", textBox5.Text);
            komut.Parameters.AddWithValue("@p6", comboBox1.Text);
            komut.Parameters.AddWithValue("@p7", id);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncellendi");
            Listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            guncelle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from Sinema where filmAdi like '%" + textBox7.Text + "%'";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int biletFiyat = 0;
            int menuFiyat = 0;

            if (comboBox3.Text == "Öğrenci")
            {
                biletFiyat = 100;
            }

            if (comboBox3.Text == "Tam")
            {
                biletFiyat = 150;
            }

            if (comboBox4.Text == "Küçük Menü")
            {
                menuFiyat = 100;
            }

            if (comboBox4.Text == "Orta Menü")
            {
                menuFiyat = 170;
            }

            if (comboBox4.Text == "Büyük Menü")
            {
                menuFiyat = 210;
            }

            int toplam = biletFiyat + menuFiyat;

           label12.Text = toplam.ToString();
        }
    }
}
