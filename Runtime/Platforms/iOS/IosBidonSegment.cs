#if UNITY_IOS || BIDON_DEV_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    internal class IosBidonSegment : IBidonSegment
    {
        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentGetId")]
        private static extern string BidonSegmentGetId();

        public string Id => BidonSegmentGetId();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentGetAge")]
        private static extern int BidonSegmentGetAge();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetAge")]
        private static extern void BidonSegmentSetAge(int age);

        public int Age
        {
            get => BidonSegmentGetAge();
            set => BidonSegmentSetAge(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentGetGender")]
        private static extern BidonUserGender BidonSegmentGetGender();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetGender")]
        private static extern void BidonSegmentSetGender(BidonUserGender gender);

        public BidonUserGender Gender
        {
            get => BidonSegmentGetGender();
            set => BidonSegmentSetGender(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentGetLevel")]
        private static extern int BidonSegmentGetLevel();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetLevel")]
        private static extern void BidonSegmentSetLevel(int level);

        public int Level
        {
            get => BidonSegmentGetLevel();
            set => BidonSegmentSetLevel(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentGetTotalInAppsAmount")]
        private static extern double BidonSegmentGetTotalInAppsAmount();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetTotalInAppsAmount")]
        private static extern void BidonSegmentSetTotalInAppsAmount(double inAppsAmount);

        public double TotalInAppsAmount
        {
            get => BidonSegmentGetTotalInAppsAmount();
            set => BidonSegmentSetTotalInAppsAmount(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentGetIsPaying")]
        private static extern bool BidonSegmentGetIsPaying();

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetIsPaying")]
        private static extern void BidonSegmentSetIsPaying(bool isPaying);

        public bool IsPaying
        {
            get => BidonSegmentGetIsPaying();
            set => BidonSegmentSetIsPaying(value);
        }

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentGetCustomAttributes")]
        private static extern string BidonSegmentGetCustomAttributes();

        public IDictionary<string, object> CustomAttributes =>
            IosBidonHelper.GetDictionaryFromJsonString(BidonSegmentGetCustomAttributes());

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetCustomAttributeBool")]
        private static extern void BidonSegmentSetCustomAttributeBool(string name, bool value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetCustomAttributeInt")]
        private static extern void BidonSegmentSetCustomAttributeInt(string name, int value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetCustomAttributeLong")]
        private static extern void BidonSegmentSetCustomAttributeLong(string name, long value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetCustomAttributeDouble")]
        private static extern void BidonSegmentSetCustomAttributeDouble(string name, double value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetCustomAttributeString")]
        private static extern void BidonSegmentSetCustomAttributeString(string name, string value);

        [DllImport("__Internal", EntryPoint = "BDNUnityPluginSegmentSetCustomAttributeNull")]
        private static extern void BidonSegmentSetCustomAttributeNull(string name);

        public void SetCustomAttribute(string name, object value)
        {
            if (String.IsNullOrEmpty(name)) return;
            if (!(value is bool) && !(value is int) && !(value is long) && !(value is double)
                && !(value is string) && value != null) return;

            switch (value)
            {
                case bool valueBool:
                    BidonSegmentSetCustomAttributeBool(name, valueBool);
                    break;
                case int valueInt:
                    BidonSegmentSetCustomAttributeInt(name, valueInt);
                    break;
                case long valueLong:
                    BidonSegmentSetCustomAttributeLong(name, valueLong);
                    break;
                case double valueDouble:
                    BidonSegmentSetCustomAttributeDouble(name, valueDouble);
                    break;
                case string valueString:
                    BidonSegmentSetCustomAttributeString(name, valueString);
                    break;
                case null:
                    BidonSegmentSetCustomAttributeNull(name);
                    break;
            }
        }
    }
}
#endif
