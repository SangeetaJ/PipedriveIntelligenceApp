// Decompiled with JetBrains decompiler
// Type: IntelligenceApp.Controllers.Meta
// Assembly: IntelligenceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEAE96B3-BC8E-4A80-BA78-34BF5DFDBE99
// Assembly location: E:\Sangeeta\IntelligenceApp\IntelligenceApp\bin\IntelligenceApp.dll

using System.Collections.Generic;

namespace PipedriveIntelligenceApp.Controllers
{
  public class Meta
  {
    public int v { get; set; }

    public string action { get; set; }

    public string @object { get; set; }

    public int id { get; set; }

    public int company_id { get; set; }

    public int user_id { get; set; }

    public string host { get; set; }

    public int timestamp { get; set; }

    public long timestamp_micro { get; set; }

    public List<string> permitted_user_ids { get; set; }

    public bool trans_pending { get; set; }

    public bool is_bulk_update { get; set; }

    public bool pipedrive_service_name { get; set; }

    public bool send_activity_notifications { get; set; }

    public object activity_notifications_language { get; set; }

    public MatchesFilters2 matches_filters { get; set; }

    public string webhook_id { get; set; }
  }
}
