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
            string[] lSquadre = new string[] { "Venezia", "Genoa", "Salernitana","Cagliari","Sampdoria","Spezia","Empoli","Bologna","Udinese","Torino","Sassuolo","Verona","Atalanta","Fiorentina","Lazio","Roma","Juventus","Napoli","Inter","Milan"};
            Torneo SerieA=new Torneo(lSquadre);
        }
    }


    class Torneo {

        Squadra[] partecipanti =new Squadra[20];
        int[][,] Giornate= new int[34][,];

        
        public Torneo(string[] lSquadre)
        {
            partecipanti = new Squadra[20];
            for (int i= 0; i < partecipanti.Length; i++)
            {
                partecipanti[i] = new Squadra(lSquadre[i]);
                partecipanti[i].prova();
            }
            FetchMatches();

        }

        private void FetchMatches()
        {
            string path = @"C:\Users\MicM\Desktop\SerieA.txt";
            string readText = File.ReadAllText(path);  // Read the contents of the file
            foreach (var line in readText.Split('\n'))
            {
                Console.WriteLine(line);
            }
        }
    }

    class Squadra
    {
        private string name;
        public string Name { get { return name; } }
       
        public Squadra(string nome)
        {
            name = nome;
        }

        public void prova()
        {
            Console.WriteLine("Squadra: {0}",name);
        }
    }
}
