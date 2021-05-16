using System;
using System.IO;
using System.Threading.Tasks;

namespace Test_Task
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List list = new List();
            list.Insert("First");
            list.Insert("Second");
            list.Insert("Third");
            list.Insert("Fourth");
            list.Insert("Fifth");

            list.GetHead().Next.Random = list.Tail;
            list.Tail.Random = list.GetHead().Next.Next;

            ListSerializer listSerializer = new ListSerializer();
            
            Stream fStream = new FileStream("FirstTest",
                FileMode.Create, FileAccess.Write, FileShare.None);
            
            await listSerializer.Serialize(list.GetHead(), fStream);
            
            List newList = new List();
            
            //newList.SetHead(await listSerializer.DeepCopy(list.GetHead()));
            
           // Stream fStream = File.OpenRead("FirstTest");
            //newList.SetHead(await listSerializer.Deserialize(fStream));
            
            Console.ReadKey();

        }
    }
}
        
