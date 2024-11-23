#if UNITY_ANDROID || BIDON_DEV_ANDROID
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Bidon.Mediation
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    internal class AndroidBidonSegment : IBidonSegment
    {
        private readonly AndroidJavaObject _bidonSegmentJavaObject;

        internal AndroidBidonSegment(AndroidJavaObject bidonSegmentJavaObject)
        {
            _bidonSegmentJavaObject = bidonSegmentJavaObject;
        }

        public string Uid
        {
            get => _bidonSegmentJavaObject?.Call<string>("getSegmentUid") ?? String.Empty;
        }

        public int Age
        {
            get => _bidonSegmentJavaObject?.Call<AndroidJavaObject>("getAge").Call<int>("intValue") ?? -1;
            set => _bidonSegmentJavaObject?.Call("setAge", AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public BidonUserGender Gender
        {
            get => AndroidBidonJavaHelper.GetBidonUserGender(_bidonSegmentJavaObject?.Call<AndroidJavaObject>("getGender"));
            set => _bidonSegmentJavaObject?.Call("setGender", AndroidBidonJavaHelper.GetGenderJavaObject(value));
        }

        public int Level
        {
            get => _bidonSegmentJavaObject?.Call<AndroidJavaObject>("getLevel").Call<int>("intValue") ?? -1;
            set => _bidonSegmentJavaObject?.Call("setLevel", AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public double TotalInAppsAmount
        {
            get => _bidonSegmentJavaObject?.Call<AndroidJavaObject>("getTotalInAppAmount").Call<double>("doubleValue") ?? -1d;
            set => _bidonSegmentJavaObject?.Call("setTotalInAppAmount", AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public bool IsPaying
        {
            get => _bidonSegmentJavaObject?.Call<bool>("isPaying") ?? false;
            set => _bidonSegmentJavaObject?.Call("setPaying", value);
        }

        public IDictionary<string, object> CustomAttributes => new Dictionary<string, object>();

        public void SetCustomAttribute(string name, object value)
        {
            if (String.IsNullOrEmpty(name)) return;
            if (!(value is bool) && !(value is int) && !(value is long) && !(value is double)
                && !(value is string) && value != null) return;

            _bidonSegmentJavaObject?.Call("putCustomAttribute",
                AndroidBidonJavaHelper.GetJavaObject(name),
                value == null ? null : AndroidBidonJavaHelper.GetJavaObject(value));
        }
    }
}
#endif
