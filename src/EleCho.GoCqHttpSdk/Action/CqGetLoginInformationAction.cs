﻿using EleCho.GoCqHttpSdk.Action.Model.Params;
using EleCho.GoCqHttpSdk.Enumeration;

namespace EleCho.GoCqHttpSdk.Action;

/// <summary>
/// 获取登陆信息操作
/// </summary>
public class CqGetLoginInformationAction : CqAction
{
    /// <summary>
    /// 操作类型: 获取登陆信息
    /// </summary>
    public override CqActionType ActionType => CqActionType.GetLoginInformation;

    internal override CqActionParamsModel GetParamsModel()
    {
        return new CqGetLoginInformationActionParamsModel();
    }
}
