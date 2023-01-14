// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

var httpClient = new HttpClient();
var key = "93db14a2915c481c80b5c270223ae136";
var country = "us";
var topic = "world";

var fromDate = new DateTime(2021, 1, 1, 0, 0, 0);
var toDate = new DateTime(2021, 1, 1, 23, 59, 59);

while (fromDate < DateTime.Now)
{
    var requestUri = $"https://newsapi.org/v2/top-headlines?country=us&apiKey=93db14a2915c481c80b5c270223ae136";
    var response = await httpClient.GetAsync(requestUri);
    if (response.IsSuccessStatusCode)
    {
        Rootobject? obj = JsonConvert.DeserializeObject<Rootobject>(await response.Content.ReadAsStringAsync());
        if (obj != null)
        {
            foreach (var article in obj.articles.Take(3))
            {
                Console.WriteLine(article);
            }
        }
    }

    fromDate = fromDate.AddDays(1);
    toDate = toDate.AddDays(1);
}

Console.ReadLine();


public class Rootobject
{
    public int totalArticles { get; set; }
    public Article[] articles { get; set; }
}

public class Article
{
    public string title { get; set; }
    public string description { get; set; }
    public string content { get; set; }
    public string url { get; set; }
    public string image { get; set; }
    public DateTime publishedAt { get; set; }
    public Source source { get; set; }
    
    public override string ToString()
    {
        return $"({publishedAt.ToString("D")}) {title} - {source.name} - {source.url}";
    }
}

public class Source
{
    public string name { get; set; }
    public string url { get; set; }
}
