using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public VirtualMachine(string program, int memorySize)
        {
            Memory = new byte[memorySize];
            MemoryPointer = 0;
            InstructionPointer = 0;
            Instructions = program;
			commands = new Dictionary<char, Action<IVirtualMachine>>();
        }

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			commands.Add(symbol, execute);
		}

		private readonly Dictionary<char, Action<IVirtualMachine>> commands;
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
        public int MemoryPointer { get ; set; }
		public void Run()
        {
            while (InstructionPointer < Instructions.Length)
            {
				if(commands.ContainsKey(Instructions[InstructionPointer]))
				    commands[Instructions[InstructionPointer]].Invoke(this);
                InstructionPointer++;
            }
        }
	}
}