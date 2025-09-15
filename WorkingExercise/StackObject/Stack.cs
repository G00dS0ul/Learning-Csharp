namespace StackObject
{
    public class Stack
    {
        private readonly List<object> element = new List<object>();
        

        public void Push(object value)
        {
            if (value == null)
                throw new InvalidOperationException("You cannot Add a Null elment");
            
                this.element.Add(value);
        }

        public object Pop()
        {
            if (element.Count == 0)
                throw new InvalidOperationException("The element is empty hence, cannot display stack");

            var index = element.Count - 1;
            var display = element[index];
            element.RemoveAt(index);

            return display;

        }

        public void Clear()
        {
            this.element.Clear();
        }

        public IReadOnlyList<object> Items => element;
    }
}