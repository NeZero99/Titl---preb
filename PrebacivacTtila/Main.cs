using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PrebacivacTtila
{
    public partial class Main : Form
    {
        private string originalTitl;
        private string izmenjenTitl;
        private string imeTitla;

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imeTitla = openFileDialog1.SafeFileName;
                    izFajlaUString(openFileDialog1.FileName);

                    lblIzmenjen.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void izFajlaUString(string putanja)
        {
            StreamReader rider = new StreamReader(putanja, Encoding.GetEncoding("Windows-1250"), true);
            originalTitl = rider.ReadToEnd();
            izmenjenTitl = prebacivanjeSlova (originalTitl);
            richTextBox1.Text = izmenjenTitl;
            rider.Dispose();
            rider.Close();
        }

        private string prebacivanjeSlova(string titlZaPreb)
        {
            StringBuilder bilder = new StringBuilder(titlZaPreb);
            //šđžčć ŠĐŽČĆ
            for(int i = 0; i < bilder.Length; i++)
            {
                switch (bilder[i])
                {
                    case 'š':
                        bilder[i] = 's';
                        break;
                    case 'đ':
                        bilder[i] = 'd';
                        break;
                    case 'ž':
                        bilder[i] = 'z';
                        break;
                    case 'č':
                        bilder[i] = 'c';
                        break;
                    case 'ć':
                        bilder[i] = 'c';
                        break;
                    case 'Š':
                        bilder[i] = 'S';
                        break;
                    case 'Đ':
                        bilder[i] = 'D';
                        break;
                    case 'Ž':
                        bilder[i] = 'Z';
                        break;
                    case 'Č':
                        bilder[i] = 'C';
                        break;
                    case 'Ć':
                        bilder[i] = 'C';
                        break;
                }
            }
            return bilder.ToString();
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            SaveFileDialog cuvanjeDialog = new SaveFileDialog();
            cuvanjeDialog.FileName = imeTitla;
            cuvanjeDialog.Filter = "Subtitles|*.srt";
            cuvanjeDialog.Title = "Cuvanje izmenjenog titla";
            cuvanjeDialog.RestoreDirectory = true;

            if (cuvanjeDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter cuvanje = new StreamWriter(cuvanjeDialog.OpenFile(), Encoding.GetEncoding("Windows-1250"));
                cuvanje.Write(izmenjenTitl);
                cuvanje.Dispose();
                cuvanje.Close();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            vidljivostStavki(false);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Main_DragLeave(object sender, EventArgs e)
        {
            vidljivostStavki(true);
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            izFajlaUString(files[0]);
            imeTitla = izvlacenjeImenaIzPutanje(files[0]);
            izmenjenTitl = prebacivanjeSlova(originalTitl);;
            richTextBox1.Text = izmenjenTitl;

            vidljivostStavki(true);

            lblIzmenjen.Visible = true;
        }

        private string izvlacenjeImenaIzPutanje(string putanja)
        {
            string[] temp = putanja.Split('\\');

            return temp[(temp.Length - 1)];
        }

        private void vidljivostStavki(bool vidljivost)
        {
            button1.Visible = vidljivost;
            richTextBox1.Visible = vidljivost;
            btnSacuvaj.Visible = vidljivost;
            if (!vidljivost)
            {
                lblIzmenjen.Visible = vidljivost;
            }
        }
    }
}
