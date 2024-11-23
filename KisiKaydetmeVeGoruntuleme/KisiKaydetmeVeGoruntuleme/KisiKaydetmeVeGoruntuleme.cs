using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KisiKaydetmeVeGoruntuleme
{
    public partial class KisiKaydetmeVeGoruntuleme : Form
    {
        KimlikBilgileri k = new KimlikBilgileri();
        public KisiKaydetmeVeGoruntuleme()
        {
            InitializeComponent();
            rb_erkek.Checked = true;
            rb_evli.Checked = true;
            string yol = "C://Kayıtlı Kişiler";
            DirectoryInfo di = new DirectoryInfo(yol);
            di.Create();
            string kisiyol = "C://Kayıtlı Kişiler";
            DirectoryInfo di2 = new DirectoryInfo(kisiyol);
            List<string> kisiliste = new List<string>();
            FileInfo[] dosyalar = di2.GetFiles();
            foreach (FileInfo item in dosyalar)
            {
                kisiliste.Add(item.Name);
            }
            foreach (string item in kisiliste)
            {
                lb_kisiler.Items.Add(item.Remove(item.Length - 4, 4));
            }
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            k.Isim = tb_isim.Text;
            k.Soyisim = tb_soyisim.Text;
            k.KimlikNo = mtb_kimlikNo.Text;
            k.DogumTarihi = Convert.ToDateTime(dtp_dogumTarihi.Text);
            k.TelefonNo = mtb_telNo.Text;
            k.Cinsiyet = "Erkek";
            if (!rb_erkek.Checked) { k.Cinsiyet = "Kadın"; }
            k.MedeniDurum = "Evli";
            if (rb_evli.Checked) { k.MedeniDurum = "Bekar"; }
            k.Uyruk = tb_uyruk.Text;
            k.KanGrubu = tb_kanGrubu.Text;
            k.Adres = tb_adres.Text;

            if (tb_isim.Text != "")
            {
                string yoltxt = "C://Kayıtlı Kişiler//" + k.Isim + " " + k.Soyisim + ".txt";
                FileInfo fi = new FileInfo(yoltxt);

                StreamWriter sw = new StreamWriter(yoltxt);
                string metin = $"{k.Isim}\n\n{k.Soyisim}\n\n{k.KimlikNo}\n\n{k.DogumTarihi.ToShortDateString()}\n\n{k.TelefonNo}\n\n{k.Cinsiyet}\n\n{k.MedeniDurum}\n\n{k.Uyruk}\n\n{k.KanGrubu}\n\n{k.Adres}";
                sw.WriteLine(metin);
                sw.Close();
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurduğunuzdan emin olunuz.", "UYARI");
            }
        }

        private void lb_kisiler_SelectedIndexChanged(object sender, EventArgs e)
        {
            string yol = "C://Kayıtlı Kişiler";
            DirectoryInfo di = new DirectoryInfo(yol);
            List<string> kisiliste = new List<string>();
            FileInfo[] dosyalar = di.GetFiles();
            foreach (FileInfo fi in dosyalar)
            {
                kisiliste.Add(fi.Name);
            }
            string kisiyol = "C://Kayıtlı Kişiler//" + kisiliste[lb_kisiler.SelectedIndex];
            StreamReader sr = new StreamReader(kisiyol);
            lbl_isim.Text = sr.ReadToEnd();
        }

        private void btn_yenile_Click(object sender, EventArgs e)
        {
            string kisiyol = "C://Kayıtlı Kişiler";
            DirectoryInfo di2 = new DirectoryInfo(kisiyol);
            List<string> kisiliste = new List<string>();
            FileInfo[] dosyalar = di2.GetFiles();
            lb_kisiler.Items.Clear();
            foreach (FileInfo item in dosyalar)
            {
                kisiliste.Add(item.Name);
            }
            foreach (string item in kisiliste)
            {
                lb_kisiler.Items.Add(item.Remove(item.Length - 4, 4));
            }
        }
    }
}
