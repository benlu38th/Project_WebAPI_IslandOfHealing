using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public enum Progress
    {
        草稿,
        待審核,
        審核中,
        審核失敗,
        審核成功
    }
    public enum WriterProgress
    {
        未申請,
        已申請,
        申請失敗,
        申請成功
    }

    public enum AI
    {
        外星狗,
        巡航者,
        引路人,
        療癒師
    }

    public enum NotificationContentId
    {
        恭喜你成為作家,
        你追蹤的作家發表了新文章,
        你發表的新文章審核成功
    }
}