using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string user = " ";
        public string folder = " ";
        public string s_path = "";
        public string s_name = "";

        public Form1()
        {
            InitializeComponent();
            
            comboBox1.Items.Add("Klient1"); //pozycja 0. w combo boxie
            comboBox1.Items.Add("Klient2");
            comboBox1.Items.Add("Klient3");
            comboBox1.Items.Add("Klient4");
            comboBox1.Items.Add("Klient5");
            comboBox1.SelectedIndex = 0;

            button2.Enabled = false;
            textBox2.Enabled = false;
            listBox1.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            checkBox1.Enabled = false;
            textBox1.Enabled = false;
            button5.Enabled = false;
            
        }
        
        public void przeslij_klient_bufor(string klient, string nazwa_klient) //nazwa_klienta = nazwa pliku
        {
            System.IO.File.Copy(folder + @"\" + nazwa_klient, @"D:\Bufor\" + klient + @"\" + nazwa_klient, true);  //kopia klient do bufor
        }
        public void przeslij_bufor_klient(string klient, string nazwa_klient)
        {
            System.IO.File.Copy( @"D:\Bufor\" + klient + @"\" + nazwa_klient, folder +  @"\" + nazwa_klient,  true);  //kopia klient do bufor
        }
        
        public void sprawdzanie_klient_bufor_petla(string user)
        {
            while(checkBox1.Checked == true)
            {
                string[] fileKlient1 = System.IO.Directory.GetFiles(folder);
                string[] fileBuforK1 = System.IO.Directory.GetFiles(@"D:\Bufor\" + user);

                //wyszukiwanie
                foreach (string fileKlient in fileKlient1)  //patrzymy czy pliki z folderu klienta są w buforze jeśli nie lub data jest starsza
                {
                    string nazwa_klient = System.IO.Path.GetFileName(fileKlient);

                    bool czy_plik_istnieje = false;

                    foreach (string fileBufor in fileBuforK1)
                    {
                        string nazwa_bufor = System.IO.Path.GetFileName(fileBufor);

                        if (nazwa_klient == nazwa_bufor) //porównujemy gdy są te pliki są równe - patrzymy na daty modyfikacji
                        {
                            czy_plik_istnieje = true;

                            DateTime mod_Klient = System.IO.File.GetLastWriteTime(fileKlient);
                            DateTime mod_Bufor = System.IO.File.GetLastWriteTime(fileBufor);

                            if (mod_Klient != mod_Bufor) //przesyła
                            {
                                if (mod_Klient > mod_Bufor) //w folderze klienta jest nowszy ten plik
                                {
                                    System.Threading.ThreadStart parametry = delegate { przeslij_klient_bufor(user, nazwa_klient); };

                                    System.Threading.Thread thr = new System.Threading.Thread(parametry);

                                    thr.Start();
                                }
                                else //w buforze jest nowsze więc pobierz
                                {
                                    System.Threading.ThreadStart parametry = delegate { przeslij_bufor_klient(user, nazwa_bufor); };

                                    System.Threading.Thread thr = new System.Threading.Thread(parametry);

                                    thr.Start();
                                }
                            }
                        }
                        break;
                    }//wyszlismy z wewnetrznego foreacha

                    if (!czy_plik_istnieje) //ten plik jest nowy i trzeba go przesłać na serwer
                    {
                        System.Threading.ThreadStart parametry = delegate { przeslij_klient_bufor(user, nazwa_klient); };

                        System.Threading.Thread thr = new System.Threading.Thread(parametry);

                        thr.Start();
                    }



                } //patrzymy czy coś jest w folderze klienta i przesyłamy + czy modyfikowany

                //Teraz patrzymy czy coś jest w buforze a nie ma u klienta to pobieramy do klienta
                foreach (string fileBufor in fileBuforK1)  //patrzymy czy pliki z folderu klienta są w buforze jeśli nie lub data jest starsza
                {
                    string nazwa_bufor = System.IO.Path.GetFileName(fileBufor);

                    bool czy_plik_istnieje = false;

                    foreach (string fileKlient in fileKlient1)
                    {
                        string nazwa_klient = System.IO.Path.GetFileName(fileKlient);

                        if (nazwa_klient == nazwa_bufor) //porównujemy gdy są te pliki są równe - patrzymy na daty modyfikacji
                        {
                            czy_plik_istnieje = true;
                        }
                    }

                    if (!czy_plik_istnieje) //ten plik jest nowy i trzeba go przesłać na serwer
                    {
                        System.Threading.ThreadStart parametry = delegate { przeslij_bufor_klient(user, nazwa_bufor); };

                        System.Threading.Thread thr = new System.Threading.Thread(parametry,40000000);

                        thr.Start();
                    }



                } //patrzymy czy coś jest w buforze a nie ma u klienta

                System.Threading.Thread.Sleep(3000);
            } //while
              // SendEmail.BeginInvoke((Action)delegate () { button3.Enabled = true; });
              //Console.WriteLine("ok");
            button3.BeginInvoke((Action)delegate () { button3.Visible = true; });
            checkBox1.BeginInvoke((Action)delegate () { checkBox1.Enabled = false; });
            //checkBox1.Enabled = false;
        } //sprawdzanie_klient_bufor_petla(string user)

        private void button1_Click(object sender, EventArgs e)  
        {
            listBox1.Items.Clear();

            string[] fileEntries = System.IO.Directory.GetFiles(folder);
            foreach (string fileName in fileEntries)
                listBox1.Items.Add(fileName);
      
        }

        //w zwykłej puli wątków upload do folderu Klineta
        private void button2_Click(object sender, EventArgs e) //upload pliku do folderu klienta1 
        {
            OpenFileDialog O_F_D = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\%USERPROFILE%\Desktop";
            openFileDialog1.RestoreDirectory = true;

            

            DialogResult result = openFileDialog1.ShowDialog();
             s_path = "";
             s_name = "";
            if (result == DialogResult.OK)
            {
                s_path = openFileDialog1.FileName;
                s_name = System.IO.Path.GetFileName(s_path);
                string nK = System.IO.Path.GetFileName(s_path);
                textBox2.Text = nK;
            }
            else
            {
                label1.Text = "Nie wybrałeś pliku";
            }
            Random rd = new Random();
            textBox1.Text = rd.Next(1, 15).ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string tmp_help = s_name + ";" + textBox1.Text + ";";
    

            try
            {
                System.IO.File.Copy(s_path, folder + @"\" + s_name, true);  //overwrite też tak działa GOOGLE
                label1.Text = "Plik został pomyślnie przesłany na serwer";
                
            }
            catch
            {
                label1.Text = "Nie udało się przesłać pliku";
            }

            string wer = @"D:\Bufor\" + user + @"_config.csv";
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, wer.Replace("\\", ""));
            try
            {
                mutex.WaitOne();
                //do something with the file....
                StreamWriter outputFile = new StreamWriter(Path.Combine(@"D:\Bufor\" + user + "_config.csv"), true);
                outputFile.WriteLine(tmp_help);
                outputFile.Close();
            }
            catch
            {
                MessageBox.Show("Mutex błąd");
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            
        }

        private void button3_Click(object sender, EventArgs e) //start serwer do bufor
        {
            checkBox1.Checked = true;
            checkBox1.Enabled = true;
            
            comboBox1.Enabled = false;
            textBox3.Enabled = false;
           
            button3.Visible = false;
            
            System.Threading.ThreadStart parametry = delegate { sprawdzanie_klient_bufor_petla(user); };

            System.Threading.Thread thr_check = new System.Threading.Thread(parametry);

            thr_check.Start();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            user = comboBox1.SelectedItem.ToString();
           
            button2.Enabled = true;
            textBox2.Enabled = true;
            listBox1.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = true;
            checkBox1.Enabled = false;
            textBox1.Enabled = true;
            button5.Enabled = true;
           
            comboBox1.Enabled = false;
            button6.Enabled = false;
            button4.Enabled = false;

            label2.Text = "Jesteś zalogowany jako " + user;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                  
                    textBox3.Text = fbd.SelectedPath;
                    folder = fbd.SelectedPath;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            textBox1.Enabled = false;
            button5.Enabled = false;
            listBox1.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            checkBox1.Enabled = false;

            if(checkBox1.Checked == true)
            {
                checkBox1.Checked = false;
                System.Threading.Thread.Sleep(3100);
            }

            comboBox1.Enabled = true;
            button6.Enabled = true;
            button4.Enabled = true;

            textBox2.Text = "";
            textBox1.Text = "";
            listBox1.Items.Clear();
            textBox3.Text = "";

            label2.Text = "";

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
                System.Threading.Thread.Sleep(3100);
            }
        }
    }
}
