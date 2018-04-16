using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{

    public enum Setting
    {
        Private,
        Friends,
        Public
    }

    //defines standard friendsbookSettings, which are applied to newly created characters. Can be overwritten
    public struct StandardFriendsbookSettings
    {
        public static Setting informationSetting = Setting.Friends;
        public static Setting friendsSetting = Setting.Public;
        public static Setting postsSetting = Setting.Friends;
    }
}