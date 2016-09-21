using System;
using System.Collections.Generic;

namespace NetHuffman
{
    public class Tree
    {
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
            List<byte> clist = new List<byte>();
            for (byte i = 0; i < 255; i++)
            {
                byte[] arr = test.Decode(test.Encode(new byte[1] { i }));
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
    }
}