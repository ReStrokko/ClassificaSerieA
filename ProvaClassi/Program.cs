using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProvaClassi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lSquadre = new string[] { "Sampdoria","Spezia", "Venezia", "Empoli","Udinese","Torino","Sassuolo","Verona","Atalanta","Fiorentina","Lazio","Roma", "Genoa", "Salernitana", "Cagliari","Juventus","Napoli","Inter","Milan", "Bologna" };
            Torneo SerieA=new Torneo(lSquadre);
        }
    }


    class Torneo {

        Squadra[] partecipanti = new Squadra[20];
        int[][][,] Giornate = new int[34][][,];              //---> first array is for the day in wich the game was played, Second is for the match. 
                                                             //---> The matrix has in the first row there's the team's pointer while on the second the row there's the score of each team
        string[] lSquadre;

        public Torneo(string[] lSquadre)
        {
            this.lSquadre = lSquadre;
            partecipanti = new Squadra[20];
            for (int i = 0; i < partecipanti.Length; i++)
            {
                partecipanti[i] = new Squadra(lSquadre[i]);
            }
            InitializeDb();
            FetchMatches();
            Console.WriteLine("The Match database has been succesfully loaded");
            Console.WriteLine("Showing the Leaderboard");
            AssingPoints();
            Classifica();
            ToJsonFile();
        }


        private void FetchMatches()
        {
            int giornata=33;
            int partita = 0;
            bool teamOrResult = true;          //true = teamname, false=match score
            bool team = true;
            string path = @"C:\Users\MicM\Desktop\SerieA.txt";
            string readText = File.ReadAllText(path);  // Read the contents of the file
            foreach (var line in readText.Split('\n'))  //read the line 
            {
                //Console.WriteLine(line);
                if (line.Contains("GIORNATA"))          //This goes trough the days meaning that there is a new set of matches
                {
                    //Console.WriteLine(giornata+"-"+partita);
                    giornata = Convert.ToInt32(line.Split(' ')[1])-1;
                    partita = 0;
                }
                else                                        //being here means that there are matches for the day
                {
                    foreach (string obj in line.Split(' '))     //match
                    {
                        foreach (string TPorSc in obj.Split('-'))       //Team names or Scores
                        {
                            if (teamOrResult)
                            {
                                if (team)
                                {
                                    Giornate[giornata][partita][0, 0] = StringToTeamId(TPorSc);
                                    StringToTeamId(TPorSc);
                                }
                                else
                                {
                                    Giornate[giornata][partita][0, 1] = StringToTeamId(TPorSc);
                                    StringToTeamId(TPorSc);
                                }
                            }
                            else
                            {
                                if (team)
                                {
                                    Giornate[giornata][partita][1, 0] = Convert.ToInt32(TPorSc);
                                }
                                else
                                {
                                    Giornate[giornata][partita][1, 1] = Convert.ToInt32(TPorSc);
                                }
                            }
                            team = !team;
                        }
                        teamOrResult = !teamOrResult;
                    }
                    partita++;
                }
            }
        }

        private void InitializeDb()
        {
            for (int i = 0; i < 34; i++)
            {
                Giornate[i] = new int[10][,];
                for (int j = 0; j < 10; j++)
                {
                    Giornate[i][j] = new int[2, 2];
                }
            }
        }

        private int StringToTeamId(string team)
        {
            int sol;

            sol = Array.IndexOf(lSquadre, team);
            return sol;
        }
        
        private void AssingPoints()
        {
            for (int i = 0; i < 34; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if (!(Giornate[i][j][0, 0] == 0 && Giornate[i][j][1, 0] == 0 && Giornate[i][j][0, 1] == 0 && Giornate[i][j][1, 1] == 0)) {
                        if (Giornate[i][j][1, 0] > Giornate[i][j][1, 1]) { partecipanti[Giornate[i][j][0, 0]].punti += 3; partecipanti[Giornate[i][j][0, 0]].VSP[0]++; partecipanti[Giornate[i][j][0, 1]].VSP[1]++; }
                        if (Giornate[i][j][1, 0] == Giornate[i][j][1, 1]) { partecipanti[Giornate[i][j][0, 0]].punti++; partecipanti[Giornate[i][j][0, 1]].punti++; partecipanti[Giornate[i][j][0, 0]].VSP[2]++; partecipanti[Giornate[i][j][0, 1]].VSP[2]++; }
                        if (Giornate[i][j][1, 1] > Giornate[i][j][1, 0]) { partecipanti[Giornate[i][j][0, 1]].punti += 3; partecipanti[Giornate[i][j][0, 1]].VSP[0]++; partecipanti[Giornate[i][j][0, 0]].VSP[1]++; }
                    }
                }
            }
        }

        private void Classifica()
        {
            int[,] classifica = new int[20, 2];
            for (int i = 0; i < classifica.GetLength(0); i++)   //I copy id and points into a new matrix.
            {
                classifica[i, 0] = i;
                classifica[i, 1] = partecipanti[i].punti;
            }
            int tempid,tempval;
            for(int i = 0; i < classifica.GetLength(0); i++)    //and sort it using a simple bubble sort not the fastest but the easiest.
            {
                for (int j = 0; j < classifica.GetLength(0)-1; j++)
                {
                    if(classifica[j, 1] < classifica[j+1,1 ]){
                        tempid = classifica[j,0] ;
                        tempval = classifica[j,1] ;
                        classifica[j,0]=classifica[j+1,0] ;
                        classifica[j,1]=classifica[j+1,1] ;
                        classifica[j+1,0]=tempid ;
                        classifica[j+1,1]=tempval ;
                    }
                }
            }
            for(int j = 0; j < classifica.GetLength(0); j++)
            {
                Console.WriteLine("{0} - {1} - {2} - {3} - {4}",classifica[j,1],partecipanti[classifica[j,0]].Name, partecipanti[classifica[j, 0]].VSP[0], partecipanti[classifica[j, 0]].VSP[1] , partecipanti[classifica[j, 0]].VSP[2]);
            }
        }

        private void ToJsonFile()
        {
            string path = @"C:\Users\MicM\Desktop\DB.json";
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine('{');
                for (int i = 0; i < 34; i++)
                {
                    sw.WriteLine("  "+'"' + "Giornata" + '"' + ':' + '"' + i + '"' + '{');
                    for (int j = 0; j < 10; j++)
                    {
                        sw.WriteLine("      " + '"' + "Match" + '"' + ':' + '"' + j + '"' + '{');
                        sw.WriteLine("          " + '"' + Giornate[i][j][0, 0] + '"' + ':' + '"' + Giornate[i][j][1, 0] + '"' + ',');
                        sw.WriteLine("          " + '"' + Giornate[i][j][0, 1] + '"' + ':' + '"' + Giornate[i][j][1, 1] + '"' + ',');
                        sw.WriteLine("      " + '}');

                    }
                }
                sw.WriteLine("  }");
                sw.WriteLine("}");
            }
        }
    }

    class Squadra
    {
        private string name;
        public string Name { get { return name; } }
        public int punti;
        public int[] VSP;
        public Squadra(string nome)
        {
            name = nome;
            punti = 0;
            VSP = new int[3];
        }
    }
}
