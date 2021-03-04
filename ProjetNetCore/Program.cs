using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace ProjetNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            bool afficherMenu = true;
            while (afficherMenu)
            {
                afficherMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            string apiKey = "5202e95cef3bdc147fc5de1db342136b";
            
            Console.Clear();
            Console.WriteLine("choisissez votre fonction");
            Console.WriteLine("1) Recherche par nom du media");
            Console.WriteLine("2) consulter details par ID");
            Console.WriteLine("3) exit");
            Console.Write("\r\nSelectionnez une fonction par sont chiffre: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("entrez un nom: ");
                    string nom = Console.ReadLine();
                    SearchByName(nom,apiKey);
                    return true;
                case "2":
                    try
                    {
                        Console.WriteLine("entrez un ID: ");
                        string id = Console.ReadLine();
                        AllMovieDetailsById(int.Parse(id), apiKey);
                        AllSeriesDetailsById(int.Parse(id), apiKey);
                    }
                    catch(FormatException e)
                    {
                        Console.WriteLine("Le format n'est pas respecté, veuillez ressayer");
                    }
                    return true;
                case "3":
                    //exit
                    return false;
                default:
                    return true;
            }
        }

        private static void SearchByName(string search, string apiKey)
        {
            //version library
            //TMDbClient client = new TMDbClient("5202e95cef3bdc147fc5de1db342136b");
            //SearchContainer<SearchMovie> results = client.SearchMovieAsync(search).Result;
            //int max = Math.Min(15, results.Results.Count);
            //for (int i = 0; i < max; i++)
            //{
            //    var fin = results.Results[i];
            //    Console.WriteLine(fin.Id);
            //    Console.WriteLine(fin.Title);
            //    Console.WriteLine(fin.MediaType + "\n");
            //}

            string strurl = String.Format("https://api.themoviedb.org/3/search/multi?api_key=" + apiKey + "&query=" + search);

            using (var webClient = new WebClient())
            {
                String rawJson = webClient.DownloadString(strurl);

                Search search1 = JsonConvert.DeserializeObject<Search>(rawJson);

                try
                {
                    for (int i = 0; i < 15; i++)
                    {
                     Console.WriteLine(search1.results[i]);
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Aucun resultat trouvé");
                }
            }
            Console.WriteLine("type enter to return to menu");
            Console.ReadLine();
        }

        private static void AllMovieDetailsById(int id, string apiKey)
        {
            //version library
            //TMDbClient client = new TMDbClient("5202e95cef3bdc147fc5de1db342136b");
            //Movie movie = client.GetMovieAsync(id).Result;


            string strurl = String.Format("https://api.themoviedb.org/3/movie/"+ id +"?api_key=" + apiKey );

            using (var webClient = new WebClient())
            {
                try
                {
                    String rawJson = webClient.DownloadString(strurl);

                    SearchById search1 = JsonConvert.DeserializeObject<SearchById>(rawJson);


                    if (search1 != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Id: {search1.Id}", Console.ForegroundColor);
                        Console.WriteLine($"Title: {search1.Title}", Console.ForegroundColor);
                        Console.WriteLine($"Movie", Console.ForegroundColor);
                        Console.WriteLine($"Description: {search1.Overview}", Console.ForegroundColor);
                        Console.WriteLine($"Vote average: {search1.Vote_average}", Console.ForegroundColor);
                        Console.WriteLine($"Vote count: {search1.Vote_count}", Console.ForegroundColor);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                catch (WebException e)
                {
                    Console.WriteLine("No movie exist for this Id");
                }
                
            }
            
        }

        private static void AllSeriesDetailsById(int id,string apiKey)
        {
            //version library
            //TMDbClient client = new TMDbClient("5202e95cef3bdc147fc5de1db342136b");
            //TvShow tvShow = client.GetTvShowAsync(id).Result;

            string strurl = String.Format("https://api.themoviedb.org/3/tv/" + id + "?api_key=" + apiKey);

            using (var webClient = new WebClient())
            {
                try
                {
                    String rawJson = webClient.DownloadString(strurl);

                    SearchById search1 = JsonConvert.DeserializeObject<SearchById>(rawJson);


                    if (search1 != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Id: {search1.Id}", Console.ForegroundColor);
                        Console.WriteLine($"Title: {search1.Name}", Console.ForegroundColor);
                        Console.WriteLine($"TvShow", Console.ForegroundColor);
                        Console.WriteLine($"Description: {search1.Overview}", Console.ForegroundColor);
                        Console.WriteLine($"Vote average: {search1.Vote_average}", Console.ForegroundColor);
                        Console.WriteLine($"Vote count: {search1.Vote_count}", Console.ForegroundColor);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                catch (WebException e)
                {
                    Console.WriteLine("No tv show exist for this Id");
                }
            }

            Console.WriteLine("type enter to return to menu");
            Console.ReadLine();

        }

    }
}
