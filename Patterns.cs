//Factory pattern
public class FlyweightFactory
{
    private Dictionary<char, Flyweight> flyweights = new Dictionary<char, Flyweight>();
    public Flyweight GetFlyweight(char key)
    {
        Flyweight flyweight;
        if (flyweights.ContainsKey(key))
        {
            flyweight = flyweights[key];
        }
        else
        {
            switch (key)
            {
                case 'A':
                    flyweight = new FlyweightA();
                    break;
                case 'B':
                    flyweight = new FlyweightB();
                    break;
                default:
                    throw new ArgumentException("Invalid key.");
            }
            flyweights.Add(key, flyweight);
        }
        return flyweight;
    }
}
//Flyweight pattern
public abstract class Flyweight
{
    public abstract void Operation(int extrinsicState);
}
public class FlyweightA : Flyweight
{
    public override void Operation(int extrinsicState)
    {
        Console.WriteLine("ConcreteFlyweightA: " + extrinsicState);
    }
}
public class FlyweightB : Flyweight
{
    public override void Operation(int extrinsicState)
    {
        Console.WriteLine("ConcreteFlyweightB: " + extrinsicState);
    }
}
// Iterator pattern
public class Aggregate
{
    private List<object> items = new List<object>();
    public Iterator CreateIterator()
    {
        return new Iterator(this);
    }
    public int Count
    {
        get { return items.Count; }
    }
    public object this[int index]
    {
        get { return items[index]; }
        set { items.Insert(index, value); }
    }
}
public class Iterator
{
    private Aggregate aggregate;
    private int index = 0;
    public Iterator(Aggregate aggregate)
    {
        this.aggregate = aggregate;
    }
    public object Next()
    {
        object obj = null;
        if (index < aggregate.Count)
        {
            obj = aggregate[index];
            index++;
        }
        return obj;
    }
    public bool IsDone
    {
        get { return index >= aggregate.Count; }
    }
    public object CurrentItem
    {
        get { return index < aggregate.Count ? aggregate[index] : null; }
    }
}
// Usage example
class Program
{
    static void Main(string[] args)
    {
        // Singleton pattern
        FlyweightFactory factory = new FlyweightFactory();
        // Flyweight pattern
        
        Flyweight flyweight1 = factory.GetFlyweight('A');
        flyweight1.Operation(1);
        Flyweight flyweight2 = factory.GetFlyweight('B');
        flyweight2.Operation(2);
        Flyweight flyweight3 = factory.GetFlyweight('A');
        flyweight3.Operation(3);
        // Iterator pattern
        Aggregate aggregate = new Aggregate();
        aggregate[0] = flyweight1;
        aggregate[1] = flyweight2;
        aggregate[2] = flyweight3;
        Iterator iterator = aggregate.CreateIterator();
        while (!iterator.IsDone)
        {
            Console.WriteLine(iterator.Next());
        }
    }
}