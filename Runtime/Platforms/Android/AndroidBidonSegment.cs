#if UNITY_ANDROID || BIDON_DEV_ANDROID
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

        public string Id => _bidonSegmentJavaObject?.Call<string>("getSegmentId") ?? "";

        public int Age
        {
            get => -1;
            set => _bidonSegmentJavaObject?.Call("setAge",
                AndroidBidonJavaHelper.GetJavaObject(value));
        }

        public BidonUserGender Gender
        {
            get => BidonUserGender.Other;
            set => _bidonSegmentJavaObject?.Call("setGender",
                AndroidBidonJavaHelper.GetGenderJavaObject(value));
        }

        public int Level
        {
            get => -1;
            set => _bidonSegmentJavaObject?.Call("setLevel", value);
        }

        public double TotalInAppsAmount
        {
            get => -1d;
            set => _bidonSegmentJavaObject?.Call("setTotalInAppAmount", value);
        }

        public bool IsPaying
        {
            get => false;
            set => _bidonSegmentJavaObject?.Call("setPaying", value);
        }

        public IDictionary<string, object> CustomAttributes => new Dictionary<string, object>();

        public void SetCustomAttribute(string name, object value)
        {
            if (!(value is bool) && !(value is int) && !(value is long) && !(value is double)
                && !(value is string) && value != null) return;

            _bidonSegmentJavaObject?.Call("putCustomAttribute",
                AndroidBidonJavaHelper.GetJavaObject(name),
                value == null ? null : AndroidBidonJavaHelper.GetJavaObject(value));
        }
    }
}
#endif
