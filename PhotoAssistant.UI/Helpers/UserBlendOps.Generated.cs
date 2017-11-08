
using System;
namespace PaintDotNet {
    partial class UserBlendOps {
        static readonly uint[] masTable = { 0x00000000, 0x00000000, 0, 0x00000001, 0x00000000, 0, 0x00000001, 0x00000000, 1, 0xAAAAAAAB, 0x00000000, 33, 0x00000001, 0x00000000, 2, 0xCCCCCCCD, 0x00000000, 34, 0xAAAAAAAB, 0x00000000, 34, 0x49249249, 0x49249249, 33, 0x00000001, 0x00000000, 3, 0x38E38E39, 0x00000000, 33, 0xCCCCCCCD, 0x00000000, 35, 0xBA2E8BA3, 0x00000000, 35, 0xAAAAAAAB, 0x00000000, 35, 0x4EC4EC4F, 0x00000000, 34, 0x49249249, 0x49249249, 34, 0x88888889, 0x00000000, 35, 0x00000001, 0x00000000, 4, 0xF0F0F0F1, 0x00000000, 36, 0x38E38E39, 0x00000000, 34, 0xD79435E5, 0xD79435E5, 36, 0xCCCCCCCD, 0x00000000, 36, 0xC30C30C3, 0xC30C30C3, 36, 0xBA2E8BA3, 0x00000000, 36, 0xB21642C9, 0x00000000, 36, 0xAAAAAAAB, 0x00000000, 36, 0x51EB851F, 0x00000000, 35, 0x4EC4EC4F, 0x00000000, 35, 0x97B425ED, 0x97B425ED, 36, 0x49249249, 0x49249249, 35, 0x8D3DCB09, 0x00000000, 36, 0x88888889, 0x00000000, 36, 0x42108421, 0x42108421, 35, 0x00000001, 0x00000000, 5, 0x3E0F83E1, 0x00000000, 35, 0xF0F0F0F1, 0x00000000, 37, 0x75075075, 0x75075075, 36, 0x38E38E39, 0x00000000, 35, 0x6EB3E453, 0x6EB3E453, 36, 0xD79435E5, 0xD79435E5, 37, 0x69069069, 0x69069069, 36, 0xCCCCCCCD, 0x00000000, 37, 0xC7CE0C7D, 0x00000000, 37, 0xC30C30C3, 0xC30C30C3, 37, 0x2FA0BE83, 0x00000000, 35, 0xBA2E8BA3, 0x00000000, 37, 0x5B05B05B, 0x5B05B05B, 36, 0xB21642C9, 0x00000000, 37, 0xAE4C415D, 0x00000000, 37, 0xAAAAAAAB, 0x00000000, 37, 0x5397829D, 0x00000000, 36, 0x51EB851F, 0x00000000, 36, 0xA0A0A0A1, 0x00000000, 37, 0x4EC4EC4F, 0x00000000, 36, 0x9A90E7D9, 0x9A90E7D9, 37, 0x97B425ED, 0x97B425ED, 37, 0x94F2094F, 0x94F2094F, 37, 0x49249249, 0x49249249, 36, 0x47DC11F7, 0x47DC11F7, 36, 0x8D3DCB09, 0x00000000, 37, 0x22B63CBF, 0x00000000, 35, 0x88888889, 0x00000000, 37, 0x4325C53F, 0x00000000, 36, 0x42108421, 0x42108421, 36, 0x41041041, 0x41041041, 36, 0x00000001, 0x00000000, 6, 0xFC0FC0FD, 0x00000000, 38, 0x3E0F83E1, 0x00000000, 36, 0x07A44C6B, 0x00000000, 33, 0xF0F0F0F1, 0x00000000, 38, 0x76B981DB, 0x00000000, 37, 0x75075075, 0x75075075, 37, 0xE6C2B449, 0x00000000, 38, 0x38E38E39, 0x00000000, 36, 0x381C0E07, 0x381C0E07, 36, 0x6EB3E453, 0x6EB3E453, 37, 0x1B4E81B5, 0x00000000, 35, 0xD79435E5, 0xD79435E5, 38, 0x3531DEC1, 0x00000000, 36, 0x69069069, 0x69069069, 37, 0xCF6474A9, 0x00000000, 38, 0xCCCCCCCD, 0x00000000, 38, 0xCA4587E7, 0x00000000, 38, 0xC7CE0C7D, 0x00000000, 38, 0x3159721F, 0x00000000, 36, 0xC30C30C3, 0xC30C30C3, 38, 0xC0C0C0C1, 0x00000000, 38, 0x2FA0BE83, 0x00000000, 36, 0x2F149903, 0x00000000, 36, 0xBA2E8BA3, 0x00000000, 38, 0xB81702E1, 0x00000000, 38, 0x5B05B05B, 0x5B05B05B, 37, 0x2D02D02D, 0x2D02D02D, 36, 0xB21642C9, 0x00000000, 38, 0xB02C0B03, 0x00000000, 38, 0xAE4C415D, 0x00000000, 38, 0x2B1DA461, 0x2B1DA461, 36, 0xAAAAAAAB, 0x00000000, 38, 0xA8E83F57, 0xA8E83F57, 38, 0x5397829D, 0x00000000, 37, 0xA57EB503, 0x00000000, 38, 0x51EB851F, 0x00000000, 37, 0xA237C32B, 0xA237C32B, 38, 0xA0A0A0A1, 0x00000000, 38, 0x9F1165E7, 0x9F1165E7, 38, 0x4EC4EC4F, 0x00000000, 37, 0x27027027, 0x27027027, 36, 0x9A90E7D9, 0x9A90E7D9, 38, 0x991F1A51, 0x991F1A51, 38, 0x97B425ED, 0x97B425ED, 38, 0x2593F69B, 0x2593F69B, 36, 0x94F2094F, 0x94F2094F, 38, 0x24E6A171, 0x24E6A171, 36, 0x49249249, 0x49249249, 37, 0x90FDBC09, 0x90FDBC09, 38, 0x47DC11F7, 0x47DC11F7, 37, 0x8E78356D, 0x8E78356D, 38, 0x8D3DCB09, 0x00000000, 38, 0x23023023, 0x23023023, 36, 0x22B63CBF, 0x00000000, 36, 0x44D72045, 0x00000000, 37, 0x88888889, 0x00000000, 38, 0x8767AB5F, 0x8767AB5F, 38, 0x4325C53F, 0x00000000, 37, 0x85340853, 0x85340853, 38, 0x42108421, 0x42108421, 37, 0x10624DD3, 0x00000000, 35, 0x41041041, 0x41041041, 37, 0x10204081, 0x10204081, 35, 0x00000001, 0x00000000, 7, 0x0FE03F81, 0x00000000, 35, 0xFC0FC0FD, 0x00000000, 39, 0xFA232CF3, 0x00000000, 39, 0x3E0F83E1, 0x00000000, 37, 0xF6603D99, 0x00000000, 39, 0x07A44C6B, 0x00000000, 34, 0xF2B9D649, 0x00000000, 39, 0xF0F0F0F1, 0x00000000, 39, 0x077975B9, 0x00000000, 34, 0x76B981DB, 0x00000000, 38, 0x75DED953, 0x00000000, 38, 0x75075075, 0x75075075, 38, 0x3A196B1F, 0x00000000, 37, 0xE6C2B449, 0x00000000, 39, 0xE525982B, 0x00000000, 39, 0x38E38E39, 0x00000000, 37, 0xE1FC780F, 0x00000000, 39, 0x381C0E07, 0x381C0E07, 37, 0xDEE95C4D, 0x00000000, 39, 0x6EB3E453, 0x6EB3E453, 38, 0xDBEB61EF, 0x00000000, 39, 0x1B4E81B5, 0x00000000, 36, 0x36406C81, 0x00000000, 37, 0xD79435E5, 0xD79435E5, 39, 0xD62B80D7, 0x00000000, 39, 0x3531DEC1, 0x00000000, 37, 0xD3680D37, 0x00000000, 39, 0x69069069, 0x69069069, 38, 0x342DA7F3, 0x00000000, 37, 0xCF6474A9, 0x00000000, 39, 0xCE168A77, 0xCE168A77, 39, 0xCCCCCCCD, 0x00000000, 39, 0xCB8727C1, 0x00000000, 39, 0xCA4587E7, 0x00000000, 39, 0xC907DA4F, 0x00000000, 39, 0xC7CE0C7D, 0x00000000, 39, 0x634C0635, 0x00000000, 38, 0x3159721F, 0x00000000, 37, 0x621B97C3, 0x00000000, 38, 0xC30C30C3, 0xC30C30C3, 39, 0x60F25DEB, 0x00000000, 38, 0xC0C0C0C1, 0x00000000, 39, 0x17F405FD, 0x17F405FD, 36, 0x2FA0BE83, 0x00000000, 37, 0xBD691047, 0xBD691047, 39, 0x2F149903, 0x00000000, 37, 0x5D9F7391, 0x00000000, 38, 0xBA2E8BA3, 0x00000000, 39, 0x5C90A1FD, 0x5C90A1FD, 38, 0xB81702E1, 0x00000000, 39, 0x5B87DDAD, 0x5B87DDAD, 38, 0x5B05B05B, 0x5B05B05B, 38, 0xB509E68B, 0x00000000, 39, 0x2D02D02D, 0x2D02D02D, 37, 0xB30F6353, 0x00000000, 39, 0xB21642C9, 0x00000000, 39, 0x1623FA77, 0x1623FA77, 36, 0xB02C0B03, 0x00000000, 39, 0xAF3ADDC7, 0x00000000, 39, 0xAE4C415D, 0x00000000, 39, 0x15AC056B, 0x15AC056B, 36, 0x2B1DA461, 0x2B1DA461, 37, 0xAB8F69E3, 0x00000000, 39, 0xAAAAAAAB, 0x00000000, 39, 0x15390949, 0x00000000, 36, 0xA8E83F57, 0xA8E83F57, 39, 0x15015015, 0x15015015, 36, 0x5397829D, 0x00000000, 38, 0xA655C439, 0xA655C439, 39, 0xA57EB503, 0x00000000, 39, 0x5254E78F, 0x00000000, 38, 0x51EB851F, 0x00000000, 38, 0x028C1979, 0x00000000, 33, 0xA237C32B, 0xA237C32B, 39, 0xA16B312F, 0x00000000, 39, 0xA0A0A0A1, 0x00000000, 39, 0x4FEC04FF, 0x00000000, 38, 0x9F1165E7, 0x9F1165E7, 39, 0x27932B49, 0x00000000, 37, 0x4EC4EC4F, 0x00000000, 38, 0x9CC8E161, 0x00000000, 39, 0x27027027, 0x27027027, 37, 0x9B4C6F9F, 0x00000000, 39, 0x9A90E7D9, 0x9A90E7D9, 39, 0x99D722DB, 0x00000000, 39, 0x991F1A51, 0x991F1A51, 39, 0x4C346405, 0x00000000, 38, 0x97B425ED, 0x97B425ED, 39, 0x4B809701, 0x4B809701, 38, 0x2593F69B, 0x2593F69B, 37, 0x12B404AD, 0x12B404AD, 36, 0x94F2094F, 0x94F2094F, 39, 0x25116025, 0x25116025, 37, 0x24E6A171, 0x24E6A171, 37, 0x24BC44E1, 0x24BC44E1, 37, 0x49249249, 0x49249249, 38, 0x91A2B3C5, 0x00000000, 39, 0x90FDBC09, 0x90FDBC09, 39, 0x905A3863, 0x905A3863, 39, 0x47DC11F7, 0x47DC11F7, 38, 0x478BBCED, 0x00000000, 38, 0x8E78356D, 0x8E78356D, 39, 0x46ED2901, 0x46ED2901, 38, 0x8D3DCB09, 0x00000000, 39, 0x2328A701, 0x2328A701, 37, 0x23023023, 0x23023023, 37, 0x45B81A25, 0x45B81A25, 38, 0x22B63CBF, 0x00000000, 37, 0x08A42F87, 0x08A42F87, 35, 0x44D72045, 0x00000000, 38, 0x891AC73B, 0x00000000, 39, 0x88888889, 0x00000000, 39, 0x10FEF011, 0x00000000, 36, 0x8767AB5F, 0x8767AB5F, 39, 0x86D90545, 0x00000000, 39, 0x4325C53F, 0x00000000, 38, 0x85BF3761, 0x85BF3761, 39, 0x85340853, 0x85340853, 39, 0x10953F39, 0x10953F39, 36, 0x42108421, 0x42108421, 38, 0x41CC9829, 0x41CC9829, 38, 0x10624DD3, 0x00000000, 36, 0x828CBFBF, 0x00000000, 39, 0x41041041, 0x41041041, 38, 0x81848DA9, 0x00000000, 39, 0x10204081, 0x10204081, 36, 0x80808081, 0x00000000, 39 };
        [Serializable]
        public sealed class NormalBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Normal" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = ((rhs).B);
                    ;
                    fG = ((rhs).G);
                    ;
                    fR = ((rhs).R);
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = ((*src).B);
                        ;
                        fG = ((*src).G);
                        ;
                        fR = ((*src).R);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = ((*rhs).B);
                        ;
                        fG = ((*rhs).G);
                        ;
                        fR = ((*rhs).R);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = ((rhs).B);
                    ;
                    fG = ((rhs).G);
                    ;
                    fR = ((rhs).R);
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new NormalBlendOpWithOpacity(opacity);
 sealed class NormalBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Normal" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = ((rhs).B);
                        ;
                        fG = ((rhs).G);
                        ;
                        fR = ((rhs).R);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = ((*src).B);
                            ;
                            fG = ((*src).G);
                            ;
                            fR = ((*src).R);
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = ((*rhs).B);
                            ;
                            fG = ((*rhs).G);
                            ;
                            fR = ((*rhs).R);
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public NormalBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class MultiplyBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Multiply" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = ((((lhs).B)) * (((rhs).B)) + 0x80);
                    fB = ((((fB) >> 8) + (fB)) >> 8);
                    ;
                    fG = ((((lhs).G)) * (((rhs).G)) + 0x80);
                    fG = ((((fG) >> 8) + (fG)) >> 8);
                    ;
                    fR = ((((lhs).R)) * (((rhs).R)) + 0x80);
                    fR = ((((fR) >> 8) + (fR)) >> 8);
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = ((((*dst).B)) * (((*src).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        ;
                        fG = ((((*dst).G)) * (((*src).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        ;
                        fR = ((((*dst).R)) * (((*src).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = ((((*lhs).B)) * (((*rhs).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        ;
                        fG = ((((*lhs).G)) * (((*rhs).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        ;
                        fR = ((((*lhs).R)) * (((*rhs).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = ((((lhs).B)) * (((rhs).B)) + 0x80);
                    fB = ((((fB) >> 8) + (fB)) >> 8);
                    ;
                    fG = ((((lhs).G)) * (((rhs).G)) + 0x80);
                    fG = ((((fG) >> 8) + (fG)) >> 8);
                    ;
                    fR = ((((lhs).R)) * (((rhs).R)) + 0x80);
                    fR = ((((fR) >> 8) + (fR)) >> 8);
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new MultiplyBlendOpWithOpacity(opacity);
 sealed class MultiplyBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Multiply" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = ((((lhs).B)) * (((rhs).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        ;
                        fG = ((((lhs).G)) * (((rhs).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        ;
                        fR = ((((lhs).R)) * (((rhs).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = ((((*dst).B)) * (((*src).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            ;
                            fG = ((((*dst).G)) * (((*src).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            ;
                            fR = ((((*dst).R)) * (((*src).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = ((((*lhs).B)) * (((*rhs).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            ;
                            fG = ((((*lhs).G)) * (((*rhs).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            ;
                            fR = ((((*lhs).R)) * (((*rhs).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public MultiplyBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class AdditiveBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Additive" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = Math.Min(255, ((lhs).B) + ((rhs).B));
                    ;
                    fG = Math.Min(255, ((lhs).G) + ((rhs).G));
                    ;
                    fR = Math.Min(255, ((lhs).R) + ((rhs).R));
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Min(255, ((*dst).B) + ((*src).B));
                        ;
                        fG = Math.Min(255, ((*dst).G) + ((*src).G));
                        ;
                        fR = Math.Min(255, ((*dst).R) + ((*src).R));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Min(255, ((*lhs).B) + ((*rhs).B));
                        ;
                        fG = Math.Min(255, ((*lhs).G) + ((*rhs).G));
                        ;
                        fR = Math.Min(255, ((*lhs).R) + ((*rhs).R));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = Math.Min(255, ((lhs).B) + ((rhs).B));
                    ;
                    fG = Math.Min(255, ((lhs).G) + ((rhs).G));
                    ;
                    fR = Math.Min(255, ((lhs).R) + ((rhs).R));
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new AdditiveBlendOpWithOpacity(opacity);
 sealed class AdditiveBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Additive" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Min(255, ((lhs).B) + ((rhs).B));
                        ;
                        fG = Math.Min(255, ((lhs).G) + ((rhs).G));
                        ;
                        fR = Math.Min(255, ((lhs).R) + ((rhs).R));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = Math.Min(255, ((*dst).B) + ((*src).B));
                            ;
                            fG = Math.Min(255, ((*dst).G) + ((*src).G));
                            ;
                            fR = Math.Min(255, ((*dst).R) + ((*src).R));
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = Math.Min(255, ((*lhs).B) + ((*rhs).B));
                            ;
                            fG = Math.Min(255, ((*lhs).G) + ((*rhs).G));
                            ;
                            fR = Math.Min(255, ((*lhs).R) + ((*rhs).R));
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public AdditiveBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class ColorBurnBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "ColorBurn" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((rhs).B) == 0) {
                        fB = 0;
                    } else {
                        int i = (((rhs).B)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fB = (int)(((((255 - ((lhs).B)) * 255) * M) + A) >> (int)S);
                        ;
                        fB = 255 - fB;
                        fB = Math.Max(0, fB);
                    };
                    if(((rhs).G) == 0) {
                        fG = 0;
                    } else {
                        int i = (((rhs).G)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fG = (int)(((((255 - ((lhs).G)) * 255) * M) + A) >> (int)S);
                        ;
                        fG = 255 - fG;
                        fG = Math.Max(0, fG);
                    };
                    if(((rhs).R) == 0) {
                        fR = 0;
                    } else {
                        int i = (((rhs).R)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fR = (int)(((((255 - ((lhs).R)) * 255) * M) + A) >> (int)S);
                        ;
                        fR = 255 - fR;
                        fR = Math.Max(0, fR);
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*src).B) == 0) {
                            fB = 0;
                        } else {
                            int i = (((*src).B)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)(((((255 - ((*dst).B)) * 255) * M) + A) >> (int)S);
                            ;
                            fB = 255 - fB;
                            fB = Math.Max(0, fB);
                        };
                        if(((*src).G) == 0) {
                            fG = 0;
                        } else {
                            int i = (((*src).G)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)(((((255 - ((*dst).G)) * 255) * M) + A) >> (int)S);
                            ;
                            fG = 255 - fG;
                            fG = Math.Max(0, fG);
                        };
                        if(((*src).R) == 0) {
                            fR = 0;
                        } else {
                            int i = (((*src).R)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)(((((255 - ((*dst).R)) * 255) * M) + A) >> (int)S);
                            ;
                            fR = 255 - fR;
                            fR = Math.Max(0, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*rhs).B) == 0) {
                            fB = 0;
                        } else {
                            int i = (((*rhs).B)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)(((((255 - ((*lhs).B)) * 255) * M) + A) >> (int)S);
                            ;
                            fB = 255 - fB;
                            fB = Math.Max(0, fB);
                        };
                        if(((*rhs).G) == 0) {
                            fG = 0;
                        } else {
                            int i = (((*rhs).G)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)(((((255 - ((*lhs).G)) * 255) * M) + A) >> (int)S);
                            ;
                            fG = 255 - fG;
                            fG = Math.Max(0, fG);
                        };
                        if(((*rhs).R) == 0) {
                            fR = 0;
                        } else {
                            int i = (((*rhs).R)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)(((((255 - ((*lhs).R)) * 255) * M) + A) >> (int)S);
                            ;
                            fR = 255 - fR;
                            fR = Math.Max(0, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((rhs).B) == 0) {
                        fB = 0;
                    } else {
                        int i = (((rhs).B)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fB = (int)(((((255 - ((lhs).B)) * 255) * M) + A) >> (int)S);
                        ;
                        fB = 255 - fB;
                        fB = Math.Max(0, fB);
                    };
                    if(((rhs).G) == 0) {
                        fG = 0;
                    } else {
                        int i = (((rhs).G)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fG = (int)(((((255 - ((lhs).G)) * 255) * M) + A) >> (int)S);
                        ;
                        fG = 255 - fG;
                        fG = Math.Max(0, fG);
                    };
                    if(((rhs).R) == 0) {
                        fR = 0;
                    } else {
                        int i = (((rhs).R)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fR = (int)(((((255 - ((lhs).R)) * 255) * M) + A) >> (int)S);
                        ;
                        fR = 255 - fR;
                        fR = Math.Max(0, fR);
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new ColorBurnBlendOpWithOpacity(opacity);
 sealed class ColorBurnBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "ColorBurn" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((rhs).B) == 0) {
                            fB = 0;
                        } else {
                            int i = (((rhs).B)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)(((((255 - ((lhs).B)) * 255) * M) + A) >> (int)S);
                            ;
                            fB = 255 - fB;
                            fB = Math.Max(0, fB);
                        };
                        if(((rhs).G) == 0) {
                            fG = 0;
                        } else {
                            int i = (((rhs).G)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)(((((255 - ((lhs).G)) * 255) * M) + A) >> (int)S);
                            ;
                            fG = 255 - fG;
                            fG = Math.Max(0, fG);
                        };
                        if(((rhs).R) == 0) {
                            fR = 0;
                        } else {
                            int i = (((rhs).R)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)(((((255 - ((lhs).R)) * 255) * M) + A) >> (int)S);
                            ;
                            fR = 255 - fR;
                            fR = Math.Max(0, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*src).B) == 0) {
                                fB = 0;
                            } else {
                                int i = (((*src).B)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fB = (int)(((((255 - ((*dst).B)) * 255) * M) + A) >> (int)S);
                                ;
                                fB = 255 - fB;
                                fB = Math.Max(0, fB);
                            };
                            if(((*src).G) == 0) {
                                fG = 0;
                            } else {
                                int i = (((*src).G)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fG = (int)(((((255 - ((*dst).G)) * 255) * M) + A) >> (int)S);
                                ;
                                fG = 255 - fG;
                                fG = Math.Max(0, fG);
                            };
                            if(((*src).R) == 0) {
                                fR = 0;
                            } else {
                                int i = (((*src).R)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fR = (int)(((((255 - ((*dst).R)) * 255) * M) + A) >> (int)S);
                                ;
                                fR = 255 - fR;
                                fR = Math.Max(0, fR);
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*rhs).B) == 0) {
                                fB = 0;
                            } else {
                                int i = (((*rhs).B)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fB = (int)(((((255 - ((*lhs).B)) * 255) * M) + A) >> (int)S);
                                ;
                                fB = 255 - fB;
                                fB = Math.Max(0, fB);
                            };
                            if(((*rhs).G) == 0) {
                                fG = 0;
                            } else {
                                int i = (((*rhs).G)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fG = (int)(((((255 - ((*lhs).G)) * 255) * M) + A) >> (int)S);
                                ;
                                fG = 255 - fG;
                                fG = Math.Max(0, fG);
                            };
                            if(((*rhs).R) == 0) {
                                fR = 0;
                            } else {
                                int i = (((*rhs).R)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fR = (int)(((((255 - ((*lhs).R)) * 255) * M) + A) >> (int)S);
                                ;
                                fR = 255 - fR;
                                fR = Math.Max(0, fR);
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public ColorBurnBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class ColorDodgeBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "ColorDodge" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((rhs).B) == 255) {
                        fB = 255;
                    } else {
                        int i = ((255 - ((rhs).B))) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fB = (int)((((((lhs).B) * 255) * M) + A) >> (int)S);
                        ;
                        fB = Math.Min(255, fB);
                    };
                    if(((rhs).G) == 255) {
                        fG = 255;
                    } else {
                        int i = ((255 - ((rhs).G))) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fG = (int)((((((lhs).G) * 255) * M) + A) >> (int)S);
                        ;
                        fG = Math.Min(255, fG);
                    };
                    if(((rhs).R) == 255) {
                        fR = 255;
                    } else {
                        int i = ((255 - ((rhs).R))) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fR = (int)((((((lhs).R) * 255) * M) + A) >> (int)S);
                        ;
                        fR = Math.Min(255, fR);
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*src).B) == 255) {
                            fB = 255;
                        } else {
                            int i = ((255 - ((*src).B))) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)((((((*dst).B) * 255) * M) + A) >> (int)S);
                            ;
                            fB = Math.Min(255, fB);
                        };
                        if(((*src).G) == 255) {
                            fG = 255;
                        } else {
                            int i = ((255 - ((*src).G))) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)((((((*dst).G) * 255) * M) + A) >> (int)S);
                            ;
                            fG = Math.Min(255, fG);
                        };
                        if(((*src).R) == 255) {
                            fR = 255;
                        } else {
                            int i = ((255 - ((*src).R))) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)((((((*dst).R) * 255) * M) + A) >> (int)S);
                            ;
                            fR = Math.Min(255, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*rhs).B) == 255) {
                            fB = 255;
                        } else {
                            int i = ((255 - ((*rhs).B))) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)((((((*lhs).B) * 255) * M) + A) >> (int)S);
                            ;
                            fB = Math.Min(255, fB);
                        };
                        if(((*rhs).G) == 255) {
                            fG = 255;
                        } else {
                            int i = ((255 - ((*rhs).G))) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)((((((*lhs).G) * 255) * M) + A) >> (int)S);
                            ;
                            fG = Math.Min(255, fG);
                        };
                        if(((*rhs).R) == 255) {
                            fR = 255;
                        } else {
                            int i = ((255 - ((*rhs).R))) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)((((((*lhs).R) * 255) * M) + A) >> (int)S);
                            ;
                            fR = Math.Min(255, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((rhs).B) == 255) {
                        fB = 255;
                    } else {
                        int i = ((255 - ((rhs).B))) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fB = (int)((((((lhs).B) * 255) * M) + A) >> (int)S);
                        ;
                        fB = Math.Min(255, fB);
                    };
                    if(((rhs).G) == 255) {
                        fG = 255;
                    } else {
                        int i = ((255 - ((rhs).G))) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fG = (int)((((((lhs).G) * 255) * M) + A) >> (int)S);
                        ;
                        fG = Math.Min(255, fG);
                    };
                    if(((rhs).R) == 255) {
                        fR = 255;
                    } else {
                        int i = ((255 - ((rhs).R))) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fR = (int)((((((lhs).R) * 255) * M) + A) >> (int)S);
                        ;
                        fR = Math.Min(255, fR);
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new ColorDodgeBlendOpWithOpacity(opacity);
 sealed class ColorDodgeBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "ColorDodge" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((rhs).B) == 255) {
                            fB = 255;
                        } else {
                            int i = ((255 - ((rhs).B))) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)((((((lhs).B) * 255) * M) + A) >> (int)S);
                            ;
                            fB = Math.Min(255, fB);
                        };
                        if(((rhs).G) == 255) {
                            fG = 255;
                        } else {
                            int i = ((255 - ((rhs).G))) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)((((((lhs).G) * 255) * M) + A) >> (int)S);
                            ;
                            fG = Math.Min(255, fG);
                        };
                        if(((rhs).R) == 255) {
                            fR = 255;
                        } else {
                            int i = ((255 - ((rhs).R))) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)((((((lhs).R) * 255) * M) + A) >> (int)S);
                            ;
                            fR = Math.Min(255, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*src).B) == 255) {
                                fB = 255;
                            } else {
                                int i = ((255 - ((*src).B))) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fB = (int)((((((*dst).B) * 255) * M) + A) >> (int)S);
                                ;
                                fB = Math.Min(255, fB);
                            };
                            if(((*src).G) == 255) {
                                fG = 255;
                            } else {
                                int i = ((255 - ((*src).G))) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fG = (int)((((((*dst).G) * 255) * M) + A) >> (int)S);
                                ;
                                fG = Math.Min(255, fG);
                            };
                            if(((*src).R) == 255) {
                                fR = 255;
                            } else {
                                int i = ((255 - ((*src).R))) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fR = (int)((((((*dst).R) * 255) * M) + A) >> (int)S);
                                ;
                                fR = Math.Min(255, fR);
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*rhs).B) == 255) {
                                fB = 255;
                            } else {
                                int i = ((255 - ((*rhs).B))) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fB = (int)((((((*lhs).B) * 255) * M) + A) >> (int)S);
                                ;
                                fB = Math.Min(255, fB);
                            };
                            if(((*rhs).G) == 255) {
                                fG = 255;
                            } else {
                                int i = ((255 - ((*rhs).G))) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fG = (int)((((((*lhs).G) * 255) * M) + A) >> (int)S);
                                ;
                                fG = Math.Min(255, fG);
                            };
                            if(((*rhs).R) == 255) {
                                fR = 255;
                            } else {
                                int i = ((255 - ((*rhs).R))) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fR = (int)((((((*lhs).R) * 255) * M) + A) >> (int)S);
                                ;
                                fR = Math.Min(255, fR);
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public ColorDodgeBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class ReflectBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Reflect" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((rhs).B) == 255) {
                        fB = 255;
                    } else {
                        int i = (255 - ((rhs).B)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fB = (int)(((((lhs).B) * ((lhs).B) * M) + A) >> (int)S);
                        ;
                        fB = Math.Min(255, fB);
                    };
                    if(((rhs).G) == 255) {
                        fG = 255;
                    } else {
                        int i = (255 - ((rhs).G)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fG = (int)(((((lhs).G) * ((lhs).G) * M) + A) >> (int)S);
                        ;
                        fG = Math.Min(255, fG);
                    };
                    if(((rhs).R) == 255) {
                        fR = 255;
                    } else {
                        int i = (255 - ((rhs).R)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fR = (int)(((((lhs).R) * ((lhs).R) * M) + A) >> (int)S);
                        ;
                        fR = Math.Min(255, fR);
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*src).B) == 255) {
                            fB = 255;
                        } else {
                            int i = (255 - ((*src).B)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)(((((*dst).B) * ((*dst).B) * M) + A) >> (int)S);
                            ;
                            fB = Math.Min(255, fB);
                        };
                        if(((*src).G) == 255) {
                            fG = 255;
                        } else {
                            int i = (255 - ((*src).G)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)(((((*dst).G) * ((*dst).G) * M) + A) >> (int)S);
                            ;
                            fG = Math.Min(255, fG);
                        };
                        if(((*src).R) == 255) {
                            fR = 255;
                        } else {
                            int i = (255 - ((*src).R)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)(((((*dst).R) * ((*dst).R) * M) + A) >> (int)S);
                            ;
                            fR = Math.Min(255, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*rhs).B) == 255) {
                            fB = 255;
                        } else {
                            int i = (255 - ((*rhs).B)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)(((((*lhs).B) * ((*lhs).B) * M) + A) >> (int)S);
                            ;
                            fB = Math.Min(255, fB);
                        };
                        if(((*rhs).G) == 255) {
                            fG = 255;
                        } else {
                            int i = (255 - ((*rhs).G)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)(((((*lhs).G) * ((*lhs).G) * M) + A) >> (int)S);
                            ;
                            fG = Math.Min(255, fG);
                        };
                        if(((*rhs).R) == 255) {
                            fR = 255;
                        } else {
                            int i = (255 - ((*rhs).R)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)(((((*lhs).R) * ((*lhs).R) * M) + A) >> (int)S);
                            ;
                            fR = Math.Min(255, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((rhs).B) == 255) {
                        fB = 255;
                    } else {
                        int i = (255 - ((rhs).B)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fB = (int)(((((lhs).B) * ((lhs).B) * M) + A) >> (int)S);
                        ;
                        fB = Math.Min(255, fB);
                    };
                    if(((rhs).G) == 255) {
                        fG = 255;
                    } else {
                        int i = (255 - ((rhs).G)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fG = (int)(((((lhs).G) * ((lhs).G) * M) + A) >> (int)S);
                        ;
                        fG = Math.Min(255, fG);
                    };
                    if(((rhs).R) == 255) {
                        fR = 255;
                    } else {
                        int i = (255 - ((rhs).R)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fR = (int)(((((lhs).R) * ((lhs).R) * M) + A) >> (int)S);
                        ;
                        fR = Math.Min(255, fR);
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new ReflectBlendOpWithOpacity(opacity);
 sealed class ReflectBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Reflect" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((rhs).B) == 255) {
                            fB = 255;
                        } else {
                            int i = (255 - ((rhs).B)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)(((((lhs).B) * ((lhs).B) * M) + A) >> (int)S);
                            ;
                            fB = Math.Min(255, fB);
                        };
                        if(((rhs).G) == 255) {
                            fG = 255;
                        } else {
                            int i = (255 - ((rhs).G)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)(((((lhs).G) * ((lhs).G) * M) + A) >> (int)S);
                            ;
                            fG = Math.Min(255, fG);
                        };
                        if(((rhs).R) == 255) {
                            fR = 255;
                        } else {
                            int i = (255 - ((rhs).R)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)(((((lhs).R) * ((lhs).R) * M) + A) >> (int)S);
                            ;
                            fR = Math.Min(255, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*src).B) == 255) {
                                fB = 255;
                            } else {
                                int i = (255 - ((*src).B)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fB = (int)(((((*dst).B) * ((*dst).B) * M) + A) >> (int)S);
                                ;
                                fB = Math.Min(255, fB);
                            };
                            if(((*src).G) == 255) {
                                fG = 255;
                            } else {
                                int i = (255 - ((*src).G)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fG = (int)(((((*dst).G) * ((*dst).G) * M) + A) >> (int)S);
                                ;
                                fG = Math.Min(255, fG);
                            };
                            if(((*src).R) == 255) {
                                fR = 255;
                            } else {
                                int i = (255 - ((*src).R)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fR = (int)(((((*dst).R) * ((*dst).R) * M) + A) >> (int)S);
                                ;
                                fR = Math.Min(255, fR);
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*rhs).B) == 255) {
                                fB = 255;
                            } else {
                                int i = (255 - ((*rhs).B)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fB = (int)(((((*lhs).B) * ((*lhs).B) * M) + A) >> (int)S);
                                ;
                                fB = Math.Min(255, fB);
                            };
                            if(((*rhs).G) == 255) {
                                fG = 255;
                            } else {
                                int i = (255 - ((*rhs).G)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fG = (int)(((((*lhs).G) * ((*lhs).G) * M) + A) >> (int)S);
                                ;
                                fG = Math.Min(255, fG);
                            };
                            if(((*rhs).R) == 255) {
                                fR = 255;
                            } else {
                                int i = (255 - ((*rhs).R)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fR = (int)(((((*lhs).R) * ((*lhs).R) * M) + A) >> (int)S);
                                ;
                                fR = Math.Min(255, fR);
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public ReflectBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class GlowBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Glow" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((lhs).B) == 255) {
                        fB = 255;
                    } else {
                        int i = (255 - ((lhs).B)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fB = (int)(((((rhs).B) * ((rhs).B) * M) + A) >> (int)S);
                        ;
                        fB = Math.Min(255, fB);
                    };
                    if(((lhs).G) == 255) {
                        fG = 255;
                    } else {
                        int i = (255 - ((lhs).G)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fG = (int)(((((rhs).G) * ((rhs).G) * M) + A) >> (int)S);
                        ;
                        fG = Math.Min(255, fG);
                    };
                    if(((lhs).R) == 255) {
                        fR = 255;
                    } else {
                        int i = (255 - ((lhs).R)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fR = (int)(((((rhs).R) * ((rhs).R) * M) + A) >> (int)S);
                        ;
                        fR = Math.Min(255, fR);
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*dst).B) == 255) {
                            fB = 255;
                        } else {
                            int i = (255 - ((*dst).B)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)(((((*src).B) * ((*src).B) * M) + A) >> (int)S);
                            ;
                            fB = Math.Min(255, fB);
                        };
                        if(((*dst).G) == 255) {
                            fG = 255;
                        } else {
                            int i = (255 - ((*dst).G)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)(((((*src).G) * ((*src).G) * M) + A) >> (int)S);
                            ;
                            fG = Math.Min(255, fG);
                        };
                        if(((*dst).R) == 255) {
                            fR = 255;
                        } else {
                            int i = (255 - ((*dst).R)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)(((((*src).R) * ((*src).R) * M) + A) >> (int)S);
                            ;
                            fR = Math.Min(255, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*lhs).B) == 255) {
                            fB = 255;
                        } else {
                            int i = (255 - ((*lhs).B)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)(((((*rhs).B) * ((*rhs).B) * M) + A) >> (int)S);
                            ;
                            fB = Math.Min(255, fB);
                        };
                        if(((*lhs).G) == 255) {
                            fG = 255;
                        } else {
                            int i = (255 - ((*lhs).G)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)(((((*rhs).G) * ((*rhs).G) * M) + A) >> (int)S);
                            ;
                            fG = Math.Min(255, fG);
                        };
                        if(((*lhs).R) == 255) {
                            fR = 255;
                        } else {
                            int i = (255 - ((*lhs).R)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)(((((*rhs).R) * ((*rhs).R) * M) + A) >> (int)S);
                            ;
                            fR = Math.Min(255, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((lhs).B) == 255) {
                        fB = 255;
                    } else {
                        int i = (255 - ((lhs).B)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fB = (int)(((((rhs).B) * ((rhs).B) * M) + A) >> (int)S);
                        ;
                        fB = Math.Min(255, fB);
                    };
                    if(((lhs).G) == 255) {
                        fG = 255;
                    } else {
                        int i = (255 - ((lhs).G)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fG = (int)(((((rhs).G) * ((rhs).G) * M) + A) >> (int)S);
                        ;
                        fG = Math.Min(255, fG);
                    };
                    if(((lhs).R) == 255) {
                        fR = 255;
                    } else {
                        int i = (255 - ((lhs).R)) * 3;
                        uint M = masTable[i];
                        uint A = masTable[i + 1];
                        uint S = masTable[i + 2];
                        fR = (int)(((((rhs).R) * ((rhs).R) * M) + A) >> (int)S);
                        ;
                        fR = Math.Min(255, fR);
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new GlowBlendOpWithOpacity(opacity);
 sealed class GlowBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Glow" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((lhs).B) == 255) {
                            fB = 255;
                        } else {
                            int i = (255 - ((lhs).B)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fB = (int)(((((rhs).B) * ((rhs).B) * M) + A) >> (int)S);
                            ;
                            fB = Math.Min(255, fB);
                        };
                        if(((lhs).G) == 255) {
                            fG = 255;
                        } else {
                            int i = (255 - ((lhs).G)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fG = (int)(((((rhs).G) * ((rhs).G) * M) + A) >> (int)S);
                            ;
                            fG = Math.Min(255, fG);
                        };
                        if(((lhs).R) == 255) {
                            fR = 255;
                        } else {
                            int i = (255 - ((lhs).R)) * 3;
                            uint M = masTable[i];
                            uint A = masTable[i + 1];
                            uint S = masTable[i + 2];
                            fR = (int)(((((rhs).R) * ((rhs).R) * M) + A) >> (int)S);
                            ;
                            fR = Math.Min(255, fR);
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*dst).B) == 255) {
                                fB = 255;
                            } else {
                                int i = (255 - ((*dst).B)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fB = (int)(((((*src).B) * ((*src).B) * M) + A) >> (int)S);
                                ;
                                fB = Math.Min(255, fB);
                            };
                            if(((*dst).G) == 255) {
                                fG = 255;
                            } else {
                                int i = (255 - ((*dst).G)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fG = (int)(((((*src).G) * ((*src).G) * M) + A) >> (int)S);
                                ;
                                fG = Math.Min(255, fG);
                            };
                            if(((*dst).R) == 255) {
                                fR = 255;
                            } else {
                                int i = (255 - ((*dst).R)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fR = (int)(((((*src).R) * ((*src).R) * M) + A) >> (int)S);
                                ;
                                fR = Math.Min(255, fR);
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*lhs).B) == 255) {
                                fB = 255;
                            } else {
                                int i = (255 - ((*lhs).B)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fB = (int)(((((*rhs).B) * ((*rhs).B) * M) + A) >> (int)S);
                                ;
                                fB = Math.Min(255, fB);
                            };
                            if(((*lhs).G) == 255) {
                                fG = 255;
                            } else {
                                int i = (255 - ((*lhs).G)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fG = (int)(((((*rhs).G) * ((*rhs).G) * M) + A) >> (int)S);
                                ;
                                fG = Math.Min(255, fG);
                            };
                            if(((*lhs).R) == 255) {
                                fR = 255;
                            } else {
                                int i = (255 - ((*lhs).R)) * 3;
                                uint M = masTable[i];
                                uint A = masTable[i + 1];
                                uint S = masTable[i + 2];
                                fR = (int)(((((*rhs).R) * ((*rhs).R) * M) + A) >> (int)S);
                                ;
                                fR = Math.Min(255, fR);
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public GlowBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class OverlayBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Overlay" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((lhs).B) < 128) {
                        fB = ((2 * ((lhs).B)) * (((rhs).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        ;
                    } else {
                        fB = ((2 * (255 - ((lhs).B))) * (255 - ((rhs).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        ;
                        fB = 255 - fB;
                    };
                    if(((lhs).G) < 128) {
                        fG = ((2 * ((lhs).G)) * (((rhs).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        ;
                    } else {
                        fG = ((2 * (255 - ((lhs).G))) * (255 - ((rhs).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        ;
                        fG = 255 - fG;
                    };
                    if(((lhs).R) < 128) {
                        fR = ((2 * ((lhs).R)) * (((rhs).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        ;
                    } else {
                        fR = ((2 * (255 - ((lhs).R))) * (255 - ((rhs).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        ;
                        fR = 255 - fR;
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*dst).B) < 128) {
                            fB = ((2 * ((*dst).B)) * (((*src).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            ;
                        } else {
                            fB = ((2 * (255 - ((*dst).B))) * (255 - ((*src).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            ;
                            fB = 255 - fB;
                        };
                        if(((*dst).G) < 128) {
                            fG = ((2 * ((*dst).G)) * (((*src).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            ;
                        } else {
                            fG = ((2 * (255 - ((*dst).G))) * (255 - ((*src).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            ;
                            fG = 255 - fG;
                        };
                        if(((*dst).R) < 128) {
                            fR = ((2 * ((*dst).R)) * (((*src).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            ;
                        } else {
                            fR = ((2 * (255 - ((*dst).R))) * (255 - ((*src).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            ;
                            fR = 255 - fR;
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((*lhs).B) < 128) {
                            fB = ((2 * ((*lhs).B)) * (((*rhs).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            ;
                        } else {
                            fB = ((2 * (255 - ((*lhs).B))) * (255 - ((*rhs).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            ;
                            fB = 255 - fB;
                        };
                        if(((*lhs).G) < 128) {
                            fG = ((2 * ((*lhs).G)) * (((*rhs).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            ;
                        } else {
                            fG = ((2 * (255 - ((*lhs).G))) * (255 - ((*rhs).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            ;
                            fG = 255 - fG;
                        };
                        if(((*lhs).R) < 128) {
                            fR = ((2 * ((*lhs).R)) * (((*rhs).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            ;
                        } else {
                            fR = ((2 * (255 - ((*lhs).R))) * (255 - ((*rhs).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            ;
                            fR = 255 - fR;
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    if(((lhs).B) < 128) {
                        fB = ((2 * ((lhs).B)) * (((rhs).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        ;
                    } else {
                        fB = ((2 * (255 - ((lhs).B))) * (255 - ((rhs).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        ;
                        fB = 255 - fB;
                    };
                    if(((lhs).G) < 128) {
                        fG = ((2 * ((lhs).G)) * (((rhs).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        ;
                    } else {
                        fG = ((2 * (255 - ((lhs).G))) * (255 - ((rhs).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        ;
                        fG = 255 - fG;
                    };
                    if(((lhs).R) < 128) {
                        fR = ((2 * ((lhs).R)) * (((rhs).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        ;
                    } else {
                        fR = ((2 * (255 - ((lhs).R))) * (255 - ((rhs).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        ;
                        fR = 255 - fR;
                    };
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new OverlayBlendOpWithOpacity(opacity);
 sealed class OverlayBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Overlay" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        if(((lhs).B) < 128) {
                            fB = ((2 * ((lhs).B)) * (((rhs).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            ;
                        } else {
                            fB = ((2 * (255 - ((lhs).B))) * (255 - ((rhs).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            ;
                            fB = 255 - fB;
                        };
                        if(((lhs).G) < 128) {
                            fG = ((2 * ((lhs).G)) * (((rhs).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            ;
                        } else {
                            fG = ((2 * (255 - ((lhs).G))) * (255 - ((rhs).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            ;
                            fG = 255 - fG;
                        };
                        if(((lhs).R) < 128) {
                            fR = ((2 * ((lhs).R)) * (((rhs).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            ;
                        } else {
                            fR = ((2 * (255 - ((lhs).R))) * (255 - ((rhs).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            ;
                            fR = 255 - fR;
                        };
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*dst).B) < 128) {
                                fB = ((2 * ((*dst).B)) * (((*src).B)) + 0x80);
                                fB = ((((fB) >> 8) + (fB)) >> 8);
                                ;
                            } else {
                                fB = ((2 * (255 - ((*dst).B))) * (255 - ((*src).B)) + 0x80);
                                fB = ((((fB) >> 8) + (fB)) >> 8);
                                ;
                                fB = 255 - fB;
                            };
                            if(((*dst).G) < 128) {
                                fG = ((2 * ((*dst).G)) * (((*src).G)) + 0x80);
                                fG = ((((fG) >> 8) + (fG)) >> 8);
                                ;
                            } else {
                                fG = ((2 * (255 - ((*dst).G))) * (255 - ((*src).G)) + 0x80);
                                fG = ((((fG) >> 8) + (fG)) >> 8);
                                ;
                                fG = 255 - fG;
                            };
                            if(((*dst).R) < 128) {
                                fR = ((2 * ((*dst).R)) * (((*src).R)) + 0x80);
                                fR = ((((fR) >> 8) + (fR)) >> 8);
                                ;
                            } else {
                                fR = ((2 * (255 - ((*dst).R))) * (255 - ((*src).R)) + 0x80);
                                fR = ((((fR) >> 8) + (fR)) >> 8);
                                ;
                                fR = 255 - fR;
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            if(((*lhs).B) < 128) {
                                fB = ((2 * ((*lhs).B)) * (((*rhs).B)) + 0x80);
                                fB = ((((fB) >> 8) + (fB)) >> 8);
                                ;
                            } else {
                                fB = ((2 * (255 - ((*lhs).B))) * (255 - ((*rhs).B)) + 0x80);
                                fB = ((((fB) >> 8) + (fB)) >> 8);
                                ;
                                fB = 255 - fB;
                            };
                            if(((*lhs).G) < 128) {
                                fG = ((2 * ((*lhs).G)) * (((*rhs).G)) + 0x80);
                                fG = ((((fG) >> 8) + (fG)) >> 8);
                                ;
                            } else {
                                fG = ((2 * (255 - ((*lhs).G))) * (255 - ((*rhs).G)) + 0x80);
                                fG = ((((fG) >> 8) + (fG)) >> 8);
                                ;
                                fG = 255 - fG;
                            };
                            if(((*lhs).R) < 128) {
                                fR = ((2 * ((*lhs).R)) * (((*rhs).R)) + 0x80);
                                fR = ((((fR) >> 8) + (fR)) >> 8);
                                ;
                            } else {
                                fR = ((2 * (255 - ((*lhs).R))) * (255 - ((*rhs).R)) + 0x80);
                                fR = ((((fR) >> 8) + (fR)) >> 8);
                                ;
                                fR = 255 - fR;
                            };
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public OverlayBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class DifferenceBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Difference" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = Math.Abs(((rhs).B) - ((lhs).B));
                    ;
                    fG = Math.Abs(((rhs).G) - ((lhs).G));
                    ;
                    fR = Math.Abs(((rhs).R) - ((lhs).R));
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Abs(((*src).B) - ((*dst).B));
                        ;
                        fG = Math.Abs(((*src).G) - ((*dst).G));
                        ;
                        fR = Math.Abs(((*src).R) - ((*dst).R));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Abs(((*rhs).B) - ((*lhs).B));
                        ;
                        fG = Math.Abs(((*rhs).G) - ((*lhs).G));
                        ;
                        fR = Math.Abs(((*rhs).R) - ((*lhs).R));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = Math.Abs(((rhs).B) - ((lhs).B));
                    ;
                    fG = Math.Abs(((rhs).G) - ((lhs).G));
                    ;
                    fR = Math.Abs(((rhs).R) - ((lhs).R));
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new DifferenceBlendOpWithOpacity(opacity);
 sealed class DifferenceBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Difference" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Abs(((rhs).B) - ((lhs).B));
                        ;
                        fG = Math.Abs(((rhs).G) - ((lhs).G));
                        ;
                        fR = Math.Abs(((rhs).R) - ((lhs).R));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = Math.Abs(((*src).B) - ((*dst).B));
                            ;
                            fG = Math.Abs(((*src).G) - ((*dst).G));
                            ;
                            fR = Math.Abs(((*src).R) - ((*dst).R));
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = Math.Abs(((*rhs).B) - ((*lhs).B));
                            ;
                            fG = Math.Abs(((*rhs).G) - ((*lhs).G));
                            ;
                            fR = Math.Abs(((*rhs).R) - ((*lhs).R));
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public DifferenceBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class NegationBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Negation" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = (255 - Math.Abs(255 - ((lhs).B) - ((rhs).B)));
                    ;
                    fG = (255 - Math.Abs(255 - ((lhs).G) - ((rhs).G)));
                    ;
                    fR = (255 - Math.Abs(255 - ((lhs).R) - ((rhs).R)));
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = (255 - Math.Abs(255 - ((*dst).B) - ((*src).B)));
                        ;
                        fG = (255 - Math.Abs(255 - ((*dst).G) - ((*src).G)));
                        ;
                        fR = (255 - Math.Abs(255 - ((*dst).R) - ((*src).R)));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = (255 - Math.Abs(255 - ((*lhs).B) - ((*rhs).B)));
                        ;
                        fG = (255 - Math.Abs(255 - ((*lhs).G) - ((*rhs).G)));
                        ;
                        fR = (255 - Math.Abs(255 - ((*lhs).R) - ((*rhs).R)));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = (255 - Math.Abs(255 - ((lhs).B) - ((rhs).B)));
                    ;
                    fG = (255 - Math.Abs(255 - ((lhs).G) - ((rhs).G)));
                    ;
                    fR = (255 - Math.Abs(255 - ((lhs).R) - ((rhs).R)));
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new NegationBlendOpWithOpacity(opacity);
 sealed class NegationBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Negation" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = (255 - Math.Abs(255 - ((lhs).B) - ((rhs).B)));
                        ;
                        fG = (255 - Math.Abs(255 - ((lhs).G) - ((rhs).G)));
                        ;
                        fR = (255 - Math.Abs(255 - ((lhs).R) - ((rhs).R)));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = (255 - Math.Abs(255 - ((*dst).B) - ((*src).B)));
                            ;
                            fG = (255 - Math.Abs(255 - ((*dst).G) - ((*src).G)));
                            ;
                            fR = (255 - Math.Abs(255 - ((*dst).R) - ((*src).R)));
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = (255 - Math.Abs(255 - ((*lhs).B) - ((*rhs).B)));
                            ;
                            fG = (255 - Math.Abs(255 - ((*lhs).G) - ((*rhs).G)));
                            ;
                            fR = (255 - Math.Abs(255 - ((*lhs).R) - ((*rhs).R)));
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public NegationBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class LightenBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Lighten" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = Math.Max((lhs).B, (rhs).B);
                    ;
                    fG = Math.Max((lhs).G, (rhs).G);
                    ;
                    fR = Math.Max((lhs).R, (rhs).R);
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Max((*dst).B, (*src).B);
                        ;
                        fG = Math.Max((*dst).G, (*src).G);
                        ;
                        fR = Math.Max((*dst).R, (*src).R);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Max((*lhs).B, (*rhs).B);
                        ;
                        fG = Math.Max((*lhs).G, (*rhs).G);
                        ;
                        fR = Math.Max((*lhs).R, (*rhs).R);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = Math.Max((lhs).B, (rhs).B);
                    ;
                    fG = Math.Max((lhs).G, (rhs).G);
                    ;
                    fR = Math.Max((lhs).R, (rhs).R);
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new LightenBlendOpWithOpacity(opacity);
 sealed class LightenBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Lighten" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Max((lhs).B, (rhs).B);
                        ;
                        fG = Math.Max((lhs).G, (rhs).G);
                        ;
                        fR = Math.Max((lhs).R, (rhs).R);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = Math.Max((*dst).B, (*src).B);
                            ;
                            fG = Math.Max((*dst).G, (*src).G);
                            ;
                            fR = Math.Max((*dst).R, (*src).R);
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = Math.Max((*lhs).B, (*rhs).B);
                            ;
                            fG = Math.Max((*lhs).G, (*rhs).G);
                            ;
                            fR = Math.Max((*lhs).R, (*rhs).R);
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public LightenBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class DarkenBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Darken" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = Math.Min((lhs).B, (rhs).B);
                    ;
                    fG = Math.Min((lhs).G, (rhs).G);
                    ;
                    fR = Math.Min((lhs).R, (rhs).R);
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Min((*dst).B, (*src).B);
                        ;
                        fG = Math.Min((*dst).G, (*src).G);
                        ;
                        fR = Math.Min((*dst).R, (*src).R);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Min((*lhs).B, (*rhs).B);
                        ;
                        fG = Math.Min((*lhs).G, (*rhs).G);
                        ;
                        fR = Math.Min((*lhs).R, (*rhs).R);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = Math.Min((lhs).B, (rhs).B);
                    ;
                    fG = Math.Min((lhs).G, (rhs).G);
                    ;
                    fR = Math.Min((lhs).R, (rhs).R);
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new DarkenBlendOpWithOpacity(opacity);
 sealed class DarkenBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Darken" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = Math.Min((lhs).B, (rhs).B);
                        ;
                        fG = Math.Min((lhs).G, (rhs).G);
                        ;
                        fR = Math.Min((lhs).R, (rhs).R);
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = Math.Min((*dst).B, (*src).B);
                            ;
                            fG = Math.Min((*dst).G, (*src).G);
                            ;
                            fR = Math.Min((*dst).R, (*src).R);
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = Math.Min((*lhs).B, (*rhs).B);
                            ;
                            fG = Math.Min((*lhs).G, (*rhs).G);
                            ;
                            fR = Math.Min((*lhs).R, (*rhs).R);
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public DarkenBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class ScreenBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Screen" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = ((((rhs).B)) * (((lhs).B)) + 0x80);
                    fB = ((((fB) >> 8) + (fB)) >> 8);
                    fB = ((rhs).B) + ((lhs).B) - fB;
                    ;
                    fG = ((((rhs).G)) * (((lhs).G)) + 0x80);
                    fG = ((((fG) >> 8) + (fG)) >> 8);
                    fG = ((rhs).G) + ((lhs).G) - fG;
                    ;
                    fR = ((((rhs).R)) * (((lhs).R)) + 0x80);
                    fR = ((((fR) >> 8) + (fR)) >> 8);
                    fR = ((rhs).R) + ((lhs).R) - fR;
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = ((((*src).B)) * (((*dst).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        fB = ((*src).B) + ((*dst).B) - fB;
                        ;
                        fG = ((((*src).G)) * (((*dst).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        fG = ((*src).G) + ((*dst).G) - fG;
                        ;
                        fR = ((((*src).R)) * (((*dst).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        fR = ((*src).R) + ((*dst).R) - fR;
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = ((((*rhs).B)) * (((*lhs).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        fB = ((*rhs).B) + ((*lhs).B) - fB;
                        ;
                        fG = ((((*rhs).G)) * (((*lhs).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        fG = ((*rhs).G) + ((*lhs).G) - fG;
                        ;
                        fR = ((((*rhs).R)) * (((*lhs).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        fR = ((*rhs).R) + ((*lhs).R) - fR;
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = ((((rhs).B)) * (((lhs).B)) + 0x80);
                    fB = ((((fB) >> 8) + (fB)) >> 8);
                    fB = ((rhs).B) + ((lhs).B) - fB;
                    ;
                    fG = ((((rhs).G)) * (((lhs).G)) + 0x80);
                    fG = ((((fG) >> 8) + (fG)) >> 8);
                    fG = ((rhs).G) + ((lhs).G) - fG;
                    ;
                    fR = ((((rhs).R)) * (((lhs).R)) + 0x80);
                    fR = ((((fR) >> 8) + (fR)) >> 8);
                    fR = ((rhs).R) + ((lhs).R) - fR;
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new ScreenBlendOpWithOpacity(opacity);
 sealed class ScreenBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Screen" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = ((((rhs).B)) * (((lhs).B)) + 0x80);
                        fB = ((((fB) >> 8) + (fB)) >> 8);
                        fB = ((rhs).B) + ((lhs).B) - fB;
                        ;
                        fG = ((((rhs).G)) * (((lhs).G)) + 0x80);
                        fG = ((((fG) >> 8) + (fG)) >> 8);
                        fG = ((rhs).G) + ((lhs).G) - fG;
                        ;
                        fR = ((((rhs).R)) * (((lhs).R)) + 0x80);
                        fR = ((((fR) >> 8) + (fR)) >> 8);
                        fR = ((rhs).R) + ((lhs).R) - fR;
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = ((((*src).B)) * (((*dst).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            fB = ((*src).B) + ((*dst).B) - fB;
                            ;
                            fG = ((((*src).G)) * (((*dst).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            fG = ((*src).G) + ((*dst).G) - fG;
                            ;
                            fR = ((((*src).R)) * (((*dst).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            fR = ((*src).R) + ((*dst).R) - fR;
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = ((((*rhs).B)) * (((*lhs).B)) + 0x80);
                            fB = ((((fB) >> 8) + (fB)) >> 8);
                            fB = ((*rhs).B) + ((*lhs).B) - fB;
                            ;
                            fG = ((((*rhs).G)) * (((*lhs).G)) + 0x80);
                            fG = ((((fG) >> 8) + (fG)) >> 8);
                            fG = ((*rhs).G) + ((*lhs).G) - fG;
                            ;
                            fR = ((((*rhs).R)) * (((*lhs).R)) + 0x80);
                            fR = ((((fR) >> 8) + (fR)) >> 8);
                            fR = ((*rhs).R) + ((*lhs).R) - fR;
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public ScreenBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
        [Serializable]
        public sealed class XorBlendOp : UserBlendOp {
            public static string StaticName => "UserBlendOps." + "Xor" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = (((lhs).B) ^ ((rhs).B));
                    ;
                    fG = (((lhs).G) ^ ((rhs).G));
                    ;
                    fR = (((lhs).R) ^ ((rhs).R));
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*dst).A);
                    ;
                    int rhsA;
                    rhsA = ((*src).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = (((*dst).B) ^ ((*src).B));
                        ;
                        fG = (((*dst).G) ^ ((*src).G));
                        ;
                        fR = (((*dst).R) ^ ((*src).R));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++src;
                    --length;
                }
            }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                    int lhsA;
                    lhsA = ((*lhs).A);
                    ;
                    int rhsA;
                    rhsA = ((*rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = (((*lhs).B) ^ ((*rhs).B));
                        ;
                        fG = (((*lhs).G) ^ ((*rhs).G));
                        ;
                        fR = (((*lhs).R) ^ ((*rhs).R));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    dst->Bgra = ret;
                    ++dst;
                    ++lhs;
                    ++rhs;
                    --length;
                }
            }
 public static ColorBgra ApplyStatic(ColorBgra lhs, ColorBgra rhs) {
                int lhsA;
                lhsA = ((lhs).A);
                ;
                int rhsA;
                rhsA = ((rhs).A);
                ;
                int y;
                y = ((lhsA) * (255 - rhsA) + 0x80);
                y = ((((y) >> 8) + (y)) >> 8);
                ;
                int totalA = y + rhsA;
                uint ret;
                if(totalA == 0) {
                    ret = 0;
                } else {
                    int fB;
                    int fG;
                    int fR;
                    fB = (((lhs).B) ^ ((rhs).B));
                    ;
                    fG = (((lhs).G) ^ ((rhs).G));
                    ;
                    fR = (((lhs).R) ^ ((rhs).R));
                    ;
                    int x;
                    x = ((lhsA) * (rhsA) + 0x80);
                    x = ((((x) >> 8) + (x)) >> 8);
                    ;
                    int z = rhsA - x;
                    int masIndex = totalA * 3;
                    uint taM = masTable[masIndex];
                    uint taA = masTable[masIndex + 1];
                    uint taS = masTable[masIndex + 2];
                    uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                    uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                    uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                    int a;
                    a = ((lhsA) * (255 - (rhsA)) + 0x80);
                    a = ((((a) >> 8) + (a)) >> 8);
                    a += (rhsA);
                    ;
                    ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                };
                return ColorBgra.FromUInt32(ret);
            }
            public override UserBlendOp CreateWithOpacity(int opacity) => new XorBlendOpWithOpacity(opacity);
 sealed class XorBlendOpWithOpacity : UserBlendOp {
     int opacity;
 byte ApplyOpacity(byte a) {
                    int r;
                    r = (a);
                    ;
                    r = ((r) * (opacity) + 0x80);
                    r = ((((r) >> 8) + (r)) >> 8);
                    ;
                    return (byte)r;
                }
                public static string StaticName => "UserBlendOps." + "Xor" + "BlendOp.Name";
 public override ColorBgra Apply(ColorBgra lhs, ColorBgra rhs) {
                    int lhsA;
                    lhsA = ((lhs).A);
                    ;
                    int rhsA;
                    rhsA = ApplyOpacity((rhs).A);
                    ;
                    int y;
                    y = ((lhsA) * (255 - rhsA) + 0x80);
                    y = ((((y) >> 8) + (y)) >> 8);
                    ;
                    int totalA = y + rhsA;
                    uint ret;
                    if(totalA == 0) {
                        ret = 0;
                    } else {
                        int fB;
                        int fG;
                        int fR;
                        fB = (((lhs).B) ^ ((rhs).B));
                        ;
                        fG = (((lhs).G) ^ ((rhs).G));
                        ;
                        fR = (((lhs).R) ^ ((rhs).R));
                        ;
                        int x;
                        x = ((lhsA) * (rhsA) + 0x80);
                        x = ((((x) >> 8) + (x)) >> 8);
                        ;
                        int z = rhsA - x;
                        int masIndex = totalA * 3;
                        uint taM = masTable[masIndex];
                        uint taA = masTable[masIndex + 1];
                        uint taS = masTable[masIndex + 2];
                        uint b = (uint)(((((long)((((lhs).B * y) + ((rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                        uint g = (uint)(((((long)((((lhs).G * y) + ((rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                        uint r = (uint)(((((long)((((lhs).R * y) + ((rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                        int a;
                        a = ((lhsA) * (255 - (rhsA)) + 0x80);
                        a = ((((a) >> 8) + (a)) >> 8);
                        a += (rhsA);
                        ;
                        ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                    };
                    return ColorBgra.FromUInt32(ret);
                }
                public override unsafe void Apply(ColorBgra* dst, ColorBgra* src, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*dst).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*src).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = (((*dst).B) ^ ((*src).B));
                            ;
                            fG = (((*dst).G) ^ ((*src).G));
                            ;
                            fR = (((*dst).R) ^ ((*src).R));
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*dst).B * y) + ((*src).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*dst).G * y) + ((*src).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*dst).R * y) + ((*src).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++src;
                        --length;
                    }
                }
 public override unsafe void Apply(ColorBgra* dst, ColorBgra* lhs, ColorBgra* rhs, int length) {
     while(length > 0) {
                        int lhsA;
                        lhsA = ((*lhs).A);
                        ;
                        int rhsA;
                        rhsA = ApplyOpacity((*rhs).A);
                        ;
                        int y;
                        y = ((lhsA) * (255 - rhsA) + 0x80);
                        y = ((((y) >> 8) + (y)) >> 8);
                        ;
                        int totalA = y + rhsA;
                        uint ret;
                        if(totalA == 0) {
                            ret = 0;
                        } else {
                            int fB;
                            int fG;
                            int fR;
                            fB = (((*lhs).B) ^ ((*rhs).B));
                            ;
                            fG = (((*lhs).G) ^ ((*rhs).G));
                            ;
                            fR = (((*lhs).R) ^ ((*rhs).R));
                            ;
                            int x;
                            x = ((lhsA) * (rhsA) + 0x80);
                            x = ((((x) >> 8) + (x)) >> 8);
                            ;
                            int z = rhsA - x;
                            int masIndex = totalA * 3;
                            uint taM = masTable[masIndex];
                            uint taA = masTable[masIndex + 1];
                            uint taS = masTable[masIndex + 2];
                            uint b = (uint)(((((long)((((*lhs).B * y) + ((*rhs).B * z) + (fB * x)))) * taM) + taA) >> (int)taS);
                            uint g = (uint)(((((long)((((*lhs).G * y) + ((*rhs).G * z) + (fG * x)))) * taM) + taA) >> (int)taS);
                            uint r = (uint)(((((long)((((*lhs).R * y) + ((*rhs).R * z) + (fR * x)))) * taM) + taA) >> (int)taS);
                            int a;
                            a = ((lhsA) * (255 - (rhsA)) + 0x80);
                            a = ((((a) >> 8) + (a)) >> 8);
                            a += (rhsA);
                            ;
                            ret = b + (g << 8) + (r << 16) + ((uint)a << 24);
                        };
                        dst->Bgra = ret;
                        ++dst;
                        ++lhs;
                        ++rhs;
                        --length;
                    }
                }
 public XorBlendOpWithOpacity(int opacity) {
 if(this.opacity < 0 || this.opacity > 255) {
     throw new ArgumentOutOfRangeException();
 } this.opacity = opacity;
 }
 }
        }
    }
}
