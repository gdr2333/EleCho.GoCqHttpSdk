﻿#pragma warning disable IDE1006 // Naming Styles

using EleCho.GoCqHttpSdk.Post.Model.Base;
using EleCho.GoCqHttpSdk.Utils;

namespace EleCho.GoCqHttpSdk.Post.Model;

internal class CqNoticeFriendAddPostModel : CqNoticePostModel
{
    public override string notice_type => Consts.NoticeType.FriendAdd;

    public long user_id { get; set; }
}