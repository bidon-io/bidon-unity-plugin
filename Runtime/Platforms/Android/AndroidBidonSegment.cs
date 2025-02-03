#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bidon.Mediation
{
    internal class AndroidBidonSegment : IBidonSegment
    {
        private readonly AndroidJavaObject _bidonSegmentJavaObject;

        internal AndroidBidonSegment(AndroidJavaObject bidonSegmentJavaObject)
        {
            _bidonSegmentJavaObject = bidonSegmentJavaObject;
        }

        public string Uid
        {
            get => _bidonSegmentJavaObject.SafeCall<string>("getSegmentUid");
        }

        public int? Age
        {
            get => _bidonSegmentJavaObject.SafeCall<AndroidJavaObject>("getAge")?.SafeCall<int>("intValue");
            set => _bidonSegmentJavaObject.SafeCall("setAge", AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public BidonUserGender? Gender
        {
            get => _bidonSegmentJavaObject.SafeCall<AndroidJavaObject>("getGender").ToBidonUserGender();
            set => _bidonSegmentJavaObject.SafeCall("setGender", value.ToJavaObject());
        }

        public int? Level
        {
            get => _bidonSegmentJavaObject.SafeCall<AndroidJavaObject>("getLevel")?.SafeCall<int>("intValue");
            set => _bidonSegmentJavaObject.SafeCall("setLevel", AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public double? TotalInAppsAmount
        {
            get => _bidonSegmentJavaObject.SafeCall<AndroidJavaObject>("getTotalInAppAmount")?.SafeCall<double>("doubleValue");
            set => _bidonSegmentJavaObject.SafeCall("setTotalInAppAmount", AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public bool IsPaying
        {
            get => _bidonSegmentJavaObject.SafeCall<bool>("isPaying");
            set => _bidonSegmentJavaObject.SafeCall("setPaying", value);
        }

        public IDictionary<string, object> CustomAttributes
        {
            get => AndroidBidonJavaHelper.GetDictionaryFromJavaMap(_bidonSegmentJavaObject.SafeCall<AndroidJavaObject>("getCustomAttributes"));
        }

        public void SetCustomAttribute(string name, object value)
        {
            if (String.IsNullOrEmpty(name)) return;
            if (!(value is bool) && !(value is int) && !(value is long) && !(value is double)
                && !(value is string) && value != null) return;

            _bidonSegmentJavaObject.SafeCall("putCustomAttribute", name, AndroidBidonJavaHelper.GetJavaObject(value));
        }
    }
}
#endif
