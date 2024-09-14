using PkusForum.Services.DatabaseService.TableStruct;

namespace PkusForum.Services.DatabaseService.Managers
{
    public class ReplyManager
    {
        static readonly LogManager logManager = new LogManager("ReplyManager");

        public static void PublishReply(Reply reply)
        {
            Database.ReplyRepository.Insert(reply);

            logManager.PrintLog($"New reply published!\nTarget:\t{reply.BindTarget}\nBind:\t{reply.BindId}\nOwner:\t{reply.OwnerAccountId}\nBody:\t{reply.Body}");
        }
    }
}
