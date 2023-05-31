using System;
using System.Collections.Generic;

//Flyweight pattern
public class DataFactory
{
    public Dictionary<char, Data> Datas = new Dictionary<char, Data>();
    public Data GetData(char key)
    {
        Data Data;
        if (Datas.ContainsKey(key))
        {
            Data = Datas[key];
        }
        else
        {
            switch (key)
            {
                case 'A':
                    Data = new DataA();
                    break;
                case 'B':
                    Data = new DataB();
                    break;
                default:
                    throw new ArgumentException("Invalid key.");
            }
            Datas.Add(key, Data);
        }
        return Data;
    }
}
//Data pattern
public abstract class Data
{
    public abstract void Operation(int extrinsicState);
}
public class DataA : Data
{
    public override void Operation(int extrinsicState)
    {
        Console.WriteLine("ConcreteDataA: " + extrinsicState);
    }
}
public class DataB : Data
{
    public override void Operation(int extrinsicState)
    {
        Console.WriteLine("ConcreteDataB: " + extrinsicState);
    }
}
// Iterator pattern
public class Aggregate
{
    public List<object> items = new List<object>();
    public DataList CreateDataList()
    {
        return new DataList(this);
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
public class DataList
{
    public Aggregate aggregate;
    public int index = 0;
    public DataList(Aggregate aggregate)
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

// Singleton pattern
class Instance
{
    public static Instance instance;

    public Instance() { }

    public static Instance GetInstance()
    {
        if (instance == null)
        {
            instance = new Instance();
        }
        return instance;
    }

    public DataFactory Operation()
    {
        DataFactory a = new DataFactory();
        return a;
    }
}

// Usage example
class Program
{
    static void Main(string[] args)
    {

        Instance checkinstance = new Instance();
        DataFactory factory =checkinstance.Operation();


        Data Data1 = factory.GetData('A');
        Data1.Operation(1);
        Data Data2 = factory.GetData('B');
        Data2.Operation(2);
        Data Data3 = factory.GetData('A');
        Data3.Operation(3);

        Aggregate aggregate = new Aggregate();
        aggregate[0] = Data1;
        aggregate[1] = Data2;
        aggregate[2] = Data3;
        DataList DataList = aggregate.CreateDataList();
        while (!DataList.IsDone)
        {
            Console.WriteLine(DataList.Next());
        }
    }
}
