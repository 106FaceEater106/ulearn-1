using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Compatibility;

namespace hashes
{
    public class ReadonlyBytes
    {
        private readonly byte[] bytes;
        public int Length => bytes.Length;
        private readonly int myHash;

        public ReadonlyBytes(params byte[] bytes)
        {
            this.bytes = bytes ?? throw new ArgumentNullException();

            myHash = -2128831035;
            foreach (var b in bytes)
            {
                myHash ^= b;
                myHash = unchecked(myHash *  16777619);
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReadonlyBytes) || obj.GetType().IsSubclassOf(typeof(ReadonlyBytes))) return false;
            var readonlyBytes = (ReadonlyBytes) obj;
            if (Length != readonlyBytes.Length) return false;

            for (var i = 0; i < Length; i++)
            {
                if (this[i] != readonlyBytes[i]) return false; 
            }

            return true;
        }

        public override int GetHashCode()
        {
            return myHash;
        }


        internal byte this[int index]
        {
            get => bytes[index];
            set => bytes[index] = value;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (var i = 0; i < Length; i++)
                yield return bytes[i];
        }

        public override string ToString()
        {
            if (Length == 0) return "[]";
            var builder = new StringBuilder();
            builder.Append("[" + this[0]);
            for (var i = 1; i < Length; i++)
            {
                builder.Append(", " + this[i]);
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}