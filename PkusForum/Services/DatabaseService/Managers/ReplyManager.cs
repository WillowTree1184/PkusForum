﻿using PkusForum.Services.DatabaseService.TableStruct;

namespace PkusForum.Services.DatabaseService.Managers
{
    public class ReplyManager
    {
        static readonly LogManager logManager = new LogManager("ReplyManager");

        public static void PublishReply(Reply reply)
        {
            if (reply.BindTarget == Reply.ReplyTarget.Article && Database.ArticleRepository.Select.Where(target => target.ArticleId == reply.BindId).Count() <= 0)
            {
                return;
            }

            Database.ReplyRepository.Insert(reply);

            logManager.PrintLog($"New reply published!\nTarget:\t{reply.BindTarget}\nBind:\t{reply.BindId}\nOwner:\t{reply.OwnerAccountId}\nBody:\t{reply.Body}");
        }
    }
}
