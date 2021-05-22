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
        public int Bewertung = 0;/// muss 0 sein!
        [Serializable]
        public struct Schueler {
            public Schueler(int Nummer, String Schueler, String W1, String W2, String W3, String W4, String W5, List<string> Bl, List<string> Si)
            {
                Nr = Nummer;
                Name = Schueler;
                Wunsch1 = W1;
                Wunsch2 = W2;
                Wunsch3 = W3;
                Wunsch4 = W4;
                Wunsch5 = W5;
                //Blockiert = new List<string>();
                Blockiert = Bl;
                Sitznachbar = Si;
            }
            public int Nr { get; set; }
            public String Name { get; set; }
            public String Wunsch1 { get; set; }
            public String Wunsch2 { get; set; }
            public String Wunsch3 { get; set; }
            public String Wunsch4 { get; set; }
            public String Wunsch5 { get; set; }
            public List<string> Blockiert { get; set; }
            public List<string> Sitznachbar { get; set; }
        }
        public List<List<List<Schueler>>> allePäärchen;
        public List<Schueler> lückenfüller1 = new List<Schueler>();
        public List<List<Schueler>> lückenfüller2 = new List<List<Schueler>>();
        public List<List<List<Schueler>>> lückenfüller3 = new List<List<List<Schueler>>>();
        public List<List<List<Schueler>>> alleKombinationen;
        public List<List<Schueler>> EineKombination;
        public List<List<Schueler>> ErgebnisKombination = new List<List<Schueler>>();
        public List<List<Schueler>> gewuenschtePaerchen = new List<List<Schueler>>();
        public string safe;
        public IFormatter formatter = new BinaryFormatter();
        public int Iterationen, Gewichtung;


        public void BelegeKlassenlisteName(String Name)
        {

        }
        
        public void BerechneSitzplan(List<TSitzplan.Schueler> temp)
        {
            
            if (temp.Count() % 2 != 0)
            {
                temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
            }
            schueler = temp;
            allePäärchen = new List<List<List<Schueler>>>();
         
            alleKombinationen = new List<List<List<Schueler>>>();
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }

                for (int j = 0; j < schueler.Count() / 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (Score(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = Score(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }

                }

            }

        }
        public void BerechneBlockiertRandomSitzplan(List<TSitzplan.Schueler> temp)
        {
            List<TSitzplan.Schueler> neuerAufruf = new List<Schueler>();
            neuerAufruf = temp;
            if (temp.Count() % 2 != 0)
            {
                temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
            }
            schueler = temp;


            EineKombination = new List<List<Schueler>>();
            List<Schueler> Namen = new List<Schueler>();
            foreach (Schueler schueler1 in schueler)
            {
                Namen.Add(schueler1);
            }

            for (int j = 0; j < schueler.Count() / 2; j++)
            {
                EineKombination.Add(new List<Schueler>());
                for (int k = 0; k <= 1; k++)
                {
                    var random = new Random();
                    int index = random.Next(Namen.Count);
                    EineKombination[j].Add(Namen[index]);
                    Namen.RemoveAt(index);
                }
            }
            if (KombinationOK(EineKombination))
            {
                ErgebnisKombination = EineKombination;
                return;
            }
            else
            {
                BerechneBlockiertRandomSitzplan(neuerAufruf);
            }



        }

        

        public void BerechneTrueRandomSitzplan(List<TSitzplan.Schueler> temp)
        {
            if (temp.Count() % 2 != 0)
            {
                temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
            }
            schueler = temp;


            EineKombination = new List<List<Schueler>>();
            List<Schueler> Namen = new List<Schueler>();
            foreach (Schueler schueler1 in schueler)
            {
                Namen.Add(schueler1);
            }

            for (int j = 0; j < schueler.Count() / 2; j++)
            {
                EineKombination.Add(new List<Schueler>());
                for (int k = 0; k <= 1; k++)
                {
                    var random = new Random();
                    int index = random.Next(Namen.Count);
                    EineKombination[j].Add(Namen[index]);
                    Namen.RemoveAt(index);
                }
            }


            
            ErgebnisKombination = EineKombination;

        }

        internal void BerechneSitzplanSitznachbar(List<Schueler> temp)
        {
            if (temp.Count() % 2 != 0)
            {
                temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
            }
            schueler = temp;
            allePäärchen = new List<List<List<Schueler>>>();

            alleKombinationen = new List<List<List<Schueler>>>();
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }

                for (int j = 0; j < schueler.Count() / 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreSitznachbar(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreSitznachbar(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }
                }

            }
        }

        internal void BerechneSitzplanOhneBlockierungen(List<Schueler> temp)
        {
            if (temp.Count() % 2 != 0)
            {
                temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
            }
            schueler = temp;
            allePäärchen = new List<List<List<Schueler>>>();

            alleKombinationen = new List<List<List<Schueler>>>();
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }

                for (int j = 0; j < schueler.Count() / 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (Score(EineKombination) > Bewertung)
                {
                    
                    Bewertung = Score(EineKombination);
                    ErgebnisKombination = EineKombination;
                    

                }

            }
        }

        internal void BerechneSitzplanOhneBlockierungenZweiDreiZwei(List<Schueler> temp)
        {
            int t = 7 - (temp.Count() % 7);

            if (temp.Count() % 7 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 7;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 2; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreZweiDreiZwei(EineKombination) > Bewertung)
                {
                    
                        Bewertung = ScoreZweiDreiZwei(EineKombination);
                        ErgebnisKombination = EineKombination;
                    

                }

            }
        }

        internal void BerechneSitzplanSitznachbarZweiDreiZwei(List<Schueler> temp)
        {
            int t = 7 - (temp.Count() % 7);

            if (temp.Count() % 7 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 7;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 2; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreZweiDreiZweiSitznachbar(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreZweiDreiZweiSitznachbar(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }

                }

            }
        }

        internal void BerechneSitzplanOhneBlockierungenMitSitznachbarZweiDreiZwei(List<Schueler> temp)
        {
            int t = 7 - (temp.Count() % 7);

            if (temp.Count() % 7 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 7;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 2; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreZweiDreiZweiSitznachbar(EineKombination) > Bewertung)
                {
                    
                        Bewertung = ScoreZweiDreiZweiSitznachbar(EineKombination);
                        ErgebnisKombination = EineKombination;
                    

                }

            }
        }

        public void BerechneBlockiertRandomSitzplanZweiDreiZwei(List<Schueler> temp)
        {
            List<TSitzplan.Schueler> neuerAufruf = new List<Schueler>();
            neuerAufruf = temp;
            int t = 7 - (temp.Count() % 7);

            if (temp.Count() % 7 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 7;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 2; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreZweiDreiZwei(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }
                else
                {
                    BerechneBlockiertRandomSitzplanZweiDreiZwei(neuerAufruf);
                }

                

            }
        }

        internal void BerechneSitzplanOhneBlockierungenMitSitznachbarZweiVierZwei(List<Schueler> temp)
        {
            int t = 8 - (temp.Count() % 8);

            if (temp.Count() % 8 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 8;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreZweiVierZweiSitznachbar(EineKombination) > Bewertung)
                {
                    
                        Bewertung = ScoreZweiVierZweiSitznachbar(EineKombination);
                        ErgebnisKombination = EineKombination;
                    

                }

            }
        }

        internal void BerechneSitzplanOhneBlockierungenZweiVierZwei(List<Schueler> temp)
        {
            int t = 8 - (temp.Count() % 8);

            if (temp.Count() % 8 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 8;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreZweiVierZwei(EineKombination) > Bewertung)
                {
                    
                        Bewertung = ScoreZweiVierZwei(EineKombination);
                        ErgebnisKombination = EineKombination;
                    

                }

            }
        }

        internal void BerechneSitzplanSitznachbarZweiVierZwei(List<Schueler> temp)
        {
            int t = 8 - (temp.Count() % 8);

            if (temp.Count() % 8 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 8;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreZweiVierZweiSitznachbar(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreZweiVierZweiSitznachbar(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }

                }

            }
        }

        internal void BerechneSitzplanOhneBlockierungenMitSitznachbar(List<Schueler> temp)
        {
            if (temp.Count() % 2 != 0)
            {
                temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
            }
            schueler = temp;
            allePäärchen = new List<List<List<Schueler>>>();

            alleKombinationen = new List<List<List<Schueler>>>();
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }

                for (int j = 0; j < schueler.Count() / 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreSitznachbar(EineKombination) > Bewertung)
                {
                    
                    Bewertung = ScoreSitznachbar(EineKombination);
                    ErgebnisKombination = EineKombination;
                    
                }

            }
        }

        internal void BerechneSitzplanOhneBlockierungenVierVier(List<Schueler> temp)
        {
            int t = 4 - (temp.Count() % 4);

            if (temp.Count() % 4 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 4;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreVierVier(EineKombination) > Bewertung)
                {
                    
                        Bewertung = ScoreVierVier(EineKombination);
                        ErgebnisKombination = EineKombination;
                    

                }

            }
        }

        internal void BerechneSitzplanOhneBlockierungenMitSitznachbarVierVier(List<Schueler> temp)
        {
            int t = 4 - (temp.Count() % 4);

            if (temp.Count() % 4 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 4;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreVierVierSitznachbar(EineKombination) > Bewertung)
                {
                    
                        Bewertung = ScoreVierVierSitznachbar(EineKombination);
                        ErgebnisKombination = EineKombination;
                    

                }

            }
        }

        internal void BerechneSitzplanSitznachbarVierVier(List<Schueler> temp)
        {
            int t = 4 - (temp.Count() % 4);

            if (temp.Count() % 4 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 4;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreVierVierSitznachbar(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreVierVierSitznachbar(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }

                }

            }
        }

        public void BerechneTrueRandomSitzplanZweiDreiZwei(List<Schueler> temp)
        {
            int t = 7 - (temp.Count() % 7);

            if (temp.Count() % 7 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 7;
            
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 2; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

               
                        
                        ErgebnisKombination = EineKombination;
                    

            
        }

        internal void BerechneSitzplanOhneBlockierungenFuenfFuenf(List<Schueler> temp)
        {
            int t = 5 - (temp.Count() % 5);

            if (temp.Count() % 5 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 5;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 4; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreFuenfFuenf(EineKombination) > Bewertung)
                {
                    
                        Bewertung = ScoreFuenfFuenf(EineKombination);
                        ErgebnisKombination = EineKombination;
                    

                }

            }
        }

        internal void BerechneSitzplanOhneBlockierungenMitSitznachbarFuenfFuenf(List<Schueler> temp)
        {
            int t = 5 - (temp.Count() % 5);

            if (temp.Count() % 5 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 5;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 4; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreFuenfFuenfSitznachbar(EineKombination) > Bewertung)
                {
                    
                        Bewertung = ScoreFuenfFuenfSitznachbar(EineKombination);
                        ErgebnisKombination = EineKombination;
                    

                }

            }
        }

        internal void BerechneSitzplanSitznachbarFuenfFuenf(List<Schueler> temp)
        {
            int t = 5 - (temp.Count() % 5);

            if (temp.Count() % 5 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 5;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 4; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreFuenfFuenfSitznachbar(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreFuenfFuenfSitznachbar(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }

                }

            }
        }

        public void BerechneSitzplanZweiDreiZwei(List<Schueler> temp)
        {
            int t = 7 - (temp.Count() % 7);
            
            if (temp.Count() % 7 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 7;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                

                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for(int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 2; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreZweiDreiZwei(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreZweiDreiZwei(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }

                }

            }
        }

        public void BerechneSitzplanFuenfFuenf(List<Schueler> temp)
        {
            int t = 5 - (temp.Count() % 5);

            if (temp.Count() % 5 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 5;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 4; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreFuenfFuenf(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreFuenfFuenf(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }

                }

            }
        }

        public void BerechneBlockiertRandomSitzplanFuenfFuenf(List<Schueler> temp)
        {
            List<TSitzplan.Schueler> neuerAufruf = new List<Schueler>();
            neuerAufruf = temp;
            int t = 5 - (temp.Count() % 5);

            if (temp.Count() % 5 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 5;
            
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 4; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                
                    if (KombinationOK(EineKombination))
                    {
                        
                        ErgebnisKombination = EineKombination;
                    }
            else
            {
                BerechneBlockiertRandomSitzplanFuenfFuenf(neuerAufruf);
            }

                

            
        }

        public void BerechneTrueRandomSitzplanFuenfFuenf(List<Schueler> temp)
        {
            int t = 5 - (temp.Count() % 5);

            if (temp.Count() % 5 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 5;
            
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 4; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

               
                       
                        ErgebnisKombination = EineKombination;
                 

            
            
        }

        public void BerechneSitzplanZweiVierZwei(List<Schueler> temp)
        {
            int t = 8 - (temp.Count() % 8);

            if (temp.Count() % 8 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 8;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreZweiVierZwei(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreZweiVierZwei(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }

                }

            }
        }

        public void BerechneTrueRandomSitzplanVierVier(List<Schueler> temp)
        {
            int t = 4 - (temp.Count() % 4);

            if (temp.Count() % 4 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 4;
            
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

               
                        
                        ErgebnisKombination = EineKombination;

            
        }

        public void BerechneBlockiertRandomSitzplanZweiVierZwei(List<Schueler> temp)
        {
            List<TSitzplan.Schueler> neuerAufruf = new List<Schueler>();
            neuerAufruf = temp;
            int t = 8 - (temp.Count() % 8);

            if (temp.Count() % 8 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 8;
            
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

               
                    if (KombinationOK(EineKombination))
                    {
                        
                        ErgebnisKombination = EineKombination;
                    }
            else
            {
                BerechneBlockiertRandomSitzplanZweiVierZwei(neuerAufruf);
            }

                

            
        }

        public void BerechneBlockiertRandomSitzplanVierVier(List<Schueler> temp)
        {
            List<TSitzplan.Schueler> neuerAufruf = new List<Schueler>();
            neuerAufruf = temp;
            int t = 4 - (temp.Count() % 4);

            if (temp.Count() % 4 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 4;
            
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                
                    if (KombinationOK(EineKombination))
                    {
                        
                        ErgebnisKombination = EineKombination;
                    }
            else
            {
                BerechneBlockiertRandomSitzplanVierVier(neuerAufruf);
            }

                

            
        }

        public void BerechneTrueRandomSitzplanZweiVierZwei(List<Schueler> temp)
        {
            int t = 8 - (temp.Count() % 8);

            if (temp.Count() % 8 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 8;
            
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }


                for (int j = 0; j < Reihen * 2; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 1; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[j].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }
                        
                        ErgebnisKombination = EineKombination;

            
        }

        public void BerechneSitzplanVierVier(List<Schueler> temp)
        {
            int t = 4 - (temp.Count() % 4);

            if (temp.Count() % 4 != 0)
            {
                for (int i = 0; i < t; i++)
                {
                    temp.Add(new Schueler(0, null, null, null, null, null, null, new List<string>(), new List<string>()));
                }
            }
            schueler = temp;
            int Reihen = schueler.Count() / 4;
            for (int i = 0; i < Iterationen; i++)
            {
                EineKombination = new List<List<Schueler>>();
                List<Schueler> Namen = new List<Schueler>();
                foreach (Schueler schueler1 in schueler)
                {
                    Namen.Add(schueler1);
                }
                for (int j = 0; j < Reihen; j++)
                {
                    EineKombination.Add(new List<Schueler>());
                    for (int k = 0; k <= 3; k++)
                    {
                        var random = new Random();
                        int index = random.Next(Namen.Count);
                        EineKombination[EineKombination.Count - 1].Add(Namen[index]);
                        Namen.RemoveAt(index);
                    }
                }

                if (ScoreVierVier(EineKombination) > Bewertung)
                {
                    if (KombinationOK(EineKombination))
                    {
                        Bewertung = ScoreVierVier(EineKombination);
                        ErgebnisKombination = EineKombination;
                    }

                }

            }
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
        private int ScoreSitznachbar(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach (List<Schueler> paar in Kombination)
            {
                if (paar[0].Wunsch1 == paar[1].Name || paar[0].Wunsch2 == paar[1].Name || paar[0].Wunsch3 == paar[1].Name || paar[0].Wunsch4 == paar[1].Name || paar[0].Wunsch5 == paar[1].Name)
                {
                    score++;
                }
                if (paar[1].Wunsch1 == paar[0].Name || paar[1].Wunsch2 == paar[0].Name || paar[1].Wunsch3 == paar[0].Name || paar[1].Wunsch4 == paar[0].Name || paar[1].Wunsch5 == paar[0].Name)
                {
                    score++;
                }
                for(int i = 0; i < paar[0].Sitznachbar.Count(); i++)
                {
                    if(paar[0].Sitznachbar[i] == paar[1].Name)
                    {
                        score = score + Gewichtung;
                    }
                }
            }
            return score;
        }
        private int ScoreZweiDreiZwei(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach(List<Schueler> Tisch in Kombination)
            {
                if (Tisch.Count == 2)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                }
                if (Tisch.Count == 3)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[2].Name || Tisch[1].Wunsch2 == Tisch[2].Name || Tisch[1].Wunsch3 == Tisch[2].Name || Tisch[1].Wunsch4 == Tisch[2].Name || Tisch[1].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[1].Name || Tisch[2].Wunsch2 == Tisch[1].Name || Tisch[2].Wunsch3 == Tisch[1].Name || Tisch[2].Wunsch4 == Tisch[1].Name || Tisch[2].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                }
            }
            return score;
        }
        private int ScoreZweiDreiZweiSitznachbar(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach (List<Schueler> Tisch in Kombination)
            {
                if (Tisch.Count == 2)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[0].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[0].Sitznachbar[i] == Tisch[1].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                }
                if (Tisch.Count == 3)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[0].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[0].Sitznachbar[i] == Tisch[1].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                    if (Tisch[1].Wunsch1 == Tisch[2].Name || Tisch[1].Wunsch2 == Tisch[2].Name || Tisch[1].Wunsch3 == Tisch[2].Name || Tisch[1].Wunsch4 == Tisch[2].Name || Tisch[1].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[1].Name || Tisch[2].Wunsch2 == Tisch[1].Name || Tisch[2].Wunsch3 == Tisch[1].Name || Tisch[2].Wunsch4 == Tisch[1].Name || Tisch[2].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[1].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[1].Sitznachbar[i] == Tisch[2].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                }
            }
            return score;
        }
        private int ScoreZweiVierZwei(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach (List<Schueler> Tisch in Kombination)
            {
                if (Tisch.Count == 2)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                }
                if (Tisch.Count == 4)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[2].Name || Tisch[1].Wunsch2 == Tisch[2].Name || Tisch[1].Wunsch3 == Tisch[2].Name || Tisch[1].Wunsch4 == Tisch[2].Name || Tisch[1].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[1].Name || Tisch[2].Wunsch2 == Tisch[1].Name || Tisch[2].Wunsch3 == Tisch[1].Name || Tisch[2].Wunsch4 == Tisch[1].Name || Tisch[2].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[3].Name || Tisch[2].Wunsch2 == Tisch[3].Name || Tisch[2].Wunsch3 == Tisch[3].Name || Tisch[2].Wunsch4 == Tisch[3].Name || Tisch[2].Wunsch5 == Tisch[3].Name)
                    {
                        score++;
                    }
                    if (Tisch[3].Wunsch1 == Tisch[2].Name || Tisch[3].Wunsch2 == Tisch[2].Name || Tisch[3].Wunsch3 == Tisch[2].Name || Tisch[3].Wunsch4 == Tisch[2].Name || Tisch[3].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                }
            }
            return score;
        }
        private int ScoreZweiVierZweiSitznachbar(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach (List<Schueler> Tisch in Kombination)
            {
                if (Tisch.Count == 2)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[0].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[0].Sitznachbar[i] == Tisch[1].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                }
                if (Tisch.Count == 4)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[0].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[0].Sitznachbar[i] == Tisch[1].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                    if (Tisch[1].Wunsch1 == Tisch[2].Name || Tisch[1].Wunsch2 == Tisch[2].Name || Tisch[1].Wunsch3 == Tisch[2].Name || Tisch[1].Wunsch4 == Tisch[2].Name || Tisch[1].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[1].Name || Tisch[2].Wunsch2 == Tisch[1].Name || Tisch[2].Wunsch3 == Tisch[1].Name || Tisch[2].Wunsch4 == Tisch[1].Name || Tisch[2].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[1].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[1].Sitznachbar[i] == Tisch[2].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                    if (Tisch[2].Wunsch1 == Tisch[3].Name || Tisch[2].Wunsch2 == Tisch[3].Name || Tisch[2].Wunsch3 == Tisch[3].Name || Tisch[2].Wunsch4 == Tisch[3].Name || Tisch[2].Wunsch5 == Tisch[3].Name)
                    {
                        score++;
                    }
                    if (Tisch[3].Wunsch1 == Tisch[2].Name || Tisch[3].Wunsch2 == Tisch[2].Name || Tisch[3].Wunsch3 == Tisch[2].Name || Tisch[3].Wunsch4 == Tisch[2].Name || Tisch[3].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[2].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[2].Sitznachbar[i] == Tisch[3].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                }
            }
            return score;
        }
        private int ScoreVierVier(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach (List<Schueler> Tisch in Kombination)
            {
                if (Tisch.Count == 4)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[2].Name || Tisch[1].Wunsch2 == Tisch[2].Name || Tisch[1].Wunsch3 == Tisch[2].Name || Tisch[1].Wunsch4 == Tisch[2].Name || Tisch[1].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[1].Name || Tisch[2].Wunsch2 == Tisch[1].Name || Tisch[2].Wunsch3 == Tisch[1].Name || Tisch[2].Wunsch4 == Tisch[1].Name || Tisch[2].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[3].Name || Tisch[2].Wunsch2 == Tisch[3].Name || Tisch[2].Wunsch3 == Tisch[3].Name || Tisch[2].Wunsch4 == Tisch[3].Name || Tisch[2].Wunsch5 == Tisch[3].Name)
                    {
                        score++;
                    }
                    if (Tisch[3].Wunsch1 == Tisch[2].Name || Tisch[3].Wunsch2 == Tisch[2].Name || Tisch[3].Wunsch3 == Tisch[2].Name || Tisch[3].Wunsch4 == Tisch[2].Name || Tisch[3].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                }
            }
            return score;
        }
        private int ScoreVierVierSitznachbar(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach (List<Schueler> Tisch in Kombination)
            {
                if (Tisch.Count == 4)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[0].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[0].Sitznachbar[i] == Tisch[1].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                    if (Tisch[1].Wunsch1 == Tisch[2].Name || Tisch[1].Wunsch2 == Tisch[2].Name || Tisch[1].Wunsch3 == Tisch[2].Name || Tisch[1].Wunsch4 == Tisch[2].Name || Tisch[1].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[1].Name || Tisch[2].Wunsch2 == Tisch[1].Name || Tisch[2].Wunsch3 == Tisch[1].Name || Tisch[2].Wunsch4 == Tisch[1].Name || Tisch[2].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[1].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[1].Sitznachbar[i] == Tisch[2].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                    if (Tisch[2].Wunsch1 == Tisch[3].Name || Tisch[2].Wunsch2 == Tisch[3].Name || Tisch[2].Wunsch3 == Tisch[3].Name || Tisch[2].Wunsch4 == Tisch[3].Name || Tisch[2].Wunsch5 == Tisch[3].Name)
                    {
                        score++;
                    }
                    if (Tisch[3].Wunsch1 == Tisch[2].Name || Tisch[3].Wunsch2 == Tisch[2].Name || Tisch[3].Wunsch3 == Tisch[2].Name || Tisch[3].Wunsch4 == Tisch[2].Name || Tisch[3].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[2].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[2].Sitznachbar[i] == Tisch[3].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                }
            }
            return score;
        }
        private int ScoreFuenfFuenf(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach (List<Schueler> Tisch in Kombination)
            {
                if (Tisch.Count == 5)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[2].Name || Tisch[1].Wunsch2 == Tisch[2].Name || Tisch[1].Wunsch3 == Tisch[2].Name || Tisch[1].Wunsch4 == Tisch[2].Name || Tisch[1].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[1].Name || Tisch[2].Wunsch2 == Tisch[1].Name || Tisch[2].Wunsch3 == Tisch[1].Name || Tisch[2].Wunsch4 == Tisch[1].Name || Tisch[2].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[3].Name || Tisch[2].Wunsch2 == Tisch[3].Name || Tisch[2].Wunsch3 == Tisch[3].Name || Tisch[2].Wunsch4 == Tisch[3].Name || Tisch[2].Wunsch5 == Tisch[3].Name)
                    {
                        score++;
                    }
                    if (Tisch[3].Wunsch1 == Tisch[2].Name || Tisch[3].Wunsch2 == Tisch[2].Name || Tisch[3].Wunsch3 == Tisch[2].Name || Tisch[3].Wunsch4 == Tisch[2].Name || Tisch[3].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    if (Tisch[3].Wunsch1 == Tisch[4].Name || Tisch[3].Wunsch2 == Tisch[4].Name || Tisch[3].Wunsch3 == Tisch[4].Name || Tisch[3].Wunsch4 == Tisch[4].Name || Tisch[3].Wunsch5 == Tisch[4].Name)
                    {
                        score++;
                    }
                    if (Tisch[4].Wunsch1 == Tisch[3].Name || Tisch[4].Wunsch2 == Tisch[3].Name || Tisch[4].Wunsch3 == Tisch[3].Name || Tisch[4].Wunsch4 == Tisch[3].Name || Tisch[4].Wunsch5 == Tisch[3].Name)
                    {
                        score++;
                    }
                }
            }
            return score;
        }
        private int ScoreFuenfFuenfSitznachbar(List<List<Schueler>> Kombination)
        {
            int score = 0;
            foreach (List<Schueler> Tisch in Kombination)
            {
                if (Tisch.Count == 5)
                {
                    if (Tisch[0].Wunsch1 == Tisch[1].Name || Tisch[0].Wunsch2 == Tisch[1].Name || Tisch[0].Wunsch3 == Tisch[1].Name || Tisch[0].Wunsch4 == Tisch[1].Name || Tisch[0].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    if (Tisch[1].Wunsch1 == Tisch[0].Name || Tisch[1].Wunsch2 == Tisch[0].Name || Tisch[1].Wunsch3 == Tisch[0].Name || Tisch[1].Wunsch4 == Tisch[0].Name || Tisch[1].Wunsch5 == Tisch[0].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[0].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[0].Sitznachbar[i] == Tisch[1].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                    if (Tisch[1].Wunsch1 == Tisch[2].Name || Tisch[1].Wunsch2 == Tisch[2].Name || Tisch[1].Wunsch3 == Tisch[2].Name || Tisch[1].Wunsch4 == Tisch[2].Name || Tisch[1].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    if (Tisch[2].Wunsch1 == Tisch[1].Name || Tisch[2].Wunsch2 == Tisch[1].Name || Tisch[2].Wunsch3 == Tisch[1].Name || Tisch[2].Wunsch4 == Tisch[1].Name || Tisch[2].Wunsch5 == Tisch[1].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[1].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[1].Sitznachbar[i] == Tisch[2].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                    if (Tisch[2].Wunsch1 == Tisch[3].Name || Tisch[2].Wunsch2 == Tisch[3].Name || Tisch[2].Wunsch3 == Tisch[3].Name || Tisch[2].Wunsch4 == Tisch[3].Name || Tisch[2].Wunsch5 == Tisch[3].Name)
                    {
                        score++;
                    }
                    if (Tisch[3].Wunsch1 == Tisch[2].Name || Tisch[3].Wunsch2 == Tisch[2].Name || Tisch[3].Wunsch3 == Tisch[2].Name || Tisch[3].Wunsch4 == Tisch[2].Name || Tisch[3].Wunsch5 == Tisch[2].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[2].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[2].Sitznachbar[i] == Tisch[3].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                    if (Tisch[3].Wunsch1 == Tisch[4].Name || Tisch[3].Wunsch2 == Tisch[4].Name || Tisch[3].Wunsch3 == Tisch[4].Name || Tisch[3].Wunsch4 == Tisch[4].Name || Tisch[3].Wunsch5 == Tisch[4].Name)
                    {
                        score++;
                    }
                    if (Tisch[4].Wunsch1 == Tisch[3].Name || Tisch[4].Wunsch2 == Tisch[3].Name || Tisch[4].Wunsch3 == Tisch[3].Name || Tisch[4].Wunsch4 == Tisch[3].Name || Tisch[4].Wunsch5 == Tisch[3].Name)
                    {
                        score++;
                    }
                    for (int i = 0; i < Tisch[3].Sitznachbar.Count(); i++)
                    {
                        if (Tisch[3].Sitznachbar[i] == Tisch[4].Name)
                        {
                            score = score + Gewichtung;
                        }
                    }
                }
            }
            return score;
        }
        private void sichereDenScheiß(List<List<Schueler>> sicher)
        {
            alleKombinationen.Add(sicher);
            
        }
        private bool KombinationOK(List<List<Schueler>> Kombination)
        {
            
            foreach(List<Schueler> Tisch in Kombination)
            {
                if (Tisch.Count == 2)
                {
                    foreach (string b in Tisch[0].Blockiert)
                    {
                        if (b.Equals(Tisch[1].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[1].Blockiert)
                    {
                        if (b.Equals(Tisch[0].Name))
                        {
                            return false;
                        }
                    }
                }
                if (Tisch.Count == 3)
                {
                    foreach (string b in Tisch[0].Blockiert)
                    {
                        if (b.Equals(Tisch[1].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[1].Blockiert)
                    {
                        if (b.Equals(Tisch[0].Name))
                        {
                            return false;
                        }
                        if (b.Equals(Tisch[2].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[2].Blockiert)
                    {
                        if (b.Equals(Tisch[1].Name))
                        {
                            return false;
                        }
                    }
                }
                if (Tisch.Count == 4)
                {
                    foreach (string b in Tisch[0].Blockiert)
                    {
                        if (b.Equals(Tisch[1].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[1].Blockiert)
                    {
                        if (b.Equals(Tisch[0].Name))
                        {
                            return false;
                        }
                        if (b.Equals(Tisch[2].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[2].Blockiert)
                    {
                        if (b.Equals(Tisch[1].Name))
                        {
                            return false;
                        }
                        if (b.Equals(Tisch[3].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[3].Blockiert)
                    {
                        if (b.Equals(Tisch[2].Name))
                        {
                            return false;
                        }
                    }
                }
                if (Tisch.Count == 5)
                {
                    foreach (string b in Tisch[0].Blockiert)
                    {
                        if (b.Equals(Tisch[1].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[1].Blockiert)
                    {
                        if (b.Equals(Tisch[0].Name))
                        {
                            return false;
                        }
                        if (b.Equals(Tisch[2].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[2].Blockiert)
                    {
                        if (b.Equals(Tisch[1].Name))
                        {
                            return false;
                        }
                        if (b.Equals(Tisch[3].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[3].Blockiert)
                    {
                        if (b.Equals(Tisch[2].Name))
                        {
                            return false;
                        }
                        if (b.Equals(Tisch[4].Name))
                        {
                            return false;
                        }
                    }
                    foreach (string b in Tisch[4].Blockiert)
                    {
                        if (b.Equals(Tisch[3].Name))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }   
    }
    
}
