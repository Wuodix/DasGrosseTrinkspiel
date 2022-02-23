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
        static SQLiteAsyncConnection m_spielerdb;
        static SQLiteAsyncConnection m_fragendb;
        static public int m_listprimarykey { get; set; }
        private static async Task Init()
        {
            if (m_spielerdb != null)
                return;

            //Get Database Path
            var SpielerdatabasePath = Path.Combine(FileSystem.AppDataDirectory, "SpielerDatabase.db");
            var FragendatabasePath = Path.Combine(FileSystem.AppDataDirectory, "FragenDatabase.db");

            //Create Connections
            m_spielerdb = new SQLiteAsyncConnection(SpielerdatabasePath);
            m_fragendb = new SQLiteAsyncConnection(FragendatabasePath);

            //Create Tables
            await m_spielerdb.CreateTableAsync<Spieler>();
            await m_spielerdb.CreateTableAsync<Spielerliste>();
            //Fragen Tables sind noch nicht erstellt (weil es gibt noch keine Klassen dafür)

            try
            {
                m_listprimarykey = (await GetAllSpielerlisten()).LastOrDefault().Id+1;
            }
            catch
            {
                m_listprimarykey = 1;
            }
        }

        public static async Task<int> AddSpieler(string name, string Geschlecht)
        {
            await Init();

            Spieler spieler = new Spieler()
            {
                Name = name,
                Geschlecht = Geschlecht,
                Listennummer = m_listprimarykey
            };

            Debug.WriteLine("SpielerListennummer: " + spieler.Listennummer);

            int id = await m_spielerdb.InsertAsync(spieler);
            return id;
        } 

        public static async Task Getlistprimarykey()
        {
            try
            {
                m_listprimarykey = (await GetAllSpielerlisten()).LastOrDefault().Id;
            }
            catch
            {
                m_listprimarykey = 1;
            }
        }

        public static async Task DeleteSpieler(int id)
        {
            await Init();

            await m_spielerdb.DeleteAsync<Spieler>(id);
        }

        public static async Task DeleteSpielervonListe(int ListenId)
        {
            await Init();

            var query = m_spielerdb.Table<Spieler>().Where(x => x.Listennummer.Equals(ListenId));

            await query.DeleteAsync();
        }

        public static async Task<List<Spieler>> GetSpieler(int Listid)
        {
            await Init();

            var query = m_spielerdb.Table<Spieler>().Where(x => x.Listennummer.Equals(Listid));

            var Spielerreturn = await query.ToListAsync();

            return Spielerreturn;
        }

        public static async Task<List<Spieler>> GetAllSpieler()
        {
            await Init();

            var Spielers = await m_spielerdb.Table<Spieler>().ToListAsync();

            return Spielers;
        }

        public static async Task AddSpielerliste(string name)
        {
            await Init();

            Spielerliste spielerliste = new Spielerliste()
            {
                Name = name
            };

            await m_spielerdb.InsertAsync(spielerliste);
            m_listprimarykey = spielerliste.Id;

            Debug.WriteLine("SpielerlistenId: " + spielerliste.Id);
        }

        public static async Task DeleteSpielerliste(int id)
        {
            await Init();

            await m_spielerdb.DeleteAsync<Spielerliste>(id);
        }

        public static async Task DeleteEverything()
        {
            await Init();

            Debug.WriteLine("Deleted from Spieler: " + await m_spielerdb.DropTableAsync<Spieler>());
            Debug.WriteLine("Deleted from Listen: " + await m_spielerdb.DropTableAsync<Spielerliste>());

            await m_spielerdb.CreateTableAsync<Spieler>();
            await m_spielerdb.CreateTableAsync<Spielerliste>();

            m_listprimarykey = 1;
        }

        public static async Task<List<Spielerliste>> GetAllSpielerlisten()
        {
            await Init();

            var Spielerliste = await m_spielerdb.Table<Spielerliste>().ToListAsync();

            return Spielerliste;
        }
    }
}
