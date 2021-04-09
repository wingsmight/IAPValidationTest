#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("oeNbBbBC6Rkl4MQnwOkKm5QT7REfzRyOppwkiv1uqJw19ylxJg0DBA0Tmkz/dLqRTJAfjAuI+acUHiwpjfCxBtcjI7kXeTCLoiTwZF9im80tIhHWXVJg5Xs42875j20GD7NB2rnWRhX3hLV11dxdwiyejYXS888ZEaMgAxEsJygLp2mn1iwgICAkISKjIC4hEaMgKyOjICAhpN85YnWqGcCn9kHx5Dr0lKpncGd0Mm8LOSVZKefCd6NAquMDTJhgum1RIJGPBlIm1BnInDmY7F84uG6L5L6bAVEBsNdsimfK+Pkf9gIhktufyv5i7poINbhnHj8UjrBFafCLo8hJfOTFEMce6rKiL4qb8izRmnkaWxgRdRma1+EmTvLr1IQmaCMiICEg");
        private static int[] order = new int[] { 8,4,10,12,10,6,8,10,13,10,10,12,13,13,14 };
        private static int key = 33;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
