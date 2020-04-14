using System;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;

namespace hashes
{
	public class GhostsTask : 
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
		IMagic
    {
        private readonly Vector vector;
        private readonly Segment segment;
        private readonly Document document;
        private readonly Robot robot;
        private readonly Cat cat;
        private readonly byte[] array;

        public GhostsTask()
        { 
            vector = new Vector(1,1);
            segment = new Segment(vector, vector);

            array = new byte[]{1,2,4,5,6};
            document = new Document("Mama mia", Encoding.Unicode, array);

            robot = new Robot("Thing");
            cat = new Cat("Cat", "12", DateTime.Now);
        }

		public void DoMagic()
        {
            vector.Add(vector);
            Robot.BatteryCapacity--;
            array[3] = 255;
            cat.Rename("Dog");
        }

        Vector IFactory<Vector>.Create()
		{
            return vector;
        }

		Segment IFactory<Segment>.Create()
        {
            return segment;
        }

        Document IFactory<Document>.Create()
        {
            return document;
        }

        Cat IFactory<Cat>.Create()
        {
            return cat;
        }

        Robot IFactory<Robot>.Create()
        {
            return robot;
        }
    }
}