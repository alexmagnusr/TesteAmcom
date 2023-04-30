using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using static Program;

public class Program
{
    private const string baseUrl = "https://jsonmock.hackerrank.com/api/football_matches";
    public class DataObject
    {
        public int pag { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<TeamData> data { get; set; }

    }
    public class TeamData
    {
        public string competition { get; set; }
        public int year { get; set; }
        public string round { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public int team1goals { get; set; }
        public int team2goals { get; set; }

    }
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;


        Console.WriteLine("Team " + teamName + " scored " + GetScoredGoals(teamName, year) + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;

        Console.WriteLine("Team " + teamName + " scored " + GetScoredGoals(teamName, year) + " goals in " + year);

        //OBS.: os valores esperados no output não correspondem aos valores que estão na database da API

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }
    public static string GetScoredGoals(string teamName, int year)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        string urlParameters = "";

        int scoredGoals = 0;

        if (year != 0 && teamName != "")
        {
            List<TeamData> teamDatas = new List<TeamData>();
            urlParameters = "?year=" + year + "&team1=" + teamName + "&page=" + 0;

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseTeam1 = client.GetAsync(urlParameters).Result;


            urlParameters = "?year=" + year + "&team2=" + teamName + "&page=" + 0;

            HttpResponseMessage responseTeam2 = client.GetAsync(urlParameters).Result;

            if (responseTeam1.IsSuccessStatusCode)
            {
                var data = responseTeam1.Content.ReadFromJsonAsync<DataObject>().Result;
                if (data != null && data.data != null)
                {
                    teamDatas = new List<TeamData>(data.data);
                    int page = data.pag;
                    while (page < data.total_pages)
                    {
                        page++;
                        var result = GetNextPageTeam1(teamName, year, page);
                        teamDatas.AddRange(result);
                    }
                    if (data != null)
                    {
                        foreach (var d in teamDatas)
                        {
                            scoredGoals += d.team2goals;
                        }
                    }

                }
            }
            if (responseTeam2.IsSuccessStatusCode)
            {
                var data = responseTeam2.Content.ReadFromJsonAsync<DataObject>().Result;
                if (data != null && data.data != null)
                {
                    teamDatas = new List<TeamData>(data.data);
                    int page = data.pag;
                    while (page < data.total_pages)
                    {
                        page++;
                        var result = GetNextPageTeam2(teamName, year, page);
                        teamDatas.AddRange(result);
                    }
                    if (data != null)
                    {
                        foreach (var d in teamDatas)
                        {
                            scoredGoals += d.team2goals;
                        }
                    }

                }
            }
            else
            {
                return "Erro na requisição.";
            }

        }

        return scoredGoals.ToString();

    }

    public static IEnumerable<TeamData> GetNextPageTeam1(string teamName, int year, int page)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        string urlParameters = "?year=" + year + "&team1=" + teamName + "&page=" + page;
        HttpResponseMessage responseTeam = client.GetAsync(urlParameters).Result;

        if (responseTeam.IsSuccessStatusCode)
        {
            var result = responseTeam.Content.ReadFromJsonAsync<DataObject>().Result;
            if (result != null)
                return result.data;

        }
        return new List<TeamData>();


    }

    public static IEnumerable<TeamData> GetNextPageTeam2(string teamName, int year, int page)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        string urlParameters = "?year=" + year + "&team2=" + teamName + "&page=" + page;
        HttpResponseMessage responseTeam = client.GetAsync(urlParameters).Result;

        if (responseTeam.IsSuccessStatusCode)
        {
            var result = responseTeam.Content.ReadFromJsonAsync<DataObject>().Result;
            if (result != null)
                return result.data;

        }
        return new List<TeamData>();


    }

}