﻿/*
 * Copyright 2014 Google Inc. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

//#define UNSAFE_BYTEBUFFER  // uncomment this line to use faster ByteBuffer
//#define NOASSERT_BYTEBUFFER // define this to remove bounds checking from the Get/Put methods. Could be dangerous with UNSAFE_BYTEBUFFER

using System;

namespace FlatBuffers
{
    /// <summary>
    /// Class to mimic Java's ByteBuffer which is used heavily in Flatbuffers.
    /// If your execution environment allows unsafe code, you should enable
    /// unsafe code in your project and #define UNSAFE_BYTEBUFFER to use a
    /// MUCH faster version of ByteBuffer.
    /// </summary>
    public class ByteBuffer
    {
        private readonly byte[] _buffer;
        private int _pos;  // Must track start of the buffer.

        public int Length { get { return _buffer.Length; } }

        public byte[] Data { get { return _buffer; } }

        public ByteBuffer(byte[] buffer) : this(buffer, 0) { }

        public ByteBuffer(byte[] buffer, int pos)
        {
            _buffer = buffer;
            _pos = pos;
        }

        public int Position {
            get { return _pos; }
            set { _pos = value; }
        }

        public void Reset()
        {
            _pos = 0;
        }

        // Pre-allocated helper arrays for convertion.
        private float[] floathelper = new[] { 0.0f };
        private int[] inthelper = new[] { 0 };
        private double[] doublehelper = new[] { 0.0 };
        private ulong[] ulonghelper = new[] { 0UL };

        // Helper functions for the unsafe version.
        static public ushort ReverseBytes(ushort input)
        {
            return (ushort)(((input & 0x00FFU) << 8) |
                            ((input & 0xFF00U) >> 8));
        }
        static public uint ReverseBytes(uint input)
        {
            return ((input & 0x000000FFU) << 24) |
                   ((input & 0x0000FF00U) <<  8) |
                   ((input & 0x00FF0000U) >>  8) |
                   ((input & 0xFF000000U) >> 24);
        }
        static public ulong ReverseBytes(ulong input)
        {
            return (((input & 0x00000000000000FFUL) << 56) |
                    ((input & 0x000000000000FF00UL) << 40) |
                    ((input & 0x0000000000FF0000UL) << 24) |
                    ((input & 0x00000000FF000000UL) <<  8) |
                    ((input & 0x000000FF00000000UL) >>  8) |
                    ((input & 0x0000FF0000000000UL) >> 24) |
                    ((input & 0x00FF000000000000UL) >> 40) |
                    ((input & 0xFF00000000000000UL) >> 56));
        }

#if !UNSAFE_BYTEBUFFER
        // Helper functions for the safe (but slower) version.
        protected void WriteLittleEndian(int offset, int count, ulong data)
        {
            if (BitConverter.IsLittleEndian)
            {
                for (int i = 0; i < count; i++)
                {
                    _buffer[offset + i] = (byte)(data >> i * 8);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    _buffer[offset + count - 1 - i] = (byte)(data >> i * 8);
                }
            }
        }

        protected ulong ReadLittleEndian(int offset, int count)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, count);
#endif
            ulong r = 0;
            if (BitConverter.IsLittleEndian)
            {
                for (int i = 0; i < count; i++)
                {
                  r |= (ulong)_buffer[offset + i] << i * 8;
                }
            }
            else
            {
              for (int i = 0; i < count; i++)
              {
                r |= (ulong)_buffer[offset + count - 1 - i] << i * 8;
              }
            }
            return r;
        }
#endif // !UNSAFE_BYTEBUFFER

#if !NOASSERT_BYTEBUFFER
        private void AssertOffsetAndLength(int offset, int length)
        {
            if (offset < 0 ||
                offset >= _buffer.Length ||
                offset + length > _buffer.Length)
                throw new ArgumentOutOfRangeException();
        }
#endif

        public void PutSbyte(int offset, sbyte value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(sbyte));
#endif
            _buffer[offset] = (byte)value;
        }

        public void PutByte(int offset, byte value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(byte));
#endif
            _buffer[offset] = value;
        }
#if PERF_PAD_OPTIMIZATION
        public void PutByte(int offset, byte value, int count)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(byte) * count);
#endif
            for (var i = 0; i < count; ++i)
                _buffer[offset + i] = value;
        }
#endif
        // this method exists in order to conform with Java ByteBuffer standards
        public void Put(int offset, byte value)
        {
            PutByte(offset, value);
        }

#if UNSAFE_BYTEBUFFER
        // Unsafe but more efficient versions of Put*.
        public void PutShort(int offset, short value)
        {
            PutUshort(offset, (ushort)value);
        }

        public unsafe void PutUshort(int offset, ushort value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(ushort));
#endif
            fixed (byte* ptr = _buffer)
            {
                *(ushort*)(ptr + offset) = BitConverter.IsLittleEndian
                    ? value
                    : ReverseBytes(value);
            }
        }

        public void PutInt(int offset, int value)
        {
            PutUint(offset, (uint)value);
        }

        public unsafe void PutUint(int offset, uint value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(uint));
#endif
            fixed (byte* ptr = _buffer)
            {
                *(uint*)(ptr + offset) = BitConverter.IsLittleEndian
                    ? value
                    : ReverseBytes(value);
            }
        }

        public unsafe void PutLong(int offset, long value)
        {
            PutUlong(offset, (ulong)value);
        }

        public unsafe void PutUlong(int offset, ulong value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(ulong));
#endif
            fixed (byte* ptr = _buffer)
            {
                *(ulong*)(ptr + offset) = BitConverter.IsLittleEndian
                    ? value
                    : ReverseBytes(value);
            }
        }

        public unsafe void PutFloat(int offset, float value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(float));
#endif
            fixed (byte* ptr = _buffer)
            {
                if (BitConverter.IsLittleEndian)
                {
                    *(float*)(ptr + offset) = value;
                }
                else
                {
                    *(uint*)(ptr + offset) = ReverseBytes(*(uint*)(&value));
                }
            }
        }

        public unsafe void PutDouble(int offset, double value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(double));
#endif
            fixed (byte* ptr = _buffer)
            {
                if (BitConverter.IsLittleEndian)
                {
                    *(double*)(ptr + offset) = value;

                }
                else
                {
                    *(ulong*)(ptr + offset) = ReverseBytes(*(ulong*)(ptr + offset));
                }
            }
        }
#else // !UNSAFE_BYTEBUFFER
        // Slower versions of Put* for when unsafe code is not allowed.
        public void PutShort(int offset, short value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(short));
#endif
            WriteLittleEndian(offset, sizeof(short), (ulong)value);
        }

        public void PutUshort(int offset, ushort value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(ushort));
#endif

            WriteLittleEndian(offset, sizeof(ushort), (ulong)value);
        }

        public void PutInt(int offset, int value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(int));
#endif

            WriteLittleEndian(offset, sizeof(int), (ulong)value);
        }

        public void PutUint(int offset, uint value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(uint));
#endif

            WriteLittleEndian(offset, sizeof(uint), (ulong)value);
        }

        public void PutLong(int offset, long value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(long));
#endif

            WriteLittleEndian(offset, sizeof(long), (ulong)value);
        }

        public void PutUlong(int offset, ulong value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(ulong));
#endif

            WriteLittleEndian(offset, sizeof(ulong), value);
        }

        public void PutFloat(int offset, float value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(float));
#endif

            floathelper[0] = value;
            Buffer.BlockCopy(floathelper, 0, inthelper, 0, sizeof(float));
            WriteLittleEndian(offset, sizeof(float), (ulong)inthelper[0]);
        }

        public void PutDouble(int offset, double value)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(double));
#endif

            doublehelper[0] = value;
            Buffer.BlockCopy(doublehelper, 0, ulonghelper, 0, sizeof(double));
            WriteLittleEndian(offset, sizeof(double), ulonghelper[0]);
        }

#endif // UNSAFE_BYTEBUFFER

        public sbyte GetSbyte(int index)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(index, sizeof(sbyte));
#endif

            return (sbyte)_buffer[index];
        }

        public byte Get(int index)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(index, sizeof(byte));
#endif

            return _buffer[index];
        }

#if UNSAFE_BYTEBUFFER
        // Unsafe but more efficient versions of Get*.
        public short GetShort(int offset)
        {
            return (short)GetUshort(offset);
        }

        public unsafe ushort GetUshort(int offset)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(ushort));
#endif

            fixed (byte* ptr = _buffer)
            {
                return BitConverter.IsLittleEndian
                    ? *(ushort*)(ptr + offset)
                    : ReverseBytes(*(ushort*)(ptr + offset));
            }
        }

        public int GetInt(int offset)
        {
            return (int)GetUint(offset);
        }

        public unsafe uint GetUint(int offset)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(uint));
#endif

            fixed (byte* ptr = _buffer)
            {
                return BitConverter.IsLittleEndian
                    ? *(uint*)(ptr + offset)
                    : ReverseBytes(*(uint*)(ptr + offset));
            }
        }

        public long GetLong(int offset)
        {
            return (long)GetUlong(offset);
        }

        public unsafe ulong GetUlong(int offset)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(ulong));
#endif

            fixed (byte* ptr = _buffer)
            {
                return BitConverter.IsLittleEndian
                    ? *(ulong*)(ptr + offset)
                    : ReverseBytes(*(ulong*)(ptr + offset));
            }
        }

        public unsafe float GetFloat(int offset)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(float));
#endif

            fixed (byte* ptr = _buffer)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return *(float*)(ptr + offset);
                }
                else
                {
                    uint uvalue = ReverseBytes(*(uint*)(ptr + offset));
                    return *(float*)(&uvalue);
                }
            }
        }

        public unsafe double GetDouble(int offset)
        {
#if !NOASSERT_BYTEBUFFER
            AssertOffsetAndLength(offset, sizeof(double));
#endif

            fixed (byte* ptr = _buffer)
            {
                if (BitConverter.IsLittleEndian)
                {
                    return *(double*)(ptr + offset);
                }
                else
                {
                    ulong uvalue = ReverseBytes(*(ulong*)(ptr + offset));
                    return *(double*)(&uvalue);
                }
            }
        }
#else // !UNSAFE_BYTEBUFFER
        // Slower versions of Get* for when unsafe code is not allowed.
        public short GetShort(int index)
        {
            return (short)ReadLittleEndian(index, sizeof(short));
        }

        public ushort GetUshort(int index)
        {
            return (ushort)ReadLittleEndian(index, sizeof(ushort));
        }

        public int GetInt(int index)
        {
            return (int)ReadLittleEndian(index, sizeof(int));
        }

        public uint GetUint(int index)
        {
            return (uint)ReadLittleEndian(index, sizeof(uint));
        }

        public long GetLong(int index)
        {
           return (long)ReadLittleEndian(index, sizeof(long));
        }

        public ulong GetUlong(int index)
        {
            return ReadLittleEndian(index, sizeof(ulong));
        }

        public float GetFloat(int index)
        {
            int i = (int)ReadLittleEndian(index, sizeof(float));
            inthelper[0] = i;
            Buffer.BlockCopy(inthelper, 0, floathelper, 0, sizeof(float));
            return floathelper[0];
        }

        public double GetDouble(int index)
        {
            ulong i = ReadLittleEndian(index, sizeof(double));
            // There's Int64BitsToDouble but it uses unsafe code internally.
            ulonghelper[0] = i;
            Buffer.BlockCopy(ulonghelper, 0, doublehelper, 0, sizeof(double));
            return doublehelper[0];
        }
#endif // UNSAFE_BYTEBUFFER
    }
}
