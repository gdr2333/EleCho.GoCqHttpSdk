﻿using EleCho.GoCqHttpSdk.Action.Model.ResultData;
using EleCho.GoCqHttpSdk.DataStructure;
using EleCho.GoCqHttpSdk.Message;
using System.Linq;

namespace EleCho.GoCqHttpSdk.Action.Result;

/// <summary>
/// 获取转发消息操作结果
/// </summary>
public record class CqGetForwardMessageActionResult : CqActionResult
{
    internal CqGetForwardMessageActionResult() { }

    /// <summary>
    /// 转发消息
    /// </summary>
    public CqForwardMessage Message { get; private set; } = [];

    internal override void ReadDataModel(CqActionResultDataModel? model)
    {
        if (model is not CqGetForwardMessageActionResultDataModel dataModel)
            return;

        Message = new CqForwardMessage(dataModel.messages.Select((data) =>
        {
            CqForwardMessageNode node = new CqForwardMessageNode();
            node.ReadDataModel(data);
            return node;
        }));
    }
}