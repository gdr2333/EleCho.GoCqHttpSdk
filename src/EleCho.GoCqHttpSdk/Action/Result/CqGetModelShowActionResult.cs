﻿using System;
using EleCho.GoCqHttpSdk.Action.Model.ResultData;
using EleCho.GoCqHttpSdk.DataStructure;

namespace EleCho.GoCqHttpSdk.Action.Result;

/// <summary>
/// 获取机型显示操作结果
/// </summary>
public record class CqGetModelShowActionResult : CqActionResult
{
    internal CqGetModelShowActionResult()
    {
        Variants = [];
    }

    /// <summary>
    /// 机型可用变体
    /// </summary>
    public CqModelShowVariant[] Variants { get; private set; }

    internal override void ReadDataModel(CqActionResultDataModel? model)
    {
        if (model is not CqGetModelShowActionResultDataModel _m)
            throw new Exception();

        Variants = Array.ConvertAll(_m.variants, variantmodel => new CqModelShowVariant(variantmodel));
    }
}