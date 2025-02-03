#if UNITY_ANDROID || BIDON_DEV

// ReSharper disable CheckNamespace

using UnityEngine;

namespace Bidon.Mediation
{
    internal static class AndroidVectorToJavaPointConverter
    {
        public static AndroidJavaObject ToJavaObject(this Vector2Int vector)
        {
            return AndroidBidonFactory.SafeCreateJavaObject("android.graphics.Point", vector.x, vector.y);
        }

        public static AndroidJavaObject ToJavaObject(this Vector2 vector)
        {
            return AndroidBidonFactory.SafeCreateJavaObject("android.graphics.PointF", vector.x, vector.y);
        }
    }
}
#endif
