using FreeSql.DataAnnotations;

namespace PkusForum.Services.DatabaseService.TableStruct
{
    [Table(Name = "ACCOUNT")]
    public class Account
    {
        [Column(Name = "ACCOUNT_ID", IsNullable = false, IsPrimary = true)] public string AccountId { get; set; }
        [Column(Name = "USER_NAME", IsNullable = false)] public string UserName { get; set; }
        [Column(Name = "PASSWORD_HASH", IsNullable = false)] public string PasswordHash { get; set; }
        [Column(Name = "SELF_INTRODUCTION", IsNullable = true)] public string? SelfIntroduction { get; set; }
        [Column(Name = "USER_PERMISSION", IsNullable = true, MapType = typeof(int))] public Permission UserPermission { get; set; }

        public enum Permission
        {
            User = 0,
            Admin = 1
        }
    }

    [Table(Name = "REPLY")]
    public class Reply
    {
        [Column(Name = "REPLY_ID", IsNullable = false, IsPrimary = true, IsIdentity = true)] public long ReplyId { get; set; }
        [Column(Name = "BIND_TARGET", IsNullable = false, MapType = typeof(int))] public ReplyTarget BindTarget { get; set; }
        [Column(Name = "BIND_ID")] public long BindId { get; set; }
        [Column(Name = "OWNER", IsNullable = false)] public string OwnerAccountId { get; set; }
        [Column(Name = "BODY", IsNullable = false)] public string Body { get; set; }
        [Column(Name = "PUBLIC_DATE", IsNullable = false)] public DateTime PublicDate { get; set; }

        public enum ReplyTarget
        {
            Article = 0
        }
    }

    [Table(Name = "ARTICLE")]
    public class Article
    {
        [Column(Name = "ARTICLE_ID", IsNullable = false, IsPrimary = true, IsIdentity = true)] public long ArticleId { get; set; }
        [Column(Name = "OWNER", IsNullable = false)] public string OwnerAccountId { get; set; }
        [Column(Name = "TITLE", IsNullable = false)] public string Title { get; set; }
        [Column(Name = "BODY", IsNullable = true)] public string? Body { get; set; }
        [Column(Name = "FOLLOWED_ACCOUNT_IDS", IsNullable = false), JsonMap] public List<string> FollowedAccountIds { get; set; } = new List<string>();
        [Column(Name = "PUBLIC_DATE", IsNullable = false)] public DateTime PublicDate { get; set; }
    }
}
