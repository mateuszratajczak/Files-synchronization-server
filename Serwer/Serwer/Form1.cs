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

namespace Serwer
{
   
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            checkBox1.Visible = false;
        }

        //private static object lockObject = new object();

        public void przeslij_bufor_serwer(string klient, string nazwa_klient)
        {
            System.IO.File.Copy(@"D:\Bufor\" + klient + @"\" + nazwa_klient, @"D:\Serwer\" + klient + @"\" + nazwa_klient, true);  //kopia klient do bufor
            this.Invoke((MethodInvoker)(() => listBox1.Items.Add("Przesłano plik na serwer: " + klient + "  " + nazwa_klient )));
            
        }

        public void przeslij_bufor_serwer(string klient, string nazwa_klient, int opoznienie)
        {
            System.Threading.Thread.Sleep(opoznienie*1000);
            System.IO.File.Copy(@"D:\Bufor\" + klient + @"\" + nazwa_klient, @"D:\Serwer\" + klient + @"\" + nazwa_klient, true);  //kopia klient do bufor
            this.Invoke((MethodInvoker)(() => listBox1.Items.Add("Przesłano plik na serwer: " + klient + "  " + nazwa_klient)));

        }
        public void przeslij_serwer_bufor(string klient, string nazwa_klient)
        {
            System.IO.File.Copy(@"D:\Serwer\" + klient + @"\" + nazwa_klient, @"D:\Bufor\" + klient + @"\" + nazwa_klient, true);  //kopia klient do bufor
            this.Invoke((MethodInvoker)(() => listBox1.Items.Add("Przesłano plik do bufora: " + klient + "  " + nazwa_klient)));
        }

       public void przesylanie_kolejnosciowo(ref List<List<From_To>> ft)
        {
            int klient1_id = -1;
            int klient2_id = -1;
            int klient3_id = -1;
            int klient4_id = -1;
            int klient5_id = -1;

            while(checkBox1.Checked == true)
            {
                klient1_id = -1;
                klient2_id = -1;
                klient3_id = -1;
                klient4_id = -1;
                klient5_id = -1;

                int[] tab_op = new int[5];
                for (int i = 0; i < 5; i++)
                    tab_op[i] = 0;

                for (int i = 0; i < ft[0].Count(); i++)
                    if (ft[0][i].przesylany == 1)
                    {
                        
                        tab_op[0] = ft[0][i].deley;
                        klient1_id = i;
                        break;
                    }

                for (int i = 0; i < ft[1].Count(); i++)
                    if (ft[1][i].przesylany == 1)
                    {
                        
                        tab_op[1] = ft[1][i].deley;
                        klient2_id = i;
                        break;
                    }

                for (int i = 0; i < ft[2].Count(); i++)
                    if (ft[2][i].przesylany == 1)
                    {
                        tab_op[2] = ft[2][i].deley; 
                        klient3_id = i;
                        break;
                    }

                for (int i = 0; i < ft[3].Count(); i++)
                    if (ft[3][i].przesylany == 1)
                    {
                        tab_op[3] = ft[3][i].deley;
                        klient4_id = i;
                        break;
                    }


                for (int i = 0; i < ft[4].Count(); i++)
                    if (ft[4][i].przesylany == 1)
                    {
                        tab_op[4] = ft[4][i].deley;
                        klient5_id = i;
                        break;
                    }

                if (!(klient1_id == -1 && klient2_id == -1 && klient3_id == -1 && klient4_id == -1 && klient5_id == -1))
                {
                    Array.Sort(tab_op);
                   // MessageBox.Show(tab_op[0].ToString() + " " + tab_op[1].ToString() + " " + tab_op[2].ToString() + " " + tab_op[3].ToString() + " " + tab_op[4].ToString() );
                    //wyznaczyć ile czekamy
                    if (klient1_id != -1)
                    {
                        string nazwa_plik_00 = ft[0][klient1_id].plik;
                        int opoznienie_00 = ft[0][klient1_id].deley;

                        System.Threading.ThreadStart parametry = delegate { przeslij_bufor_serwer("Klient1", nazwa_plik_00, opoznienie_00); };

                        System.Threading.Thread thr = new System.Threading.Thread(parametry);

                        thr.Start();


                        ft[0][klient1_id].przesylany = 3;
                    }

                    if (klient2_id != -1)
                    {
                        string nazwa_plik_00 = ft[1][klient2_id].plik;
                        int opoznienie_00 = ft[1][klient2_id].deley;

                        System.Threading.ThreadStart parametry = delegate { przeslij_bufor_serwer("Klient2", nazwa_plik_00, opoznienie_00); };

                        System.Threading.Thread thr = new System.Threading.Thread(parametry);

                        thr.Start();


                        ft[1][klient2_id].przesylany = 3;
                    }

                    if (klient3_id != -1)
                    {
                        string nazwa_plik_00 = ft[2][klient3_id].plik;
                        int opoznienie_00 = ft[2][klient3_id].deley;

                        System.Threading.ThreadStart parametry = delegate { przeslij_bufor_serwer("Klient3", nazwa_plik_00, opoznienie_00); };

                        System.Threading.Thread thr = new System.Threading.Thread(parametry);

                        thr.Start();


                        ft[2][klient3_id].przesylany = 3;
                    }

                    if (klient4_id != -1)
                    {
                        string nazwa_plik_00 = ft[3][klient4_id].plik;
                        int opoznienie_00 = ft[3][klient4_id].deley;

                        System.Threading.ThreadStart parametry = delegate { przeslij_bufor_serwer("Klient4", nazwa_plik_00, opoznienie_00); };

                        System.Threading.Thread thr = new System.Threading.Thread(parametry);

                        thr.Start();


                        ft[3][klient4_id].przesylany = 3;
                    }

                    if (klient5_id != -1)
                    {
                        string nazwa_plik_00 = ft[4][klient5_id].plik;
                        int opoznienie_00 = ft[4][klient5_id].deley;

                        System.Threading.ThreadStart parametry = delegate { przeslij_bufor_serwer("Klient5", nazwa_plik_00, opoznienie_00); };

                        System.Threading.Thread thr = new System.Threading.Thread(parametry);

                        thr.Start();


                        ft[4][klient5_id].przesylany = 3;
                    }

                    System.Threading.Thread.Sleep(tab_op[4] * 1000 + 100);
                }//if
                else
                    System.Threading.Thread.Sleep(1000);
            }//while

            

            
        }//przesylanie kolejnosciowo

        public void bufor_serwer_KlientX(string kto, ref List<List<From_To>> ft)
        {
            int user_lista = 0;
            if (kto == "Klient1")
                user_lista = 0;
            else if (kto == "Klient2")
                user_lista = 1;
            else if (kto == "Klient3")
                user_lista = 2;
            else if (kto == "Klient4")
                user_lista = 3;
            else if (kto == "Klient5")
                user_lista = 4;

            string user = kto;
            //int test = 0;
            //int nowe_pliki = 0;

            while (checkBox1.Checked == true)
            {
                //nowe_pliki = 0;

                string[] fileBufor1 = System.IO.Directory.GetFiles(@"D:\Bufor\" + user);
                string[] fileSerwer1 = System.IO.Directory.GetFiles(@"D:\Serwer\" + user);

                //wyszukiwanie
                foreach (string fileBufor in fileBufor1)  //patrzymy czy pliki z folderu klienta są w buforze jeśli nie lub data jest starsza
                {
                    string nazwa_bufor = System.IO.Path.GetFileName(fileBufor);

                    bool czy_plik_istnieje = false;

                    foreach (string fileSerwer in fileSerwer1)
                    {
                        string nazwa_serwer = System.IO.Path.GetFileName(fileSerwer);

                        if (nazwa_bufor == nazwa_serwer) //porównujemy gdy są te pliki są równe - patrzymy na daty modyfikacji
                        {
                            czy_plik_istnieje = true;

                            DateTime mod_Bufor = System.IO.File.GetLastWriteTime(fileBufor);
                            DateTime mod_Serwer = System.IO.File.GetLastWriteTime(fileSerwer);

                            if (mod_Bufor != mod_Serwer) //przesyła
                            {
                                if (mod_Bufor > mod_Serwer) //w folderze klienta jest nowszy ten plik
                                {
                                    From_To k = new From_To("bufor_serwer",nazwa_bufor,0,5);
                                    ft[user_lista].Add(k);
                                    //nowe_pliki++;
                                }
                                else //w buforze jest nowsze więc pobierz
                                {
                                    From_To k = new From_To("serwer_bufor",nazwa_serwer,0,1);
                                    ft[user_lista].Add(k);
                                    //nowe_pliki++;
                                }
                            }
                        }
                    }

                    if (!czy_plik_istnieje) //ten plik jest nowy i trzeba go przesłać na serwer
                    {
                        bool czy_jest_w_liscie = false;
                        for(int i=0; i<ft[user_lista].Count(); i++)
                        {
                            if(ft[user_lista][i].plik == nazwa_bufor)
                            {
                                czy_jest_w_liscie = true;
                                //break;
                            }
                        }

                        if(!czy_jest_w_liscie)
                        {
                            From_To k = new From_To("bufor_serwer", nazwa_bufor, 0, 5);
                            //MessageBox.Show("Jestem tu");
                            ft[user_lista].Add(k);
                           // nowe_pliki++;
                        }
                    }
                } //patrzymy czy coś jest w folderze klienta i przesyłamy + czy modyfikowany

                //Teraz patrzymy czy coś jest w serwerze a nie ma w buforze to pobieramy do klienta
                foreach (string fileSerwer in fileSerwer1)  //patrzymy czy pliki z folderu klienta są w buforze jeśli nie lub data jest starsza
                {
                    string nazwa_serwer = System.IO.Path.GetFileName(fileSerwer);

                    bool czy_plik_istnieje = false;

                    foreach (string fileBufor in fileBufor1)
                    {
                        string nazwa_bufor = System.IO.Path.GetFileName(fileBufor);

                        if (nazwa_bufor == nazwa_serwer) //porównujemy gdy są te pliki są równe - patrzymy na daty modyfikacji
                        {
                            czy_plik_istnieje = true;
                            break;
                        }
                    }

                    if (!czy_plik_istnieje) //ten plik jest nowy i trzeba go przesłać na serwer
                    {
                        bool czy_jest_w_liscie = false;
                        for (int i = 0; i < ft[user_lista].Count(); i++)
                        {
                            if (ft[user_lista][i].plik == nazwa_serwer)
                            {
                                czy_jest_w_liscie = true;
                                break;
                            }
                        }

                        if (!czy_jest_w_liscie)
                        {
                            From_To k = new From_To("serwer_bufor", nazwa_serwer, 0, 1);
                            ft[user_lista].Add(k);
                            //nowe_pliki++;
                        }
                    
                    }
                } //patrzymy czy coś jest w buforze a nie ma u klienta

                /////..............................Do testu ............................./////////

                //int plikow_z_jedynka_w_liscie = 0;
                //for(int i=0; i<ft[user_lista].Count(); i++)
                //{
                //    if (ft[user_lista][i].przesylany == 1)
                //        plikow_z_jedynka_w_liscie++;
                //}

                //if ((plikow_z_jedynka_w_liscie == nowe_pliki) && nowe_pliki > 0 )
                //    test = 0;

                /////................................................................../////////

                //   Plik z opoznieniami wygenerowanymi przez uzytkownika - te opoznienia sa zadane.
                //   Gdy nie ma opoznienia losujemy od 1 - 15



                string line;
                string wer = @"D:\Bufor\" + kto + @"_config.csv";
                System.Threading.Mutex mutex = new System.Threading.Mutex(false, wer.Replace("\\", ""));
                try
                {
                    mutex.WaitOne();
                    //do something with the file....
                    

                        System.IO.StreamReader file = new System.IO.StreamReader(@"D:\Bufor\" + kto + @"_config.csv");
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] l = line.Split(new string[] { ";" }, StringSplitOptions.None);
                       
                        for (int i = 0; i < ft[user_lista].Count(); i++)
                        {
                            if (ft[user_lista][i].plik == l[0] && ft[user_lista][i].przesylany == 5)
                            {
                                //MessageBox.Show("Jestem w pliku");
                                ft[user_lista][i].deley = Int32.Parse(l[1]);
                                ft[user_lista][i].przesylany = 1;
                                break;
                            }
                        }

                    }

                    file.Close();
                    for(int i=0; i<ft[user_lista].Count(); i++)
                    {
                        if (ft[user_lista][i].przesylany == 5)
                            ft[user_lista][i].przesylany = 1;
                    }



                   // StreamWriter outputFile = new StreamWriter(Path.Combine(@"D:\Bufor\" + kto + "_config.csv"), false);
                   // outputFile.WriteLine(" ");
                   // outputFile.Close();

                }
                catch 
                {
                    MessageBox.Show("Cos z tym mutexem nie działa");
                }
                finally
                {
                    mutex.ReleaseMutex();
                }


                ////..........Dane w dwóch tablicach - pierwsza ft wraz z opoznieniami w pliku  ..............................................///
                //Tu będziemy przesyłać jakoś ...
                //Teraz tak, przekazujemy do wątku równoległego tą tablicę do przesyłania i przesyłamy równoległe
                // dodatkowo chcemy aby jeśli dodamy tu plik on od razu dodał się do listy i był dostępny we wątku

                
                /////.........................................................................//

                System.Threading.Thread.Sleep(3000);
            } //while

            button1.BeginInvoke((Action)delegate () { button1.Visible = true; });
            checkBox1.BeginInvoke((Action)delegate () { checkBox1.Visible = false; });
        } //bufor_serwer_KlientX

        public void sprawdzanie_bufor_serwer_petla()
        {
            List<List<From_To>> ft = new List<List<From_To>>();
            ft.Add(new List<From_To>());
            ft.Add(new List<From_To>());
            ft.Add(new List<From_To>());
            ft.Add(new List<From_To>());
            ft.Add(new List<From_To>());
            

            System.Threading.ThreadStart parametry1 = delegate { bufor_serwer_KlientX("Klient1", ref ft); };
            System.Threading.Thread thr_k1 = new System.Threading.Thread(parametry1);
            thr_k1.Start();

            System.Threading.ThreadStart parametry2 = delegate { bufor_serwer_KlientX("Klient2",ref  ft); };
            System.Threading.Thread thr_k2 = new System.Threading.Thread(parametry2);
            thr_k2.Start();

            System.Threading.ThreadStart parametry3 = delegate { bufor_serwer_KlientX("Klient3", ref ft); };
            System.Threading.Thread thr_k3 = new System.Threading.Thread(parametry3);
            thr_k3.Start();
            
            System.Threading.ThreadStart parametry4 = delegate { bufor_serwer_KlientX("Klient4",ref  ft); };
            System.Threading.Thread thr_k4 = new System.Threading.Thread(parametry4);
            thr_k4.Start();
            
            System.Threading.ThreadStart parametry5 = delegate { bufor_serwer_KlientX("Klient5",ref ft); };
            System.Threading.Thread thr_k5 = new System.Threading.Thread(parametry5);
            thr_k5.Start();

            System.Threading.ThreadStart parametry6 = delegate { przesylanie_kolejnosciowo(ref ft); };

            System.Threading.Thread thr_list = new System.Threading.Thread(parametry6);

            thr_list.Start();
            


        } //sprawdzanie_klient_bufor_petla(string user)

        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Visible = true;

            System.Threading.ThreadStart parametry = delegate { sprawdzanie_bufor_serwer_petla(); };

            System.Threading.Thread thr_check = new System.Threading.Thread(parametry);

            thr_check.Start();

            checkBox1.Checked = true;
            button1.Visible = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
                System.Threading.Thread.Sleep(3100);
            }

            button1.Enabled = false;

           
        }
    }
    public class From_To
    {
        public string sposob;
        public string plik;
        public int deley;
        public int przesylany;


        public From_To(string s, string p, int d, int prz)
        {
            sposob = s;
            plik = p;
            deley = d;
            przesylany = prz;
        }
    }
}
