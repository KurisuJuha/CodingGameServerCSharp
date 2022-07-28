using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace CodingGameBase
{
    public class DataWriter
    {
        private List<byte> data =new List<byte>();

        public byte[] GetData()
        {
            return data.ToArray();
        }

        #region ëgÇ›çûÇ›
        public void Put(bool value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }

        public void Put(byte value)
        {
            data.Add(value);
        }

        public void Put(char value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }

        public void Put(double value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }

        public void Put(float value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }

        public void Put(int value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }

        public void Put(uint value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }

        public void Put(long value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }

        public void Put(ulong value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }

        public void Put(short value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }
        public void Put(ushort value)
        {
            data.AddRange(BitConverter.GetBytes(value));
        }
        public void Put(string value)
        {
            byte[] _value = Encoding.UTF8.GetBytes(value);
            Put(_value);
        }

        #endregion

        #region îzóÒ
        public void Put(byte[] value)
        {
            Put(value.Length);
            data.AddRange(value);
        }
        #endregion

        public void Put(DataWriter value)
        {
            data.AddRange(value.GetData());
        }
    }
}