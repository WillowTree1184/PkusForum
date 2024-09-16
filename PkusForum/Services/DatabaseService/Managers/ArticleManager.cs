using PkusForum.Services.DatabaseService.TableStruct;
using System.Diagnostics.CodeAnalysis;

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

        public static bool GetArticleById(long articleId, [MaybeNullWhen(false)] out Article? article)
        {
            article = null;

            List<Article> selected = Database.ArticleRepository.Select.Where(target => target.ArticleId == articleId).ToList();
            if (selected.Count > 0)
            {
                article = selected[0];
                return true;
            }

            return false;
        }

        public static void RemoveArticle(Article article)
        {
            Database.ArticleRepository.Delete(article);
            Database.ReplyRepository.Delete(target => target.BindTarget == Reply.ReplyTarget.Article && target.BindId == article.ArticleId);

            logManager.PrintLog($"Article {article.ArticleId} deleted!");
        }
    }
}
