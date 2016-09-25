using System;
using System.Collections.Generic;
using System.Text;

namespace NetHuffman
{
    public class Tree
    {
        public Tree()
        {
        }

        public Tree(Dictionary<byte, Dictionary<uint, byte>> lookup)
        {
            Init(lookup);
        }

        public bool Padding = false;

        public Dictionary<byte, Dictionary<uint, ReverseLookupEntry>> ReverseLookup { private set; get; }

        public ForwardLookupEntry[] ForwardLookup { private set; get; }

        public byte MinBitLen { private set; get; }

        public byte MaxBitLen { private set; get; }

        private void Init(Dictionary<byte, Dictionary<uint, byte>> lookup)
        {
            if (lookup == null)
            {
                throw new InvalidOperationException("No lookup table has been created. Either provide one or generate it");
            }

            MinBitLen = 32;
            MaxBitLen = 0;
            foreach (byte v in lookup.Keys)
            {
                if (v < MinBitLen)
                {
                    MinBitLen = v;
                }
                if (v > MaxBitLen)
                {
                    MaxBitLen = v;
                }
            }

            ForwardLookup = new ForwardLookupEntry[256];
            ReverseLookup = new Dictionary<byte, Dictionary<uint, ReverseLookupEntry>>();
            foreach (KeyValuePair<byte, Dictionary<uint, byte>> kv in lookup)
            {
                Dictionary<uint, ReverseLookupEntry> entry = new Dictionary<uint, ReverseLookupEntry>();
                foreach (KeyValuePair<uint, byte> kkv in kv.Value)
                {
                    ForwardLookup[kkv.Value] = new ForwardLookupEntry()
                    {
                        Value = kkv.Key,
                        BitLength = kv.Key,
                        CurrentBits = (byte)(kv.Key % 8),
                        TotalBytes = (byte)(kv.Key >> 3 << 3)
                    };
                    entry.Add(kkv.Key, new ReverseLookupEntry()
                    {
                        Value = kkv.Value,
                        Collides = false
                    });
                }
                ReverseLookup.Add(kv.Key, entry);
            }

            Coder test = new Coder(this);
            HashSet<byte> clist = new HashSet<byte>();
            for (byte i = 0; i < 255; i++)
            {
                byte[] arr;
                test.Decode(test.Encode(new byte[1] { i }), out arr);
                if (arr.Length > 0)
                {
                    byte t = arr[0];
                    if (t != i)
                    {
                        if (!clist.Contains(t))
                        {
                            clist.Add(t);
                        }

                        if (!clist.Contains(i))
                        {
                            clist.Add(i);
                        }
                    }
                }
            }

            foreach (byte b in clist)
            {
                ForwardLookupEntry entry = ForwardLookup[b];
                ReverseLookup[(byte)entry.BitLength][entry.Value].Collides = true;
            }
        }

        public void BuildDictionary(byte[] sample)
        {
            Dictionary<byte, uint> freq = new Dictionary<byte, uint>();
            foreach (byte b in sample)
            {
                if (freq.ContainsKey(b))
                {
                    freq[b]++;
                }
                else
                {
                    freq.Add(b, 0); // Start at 0, this value is relative
                }
            }

            List<KeyValuePair<byte, uint>> sortingList = new List<KeyValuePair<byte, uint>>(freq.Count);
            foreach (KeyValuePair<byte, uint> kv in freq)
            {
                sortingList.Add(kv);
            }

            sortingList.Sort(DictionaryListComparer.Instance);

            HashSet<uint> usedValues = new HashSet<uint>();
            Dictionary<byte, Dictionary<uint, byte>> lookup = new Dictionary<byte, Dictionary<uint, byte>>();

            byte currentbitlen;
            if(Math.Max(Math.Ceiling(Math.Log(freq.Count) / Math.Log(2)), 1) > 8)
            {
                currentbitlen = 5;
            }
            else
            {
                for (currentbitlen = 1; currentbitlen < 8 && freq.Count > ((1 << (currentbitlen + 1)) / 2) + (((1 << (currentbitlen + 1)) - ((1 << (currentbitlen + 1)) / 2)) / 2); currentbitlen++) ;
            }

            uint value = 0;
            sbyte shiftIndex = -1;
            byte shiftedBits = 1;
            bool shouldInvert = false;
            int possibleUniques = 0x1 << currentbitlen;
            int index = 0;
            unchecked
            {
                foreach (KeyValuePair<byte, uint> kv in sortingList)
                {
                    for (;;)
                    {
                        if(index == possibleUniques)
                        {
                            currentbitlen++;
                            shiftIndex = -1;
                            shiftedBits = 0;
                            shouldInvert = false;
                        }

                        uint mask = (~((uint)0x0) >> (32 - currentbitlen));
                        if (shouldInvert)
                        {
                            value = ~value & mask;
                            shouldInvert = false;
                            if(++shiftIndex == currentbitlen)
                            {
                                shiftedBits++;
                                shiftIndex = 0;
                            }
                        }
                        else
                        {
                            value = (0x0 | ~((uint)0x0) >> (32 - shiftedBits) << shiftIndex) & mask;
                            shouldInvert = true;
                        }

                        index++;

                        if (!usedValues.Contains(value))
                        {
                            usedValues.Add(value);
                            if(lookup.ContainsKey(currentbitlen))
                            {
                                lookup[currentbitlen].Add(value, kv.Key);
                            }
                            else
                            {
                                lookup.Add(currentbitlen, new Dictionary<uint, byte>()
                                {
                                    [value] = kv.Key
                                });
                            }
                            break;
                        }
                    }
                }
            }

            Init(lookup);
        }

        public void BuildDictionary(string sample)
        {
            BuildDictionary(Encoding.UTF8.GetBytes(sample));
        }

        public class ReverseLookupEntry
        {
            public byte Value;
            public bool Collides;
        }

        public struct ForwardLookupEntry
        {
            public uint Value;
            public byte BitLength;
            public byte CurrentBits;
            public byte TotalBytes;
        }

        private class DictionaryListComparer : Comparer<KeyValuePair<byte, uint>>
        {
            public static DictionaryListComparer Instance = new DictionaryListComparer();

            public override int Compare(KeyValuePair<byte, uint> x, KeyValuePair<byte, uint> y)
            {
                return y.Value.CompareTo(x.Value);
            }
        }
    }
}