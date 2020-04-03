using System.Collections.Generic;

namespace Clones
{
	public class CloneVersionSystem : ICloneVersionSystem
    {

        private readonly Dictionary<int, Clone> clones = new Dictionary<int, Clone>
        {
            {1, new Clone()}
        };

        private int lastCloneNumber = 1;

		public string Execute(string query)
        {
            var split = query.Split();
            var cloneNumber = int.Parse(split[1]);

            switch (split[0])
            {
                case "learn":
                    clones[cloneNumber].Add(split[2]);
                    break;
                case "rollback":
                    clones[cloneNumber].Undo();
                    break;
                case "relearn":
                    clones[cloneNumber].Redo();
                    break;
                case "clone":
                    lastCloneNumber++;
                    clones.Add(lastCloneNumber, clones[cloneNumber].CloneThis());
                    break;
                case "check":
                    return clones[cloneNumber].Check();
            }

            return null;
        }
	}

    public class Clone
    {
        private readonly LinkedStack<string> progs;
        private LinkedStack<string> progsUndo;

        public Clone( LinkedStack<string> progs, LinkedStack<string> progsUndo)
        {
            this.progs = progs;
            this.progsUndo = progsUndo;
        }

        public Clone() : this(new LinkedStack<string>(), new LinkedStack<string>()){}

        public void Undo()
        {
            progsUndo.Push(progs.Pop());
        }
        public void Redo()
        {
            progs.Push(progsUndo.Pop());
        }

        public void Add(string str)
        {
            if(progsUndo.Head != null)
                progsUndo = new LinkedStack<string>();
            progs.Push(str);
        }

        public string Check()
        {
            if (progs.Head == null) return "basic";
            return progs.Head.Value;
        }

        public Clone CloneThis()
        {
            return new Clone(progs.Copy(), progsUndo.Copy());
        }
    }

    public class LinkedStack<T>
    {
        public LinkedStackNode<T> Head { get; private set; }

        public void Push(T item)
        {
            var newCell = new LinkedStackNode<T>(item) {Next = Head};
            Head = newCell;
        }

        public T Pop()
        {
            var oldHead = Head;
            Head = Head.Next;
           // oldHead.Next = null;
            return oldHead.Value;
        }

        public LinkedStack<T> Copy()
        {
            return new LinkedStack<T>(Head);
        }

        public LinkedStack() : this(null){}
        public LinkedStack(LinkedStackNode<T> head)
        {
            Head = head;
        }
    }

    public class LinkedStackNode<T>
    {
        public LinkedStackNode<T> Next { get; set; }
        public T Value;

        public LinkedStackNode(T value)
        {
            Value = value;
        }
    }
}
