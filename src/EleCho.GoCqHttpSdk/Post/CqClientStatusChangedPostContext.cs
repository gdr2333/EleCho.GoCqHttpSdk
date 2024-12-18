﻿using EleCho.GoCqHttpSdk.DataStructure;
using EleCho.GoCqHttpSdk.Enumeration;
using EleCho.GoCqHttpSdk.Post.Base;
using EleCho.GoCqHttpSdk.Post.Model;
using EleCho.GoCqHttpSdk.Post.Model.Base;

namespace EleCho.GoCqHttpSdk.Post;

/// <summary>
/// 客户端状态变更上下文
/// </summary>
public record class CqClientStatusChangedPostContext : CqNoticePostContext
{
    /// <summary>
    /// 通知类型: 客户端状态
    /// </summary>
    public override CqNoticeType NoticeType => CqNoticeType.ClientStatus;
    
    internal CqClientStatusChangedPostContext() { }

    /// <summary>
    /// 是否在线
    /// </summary>
    public bool IsOnline { get; internal set; }

    /// <summary>
    /// 客户端
    /// </summary>
    public CqDevice Client { get; internal set; } = new CqDevice();
    
    internal override object? QuickOperationModel => null;
    internal override void ReadModel(CqPostModel model)
    {
        base.ReadModel(model);

        if (model is not CqNoticeClientStatusPostModel noticeModel)
            return;

        IsOnline = noticeModel.online;
        Client = new CqDevice(noticeModel.client);
    }
}