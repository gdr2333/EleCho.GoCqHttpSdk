﻿using EleCho.GoCqHttpSdk.Action.Model.Params;
using EleCho.GoCqHttpSdk.Action.Model;
using EleCho.GoCqHttpSdk.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EleCho.GoCqHttpSdk.Post.Model.Base;
using EleCho.GoCqHttpSdk.Enumeration;
using EleCho.GoCqHttpSdk.Post.Base;
using EleCho.GoCqHttpSdk.Action.Result;

namespace EleCho.GoCqHttpSdk.Action.Sender;

/// <summary>
/// HTTP 操作发送器
/// </summary>
/// <remarks>
/// 新建一个 HTTP 操作发送器
/// </remarks>
/// <param name="client"></param>
public class CqHttpActionSender(HttpClient client) : CqActionSender
{
    /// <summary>
    /// HTTP 客户端
    /// </summary>
    public HttpClient Client { get; } = client;

    /// <summary>
    /// 执行操作
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public override async Task<CqActionResult?> InvokeActionAsync(CqAction action)
    {
        // 转为 Model
        string actionType = CqEnum.GetString(action.ActionType) ?? throw new Exception("Uknown action");
        CqActionParamsModel? paramsModel = action.GetParamsModel();
        
        // 转为 JSON 和 HTTP 内容
        string json = JsonSerializer.Serialize(paramsModel, paramsModel.GetType(), JsonHelper.Options);
        StringContent content = new StringContent(json, GlobalConfig.TextEncoding, "application/json");

        // 发送请求
        HttpResponseMessage response = await Client.PostAsync(actionType, content);
        if (!response.IsSuccessStatusCode)
            return null;

        // 读取响应
        MemoryStream ms = new MemoryStream();
        await response.Content.CopyToAsync(ms);
        string rstjson = GlobalConfig.TextEncoding.GetString(ms.ToArray());
        CqActionResultRaw? resultRaw = JsonSerializer.Deserialize<CqActionResultRaw>(rstjson, options: null);

#if DEBUG
        Debug.WriteLine($"{action.ActionType} {JsonSerializer.Serialize(JsonDocument.Parse(rstjson), JsonHelper.Options)}");
#endif

        if (resultRaw == null)
            return null;

        return CqActionResult.FromRaw(resultRaw, actionType);
    }

    /// <summary>
    /// 处理快速操作
    /// </summary>
    /// <param name="context">上报上下文</param>
    /// <param name="postModel">上报数据模型</param>
    /// <returns></returns>
    internal override async Task<bool> HandleQuickAction(CqPostContext context, CqPostModel postModel)
    {
        if (context.QuickOperationModel == null)
            return true;

        object bodyModel = new
        {
            context = postModel,
            operation = context.QuickOperationModel
        };

        // 转为 JSON 和 HTTP 内容
        string json = JsonSerializer.Serialize(bodyModel, bodyModel.GetType(), JsonHelper.Options);
        StringContent content = new StringContent(json, GlobalConfig.TextEncoding, "application/json");

        // 发送请求
        HttpResponseMessage response = await Client.PostAsync(Consts.ActionType.HandleQuickOperation, content);
        if (!response.IsSuccessStatusCode)
            return false;

        // 未来可能在这里加些逻辑
        
        return true;
    }
}