using System;
using System.Collections.Generic;

namespace Models.ImplementationModels.JsonModels
{
    public class Credential
    {
        public int expiry { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class IceServer
    {
        public string endpoint { get; set; }
        public Credential credential { get; set; }
    }

    public class Sip
    {
        public int id { get; set; }
        public string host { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string proxy { get; set; }
        public string stun { get; set; }
        public string voicemail_url { get; set; }
        public string client_ip { get; set; }
        public List<IceServer> ice_servers { get; set; }
    }

    public class Features
    {
        public bool cdma_fallback { get; set; }
        public bool e911_accepted { get; set; }
        public bool is_employee { get; set; }
    }

    public class JsonUser
    {
        public long user_id { get; set; }
        public string username { get; set; }
        public string account_status { get; set; }
        public string guid_hex { get; set; }
        public string expiry { get; set; }
        public string email { get; set; }
        public int email_verified { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string dob { get; set; }
        public bool captcha_required { get; set; }
        public int gender { get; set; }
        public string last_update { get; set; }
        public string ringtone { get; set; }
        public string signature { get; set; }
        public bool show_text_previews { get; set; }
        public int forward_messages { get; set; }
        public string incentivized_share_date_twitter { get; set; }
        public string incentivized_share_date_facebook { get; set; }
        public int credits { get; set; }
        public DateTime timestamp { get; set; }
        public string purchases_timestamp { get; set; }
        public bool has_password { get; set; }
        public string phone_number { get; set; }
        public string phone_assigned_date { get; set; }
        public string phone_last_unassigned { get; set; }
        public Sip sip { get; set; }
        public string disable_calling { get; set; }
        public bool mytempnumber_dnd { get; set; }
        public int sip_minutes { get; set; }
        public string sip_IP { get; set; }
        public string mytempnumber_voicemail_upload_url { get; set; }
        public string sip_username { get; set; }
        public string sip_password { get; set; }
        public int mytempnumber_voicemail_v2 { get; set; }
        public int referring_amount { get; set; }
        public int referred_amount { get; set; }
        public int mytempnumber_status { get; set; }
        public string mytempnumber_expiry { get; set; }
        public Features features { get; set; }
        public string forwarding_expiry { get; set; }
        public string forwarding_status { get; set; }
        public bool premium_calling { get; set; }
        public string forwarding_number { get; set; }
        public string voicemail { get; set; }
        public string voicemail_timestamp { get; set; }
        public bool show_ads { get; set; }
        public bool is_persistent { get; set; }
        public int mytempnumber_free_calling { get; set; }
        public string incentivized_share_date { get; set; }
        public int append_footer { get; set; }
        public string ads_autorenew { get; set; }
        public string voice_autorenew { get; set; }
        public string forward_email { get; set; }
        public bool mytempnumber_voicemail { get; set; }
        public string phone_expiry { get; set; }
        public string area_code { get; set; }
        public bool unlimited_calling { get; set; }
        public bool vm_transcription_enabled { get; set; }
        public string vm_transcription_user_enabled { get; set; }
        public List<string> ad_categories { get; set; }
        public string messaging_email { get; set; }
    }
}
