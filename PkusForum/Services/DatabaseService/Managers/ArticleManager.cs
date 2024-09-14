using PkusForum.Services.DatabaseService.TableStruct;

namespace PkusForum.Services.DatabaseService.Managers
{
    public static class ArticleManager
    {
        static readonly LogManager logManager = new LogManager("ArticleManager");

        public static void PublishArticle(Article article)
        {
            Database.ArticleRepository.Insert(article);

            logManager.PrintLog($"New article published!\nOwner:\t{article.OwnerAccountId}\nTitle:\t{article.Title}\nBody:\t{article.Body}");
        }
    }
}
