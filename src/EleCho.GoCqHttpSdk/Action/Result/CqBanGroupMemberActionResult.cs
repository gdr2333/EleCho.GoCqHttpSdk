﻿using EleCho.GoCqHttpSdk.Action.Model.ResultData;

namespace EleCho.GoCqHttpSdk.Action.Result;

/// <summary>
/// <inheritdoc/>
/// </summary>
public record class CqBanGroupMemberActionResult : CqActionResult
{
    internal CqBanGroupMemberActionResult() { }

    // no data

    internal override void ReadDataModel(CqActionResultDataModel? model)
    {

    }
}