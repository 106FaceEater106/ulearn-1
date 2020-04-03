using System;
using System.Collections.Generic;
using System.Linq;

namespace Clones
{
	public class CloneVersionSystem : ICloneVersionSystem
	{
	
        private readonly Dictionary<int, Clone> _clones = new Dictionary<int, Clone>
        {
            {1, new Clone()}
        };

        private int _lastCloneNumber = 1;

		public string Execute(string query)
        {
            var split = query.Split();
            var cloneNumber = int.Parse(split[1]);

            switch (split[0])
            {
                case "learn":
                    _clones[cloneNumber].Add(split[2]);
                    break;
                case "rollback":
                    _clones[cloneNumber].Undo();
                    break;
                case "relearn":
                    _clones[cloneNumber].Redo();
                    break;
                case "clone":
                    _lastCloneNumber++;
                    _clones.Add(_lastCloneNumber, _clones[cloneNumber].CloneThis());
                    break;
                case "check":
                    if (_clones[cloneNumber].Progs.Count == 0) return "basic";
                    return _clones[cloneNumber].Progs.Peek();
            }

            return null;
        }
	}

    public class Clone
    {
        public readonly Stack<string> Progs;
        public readonly Stack<string> ProgsStory;

        public Clone( Stack<string> progs, Stack<string> progsStory)
        {
            Progs = progs;
            ProgsStory = progsStory;
        }

        public Clone() : this(new Stack<string>(), new Stack<string>()){}

        public void Undo()
        {
            ProgsStory.Push(Progs.Pop());
        }
        public void Redo()
        {
            Progs.Push(ProgsStory.Pop());
        }

        public void Add(string str)
        {
            ProgsStory.Clear();
            Progs.Push(str);
        }

        public Clone CloneThis()
        {
            return new Clone(new Stack<string>(Progs.Reverse()), new Stack<string>(ProgsStory));
        }
    }

}
