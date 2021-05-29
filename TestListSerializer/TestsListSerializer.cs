using System.IO;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Test_Task;
using List = Test_Task.List;

namespace TestListSerializer
{
    public class TestsListSerializer
    {
        [Test]
        public async Task TestDeepCopy()
        {
            List expected = new List();
            expected.Insert("First");
            expected.Insert("Second");
            expected.Insert("Third"); 
            expected.Insert("Fourth");
            expected.Insert("Fifth");
 
            expected.GetHead().Next.Random = expected.Tail;
            expected.Tail.Random = expected.GetHead().Next.Next;

            ListSerializer listSerializer = new ListSerializer();

            List actual = new List();
            actual.SetHead(await listSerializer.DeepCopy(expected.GetHead()));
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public async Task TestSerializer()
        {
            List expected = new List();
            expected.Insert("First");
            expected.Insert("Second");
            expected.Insert("Third"); 
            expected.Insert("Fourth");
            expected.Insert("Fifth");
 
            expected.GetHead().Random = expected.GetHead().Next;
            expected.GetHead().Next.Random = expected.Tail;

            ListSerializer listSerializer = new ListSerializer();
            
            Stream fStream = new FileStream("MyTest11.xml",
                FileMode.Create, FileAccess.Write, FileShare.None);

            await listSerializer.Serialize(expected.GetHead(), fStream);
            
            fStream.Close();

            List actual = new List();
            
            fStream = File.OpenRead("MyTest11.xml");
            actual.SetHead(await listSerializer.Deserialize(fStream));
            
            Assert.AreEqual(expected, actual);
        }
    }
}