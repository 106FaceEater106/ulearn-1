using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnitLite;

namespace hashes
{
	class Program
	{
		static void Main(string[] args)
		{
            CheckCollisions(GetHash, "OnlyBitwise");
            CheckCollisions(GetHash2, "MyNaiveHash");
            CheckCollisions(GetHash3, "Numeric");
            CheckCollisions(GetHash4, "SmallNumeric");
            CheckCollisions(GetHash5, "Silly");
            CheckCollisions(GetHash6, "Dunno");
            CheckCollisions(GetHash7, "FNV");
            Console.ReadKey();
        }

        [MethodImpl(MethodImplOptions.NoOptimization)] 
        public static void CheckCollisions(Func<byte[], int> hashFunc, string name)
        {
            Stopwatch watch = new Stopwatch();
            Random rand = new Random();
            int collisionCount = 0;
            HashSet<int> hashsets = new HashSet<int>();
            HashSet<byte[] > allSets = new HashSet<byte[]>();

            watch.Start();
            for (int i = 0; i < 100000; i++)
            {
                var newListCount = rand.Next(100);

                byte[] array = new byte[newListCount];

                for (int j = 0; j < newListCount; j++)
                {
                    array[j] = (byte)rand.Next(256);
                }

                allSets.Add(array);
            }

            foreach (var set in allSets)
            {
                var hash = hashFunc(set);
                if (hashsets.Contains(hash)) collisionCount++;
                hashsets.Add(hash); 
            }

            watch.Stop();
            Console.WriteLine("Collisions for \"{0}\": {1}.   Elapsed: {2}", name, collisionCount, watch.ElapsedMilliseconds);
        }

        public static int GetHash(params byte[] by)
        {
            var myHash = 0;
            foreach (var b in by)
            {
                myHash = (myHash<<5) ^ b;
            }

            return myHash;
        }

        public static int GetHash2(params byte[] by)
        {
            long myHash = 0;
            foreach (var b in by)
            {
                myHash = (myHash<<3) + b;
                if (myHash > int.MaxValue) 
                    myHash %= int.MaxValue;
            }

            return (int)myHash;
        }

        public static int GetHash3(params byte[] by)
        {
            long myHash = 0;
            foreach (var b in by)
            {
                myHash = myHash*257 + b;
                if (myHash > int.MaxValue) 
                    myHash %= int.MaxValue;
            }

            return (int)myHash;
        }

        public static int GetHash4(params byte[] by)
        {
            long myHash = 0;
            foreach (var b in by)
            {
                myHash = myHash*3 + b;
                if (myHash > int.MaxValue) 
                    myHash %= int.MaxValue;
            }

            return (int)myHash;
        }

        public static int GetHash5(params byte[] by)
        {
            long myHash = 0;
            foreach (var b in by)
            {
                myHash += b;
                if (myHash > int.MaxValue) 
                    myHash %= int.MaxValue;
            }

            return (int)myHash;
        }

        public static int GetHash6(params byte[] by)
        {
            long myHash = 0;
            foreach (var b in by)
            {
                myHash = (myHash<<1) + b;
                if (myHash > int.MaxValue) 
                    myHash %= int.MaxValue;
            }

            return (int)myHash;
        }

        public static int GetHash7(params byte[] by)
        {
            var myHash = -2128831035;
            foreach (var b in by)
            {
                myHash ^= b;
                myHash = unchecked(myHash *  16777619);
            }

            return myHash;
        }
	}
}
