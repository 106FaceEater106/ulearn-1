using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
		{
            var openCloseBrackets = new Dictionary<int, int>();
            var closeOpenBrackets = new Dictionary<int, int>();
            SetUpDoubleDictionary(vm, openCloseBrackets, closeOpenBrackets);

            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0) 
                    b.InstructionPointer = openCloseBrackets[b.InstructionPointer];
            });

			vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0) 
                    b.InstructionPointer = closeOpenBrackets[b.InstructionPointer];
            });
		}

        private static void SetUpDoubleDictionary(IVirtualMachine vm, 
            Dictionary<int, int> first, Dictionary<int, int> second)
        {
            var brackets = new Stack<int>();

            for (var i = 0; i < vm.Instructions.Length; i++)
            {
                var command = vm.Instructions[i];
                if (command == '[') 
                    brackets.Push(i);
                if (command == ']')
                {
                    var openBracket = brackets.Pop();
                    first[openBracket] = i;
                    second[i] = openBracket;
                }
            }
        }
    }
}