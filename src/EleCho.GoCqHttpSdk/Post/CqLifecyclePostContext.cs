﻿using EleCho.GoCqHttpSdk.Enumeration;
using EleCho.GoCqHttpSdk.Post.Base;
using EleCho.GoCqHttpSdk.Post.Model;
using EleCho.GoCqHttpSdk.Post.Model.Base;

namespace EleCho.GoCqHttpSdk.Post;

/// <summary>
/// 生命周期上报上下文
/// </summary>
public record class CqLifecyclePostContext : CqMetaEventPostContext
{
    /// <summary>
    /// 元事件类型: 生命周期
    /// </summary>
    public override CqMetaEventType MetaEventType => CqMetaEventType.Lifecycle;

    /// <summary>
    /// 生命周期类型
    /// </summary>
    public CqLifecycleType LifecycleType { get; internal set; }

    internal CqLifecyclePostContext() { }

    internal override object? QuickOperationModel => null;
    internal override void ReadModel(CqPostModel model)
    {
        base.ReadModel(model);

        if (model is not CqMetaLifecyclePostModel metaModel)
            return;

        LifecycleType = CqEnum.GetLifecycleType(metaModel.sub_type);
    }
}