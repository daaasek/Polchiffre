using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public string recipentId;

        public Form1()
        {
            InitializeComponent();
        }

        // GRUPA TEXTBOX'A DO WPISYWANIA
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        // GRUPA TEXTBOX'A Z WYNIKIEM DZIALANIA
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        // OPCJA SZYFRUJ
        public void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
        }

        // OPCJA ODSZYFRUJ
        public void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        // WYWOLANIE CMD
        public void ExecuteCommand(string Command)
        {
            ProcessStartInfo ProcessInfo;
            Process Process;

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + Command);

            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = true;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process = Process.Start(ProcessInfo);
            Process.WaitForExit();
        }

        static public string EncodeTo64(string toEncode)

        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }

        static public string DecodeFrom64(string encodedData)

        {

            byte[] encodedDataAsBytes

                = System.Convert.FromBase64String(encodedData);

            string returnValue =

               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }
        // FUNKCJA SZYFRUJACA
        public void Encrypt(string recipentId)
        {
            if (radioButton1.Checked == true)
            {
                string text;
                string encodedText;
                if (textBox1.TextLength == 0)
                {
                    MessageBox.Show("Wpisz wiadomość!");
                }
                else
                {
                    text = textBox1.Text;
                    if (String.IsNullOrEmpty(recipentId))
                    {
                        MessageBox.Show("Wybierz klucze publiczne\ndla jakich szyfrować !");
                    }
                    else
                    {
                        ExecuteCommand("echo " + text + "|gpg --encrypt --armor --trust-model always" + recipentId + "|clip");
                        text = Clipboard.GetText();
                        encodedText = EncodeTo64(text);
                        Clipboard.SetText(encodedText);
                        //textBox2.Text = encodedText;
                        textBox2.Text = Clipboard.GetText();
                    }
                }
            }

        }

        // FUNKCJA DESZYFRUJACA
        public void Decrypt()
        {
            string text;
            text = textBox1.Text;
            if (radioButton2.Checked == true) //odszyfrowanie
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\szyfr.txt";
                string text1;
                string decodedText;
                if (textBox1.TextLength == 0)
                {
                    MessageBox.Show("Wpisz wiadomość!");
                }
                else
                {
                    text1 = textBox1.Text;
                    decodedText = DecodeFrom64(text1);
                    File.WriteAllText(path, decodedText);
                    ExecuteCommand("gpg -d --try-all-secrets " + path + " |clip");
                    textBox2.Text = Clipboard.GetText();
                }
            }

        }

        //PRZYCISK "URUCHOM NARZEDZIE"
        public void button1_Click(object sender, EventArgs e)
        {
            //Łukasza
            if (checkBox1.Checked)
            {
                recipentId = recipentId + " -r 79501F0F706A82C3C656C7691DBD19D3E45587CD";

            }
            //Gabrieli
            if (checkBox2.Checked)
            {
                recipentId = recipentId + " -r 0FB871E54C210175B87742DAF6A562AE18695955";

            }
            //Pawła
            if (checkBox3.Checked)
            {
                recipentId = recipentId + " -r 386DCEC744F9A62827F248C05CEA18D0255A858E";

            }
            //Kuby
            if (checkBox4.Checked)
            {
                recipentId = recipentId + " -r 9781445CCAD4E844A29324B059214BDB762965E9";

            }
            //Mateusza
            if (checkBox5.Checked)
            {
                recipentId = recipentId + " -r DF18EDFFA8C1087CFE12AC5B4BF62E5A0139F58E";

            }
            //Dastina
            if (checkBox6.Checked)
            {
                recipentId = recipentId + " -r 6567844E3D0FF11418E94B9673636622ACF5CB01";

            }


            //wywołanie funkcji void odpowiedzialnych za działanie programu
            if (radioButton1.Checked == true)
            {
                Encrypt(recipentId);
            }
            if (radioButton2.Checked == true)
            {
                Decrypt();
            }


        }

        // CHECKBOXY Z KLUCZAMI
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {


        }

        // GRUPA Z CHECKBOX'AMI KLUCZY
        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        // USUWANIE PLIKÓW TYMCZASOWYCH, TWORZONYCH PODCZAS DZIALANIA APKI
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\szyfr.txt";

            File.Delete(path);
        }

        // O NAS > TWORCY
        private void twórcyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Koder: Paszkowski Dastin\n" +
                "Menadżer projektu: Jakub Żurawski\n" +
                "Tester: Paweł Czarnocki\n" +
                "Research/Testerr: Mateusz Gałka\n");
        }

        // O NAS > STRONA WWW
        private void stronaWWWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://polchiffre.pl");
        }

        private void markAll_Click(object sender, EventArgs e)
        {
            foreach (Control c in groupBox4.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked == false)
                    {
                        cb.Checked = true;
                        markAll.Text = "Odznacz wszystkich";
                    }
                    else
                    {
                        cb.Checked = false;
                        markAll.Text = "Zaznacz wszystkich";
                    }
                }
            }
        }
    }
}
