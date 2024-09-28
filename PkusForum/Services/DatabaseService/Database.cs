using FreeSql;
using PkusForum.Services.DatabaseService.TableStruct;
using System.Diagnostics;

namespace PkusForum.Services.DatabaseService
{
    public static class Database
    {
        static Lazy<IFreeSql> sqliteLazy = new Lazy<IFreeSql>(() =>
        {
            IFreeSql freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=../Database.db")
                .UseAutoSyncStructure(true)
                .Build();
            return freeSql;
        });
        public static IFreeSql Sqlite => sqliteLazy.Value;
        public static IBaseRepository<Account> AccountRepository;
        public static IBaseRepository<Reply> ReplyRepository;
        public static IBaseRepository<Article> ArticleRepository;

        public static void Initialize()
        {
            Sqlite.CodeFirst.SyncStructure<Account>();
            Sqlite.CodeFirst.SyncStructure<Reply>();
            Sqlite.CodeFirst.SyncStructure<Article>();

            AccountRepository = Sqlite.GetRepository<Account>();
            ReplyRepository = Sqlite.GetRepository<Reply>();
            ArticleRepository = Sqlite.GetRepository<Article>();

            LogManager.PrintLog("DatabaseService", "Initialized");
        }
    }
}
