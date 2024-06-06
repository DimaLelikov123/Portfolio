using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zootopia
{
    public class Enclosure
    {
        public string type;
        public int number;
        public string imageSourse;
        public string description;
        

        public Enclosure(string type, int number, string imageSourse, string description)
        {
            this.type = type;
            this.number = number;
            this.imageSourse = imageSourse;
            this.description = description;
        }

    }

    class EnclosureCollection : IEnumerable
    {
        public List<Enclosure> Enclosures;

        public EnclosureCollection(List<Enclosure> input)
        {
            Enclosures = input;
        }
        public IEnumerator GetEnumerator()
        {
            return new AviaryIterator(this);
        }
    }

    class AviaryIterator : IEnumerator
    {
        private EnclosureCollection collection;
        public int currentIndex;
        private int previousIndex;
        public string type;

        public AviaryIterator(EnclosureCollection collection)
        {
            this.collection = collection;
            currentIndex = -1;
            previousIndex = -1;
            
        }

        public void SetType (string type)
        {
            this.type = type;
        }

        public object Current
        {
            get
            {
                return collection.Enclosures[currentIndex];
            }
        }

        public bool MoveNext()
        {
            var newIndex = (currentIndex+1) % collection.Enclosures.Count;
            do
            {
                if (collection.Enclosures[newIndex].type == type)
                {
                    currentIndex = newIndex;
                    return true;
                }
                newIndex = (newIndex + 1) % collection.Enclosures.Count;
            }
            while (newIndex != previousIndex);
            return false;
        }

        public void Reset()
        {
            currentIndex = -1;
            previousIndex = -1;
        }

        public bool MovePrevious()
        {
            if (collection.Enclosures.Count != 1)
            {
                if (currentIndex >= 0)
                {
                    previousIndex = currentIndex;
                    var newIndex = (currentIndex-1 + collection.Enclosures.Count) % collection.Enclosures.Count;
                    do
                    {
                        if (collection.Enclosures[newIndex].type == type)
                        {
                            currentIndex = newIndex;
                            return true;
                        }
                        newIndex = (newIndex - 1 + collection.Enclosures.Count) % collection.Enclosures.Count;
                    }
                    while ((currentIndex + 1) % collection.Enclosures.Count != newIndex);
                    return false;
                }
            }
            return false;
        }

        public bool MoveBackToType(string type)
        {
            previousIndex = currentIndex;
            var newIndex = currentIndex;
            do
            {
                if (collection.Enclosures[newIndex].type == type)
                {
                    currentIndex = newIndex;
                    return true;
                }
                newIndex = (newIndex - 1+ collection.Enclosures.Count) % collection.Enclosures.Count;
            }
            while ((currentIndex+1) % collection.Enclosures.Count != newIndex);
            return false;
        }

        public bool MoveToType(string type)
        {
            var newIndex = currentIndex;
            do
            {
                if (collection.Enclosures[newIndex].type == type)
                {
                    currentIndex = newIndex;
                    return true;
                }
                newIndex = (newIndex + 1) % collection.Enclosures.Count;
            }
            while (newIndex != previousIndex);
            return false;
        }
    }

}
