using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Sitzplan;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Sitzplan
{
    
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        int AnzahlEingetrageneSchueler = 0;
        public List<TSitzplan.Schueler> Schueler = new List<TSitzplan.Schueler>();
        
        List<TSitzplan.Schueler> W = new List<TSitzplan.Schueler>();
        TSitzplan sitzplan = new TSitzplan();
        
        
        // bool WuenscheWerdenNeuGeladen = false;
        public MainWindow()
        {
            InitializeComponent();
            DataGridKlassenliste.ItemsSource = Schueler;
            ComboboxSchueler.ItemsSource = Schueler;
            ComboboxSchueler.DisplayMemberPath = "Name";
         //   DataGridErgebnis.ItemsSource
            
        }

        private void ButtonBeenden_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonBerechnen_Click(object sender, RoutedEventArgs e)
        {
            sitzplan.schueler = Schueler;
            sitzplan.BerechneSitzplan(Schueler);
        }

        private void NeuerSchueler_Click(object sender, RoutedEventArgs e)
        {
            String Name;
            try
            {
                Name = SchuelerEingeben.Text;
                if(Name == " " || Name == "" || Name == null)
                {
                    System.Windows.MessageBox.Show("Bitte Name eingeben!");
                    SchuelerEingeben.Clear();
                    return;
                }
                
                for (int i = 1; i <= AnzahlEingetrageneSchueler; i++)
                {
                    String N = Schueler[i-1].Name;
                    if (Name == N)
                    {
                        System.Windows.MessageBox.Show("Dieser Schüler existiert bereits!");
                        SchuelerEingeben.Clear();
                        return;
                    }
                } 
            }
            catch
            {
                System.Windows.MessageBox.Show("Bitte Name eingeben!");
                SchuelerEingeben.Clear();
                return;
            }
            AnzahlEingetrageneSchueler++;

            TSitzplan.Schueler eingetragenerSchueler = new TSitzplan.Schueler(AnzahlEingetrageneSchueler, Name, null, null, null, null, null);
            Schueler.Add(eingetragenerSchueler);
            SchuelerEingeben.Clear();
            UpdateTabelle();
            UpdateComboBoxesWuensche();
        }
       private void UpdateTabelle()
        {
            DataGridKlassenliste.Items.Refresh();
        }
        
        private void WuenscheNaechsterSchueler_Click(object sender, RoutedEventArgs e)
        {
            if (ComboboxSchueler.SelectedIndex != -1)
            {
                ComboboxWunsch1.IsEnabled = true;
            }
            ComboboxSchueler.IsEnabled = true;
            ComboboxWunsch1.IsEnabled = false;
       //     ComboboxWunsch1.SelectedIndex = ComboboxSchueler.SelectedIndex;
            ComboboxWunsch2.IsEnabled = false;
            ComboboxWunsch3.IsEnabled = false;
            ComboboxWunsch4.IsEnabled = false;
            ComboboxWunsch5.IsEnabled = false;
            UpdateComboBoxesWuensche();
            EmptyComboboxes();
            
            
        }

        private void BlockierenEingeben_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboboxSchueler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboboxSchueler.SelectedIndex != -1) 
            {
                
                    /*if(schuelers.Wunsch1 != null || schuelers.Wunsch2 != null || schuelers.Wunsch3 != null || schuelers.Wunsch4 != null || schuelers.Wunsch5 != null)
                    {
                        WuenscheWerdenNeuGeladen = true;
                    }*/
                    if(Schueler[ComboboxSchueler.SelectedIndex].Wunsch1 != null || Schueler[ComboboxSchueler.SelectedIndex].Wunsch2 != null || Schueler[ComboboxSchueler.SelectedIndex].Wunsch3 != null || Schueler[ComboboxSchueler.SelectedIndex].Wunsch4 != null || Schueler[ComboboxSchueler.SelectedIndex].Wunsch5 != null)
                {
               //     WuenscheWerdenNeuGeladen = true;
              //      WuenscheNeuLaden();
                }
                
               
                //EmptyComboboxes();
                
                ComboboxWunsch1.IsEnabled = true;
                //ManageComboBoxes();


                /* foreach (TSitzplan.Schueler schueler in Schueler)
                 {
                     W1.Add(schueler);
                 }
                 TSitzplan.Schueler item = new TSitzplan.Schueler(Schueler[ComboboxSchueler.SelectedIndex].Nr, Schueler[ComboboxSchueler.SelectedIndex].Name, Schueler[ComboboxSchueler.SelectedIndex].Wunsch1, Schueler[ComboboxSchueler.SelectedIndex].Wunsch2, Schueler[ComboboxSchueler.SelectedIndex].Wunsch3, Schueler[ComboboxSchueler.SelectedIndex].Wunsch4, Schueler[ComboboxSchueler.SelectedIndex].Wunsch5);
                 W1.Remove(item);
                 //Schueler.Add(item);*/
                ManageComboBoxes();
                ComboboxWunsch1.ItemsSource = W;
                ComboboxWunsch1.DisplayMemberPath = "Name";
              
                ComboboxSchueler.IsEnabled = false;
                /*if(Schueler[ComboboxSchueler.SelectedIndex].Wunsch1 != null)
                {
                    for( int i = 0; i < Schueler.Count(); i++)
                    {
                        if (Schueler[i].Name == Schueler[ComboboxSchueler.SelectedIndex].Wunsch1)
                        {
                            ComboboxWunsch1.SelectedIndex = i;
                        }
                    }
                }*/
            }
        }
        private void ManageComboBoxes()
        {

            TSitzplan.Schueler item1, item2, item3, item4, item5, item6;
            item1 = new TSitzplan.Schueler(Schueler[ComboboxSchueler.SelectedIndex].Nr, Schueler[ComboboxSchueler.SelectedIndex].Name, Schueler[ComboboxSchueler.SelectedIndex].Wunsch1, Schueler[ComboboxSchueler.SelectedIndex].Wunsch2, Schueler[ComboboxSchueler.SelectedIndex].Wunsch3, Schueler[ComboboxSchueler.SelectedIndex].Wunsch4, Schueler[ComboboxSchueler.SelectedIndex].Wunsch5);
            item2 = item1;
            item3 = item1;
            item4 = item1;
            item5 = item1;
            item6 = item1;
            if (ComboboxWunsch1.SelectedIndex != -1)
            {
                
                for(int i = 0; i < Schueler.Count; i++)
                {
                    if (Schueler[i].Name == Schueler[ComboboxWunsch1.SelectedIndex].Name)
                    {
                        item2 = Schueler[i];
                    }
                }
            }
            
            if (ComboboxWunsch2.SelectedIndex != -1) 
            {
                
                for (int i = 0; i < Schueler.Count; i++)
                {
                    if (Schueler[i].Name == Schueler[ComboboxWunsch2.SelectedIndex].Name)
                    {
                        item3 = Schueler[i];
                    }
                }
            }
            if (ComboboxWunsch3.SelectedIndex != -1)
            {
                for (int i = 0; i < Schueler.Count; i++)
                {
                    if (Schueler[i].Name == Schueler[ComboboxWunsch3.SelectedIndex].Name)
                    {
                        item4 = Schueler[i];
                    }
                }
            }
                if (ComboboxWunsch4.SelectedIndex != -1)
            {
                for (int i = 0; i < Schueler.Count; i++)
                {
                    if (Schueler[i].Name == Schueler[ComboboxWunsch4.SelectedIndex].Name)
                    {
                        item5 = Schueler[i];
                    }
                }
            }
                if (ComboboxWunsch5.SelectedIndex != -1)
            {
                for (int i = 0; i < Schueler.Count; i++)
                {
                    if (Schueler[i].Name == Schueler[ComboboxWunsch5.SelectedIndex].Name)
                    {
                        item6 = Schueler[i];
                    }
                }
            }


            W = null;
            W = new List<TSitzplan.Schueler>();
            foreach(TSitzplan.Schueler schueler in Schueler)
            {
                W.Add(schueler);
            
            }
            for (int i = 0; i < Schueler.Count; i++)
            {
                TSitzplan.Schueler s = Schueler[i];
                if (s.Equals(item1))
                {
                    W[i] = new TSitzplan.Schueler(0, null, null, null, null, null, null);
                    
                }
            }
            for (int i = 0; i < Schueler.Count; i++)
            {
                TSitzplan.Schueler s = Schueler[i];
                if (s.Equals(item2))
                {
                    W[i] = new TSitzplan.Schueler(0, null, null, null, null, null, null);
                    ComboboxWunsch1.SelectedIndex = i;
                }
            }
            for (int i = 0; i < Schueler.Count; i++)
            {
                TSitzplan.Schueler s = Schueler[i];
                if (s.Equals(item3))
                {
                    W[i] = new TSitzplan.Schueler(0, null, null, null, null, null, null);
                }
            }
            for (int i = 0; i < Schueler.Count; i++)
            {
                TSitzplan.Schueler s = Schueler[i];
                if (s.Equals(item4))
                {
                    W[i] = new TSitzplan.Schueler(0, null, null, null, null, null, null);
                }
            }
            for (int i = 0; i < Schueler.Count; i++)
            {
                TSitzplan.Schueler s = Schueler[i];
                if (s.Equals(item5))
                {
                    W[i] = new TSitzplan.Schueler(0, null, null, null, null, null, null);
                }
            }
            for (int i = 0; i < Schueler.Count; i++)
            {
                TSitzplan.Schueler s = Schueler[i];
                if (s.Equals(item6))
                {
                    W[i] = new TSitzplan.Schueler(0, null, null, null, null, null, null);
                }
            }
            

        }


        private void UpdateComboBoxesWuensche()
        {
            ComboboxSchueler.Items.Refresh();
            ComboboxWunsch1.Items.Refresh();
            ComboboxWunsch2.Items.Refresh();
            ComboboxWunsch3.Items.Refresh();
            ComboboxWunsch4.Items.Refresh();
            ComboboxWunsch5.Items.Refresh();
            
        }
        private void EmptyComboboxes()
        {
            //if (!WuenscheWerdenNeuGeladen)
          //  {
                ComboboxWunsch1.SelectedIndex = ComboboxSchueler.SelectedIndex;
                ComboboxWunsch2.SelectedIndex = ComboboxSchueler.SelectedIndex;
                ComboboxWunsch3.SelectedIndex = ComboboxSchueler.SelectedIndex;
                ComboboxWunsch4.SelectedIndex = ComboboxSchueler.SelectedIndex;
                ComboboxWunsch5.SelectedIndex = ComboboxSchueler.SelectedIndex;
         //       WuenscheWerdenNeuGeladen = true;
         //   }
          /* else
            {
                WuenscheNeuLaden();
            }*/
            /*if(Schueler[ComboboxSchueler.SelectedIndex].Wunsch1 == null)
            {
                ComboboxWunsch1.SelectedIndex = ComboboxSchueler.SelectedIndex;
            }
            if (Schueler[ComboboxSchueler.SelectedIndex].Wunsch2 == null)
            {
                ComboboxWunsch2.SelectedIndex = ComboboxSchueler.SelectedIndex;
            }
            if (Schueler[ComboboxSchueler.SelectedIndex].Wunsch3 == null)
            {
                ComboboxWunsch3.SelectedIndex = ComboboxSchueler.SelectedIndex;
            }
            if (Schueler[ComboboxSchueler.SelectedIndex].Wunsch4 == null)
            {
                ComboboxWunsch4.SelectedIndex = ComboboxSchueler.SelectedIndex;
            }
            if (Schueler[ComboboxSchueler.SelectedIndex].Wunsch5 == null)
            {
                ComboboxWunsch5.SelectedIndex = ComboboxSchueler.SelectedIndex;
            }*/

        }
     /*   private void WuenscheNeuLaden()
        {
            if (Schueler[ComboboxSchueler.SelectedIndex].Wunsch1 != null)
            {

            }
           // WuenscheWerdenNeuGeladen = false;
        }*/
        private void ComboboxWunsch1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboboxWunsch1.SelectedIndex != -1)
            {
                
                
                if(W[ComboboxWunsch1.SelectedIndex].Name != null)
                {
                    ComboboxWunsch2.IsEnabled = true;
                }
                

              //  if (!WuenscheWerdenNeuGeladen)
               // {
                    String Wun1 = W[ComboboxWunsch1.SelectedIndex].Name;
                    TSitzplan.Schueler temp = Schueler[ComboboxSchueler.SelectedIndex];
                    temp.Wunsch1 = Wun1;
                    Schueler[ComboboxSchueler.SelectedIndex] = temp;
                //}
                    UpdateTabelle();
                ManageComboBoxes();

                /*foreach (TSitzplan.Schueler schueler in W1)
                {
                    W2.Add(schueler);
                }
                TSitzplan.Schueler item = new TSitzplan.Schueler(W1[ComboboxWunsch1.SelectedIndex].Nr, W1[ComboboxWunsch1.SelectedIndex].Name, W1[ComboboxWunsch1.SelectedIndex].Wunsch1, W1[ComboboxWunsch1.SelectedIndex].Wunsch2, W1[ComboboxWunsch1.SelectedIndex].Wunsch3, W1[ComboboxWunsch1.SelectedIndex].Wunsch4, W1[ComboboxWunsch1.SelectedIndex].Wunsch5);
                W2.Remove(item);*/
               
                ComboboxWunsch2.ItemsSource = W;
                ComboboxWunsch2.DisplayMemberPath = "Name";
             
                /*if (Schueler[ComboboxSchueler.SelectedIndex].Wunsch2 != null)
                {
                    for (int i = 0; i < Schueler.Count(); i++)
                    {
                        if (Schueler[i].Name == Schueler[ComboboxSchueler.SelectedIndex].Wunsch2)
                        {
                            ComboboxWunsch2.SelectedIndex = i;
                        }
                    }
                }*/
            }
        }

        

        

        private void ComboboxWunsch2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboboxWunsch2.SelectedIndex != -1)
            {

                
                ComboboxWunsch3.IsEnabled = true;

                String Wun1 = W[ComboboxWunsch2.SelectedIndex].Name;
                TSitzplan.Schueler temp = Schueler[ComboboxSchueler.SelectedIndex];
                temp.Wunsch2 = Wun1;
                Schueler[ComboboxSchueler.SelectedIndex] = temp;
                UpdateTabelle();
                ManageComboBoxes();
                /*foreach (TSitzplan.Schueler schueler in W2)
                {
                    W3.Add(schueler);
                }
                TSitzplan.Schueler item = new TSitzplan.Schueler(W2[ComboboxWunsch2.SelectedIndex].Nr, W2[ComboboxWunsch2.SelectedIndex].Name, W2[ComboboxWunsch2.SelectedIndex].Wunsch1, W2[ComboboxWunsch2.SelectedIndex].Wunsch2, W2[ComboboxWunsch2.SelectedIndex].Wunsch3, W2[ComboboxWunsch2.SelectedIndex].Wunsch4, W2[ComboboxWunsch2.SelectedIndex].Wunsch5);
                W3.Remove(item);*/
                
                ComboboxWunsch3.ItemsSource = W;
                ComboboxWunsch3.DisplayMemberPath = "Name";
              
                /*if (Schueler[ComboboxSchueler.SelectedIndex].Wunsch3 != null)
                {
                    for (int i = 0; i < Schueler.Count(); i++)
                    {
                        if (W[i].Name == Schueler[ComboboxSchueler.SelectedIndex].Wunsch3)
                        {
                            ComboboxWunsch3.SelectedIndex = i;
                        }
                    }
                }*/
            }

        }

        private void ComboboxWunsch3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboboxWunsch3.SelectedIndex != -1)
            {
                
               
                ComboboxWunsch4.IsEnabled = true;

                String Wun1 = W[ComboboxWunsch3.SelectedIndex].Name;
                TSitzplan.Schueler temp = Schueler[ComboboxSchueler.SelectedIndex];
                temp.Wunsch3 = Wun1;
                Schueler[ComboboxSchueler.SelectedIndex] = temp;
                UpdateTabelle();
                ManageComboBoxes();
                /*foreach (TSitzplan.Schueler schueler in W3)
                {
                    W4.Add(schueler);
                }
                TSitzplan.Schueler item = new TSitzplan.Schueler(W3[ComboboxWunsch3.SelectedIndex].Nr, W3[ComboboxWunsch3.SelectedIndex].Name, W3[ComboboxWunsch3.SelectedIndex].Wunsch1, W3[ComboboxWunsch3.SelectedIndex].Wunsch2, W3[ComboboxWunsch3.SelectedIndex].Wunsch3, W3[ComboboxWunsch3.SelectedIndex].Wunsch4, W3[ComboboxWunsch3.SelectedIndex].Wunsch5);
                W4.Remove(item);*/
                
                ComboboxWunsch4.ItemsSource = W;
                ComboboxWunsch4.DisplayMemberPath = "Name";
               
                /*if (Schueler[ComboboxSchueler.SelectedIndex].Wunsch4 != null)
                {
                    for (int i = 0; i < Schueler.Count(); i++)
                    {
                        if (Schueler[i].Name == Schueler[ComboboxSchueler.SelectedIndex].Wunsch4)
                        {
                            ComboboxWunsch4.SelectedIndex = i;
                        }
                    }
                }*/
            }
        }

        private void ComboboxWunsch4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboboxWunsch4.SelectedIndex != -1)
            {
                
                
                ComboboxWunsch5.IsEnabled = true;

                String Wun1 = W[ComboboxWunsch4.SelectedIndex].Name;
                TSitzplan.Schueler temp = Schueler[ComboboxSchueler.SelectedIndex];
                temp.Wunsch4 = Wun1;
                Schueler[ComboboxSchueler.SelectedIndex] = temp;
                UpdateTabelle();
                ManageComboBoxes();
                
                ComboboxWunsch5.ItemsSource = W;
                ComboboxWunsch5.DisplayMemberPath = "Name";
                /*if (Schueler[ComboboxSchueler.SelectedIndex].Wunsch5 != null)
                {
                    for (int i = 0; i < Schueler.Count(); i++)
                    {
                        if (Schueler[i].Name == Schueler[ComboboxSchueler.SelectedIndex].Wunsch5)
                        {
                            ComboboxWunsch5.SelectedIndex = i;
                        }
                    }
                }*/
            }
        }

        private void ComboboxWunsch5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboboxWunsch5.SelectedIndex != -1)
            {
                String Wun1 = W[ComboboxWunsch5.SelectedIndex].Name;
                TSitzplan.Schueler temp = Schueler[ComboboxSchueler.SelectedIndex];
                temp.Wunsch5 = Wun1;
                Schueler[ComboboxSchueler.SelectedIndex] = temp;
                UpdateTabelle();
                ManageComboBoxes();

            }
        }

        private void SchuelerEingeben_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                NeuerSchueler_Click(sender, e);
            }
        }

        private void Speichern_Click(object sender, RoutedEventArgs e)
        {

           // Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
            
            
            FileStream writerFileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
            
            sitzplan.formatter.Serialize(writerFileStream, Schueler);
            writerFileStream.Close();

        }

        private void Oeffnen_Click(object sender, RoutedEventArgs e)
        {
            if (Schueler.Count != 0)
            {
                OeffneDateiInNeuemFenster();
                return;
            }
            List<TSitzplan.Schueler> schueler1 = new List<TSitzplan.Schueler>();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowDialog();
            try
            {
               schueler1 = BinaryDeserialize(openFileDialog1.FileName) as List<TSitzplan.Schueler>;
            }
            catch
            {
                System.Windows.MessageBox.Show("Du machst da was falsch.");
            }
            foreach(TSitzplan.Schueler schueler in schueler1)
            {
                Schueler.Add(schueler);
                SchuelerEingeben.Clear();
                UpdateTabelle();
                UpdateComboBoxesWuensche();
            }
            AnzahlEingetrageneSchueler = Schueler.Count();
        }
        private void OeffneDateiInNeuemFenster()
        {
            
            List<TSitzplan.Schueler> schueler1 = new List<TSitzplan.Schueler>();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowDialog();
            try
            {
                schueler1 = BinaryDeserialize(openFileDialog1.FileName) as List<TSitzplan.Schueler>;
            }
            catch
            {
                System.Windows.MessageBox.Show("Du machst da was falsch.");
            }
            MainWindow window = new MainWindow();
            window.Show(); // Returns immediately
            foreach (TSitzplan.Schueler schueler in schueler1)
            {
                window.Schueler.Add(schueler);
                window.SchuelerEingeben.Clear();
                window.UpdateTabelle();
                window.UpdateComboBoxesWuensche();
            }
            window.AnzahlEingetrageneSchueler = window.Schueler.Count();
        }
        private object BinaryDeserialize(string filePath)
        {
            object obj;
            FileStream readerFileStream;
            BinaryFormatter bf = new BinaryFormatter();
            readerFileStream = File.OpenRead(filePath);
            obj = bf.Deserialize(readerFileStream);
            readerFileStream.Close();
            return obj;
        }
    }
}
