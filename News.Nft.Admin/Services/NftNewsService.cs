using Newtonsoft.Json;

namespace News.Nft.Admin.Services;

public static class NftNewsService
{
    public static async Task<List<Article>> GetLast24HourArticles()
    {
        List<Article> result = new List<Article>();
        
        var httpClient = new HttpClient();
        var key = "f944a4150fc94e5590fd102ac88a99f4";
        var country = "us";

        var requestUri = $"https://newsapi.org/v2/top-headlines?category=general&country={country}&pageSize=100&apiKey={key}";
        httpClient.DefaultRequestHeaders.Add("User-Agent", "NftNews");
        var response = await httpClient.GetAsync(requestUri);
        var contentString = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            ArticleResult? obj = JsonConvert.DeserializeObject<ArticleResult>(contentString);
            if (obj != null)
            {
                result.AddRange(obj.articles);
            }
        }
        else
        {
            Console.WriteLine(contentString);
        }

        return result;
    }
    
    public class ArticleResult
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
}