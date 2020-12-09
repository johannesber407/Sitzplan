using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sitzplan
{
    public class TSitzplan
    {
        public List<TSitzplan.Schueler> schueler;
        private int Bewertung = 0;
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
        public List<List<Schueler>> allePäärchen;
        public List<List<List<Schueler>>> alleKombinationen;
        public List<List<Schueler>> ErgebnisKombination = new List<List<Schueler>>();
        public string safe;
        
        public void BelegeKlassenlisteName(String Name)
        {

        }
        
        public void BerechneSitzplan(List<TSitzplan.Schueler> temp)
        {

            if (temp.Count() % 2 != 0)
            {
                temp.Add(new Schueler(0, null, null, null, null, null, null));
            }
            schueler = temp;
            allePäärchen = new List<List<Schueler>>();
            BildeAllePäärchen();
            alleKombinationen = new List<List<List<Schueler>>>();
            
            BildeAlleKombinationen();
           // MessageBox.Show(safe);
        }
        private void BildeAllePäärchen()
        {
            for (int i = 0; i < schueler.Count(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (i == j)
                    {
                        j++;
                    }
                    if (i >= schueler.Count() || j >= schueler.Count())
                    {
                        break;
                    }
                    List<Schueler> Päärchen = new List<Schueler>();
                    Päärchen.Add(schueler[i]);
                    Päärchen.Add(schueler[j]);
                    allePäärchen.Add(Päärchen);
                }
            }
        }
        private void BildeAlleKombinationen()
        {
            List<List<Schueler>> schuelers = new List<List<Schueler>>();
            List<List<Schueler>> Kombination = new List<List<Schueler>>();
            for (int i = 0; i < (schueler.Count() / 2); i++)
            {
                schuelers.Add(allePäärchen[0]); // lückenfüller
            }
            for(int i = 0; i < schuelers.Count()/2; i++)
            {
                for (int n = 0; n < allePäärchen.Count(); n++)
                {
                    Kombination.Add(allePäärchen[n]);
                }
            }
            //BildeAlleKombinationenRekursion(schuelers, allePäärchen, 0);
        }
        private void BildeAlleKombinationenRekursion(List<List<Schueler>> i_alleKombinationen, List<List<Schueler>> i_allePäärchen, int i_currentPos)
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
