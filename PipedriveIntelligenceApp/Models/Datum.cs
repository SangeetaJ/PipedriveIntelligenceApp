// Decompiled with JetBrains decompiler
// Type: IntelligenceApp.Controllers.Datum
// Assembly: IntelligenceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEAE96B3-BC8E-4A80-BA78-34BF5DFDBE99
// Assembly location: E:\Sangeeta\IntelligenceApp\IntelligenceApp\bin\IntelligenceApp.dll

using System.Collections.Generic;

namespace PipedriveIntelligenceApp.Controllers
{
  public class Datum
  {
    public int id { get; set; }

    public int company_id { get; set; }

    public int user_id { get; set; }

    public bool done { get; set; }

    public string type { get; set; }

    public string reference_type { get; set; }

    public object reference_id { get; set; }

    public string due_date { get; set; }

    public string due_time { get; set; }

    public string duration { get; set; }

    public string add_time { get; set; }

    public string marked_as_done_time { get; set; }

    public object last_notification_time { get; set; }

    public object last_notification_user_id { get; set; }

    public int notification_language_id { get; set; }

    public string subject { get; set; }

    public object org_id { get; set; }

    public int person_id { get; set; }

    public int deal_id { get; set; }

    public bool active_flag { get; set; }

    public string update_time { get; set; }

    public int? update_user_id { get; set; }

    public object gcal_event_id { get; set; }

    public object google_calendar_id { get; set; }

    public object google_calendar_etag { get; set; }

    public string note { get; set; }

    public int created_by_user_id { get; set; }

    public IList<Participant> participants { get; set; }

    public string note_clean { get; set; }

    public object org_name { get; set; }

    public string person_name { get; set; }

    public string deal_title { get; set; }

    public string owner_name { get; set; }

    public string person_dropbox_bcc { get; set; }

    public string deal_dropbox_bcc { get; set; }

    public int assigned_to_user_id { get; set; }
  }
}
