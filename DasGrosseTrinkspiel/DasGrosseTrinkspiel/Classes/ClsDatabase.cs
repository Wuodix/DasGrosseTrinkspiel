using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DasGrosseTrinkspiel.Classes
{
    internal class ClsDatabase
    {
        static SQLiteAsyncConnection Spielerdb;
        static SQLiteAsyncConnection Fragendb;
        static public int Listprimarykey { get; set; }
        public static async Task Init()
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

            try
            {
                Listprimarykey = (await GetAllSpielerlisten()).LastOrDefault().Id+1;
            }
            catch
            {
                Listprimarykey = 1;
            }
        }

        public static async Task<int> AddSpieler(string name, string Geschlecht)
        {
            await Init();

            Spieler spieler = new Spieler()
            {
                Name = name,
                Geschlecht = Geschlecht,
                Listennummer = Listprimarykey
            };

            Debug.WriteLine("SpielerListennummer: " + spieler.Listennummer);

            int id = await Spielerdb.InsertAsync(spieler);
            return id;
        } 

        private static async Task GetListPrimaryKey()
        {
            try
            {
                Listprimarykey = (await GetAllSpielerlisten()).LastOrDefault().Id;
            }
            catch
            {
                Listprimarykey = 1;
            }
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

            var Spielerreturn = await query.ToListAsync();

            return Spielerreturn;
        }

        public static async Task<List<Spieler>> GetAllSpieler()
        {
            await Init();

            var Spielers = await Spielerdb.Table<Spieler>().ToListAsync();

            return Spielers;
        }

        public static async Task AddSpielerliste(string name)
        {
            await Init();

            Spielerliste spielerliste = new Spielerliste()
            {
                Name = name
            };

            await Spielerdb.InsertAsync(spielerliste);
            Listprimarykey = spielerliste.Id;

            Debug.WriteLine("SpielerlistenId: " + spielerliste.Id);
        }

        public static async Task DeleteSpielerliste(int id)
        {
            await Init();

            await Spielerdb.DeleteAsync<Spielerliste>(id);
        }

        public static async Task DeleteEverything()
        {
            await Init();

            Debug.WriteLine("Deleted from Spieler: " + await Spielerdb.DropTableAsync<Spieler>());
            Debug.WriteLine("Deleted from Listen: " + await Spielerdb.DropTableAsync<Spielerliste>());

            await Spielerdb.CreateTableAsync<Spieler>();
            await Spielerdb.CreateTableAsync<Spielerliste>();

            Listprimarykey = 1;
        }

        public static async Task<List<Spielerliste>> GetAllSpielerlisten()
        {
            await Init();

            var Spielerliste = await Spielerdb.Table<Spielerliste>().ToListAsync();

            return Spielerliste;
        }
    }
}
