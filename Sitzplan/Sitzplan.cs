using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Sitzplan
{
    public class TSitzplan
    {
        public List<TSitzplan.Schueler> schueler;
        //private int Bewertung = 0;
        [Serializable]
        public struct Schueler {
            public Schueler(int Nummer, String Schueler, String W1, String W2, String W3, String W4, String W5)
            {
                Nr = Nummer;
                Name = Schueler;
                Wunsch1 = W1;
                Wunsch2 = W2;
                Wunsch3 = W3;
                Wunsch4 = W4;
                Wunsch5 = W5;
            }
            public int Nr { get; set; }
            public String Name { get; set; }
            public String Wunsch1 { get; set; }
            public String Wunsch2 { get; set; }
            public String Wunsch3 { get; set; }
            public String Wunsch4 { get; set; }
            public String Wunsch5 { get; set; }
        }
        public List<List<List<Schueler>>> allePäärchen;
        public List<Schueler> lückenfüller1 = new List<Schueler>();
        public List<List<Schueler>> lückenfüller2 = new List<List<Schueler>>();
        public List<List<List<Schueler>>> lückenfüller3 = new List<List<List<Schueler>>>();
        public List<List<List<Schueler>>> alleKombinationen;
        public List<List<Schueler>> gewuenschtePaerchen = new List<List<Schueler>>();
        public string safe;
        public IFormatter formatter = new BinaryFormatter();


        public void BelegeKlassenlisteName(String Name)
        {

        }
        
        public void BerechneSitzplan(List<TSitzplan.Schueler> temp)
        {
            lückenfüller1 = new List<Schueler>();
            lückenfüller2 = new List<List<Schueler>>();
            lückenfüller3 = new List<List<List<Schueler>>>();
            lückenfüller1.Add(schueler[0]);
            lückenfüller2.Add(lückenfüller1);
            lückenfüller2.Add(lückenfüller1);
            lückenfüller2.Add(lückenfüller1);
            lückenfüller3.Add(lückenfüller2);
            if (temp.Count() % 2 != 0)
            {
                temp.Add(new Schueler(0, null, null, null, null, null, null));
            }
            schueler = temp;
            allePäärchen = new List<List<List<Schueler>>>();
         //   BildeAllePäärchen();
            alleKombinationen = new List<List<List<Schueler>>>();
            BildeGewuenschtePaerchen();
            EntferneDopplungen();
            SortierePaare();
            FindeKombinationen();
            //    BildeAlleKombinationen();
            // MessageBox.Show(safe);
        }
        private void BildeGewuenschtePaerchen()
        {
            foreach(Schueler ASchueler in schueler)
            {
                foreach(Schueler BSchueler in schueler)
                {
                    if(BSchueler.Name == null)
                    {
                        break;
                    }
                    if(BSchueler.Name == ASchueler.Wunsch1)
                    {
                        List<Schueler> Päärchen = new List<Schueler>();
                        Päärchen.Add(ASchueler);
                        Päärchen.Add(BSchueler);
                        gewuenschtePaerchen.Add(Päärchen);
                    }
                    if (BSchueler.Name == ASchueler.Wunsch2)
                    {
                        List<Schueler> Päärchen = new List<Schueler>();
                        Päärchen.Add(ASchueler);
                        Päärchen.Add(BSchueler);
                        gewuenschtePaerchen.Add(Päärchen);
                    }
                    if (BSchueler.Name == ASchueler.Wunsch3)
                    {
                        List<Schueler> Päärchen = new List<Schueler>();
                        Päärchen.Add(ASchueler);
                        Päärchen.Add(BSchueler);
                        gewuenschtePaerchen.Add(Päärchen);
                    }
                    if (BSchueler.Name == ASchueler.Wunsch4)
                    {
                        List<Schueler> Päärchen = new List<Schueler>();
                        Päärchen.Add(ASchueler);
                        Päärchen.Add(BSchueler);
                        gewuenschtePaerchen.Add(Päärchen);
                    }
                    if (BSchueler.Name == ASchueler.Wunsch5)
                    {
                        List<Schueler> Päärchen = new List<Schueler>();
                        Päärchen.Add(ASchueler);
                        Päärchen.Add(BSchueler);
                        gewuenschtePaerchen.Add(Päärchen);
                    }
                }
            }
            
            
        }
        private void EntferneDopplungen()
        {
            foreach (List<Schueler> Paar in gewuenschtePaerchen) // doppelte rausnehmen
            {
                bool entfernt = false;
                foreach (List<Schueler> schuelers in gewuenschtePaerchen)
                {
                    if ((Paar[0].Nr == schuelers[1].Nr) && (Paar[1].Nr == schuelers[0].Nr))
                    {
                        entfernt = true;
                        gewuenschtePaerchen.RemoveAt(gewuenschtePaerchen.IndexOf(schuelers));
                        EntferneDopplungen();
                        break;
                    }
                }
                if (entfernt)
                {
                    entfernt = false;
                    break;
                }
            }
        }
        private void SortierePaare()
        {
            foreach(List<Schueler> Paar in gewuenschtePaerchen)
            {
                if (Paar[0].Nr > Paar[1].Nr)
                {
                    Schueler s1 = Paar[0];
                    Paar[0] = Paar[1];
                    Paar[1] = s1;
                }
            }
        }
        private void FindeKombinationen()
        {
            for (int i = 0; i < (schueler.Count/2); i++)
            {
                foreach (List<Schueler> ASchueler in gewuenschtePaerchen)
                {
                    foreach (List<Schueler> BSchueler in gewuenschtePaerchen)
                    {
                       // hmmmmmmmmmmmmmmmmmmmmmm, das muss Johannes, der Depp vom Dienst, in einem wacheren Zustand erledigen;
                    }
                }
            }
        }
        private void BildeAlleKombinationen()
        {
            int p = 0;
            //List<List<Schueler>> schuelers = new List<List<Schueler>>();
            List<List<List<Schueler>>> Kombination = new List<List<List<Schueler>>>();
           /* for (int i = 0; i < (schueler.Count() / 2); i++)
            {
                schuelers.Add(allePäärchen[0][0]); // lückenfüller
            }*/
           
            for(int i = 0; i < schueler.Count() - 1; i++)
            {
                

                for (int k = 0; k < 3; k++)// herausfinden, wie viele "parallele" kombinationen es gibt!!
                {
                    alleKombinationen.Add(lückenfüller2);
                    alleKombinationen[p] = new List<List<Schueler>>();
                    int zeiger1, zeiger2;
                    
                    List<int> ausgeschieden = new List<int>();
                    for (int l = 0; l < schueler.Count(); l++)
                    {

                        for (int n = 0; n <= l; n++)
                        {
                            zeiger1 = n; //zeiger 1 ist der kleinere.

                            zeiger2 = l + k;
                            if (ausgeschieden.Count == 0)
                            {


                                List<Schueler> Päärchen = new List<Schueler>();
                                Päärchen.Add(schueler[0]);
                                Päärchen.Add(schueler[i + 1]);
                                ausgeschieden.Add(0);
                                ausgeschieden.Add(i + 1);
                                alleKombinationen[p].Add(Päärchen);

                            }
                            else
                            {
                                bool valid = true;
                                for (int o = 0; o < ausgeschieden.Count; o++)
                                {

                                    if (zeiger1 == ausgeschieden[o] || zeiger2 == ausgeschieden[o] || zeiger2 <= zeiger1 || zeiger1 <= alleKombinationen.Count)
                                    {
                                        valid = false;
                                    }
                                }
                                    if (valid)
                                    {
                                        List<Schueler> Päärchen = new List<Schueler>();
                                        Päärchen.Add(schueler[zeiger1]);
                                        Päärchen.Add(schueler[zeiger2]);
                                        ausgeschieden.Add(zeiger1);
                                        ausgeschieden.Add(zeiger2);
                                        alleKombinationen[p].Add(Päärchen);
                                    }
                                    
                                    
                                

                            }
                            
                            
                        }

                    }
                    p++;
                }
            }
            //BildeAlleKombinationenRekursion(schuelers, allePäärchen, 0);
            //List<List<Schueler>> test = Kombination;
        }
        /*private void BildeAlleKombinationenRekursion(List<List<Schueler>> i_alleKombinationen, List<List<Schueler>> i_allePäärchen, int i_currentPos)
        {
            //List<List<List<Schueler>>> ErgebnisTest = new List<List<List<Schueler>>>();
            if (i_currentPos == (schueler.Count() / 2))
            {
                //alleKombinationen.Add(i_alleKombinationen); //!!!!!!!!!!!!!!!!!!!!!!!ausgabe richtig machen!
                //ErgebnisTest.Add(i_alleKombinationen);
                sichereDenScheiß(i_alleKombinationen);
                i_alleKombinationen = new List<List<Schueler>>();
              //  string combination = "";
                if(Score(i_alleKombinationen) > Bewertung)
                {
                    Bewertung = Score(i_alleKombinationen);
                    ErgebnisKombination = i_alleKombinationen;
                }
                
               // combination += i_alleKombinationen[0][0].Name + " " + i_alleKombinationen[0][1].Name + "     " + i_alleKombinationen[1][0].Name + " " + i_alleKombinationen[1][1].Name;
                
              //  safe += combination + "                       ";
                return;
            }
            for (int j = 0; j < i_allePäärchen.Count(); j++)
            {
               // List<List<List<Schueler>>> test = alleKombinationen;
                i_alleKombinationen[i_currentPos] = i_allePäärchen[j];
               
                this.BildeAlleKombinationenRekursion(i_alleKombinationen, i_allePäärchen, i_currentPos + 1);




            }
            
        }
        */
        private int Score(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach(List<Schueler> paar in Kombination)
            {
                if (paar[0].Wunsch1 == paar[1].Name || paar[0].Wunsch2 == paar[1].Name || paar[0].Wunsch3 == paar[1].Name || paar[0].Wunsch4 == paar[1].Name || paar[0].Wunsch5 == paar[1].Name)
                {
                    score++;
                }
                if (paar[1].Wunsch1 == paar[0].Name || paar[1].Wunsch2 == paar[0].Name || paar[1].Wunsch3 == paar[0].Name || paar[1].Wunsch4 == paar[0].Name || paar[1].Wunsch5 == paar[0].Name)
                {
                    score++;
                }
            }
            return score;
        }
        private void sichereDenScheiß(List<List<Schueler>> sicher)
        {
            alleKombinationen.Add(sicher);
            
        }
        
    }
}
