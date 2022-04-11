using SQLite;
using DasGrosseTrinkspiel.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DasGrosseTrinkspiel.Extentions
{
    internal class DataProvider
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
            await m_spielerdb.CreateTableAsync<ClsSpieler>();
            await m_spielerdb.CreateTableAsync<ClsSpielerliste>();
            await m_fragendb.CreateTableAsync<ClsFrage>();
            await m_fragendb.CreateTableAsync<ClsKategorie>();

            try
            {
                m_listprimarykey = (await GetAllSpielerlisten()).LastOrDefault().Id+1;
            }
            catch
            {
                m_listprimarykey = 1;
            }
        }
        public static async Task AddSpieler(string name, string Geschlecht)
        {
            await Init();

            ClsSpieler spieler = new ClsSpieler()
            {
                Name = name,
                Geschlecht = Geschlecht,
                Listennummer = m_listprimarykey
            };

            Debug.WriteLine("SpielerListennummer: " + spieler.Listennummer);

            await m_spielerdb.InsertAsync(spieler);
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

            await m_spielerdb.DeleteAsync<ClsSpieler>(id);
        }
        public static async Task DeleteSpielervonListe(int ListenId)
        {
            await Init();

            var query = m_spielerdb.Table<ClsSpieler>().Where(x => x.Listennummer.Equals(ListenId));

            await query.DeleteAsync();
        }
        public static async Task<List<ClsSpieler>> GetSpieler(int Listid)
        {
            await Init();

            return await m_spielerdb.Table<ClsSpieler>().Where(x => x.Listennummer==Listid).ToListAsync();
        }
        public static async Task<List<ClsSpieler>> GetAllSpieler()
        {
            await Init();

            var Spielers = await m_spielerdb.Table<ClsSpieler>().ToListAsync();

            return Spielers;
        }
        public static async Task AddSpielerliste(string name)
        {
            await Init();

            ClsSpielerliste spielerliste = new ClsSpielerliste()
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

            await m_spielerdb.DeleteAsync<ClsSpielerliste>(id);
        }
        public static async Task DeleteEverything()
        {
            await Init();

            await m_spielerdb.DropTableAsync<ClsSpieler>();
            await m_spielerdb.DropTableAsync<ClsSpielerliste>();
            await m_fragendb.DropTableAsync<ClsFrage>();
            await m_fragendb.DropTableAsync<ClsKategorie>();

            await m_spielerdb.CreateTableAsync<ClsSpieler>();
            await m_spielerdb.CreateTableAsync<ClsSpielerliste>();
            await m_fragendb.CreateTableAsync<ClsFrage>();
            await m_fragendb.CreateTableAsync<ClsKategorie>();

            m_listprimarykey = 1;
        }
        public static async Task<List<ClsSpielerliste>> GetAllSpielerlisten()
        {
            await Init();

            var Spielerliste = await m_spielerdb.Table<ClsSpielerliste>().ToListAsync();

            return Spielerliste;
        }
        public static async Task AddFrage(string text, int Kategorie)
        {
            await Init();

            ClsFrage frage = new ClsFrage()
            {
                Text = text,
                Kategorie = Kategorie
            };

            await m_fragendb.InsertAsync(frage);
        }
        public static async Task DeleteFrage(int id)
        {
            await Init();

            await m_fragendb.DeleteAsync<ClsFrage>(id);
        }
        public static async Task DeleteAlleFragen()
        {
            await Init();
            
            await m_fragendb.DropTableAsync<ClsFrage>();

            await m_fragendb.CreateTableAsync<ClsFrage>();
        }
        public static async Task<List<ClsFrage>> GetFrage(int Kategorieid)
        {
            await Init();

            return await m_fragendb.Table<ClsFrage>().Where(x => x.Kategorie==Kategorieid).ToListAsync();
        }
        public static async Task AddKategorie(string name)
        {
            await Init();

            ClsKategorie kategorie = new ClsKategorie()
            {
                Name = name
            };

            await m_fragendb.InsertAsync(kategorie);
        }
        public static async Task DeleteKategorie(int id)
        {
            await Init();

            await m_fragendb.DeleteAsync<ClsKategorie>(id);
        }
        public static async Task DeleteAlleKategorien()
        {
            await Init();

            await m_fragendb.DropTableAsync<ClsKategorie>();

            await m_fragendb.CreateTableAsync<ClsKategorie>();
        }
        public static async Task<List<ClsKategorie>> GetAllKategorien()
        {
            await Init();

            var Spielerliste = await m_fragendb.Table<ClsKategorie>().ToListAsync();

            return Spielerliste;
        }
    }
}
