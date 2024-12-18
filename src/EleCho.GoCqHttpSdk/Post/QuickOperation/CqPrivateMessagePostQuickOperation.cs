﻿using EleCho.GoCqHttpSdk.Message;
using System.Linq;

namespace EleCho.GoCqHttpSdk.Post.QuickOperation;


/// <summary>
/// 私聊消息上报快速操作
/// </summary>
public class CqPrivateMessagePostQuickOperation : CqMessagePostQuickOperation
{
    internal override object? GetModel()
    {
        if (Reply == null)
            return null;

        return new
        {
            reply = Reply == null ? null : Reply.Select(CqMsg.ToModel).ToArray()
            // auto_escape  // 字段无用, 不传
        };
    }
}