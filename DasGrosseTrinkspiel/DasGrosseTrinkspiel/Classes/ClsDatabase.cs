using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DasGrosseTrinkspiel.Classes
{
    internal class ClsDatabase
    {
        static SQLiteAsyncConnection Spielerdb;
        static SQLiteAsyncConnection Fragendb;
        static async Task Init()
        {
            if (Spielerdb != null)
                return;

            //Get Database Path
            var SpielerdatabasePath = Path.Combine(FileSystem.AppDataDirectory, "SpielerDatabase.db");
            var FragendatabasePath = Path.Combine(FileSystem.AppDataDirectory, "FragenDatabase.db");

            //Create Connections
            Spielerdb = new SQLiteAsyncConnection(SpielerdatabasePath);
            Fragendb = new SQLiteAsyncConnection(FragendatabasePath);

            //Create Tables
            await Spielerdb.CreateTableAsync<Spieler>();
            await Spielerdb.CreateTableAsync<Spielerliste>();
            //Fragen Tables sind noch nicht erstellt (weil es gibt noch keine Klassen dafür)
        }

        public static async Task AddSpieler(string name, string Geschlecht, int Listennummer)
        {
            await Init();

            Spieler spieler = new Spieler()
            {
                Name = name,
                Geschlecht = Geschlecht,
                Listennummer = Listennummer
            };

            int id = await Spielerdb.InsertAsync(spieler);
        } 

        public static async Task DeleteSpieler(int id)
        {
            await Init();

            await Spielerdb.DeleteAsync<Spieler>(id);
        }

        public static async Task<List<Spieler>> GetSpieler(int Listid)
        {
            await Init();

            var query = Spielerdb.Table<Spieler>().Where(x => x.Listennummer.Equals(Listid));

            //die Listennummer /anzahl muss beim erstellen der Spieler ausgelesen werden um zu wissen welche Listennummer zugewiesen werden muss
            var Spielerreturn = await query.ToListAsync();
            Debug.WriteLine(Spielerreturn[0]);
            return Spielerreturn;
        }


        public static async Task AddSpielerliste(string name)
        {
            await Init();

            Spielerliste spielerliste = new Spielerliste()
            {
                Name = name
            };

            await Spielerdb.InsertAsync(spielerliste);
        }

        public static async Task DeleteSpielerliste(int id)
        {
            await Init();

            await Spielerdb.DeleteAsync<Spielerliste>(id);
        }

        public static async Task<Spielerliste> GetSpielerliste(int Listid)
        {
            await Init();

            var Spielerliste = await Spielerdb.GetAsync<Spielerliste>(Listid);
            return Spielerliste;
        }
    }
}
