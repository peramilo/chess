using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {           
        int x, y, p, q, BKraljX=4, BKraljY=7, CKraljX=4, CKraljY=0;   //Kralj varijable cuvaju poziciju kralja
        bool BKralj, CKralj, SahB, SahC;  //true za sah
        bool klik = false;
        bool Top1, Top2, Top3, Top4;  //1 - beli levo, 2 - desno, 3 - crni levo
        bool a, pijun, potez=true;             //Za potez = true beli je na potezu!
        Image slika=null,slikaZamena;
        Button  Klik1, Klik2;     //Clicked je za sender, Start da znamo sa kog polja igramo!
        string figura,figuraZamena;
        Button[,] dugme = new Button[8, 8];
        Bitmap WP = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\White P.jpg", true), new Size(40, 40));
        Bitmap WR = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\White R.jpg", true), new Size(40, 40));
        Bitmap WN = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\White N.jpg", true), new Size(40, 40));
        Bitmap WB = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\White B.jpg", true), new Size(40, 40));
        Bitmap WQ = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\White Q.jpg", true), new Size(40, 40));
        Bitmap WK = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\White K.jpg", true), new Size(40, 40));
        Bitmap BP = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\Black P.jpg", true), new Size(40, 40));
        Bitmap BR = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\Black R.jpg", true), new Size(40, 40));
        Bitmap BN = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\Black N.jpg", true), new Size(40, 40));
        Bitmap BB = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\Black B.jpg", true), new Size(40, 40));
        Bitmap BQ = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\Black Q.jpg", true), new Size(40, 40));
        Bitmap BK = new Bitmap((Bitmap)System.Drawing.Bitmap.FromFile(@"figure\Black K.jpg", true), new Size(40, 40));
        public Form1()
        {
            InitializeComponent();
            Tabla();
        }
        
       public void Tabla()                                      //Postavlja tablu
        {          
            this.Text = "Sah";
            this.MinimumSize = new Size(675, 695);
            this.CenterToScreen();
            string imei, imej;
            for(int i = 0; i < 8; i++)
                for(int j = 0; j < 8; j++)
                {
                    dugme[i, j] = new Button()
                    {
                        Width = 80,
                        Height = 80,
                        Location = new Point(i * 80 + 1,
                                j * 80 + 1),
                        Parent = this
                    };
                    
                    dugme[i, j].Visible = true;
                    imei = i.ToString(); imej = j.ToString();
                    dugme[i, j].Name = "dugme" + imei + imej;
                    dugme[i, j].Tag = null;
                    dugme[i, j].Click += ButtonClick;
                    dugme[i, j].Show();  
                   
                }
             
           for (int i = 0; i < 8; i++)                                    //Postavljanje figura
           {
               dugme[i, 1].Image = BP; dugme[i, 1].Tag = "BP";
               dugme[i, 6].Image = WP; dugme[i, 6].Tag = "WP";
           }
           dugme[0, 0].Image = BR; dugme[0, 0].Tag = "BR";
           dugme[7, 0].Image = BR; dugme[7, 0].Tag = "BR";
           dugme[1, 0].Image = BN; dugme[1, 0].Tag = "BN";
           dugme[6, 0].Image = BN; dugme[6, 0].Tag = "BN";
           dugme[2, 0].Image = BB; dugme[2, 0].Tag = "BB";
           dugme[5, 0].Image = BB; dugme[5, 0].Tag = "BB";
           dugme[3, 0].Image = BQ; dugme[3, 0].Tag = "BQ";
           dugme[4, 0].Image = BK; dugme[4, 0].Tag = "BK";
           dugme[7, 7].Image = WR; dugme[7, 7].Tag = "WR";
           dugme[0, 7].Image = WR; dugme[0, 7].Tag = "WR";
           dugme[6, 7].Image = WN; dugme[6, 7].Tag = "WN";
           dugme[1, 7].Image = WN; dugme[1, 7].Tag = "WN";
           dugme[5, 7].Image = WB; dugme[5, 7].Tag = "WB";
           dugme[2, 7].Image = WB; dugme[2, 7].Tag = "WB";
           dugme[4, 7].Image = WK; dugme[4, 7].Tag = "WK";
           dugme[3, 7].Image = WQ; dugme[3, 7].Tag = "WQ";
           Bojenje();         
        }                           //Kraj funkcije Tabla

        public void Bojenje()
       {
           for (int i = 0; i < 8; i++)                                   //Boji polja
               for (int j = 0; j < 8; j = j + 2)
               {
                   if (i % 2 == 0)
                   {
                       dugme[j, i].BackColor = Color.AntiqueWhite;
                       dugme[j + 1, i].BackColor = Color.Sienna;
                   }
                   else
                   {
                       dugme[j + 1, i].BackColor = Color.AntiqueWhite;
                       dugme[j, i].BackColor = Color.Sienna;
                   }
               }
       }





        private void  ButtonClick(object sender, EventArgs e)     //Funkcija za klik
       {
            
            if (klik==false)
            {               
                Klik1 = (Button)sender;
                if (Klik1.Tag == null)
                {
                    MessageBox.Show("Izaberite figuru", "Greska"); 
                }
                else if (potez == true && Klik1.Tag.ToString().Substring(0, 1) == "B")
                { MessageBox.Show("Beli je na potezu!", "Greska");  }
                else if (potez == false && Klik1.Tag.ToString().Substring(0, 1) == "W")
                { MessageBox.Show("Crni je na potezu!", "Greska");  }
                else
                {                   
                    Klik1 = (Button)sender;
                    Klik1.BackColor = Color.DarkOrange;
                    figura = Klik1.Tag.ToString();
                    slika = Klik1.Image;
                    klik = true;
                }
            }
            else
            {               
                Klik2 = (Button)sender;
                switch (figura)
                {
                    case "WP":
                        {                          
                            BPijun(Klik1, Klik2);
                            Provera(a);
                        }
                        break;
                    case"WR":
                    case"BR":
                        {
                            Top(Klik1, Klik2);                           
                            Provera(a);
                            if (a == true)
                            {
                                if (potez == true && SahB == false && (Top1 == false || Top2 == false)) 
                                {
                                    if (x == 0 && y == 7) Top1 = true;
                                    if (x == 7 && y == 7) Top2 = true;
                                }
                                if (potez == false && SahC == false && (Top3==false || Top4==false)) 
                                {
                                    if (x == 0 && y == 0) Top3 = true;
                                    if (x == 7 && y == 0) Top4 = true;
                                }
                            }                                                          
                        }
                        break;
                    case"WN":
                    case"BN":
                        {
                            Konj(Klik1, Klik2);                           
                            Provera(a);
                        }
                        break;
                    case"WB":
                    case"BB":
                        {
                            Lovac(Klik1, Klik2);                            
                            Provera(a);                         
                        }
                        break;
                    case"WQ":
                    case"BQ":
                        {
                            Kraljica(Klik1, Klik2);                          
                            Provera(a);
                        }
                        break;
                    case"WK":
                    case"BK":
                        {
                            Kralj(Klik1, Klik2);
                            if (a == true && potez == true) { BKraljX = p; BKraljY = q; }
                            if (a == true && potez == false) { CKraljX = p; CKraljY = q; }
                            Provera(a);
                            if (potez == true && SahB == false && BKralj == false) BKralj = true;
                            if (potez == true && SahC == false && CKralj == false) CKralj = true;
                        }
                        break;
                    case"BP":
                        {
                            CPijun(Klik1, Klik2);                         
                            Provera(a);
                        }
                        break;
                }              
            }
       }                                        //Kraj ButtonClicked
        
        private void Provera(bool a)            //Proverava da li su zadovoljeni uslovi
        {
            if (a == true)
            {
               
                if (Klik2.Tag != null) figuraZamena = Klik2.Tag.ToString(); else figuraZamena = null;
                if (Klik2.Image != null) slikaZamena = Klik2.Image; else slikaZamena = null;
                Klik1.Tag = null;
                Klik1.Image = null;
                Klik2.Image = slika;
                Klik2.Tag = figura;
                klik = false;
                if (potez == true)
                {
                    SahBeli(); SahCrni();
                    if (SahB == true)
                    {                     
                        MessageBox.Show("Pod sahom ste!", "Greska");
                        VratiBoju(Klik1);
                        VratiPotez();
                    }
                    else
                    {
                        Bojenje();
                        Klik2.BackColor = Color.DarkOrange;
                        Klik1.BackColor = Color.DarkOrange;
                        potez = false;
                        if (SahC == true) MessageBox.Show("Sah!");
                    }
                }
                else
                {
                    SahCrni(); SahBeli();
                    if (SahC == true)
                    {                       
                        MessageBox.Show("Pod sahom ste!", "Greska");
                        VratiBoju(Klik1);
                        VratiPotez();
                    }
                    else
                    {
                        Bojenje();
                        Klik2.BackColor = Color.DarkOrange;
                        Klik1.BackColor = Color.DarkOrange;
                        potez = true;
                        if (SahB == true) MessageBox.Show("Sah!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Nemoguc potez", "Greska"); klik = false; VratiBoju(Klik1);
            }
        }                                     //Kraj Provera
        
       private bool BPijun(Button StartButton, Button ClickedButton)
       {
         
           x = Int32.Parse(StartButton.Name.ToString().Substring(5, 1));
           y = Int32.Parse(StartButton.Name.ToString().Substring(6, 1));
           p = Int32.Parse(ClickedButton.Name.ToString().Substring(5, 1));
           q = Int32.Parse(ClickedButton.Name.ToString().Substring(6, 1));
           if (y != 6) pijun = true; else pijun = false; 
           if (pijun == false)
           {
               if (((q==y-1) && (p==x+1)) || ((q==y-1) && (p==x-1)))
               {
                   if (ClickedButton.Tag == null) a = false;
                   else if (ClickedButton.Tag.ToString().Substring(0, 1) != "B") a = false;
                   else a = true;
               }
               else if (x != p || (y - q) > 2 || q > y) a = false;
               else if (ClickedButton.Tag != null)      a = false;
               else {pijun = true; a=true;}  
           }
           else 
           {
               if (((q == y - 1) && (p == x + 1)) || ((q == y - 1) && (p == x - 1)))
               {
                   if (ClickedButton.Tag == null) a = false;
                   else if (ClickedButton.Tag.ToString().Substring(0, 1) != "B") a = false;
                   else a = true;
               }
               else if ((y - q) > 1 || q > y) a = false;
               else if (ClickedButton.Tag != null) a = false;
               else a = true;
           }
           if (a == true && q == 0 && SahB == false)
           {
               Klik2.Tag = "WQ";
               Klik2.Image = WQ;
           }
           
           return a;
       }                                        //Kraj BPijun



       private bool CPijun(Button StartButton, Button ClickedButton)
       {         
           x = Int32.Parse(StartButton.Name.ToString().Substring(5, 1));
           y = Int32.Parse(StartButton.Name.ToString().Substring(6, 1));
           p = Int32.Parse(ClickedButton.Name.ToString().Substring(5, 1));
           q = Int32.Parse(ClickedButton.Name.ToString().Substring(6, 1));
           if (y != 1) pijun = true; else pijun = false;
           if (pijun == false)
           {
               if (((q == y + 1) && (p == x + 1)) || ((q == y + 1) && (p == x - 1)))
               {
                   if (ClickedButton.Tag == null) a = false;
                   else if (ClickedButton.Tag.ToString().Substring(0, 1) != "W") a = false;
                   else a = true;
               }
               else if (x != p || (q - y) > 2 || q < y) a = false;
               else if (ClickedButton.Tag != null) a = false;
               else { pijun = true; a = true; }
           }
           else 
           {
               if (((q == y + 1) && (p == x + 1)) || ((q == y + 1) && (p == x - 1)))
               {
                   if (ClickedButton.Tag == null) a = false;
                   else if (ClickedButton.Tag.ToString().Substring(0, 1) != "W") a = false;
                   else a = true;
               }
               else if (x != p || (q - y) > 1 || q < y) a = false;
               else if (ClickedButton.Tag != null)   a = false;
               else a = true;
           }
           if (a == true && q == 7 && SahC == false) 
            {
                figura = "BQ";
                slika = BQ;
            }
           return a;
       }


       private bool Top(Button StartButton, Button ClickedButton)
       {
           
            x = Int32.Parse(StartButton.Name.ToString().Substring(5, 1));
            y = Int32.Parse(StartButton.Name.ToString().Substring(6, 1));
            p = Int32.Parse(ClickedButton.Name.ToString().Substring(5, 1));
            q = Int32.Parse(ClickedButton.Name.ToString().Substring(6, 1));
            if (ClickedButton.Tag != null)
            {
                if (ClickedButton.Tag.ToString().Substring(0, 1) == StartButton.Tag.ToString().Substring(0, 1))  a = false;               
            }
            if (q == y && p == x) a = false;
            else if (q == y)
            {
                a = true;
                for (int i = x + Math.Sign(p - x); i != p; )
                {
                    if (dugme[i, y].Tag != null) { a = false; i = p - Math.Sign(p - x); }
                    i = i + Math.Sign(p - x);
                }
            }
            else if (p == x)
            {
                a = true;
                for (int i = y + Math.Sign(q - y); i != q; )
                {
                    if (dugme[x, i].Tag != null) { a = false; i = q - Math.Sign(q - y); }
                    i = i + Math.Sign(q - y);
                }
            }           
            else a = false;
            return a;
        }                                //Kraj Top


       private bool Konj(Button StartButton, Button ClickedButton)
        {
            x = Int32.Parse(StartButton.Name.ToString().Substring(5, 1));
            y = Int32.Parse(StartButton.Name.ToString().Substring(6, 1));
            p = Int32.Parse(ClickedButton.Name.ToString().Substring(5, 1));
            q = Int32.Parse(ClickedButton.Name.ToString().Substring(6, 1));
            if (Math.Abs(p - x) != 2 && Math.Abs(q - y) != 2) a = false;
            else if (Math.Abs(p - x) != 1 && Math.Abs(q - y) != 1) a = false;
            else if (ClickedButton.Tag != null)
            {
                if (ClickedButton.Tag.ToString().Substring(0, 1) == StartButton.Tag.ToString().Substring(0, 1)) a = false;
                else a = true;
            }
            else a = true;
            return a;
        }                              //Kraj Konj


       private bool Lovac(Button StartButton, Button ClickedButton)
        {
            a = true;
            x = Int32.Parse(StartButton.Name.ToString().Substring(5, 1));
            y = Int32.Parse(StartButton.Name.ToString().Substring(6, 1));
            p = Int32.Parse(ClickedButton.Name.ToString().Substring(5, 1));
            q = Int32.Parse(ClickedButton.Name.ToString().Substring(6, 1));         
            if (ClickedButton.Tag != null)
            {               
                if (ClickedButton.Tag.ToString().Substring(0, 1) == StartButton.Tag.ToString().Substring(0, 1)) a = false;              
            }
            if (Math.Abs(p - x) != Math.Abs(q - y)) a = false;
            else if (Math.Abs(p - x) > 1)
            {              
                int j = y + Math.Sign(q - y);
                for (int i = x + Math.Sign(p - x); i != p; )
                {                  
                    if (dugme[i, j].Tag != null) { a = false; i = p - Math.Sign(p - x); }
                    i = i + Math.Sign(p - x);
                    j = j + Math.Sign(q - y);
                }
            }           
            return a;
        }                           //Kraj Lovac


       private bool Kraljica(Button StartButton, Button ClickedButton)
        {
            x = Int32.Parse(StartButton.Name.ToString().Substring(5, 1));
            y = Int32.Parse(StartButton.Name.ToString().Substring(6, 1));
            p = Int32.Parse(ClickedButton.Name.ToString().Substring(5, 1));
            q = Int32.Parse(ClickedButton.Name.ToString().Substring(6, 1));
            if (p == x || q == y) Top(StartButton,ClickedButton);
            else Lovac(StartButton, ClickedButton);
            return a;
        }


       private bool Kralj(Button StartButton, Button ClickedButton)
        {
            x = Int32.Parse(StartButton.Name.ToString().Substring(5, 1));
            y = Int32.Parse(StartButton.Name.ToString().Substring(6, 1));
            p = Int32.Parse(ClickedButton.Name.ToString().Substring(5, 1));
            q = Int32.Parse(ClickedButton.Name.ToString().Substring(6, 1));
            if (Math.Abs(p - x) > 1 || Math.Abs(q - y) > 1) a = false;
            else if (ClickedButton.Tag != null)
            {
                if (ClickedButton.Tag.ToString().Substring(0, 1) == StartButton.Tag.ToString().Substring(0, 1)) a = false;
            }
            else a = true;
            if (a==false)
            {   
                if (potez == true)
                {                                     
                    {
                        if (BKralj == false && q == 7 && ((p == 2) || (p == 6)))
                        {
                            a = true; Rokada(); 
                        }
                    }
                }
                else
                {                                      
                    {
                        if (CKralj == false && q == 0 && ((p == 2) || (p == 6)))
                        {
                            a = true; Rokada();
                        }
                    }
                }
            }
           
            return a;
        }                                    //Kraj Kralj


        private void Rokada()              
        {            
            if (potez==true)
            {               
                if (p == 2 && Top1 == false && dugme[1,7].Tag==null && dugme[2,7].Tag==null && dugme[3,7].Tag==null)
                {
                    dugme[0, 7].Tag = null; 
                    dugme[0, 7].Image = null;
                    dugme[3, 7].Tag = "WR"; 
                    dugme[3, 7].Image = WR;
                    
                }
                if (p==6 && Top2==false && dugme[5,7].Tag==null && dugme[6,7].Tag==null)
                {
                    dugme[7, 7].Tag = null;
                    dugme[7, 7].Image = null;
                    dugme[5, 7].Tag = "WR";
                    dugme[5, 7].Image = WR;
                    
                }
            }
            if (potez == false)
            {             
                if (p == 2 && Top3 == false && dugme[1, 0].Tag == null && dugme[2, 0].Tag == null && dugme[3, 0].Tag == null)
                {     
                    dugme[0, 0].Tag = null;
                    dugme[0, 0].Image = null;
                    dugme[3, 0].Tag = "BR";
                    dugme[3, 0].Image = BR;
                    
                }
                if (p == 6 && Top4 == false && dugme[5, 0].Tag == null && dugme[6, 0].Tag == null)
                {                   
                    dugme[7, 0].Tag = null;
                    dugme[7, 0].Image = null;
                    dugme[5, 0].Tag = "BR";
                    dugme[5, 0].Image = BR;
                }
            }
        }                       //Kraj Rokada
      

        private bool SahBeli()          //Proverava da li je beli kralj pod sahom
        {
            SahB = false;
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if (dugme[i, j].Tag != null)
                    {
                        if (dugme[i, j].Tag.ToString().Substring(0, 1) == "B")
                        {
                            switch (dugme[i, j].Tag.ToString())
                            {
                                case "BR":
                                    {
                                        Top(dugme[i, j], dugme[BKraljX, BKraljY]);
                                        if (a == true)  SahB = true; 
                                    }
                                    break;
                                case "BN":
                                    {
                                        Konj(dugme[i, j], dugme[BKraljX, BKraljY]);
                                        if (a == true)  SahB = true; 
                                    }
                                    break;
                                case "BB":
                                    {
                                        Lovac(dugme[i, j], dugme[BKraljX, BKraljY]);
                                        if (a == true)  SahB = true; 
                                    }
                                    break;
                                case "BQ":
                                    {
                                        Kraljica(dugme[i, j], dugme[BKraljX, BKraljY]);
                                        if (a == true) SahB = true; 
                                    }
                                    break;
                                case "BP":
                                    {
                                        CPijun(dugme[i, j], dugme[BKraljX, BKraljY]);
                                        if (a == true) SahB = true;
                                    }
                                    break;
                                case "BK":
                                    {
                                        Kralj(dugme[i, j], dugme[BKraljX, BKraljY]);
                                        if (a == true) SahB = true;
                                    }
                                    break;
                            }           //Kraj switch-a
                        }

                    }

                }
            }
            a = true;
            return SahB;
        }                         //Kraj SahBeli


        private bool SahCrni()
        {
            SahC = false;
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if (dugme[i, j].Tag != null)
                    {
                        if (dugme[i, j].Tag.ToString().Substring(0, 1) == "W")
                        {
                            switch (dugme[i, j].Tag.ToString())
                            {
                                case "WR":
                                    {
                                        Top(dugme[i, j], dugme[CKraljX, CKraljY]);
                                        if (a == true) SahC = true;
                                    }
                                    break;
                                case "WN":
                                    {
                                        Konj(dugme[i, j], dugme[CKraljX, CKraljY]);
                                        if (a == true) SahC = true;
                                    }
                                    break;
                                case "WB":
                                    {
                                        Lovac(dugme[i, j], dugme[CKraljX, CKraljY]);
                                        if (a == true) SahC = true;
                                    }
                                    break;
                                case "WQ":
                                    {
                                        Kraljica(dugme[i, j], dugme[CKraljX, CKraljY]);
                                        if (a == true) SahC = true;
                                    }
                                    break;
                                case "WP":
                                    {
                                        BPijun(dugme[i, j], dugme[CKraljX, CKraljY]);
                                        if (a == true) SahC = true;
                                    }
                                    break;
                            }           //Kraj switch-a
                        }
                    }
                }
            }
             a = true;
             return SahC;
        }                         //Kraj SahCrni
        
        private void VratiPotez()
        {
            Klik1.Tag = figura;
            Klik1.Image = slika;
            Klik2.Tag = figuraZamena;
            Klik2.Image = slikaZamena;
            klik = false;
            figuraZamena = null;
            slikaZamena = null;
            a = false;
        }
   
        private void VratiBoju(Button x)
        {
            int s = Int32.Parse(x.Name.ToString().Substring(5, 1));
            int t = Int32.Parse(x.Name.ToString().Substring(6, 1));
            if (t % 2 == 0)
            {
                if (s % 2 == 0) x.BackColor = Color.AntiqueWhite;
                else x.BackColor = Color.Sienna;
            }
            else
            {
                if (s % 2 == 0) x.BackColor = Color.Sienna;
                else x.BackColor = Color.AntiqueWhite;
            }              
        } 
   
       

    }
}
