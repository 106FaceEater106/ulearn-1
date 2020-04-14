using System;

namespace func.brainfuck
{ 
	public class BrainfuckBasicCommands
    {
        private const string ConstantSymbols = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";

		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
            unchecked
            {
                vm.RegisterCommand('.', b => write((char)b.Memory[b.MemoryPointer]));
                vm.RegisterCommand('+', b => b.Memory[b.MemoryPointer]++);
                vm.RegisterCommand('-', b => b.Memory[b.MemoryPointer]--);
                vm.RegisterCommand('>', b => b.MemoryPointer = Mod(b.MemoryPointer + 1, b.Memory.Length));
                vm.RegisterCommand('<', b => b.MemoryPointer = Mod(b.MemoryPointer - 1, b.Memory.Length));
                vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = (byte)read());

                foreach (var c in ConstantSymbols)
                {
                    vm.RegisterCommand(c, b => b.Memory[b.MemoryPointer] = Convert.ToByte(c));
                }
            }
        }

        private static int Mod(int n, int m)
        {
            return ((n % m) + m) % m;
        }
    }
}