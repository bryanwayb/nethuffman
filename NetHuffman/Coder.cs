using System;
using System.Collections.Generic;

namespace NetHuffman
{
    public class Coder
    {
        public Coder(Tree tree)
        {
            _Tree = tree;
        }

        private Tree _Tree = null;

        public byte[] Encode(byte[] buffer)
        {
            List<byte> ret = new List<byte>();

            unchecked
            {
                int carryOverBits = 0,
                totalBytes,
                currentBits,
                bufferIndex = -1,
                adjust;
                byte byteValue;
                bool shiftDirection;
                Tree.ForwardLookupEntry entry;

                foreach (byte b in buffer)
                {
                    entry = _Tree.ForwardLookup[b];
                    currentBits = entry.CurrentBits;
                    totalBytes = entry.TotalBytes;

                    do
                    {
                        if (currentBits > 0)
                        {
                            byteValue = (byte)(entry.Value >> totalBytes);

                            shiftDirection = currentBits > carryOverBits;
                            adjust = shiftDirection ? currentBits - carryOverBits : carryOverBits - currentBits;

                            if (carryOverBits > 0)
                            {
                                if (shiftDirection)
                                {
                                    ret[bufferIndex] = (byte)(ret[bufferIndex] | ((byteValue & (0xFF >> (8 - currentBits))) >> adjust));
                                }
                                else
                                {
                                    ret[bufferIndex] = (byte)(ret[bufferIndex] | ((byteValue & (0xFF >> (8 - currentBits))) << adjust));
                                }
                            }

                            if (shiftDirection)
                            {
                                ret.Add((byte)(byteValue << (carryOverBits = 8 - adjust)));
                                bufferIndex++;
                            }
                            else
                            {
                                carryOverBits = adjust;
                            }
                        }

                        if (totalBytes > 0)
                        {
                            currentBits = 8;
                        }
                    } while ((totalBytes -= 8) >= 0);
                }

                if (_Tree.Padding)
                {
                    ret[bufferIndex] = (byte)(ret[bufferIndex] | (0xFF >> (8 - carryOverBits)));
                }
            }

            return ret.ToArray();
        }

        public byte[] Decode(byte[] buffer)
        {
            List<byte> ret = new List<byte>();

            unchecked
            {
                uint RemainingBufferLength = (uint)buffer.Length << 3;
                int bufferIndex = 0;
                byte carryOverByte = 0;
                int carryOverBitlen = 0;

                bool keepSearching = false,
                    processSearch = false;
                byte bestMatchIndex = 0;
                byte bestMatchValue = 0;
                for (byte i = _Tree.MinBitLen; ; i++)
                {
                    if (i > _Tree.MaxBitLen)
                    {
                        if (keepSearching)
                        {
                            processSearch = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (i <= RemainingBufferLength)
                    {
                        if (_Tree.ReverseLookup.ContainsKey(i))
                        {
                            int tmpBufferIndex = bufferIndex;
                            byte tmpCarryOverByte = carryOverByte;
                            int tmpCarryOverBitlen = carryOverBitlen;
                            int countOfBits = i;
                            uint valueIndex = 0;
                            while (countOfBits > 0)
                            {
                                int countToRead = 8;
                                if (countOfBits < countToRead)
                                {
                                    countToRead = countOfBits;
                                }

                                valueIndex <<= countToRead;

                                byte returnByte = 0;

                                if (tmpCarryOverBitlen >= countToRead)
                                {
                                    returnByte = (byte)(tmpCarryOverByte >> (8 - countToRead));
                                    tmpCarryOverByte <<= countToRead;
                                    tmpCarryOverBitlen -= countToRead;
                                }
                                else
                                {
                                    byte nextByte = buffer[tmpBufferIndex++];
                                    int offset = countToRead - tmpCarryOverBitlen;
                                    returnByte = (byte)((tmpCarryOverByte >> (8 - countToRead)) | (nextByte >> (Math.Abs(offset - 8))));
                                    tmpCarryOverByte = (byte)(nextByte << offset);
                                    tmpCarryOverBitlen = 8 - offset;
                                }

                                valueIndex |= returnByte;
                                countOfBits -= countToRead;
                            }

                            if (_Tree.ReverseLookup[i].ContainsKey(valueIndex))
                            {
                                Tree.ReverseLookupEntry entry = _Tree.ReverseLookup[i][valueIndex];
                                bestMatchIndex = i;
                                bestMatchValue = entry.Value;
                                if (entry.Collides)
                                {
                                    keepSearching = true;
                                }
                                else
                                {
                                    processSearch = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (keepSearching)
                        {
                            processSearch = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (processSearch)
                    {
                        int countOfBits = bestMatchIndex;
                        while (countOfBits > 0)
                        {
                            int countToRead = 8;
                            if (countOfBits < countToRead)
                            {
                                countToRead = countOfBits;
                            }

                            RemainingBufferLength -= (uint)countToRead;
                            if (carryOverBitlen >= countToRead)
                            {
                                carryOverByte <<= countToRead;
                                carryOverBitlen -= countToRead;
                            }
                            else
                            {
                                int offset = countToRead - carryOverBitlen;
                                carryOverByte = (byte)(buffer[bufferIndex++] << offset);
                                carryOverBitlen = 8 - offset;
                            }

                            countOfBits -= countToRead;
                        }

                        ret.Add(bestMatchValue);
                        i = (byte)(_Tree.MinBitLen - 1);
                        processSearch = keepSearching = false;
                    }
                }
            }

            return ret.ToArray();
        }
    }
}