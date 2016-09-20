﻿using NetHuffman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal static class HuffmanTableDefinition
    {
        public static Tree Basic_abcd = new Tree(new Dictionary<byte, Dictionary<uint, byte>>()
        {
            [2] = new Dictionary<uint, byte>()
            {
                [0] = 97,
                [1] = 98,
                [2] = 99,
                [3] = 100
            }
        });

        public static Tree HPACK = new Tree(new Dictionary<byte, Dictionary<uint, byte>>()
        {
            [13] = new Dictionary<uint, byte>()
            {
                [8184] = 0,
                [8185] = 36,
                [8186] = 64,
                [8187] = 91,
                [8188] = 93,
                [8189] = 126,
            },
            [23] = new Dictionary<uint, byte>()
            {
                [8388568] = 1,
                [8388569] = 135,
                [8388570] = 137,
                [8388571] = 138,
                [8388572] = 139,
                [8388573] = 140,
                [8388574] = 141,
                [8388575] = 143,
                [8388576] = 147,
                [8388577] = 149,
                [8388578] = 150,
                [8388579] = 151,
                [8388580] = 152,
                [8388581] = 155,
                [8388582] = 157,
                [8388583] = 158,
                [8388584] = 165,
                [8388585] = 166,
                [8388586] = 168,
                [8388587] = 174,
                [8388588] = 175,
                [8388589] = 180,
                [8388590] = 182,
                [8388591] = 183,
                [8388592] = 188,
                [8388593] = 191,
                [8388594] = 197,
                [8388595] = 231,
                [8388596] = 239,
            },
            [28] = new Dictionary<uint, byte>()
            {
                [268435426] = 2,
                [268435427] = 3,
                [268435428] = 4,
                [268435429] = 5,
                [268435430] = 6,
                [268435431] = 7,
                [268435432] = 8,
                [268435433] = 11,
                [268435434] = 12,
                [268435435] = 14,
                [268435436] = 15,
                [268435437] = 16,
                [268435438] = 17,
                [268435439] = 18,
                [268435440] = 19,
                [268435441] = 20,
                [268435442] = 21,
                [268435443] = 23,
                [268435444] = 24,
                [268435445] = 25,
                [268435446] = 26,
                [268435447] = 27,
                [268435448] = 28,
                [268435449] = 29,
                [268435450] = 30,
                [268435451] = 31,
                [268435452] = 127,
                [268435453] = 220,
                [268435454] = 249,
            },
            [24] = new Dictionary<uint, byte>()
            {
                [16777194] = 9,
                [16777195] = 142,
                [16777196] = 144,
                [16777197] = 145,
                [16777198] = 148,
                [16777199] = 159,
                [16777200] = 171,
                [16777201] = 206,
                [16777202] = 215,
                [16777203] = 225,
                [16777204] = 236,
                [16777205] = 237,
            },
            [30] = new Dictionary<uint, byte>()
            {
                [268435452] = 10,
                [268435453] = 13,
                [268435454] = 22,
                [1073741823] = 0,
            },
            [6] = new Dictionary<uint, byte>()
            {
                [20] = 32,
                [21] = 37,
                [22] = 45,
                [23] = 46,
                [24] = 47,
                [25] = 51,
                [26] = 52,
                [27] = 53,
                [28] = 54,
                [29] = 55,
                [30] = 56,
                [31] = 57,
                [32] = 61,
                [33] = 65,
                [34] = 95,
                [35] = 98,
                [36] = 100,
                [37] = 102,
                [38] = 103,
                [39] = 104,
                [40] = 108,
                [41] = 109,
                [42] = 110,
                [43] = 112,
                [44] = 114,
                [45] = 117,
            },
            [10] = new Dictionary<uint, byte>()
            {
                [1016] = 33,
                [1017] = 34,
                [1018] = 40,
                [1019] = 41,
                [1020] = 63,
            },
            [12] = new Dictionary<uint, byte>()
            {
                [4090] = 35,
                [4091] = 62,
            },
            [8] = new Dictionary<uint, byte>()
            {
                [248] = 38,
                [249] = 42,
                [250] = 44,
                [251] = 59,
                [252] = 88,
                [253] = 90,
            },
            [11] = new Dictionary<uint, byte>()
            {
                [2042] = 39,
                [2043] = 43,
                [2044] = 124,
            },
            [5] = new Dictionary<uint, byte>()
            {
                [0] = 48,
                [1] = 49,
                [2] = 50,
                [3] = 97,
                [4] = 99,
                [5] = 101,
                [6] = 105,
                [7] = 111,
                [8] = 115,
                [9] = 116,
            },
            [7] = new Dictionary<uint, byte>()
            {
                [92] = 58,
                [93] = 66,
                [94] = 67,
                [95] = 68,
                [96] = 69,
                [97] = 70,
                [98] = 71,
                [99] = 72,
                [100] = 73,
                [101] = 74,
                [102] = 75,
                [103] = 76,
                [104] = 77,
                [105] = 78,
                [106] = 79,
                [107] = 80,
                [108] = 81,
                [109] = 82,
                [110] = 83,
                [111] = 84,
                [112] = 85,
                [113] = 86,
                [114] = 87,
                [115] = 89,
                [116] = 106,
                [117] = 107,
                [118] = 113,
                [119] = 118,
                [120] = 119,
                [121] = 120,
                [122] = 121,
                [123] = 122,
            },
            [15] = new Dictionary<uint, byte>()
            {
                [32764] = 60,
                [32765] = 96,
                [32766] = 123,
            },
            [19] = new Dictionary<uint, byte>()
            {
                [524272] = 92,
                [524273] = 195,
                [524274] = 208,
            },
            [14] = new Dictionary<uint, byte>()
            {
                [16380] = 94,
                [16381] = 125,
            },
            [20] = new Dictionary<uint, byte>()
            {
                [1048550] = 128,
                [1048551] = 130,
                [1048552] = 131,
                [1048553] = 162,
                [1048554] = 184,
                [1048555] = 194,
                [1048556] = 224,
                [1048557] = 226,
            },
            [22] = new Dictionary<uint, byte>()
            {
                [4194258] = 129,
                [4194259] = 132,
                [4194260] = 133,
                [4194261] = 134,
                [4194262] = 136,
                [4194263] = 146,
                [4194264] = 154,
                [4194265] = 156,
                [4194266] = 160,
                [4194267] = 163,
                [4194268] = 164,
                [4194269] = 169,
                [4194270] = 170,
                [4194271] = 173,
                [4194272] = 178,
                [4194273] = 181,
                [4194274] = 185,
                [4194275] = 186,
                [4194276] = 187,
                [4194277] = 189,
                [4194278] = 190,
                [4194279] = 196,
                [4194280] = 198,
                [4194281] = 228,
                [4194282] = 232,
                [4194283] = 233,
            },
            [21] = new Dictionary<uint, byte>()
            {
                [2097116] = 153,
                [2097117] = 161,
                [2097118] = 167,
                [2097119] = 172,
                [2097120] = 176,
                [2097121] = 177,
                [2097122] = 179,
                [2097123] = 209,
                [2097124] = 216,
                [2097125] = 217,
                [2097126] = 227,
                [2097127] = 229,
                [2097128] = 230,
            },
            [26] = new Dictionary<uint, byte>()
            {
                [67108832] = 192,
                [67108833] = 193,
                [67108834] = 200,
                [67108835] = 201,
                [67108836] = 202,
                [67108837] = 205,
                [67108838] = 210,
                [67108839] = 213,
                [67108840] = 218,
                [67108841] = 219,
                [67108842] = 238,
                [67108843] = 240,
                [67108844] = 242,
                [67108845] = 243,
                [67108846] = 255,
            },
            [25] = new Dictionary<uint, byte>()
            {
                [33554412] = 199,
                [33554413] = 207,
                [33554414] = 234,
                [33554415] = 235,
            },
            [27] = new Dictionary<uint, byte>()
            {
                [134217694] = 203,
                [134217695] = 204,
                [134217696] = 211,
                [134217697] = 212,
                [134217698] = 214,
                [134217699] = 221,
                [134217700] = 222,
                [134217701] = 223,
                [134217702] = 241,
                [134217703] = 244,
                [134217704] = 245,
                [134217705] = 246,
                [134217706] = 247,
                [134217707] = 248,
                [134217708] = 250,
                [134217709] = 251,
                [134217710] = 252,
                [134217711] = 253,
                [134217712] = 254,
            },
        })
        {
            Padding = true
        };
    }
}
