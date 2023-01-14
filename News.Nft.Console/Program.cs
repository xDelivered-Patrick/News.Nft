// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

var httpClient = new HttpClient();
var key = "f944a4150fc94e5590fd102ac88a99f4";
var country = "us";
var topic = "world";

var fromDate = new DateTime(2021, 1, 1, 0, 0, 0);
var toDate = new DateTime(2021, 1, 1, 23, 59, 59);

while (fromDate < DateTime.Now)
{
    var requestUri = $"https://newsapi.org/v2/top-headlines?country={country}&apiKey={key}";
    httpClient.DefaultRequestHeaders.Add("User-Agent", "NftNews");
    var response = await httpClient.GetAsync(requestUri);
    var contentString = await response.Content.ReadAsStringAsync();
    if (response.IsSuccessStatusCode)
    {
        Rootobject? obj = JsonConvert.DeserializeObject<Rootobject>(contentString);
        if (obj != null)
        {
            foreach (var article in obj.articles.Take(3))
            {
                Console.WriteLine(article);
            }
        }
    }
    else
    {
        Console.WriteLine(contentString);
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
