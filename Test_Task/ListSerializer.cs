using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Test_Task
{
    public class ListSerializer : IListSerializer
    {
        public async Task Serialize(ListNode head, Stream s)
        {
            ListNode node = head;

            var settings = new XmlWriterSettings { Indent = true };
            var writer = XmlWriter.Create(s, settings);
            
            writer.WriteStartElement("root");
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(String));
            XmlSerializer xmlSerializerNode = new XmlSerializer(typeof(ListNode));

            while (node != null)
            {
                xmlSerializer.Serialize(writer, node.Data);
                
                xmlSerializerNode.Serialize(writer, node.Random);
                
                node = node.Next;
            }
            
            writer.Close();
        }

        public async Task<ListNode> Deserialize(Stream s)
        {
            ListNode head = null;
            ListNode tail = null;

            var reader = XmlReader.Create(s);
            reader.ReadToFollowing("string");
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(String));
            XmlSerializer xmlSerializerNode = new XmlSerializer(typeof(ListNode));
            
            String data = (String) xmlSerializer.Deserialize(reader);

            while (data != null)
            {
                if (head == null)
                {
                    head = new ListNode() {Data = data};

                    head.Random = (ListNode) xmlSerializerNode.Deserialize(reader);

                    tail = head;
                }
                else
                {
                    tail.Next = new ListNode() {Data = data};
                    tail.Next.Random = (ListNode) xmlSerializerNode.Deserialize(reader);
                    tail = tail.Next;
                }
                
                data = (String) xmlSerializer.Deserialize(reader);
            }

            return head;
        }

        public async Task<ListNode> DeepCopy(ListNode head)
        {
            for (ListNode cur = head; cur != null; cur = cur.Next)
            {
                ListNode node = new ListNode();
                node.Data = cur.Data;
                node.Next = cur.Random;
                cur.Random = node;
            }

            ListNode result = head.Random;
            
            for (ListNode cur = head; cur != null; cur = cur.Next)
            {
                ListNode node = cur.Random;
                node.Random = node.Next != null ? node.Next.Random : null;
                node.Previous = cur.Previous != null ? cur.Previous.Random : null;
            }
            
            for (ListNode cur = head; cur != null; cur = cur.Next)
            {
                ListNode node = cur.Random;
                cur.Random = node.Next;
                node.Next = cur.Next != null ? cur.Next.Random : null;
            }

            return result;
        }
    }
}