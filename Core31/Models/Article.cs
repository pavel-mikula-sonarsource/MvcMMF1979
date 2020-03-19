
namespace Core31.Models
{
    public class Article
    {
        public readonly string Title, Text;

        public Article(string title, string text)
        {
            this.Title = title;
            this.Text = text;
        }
    }
}