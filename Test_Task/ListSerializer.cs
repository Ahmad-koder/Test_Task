using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Test_Task
{
    public class ListSerializer : IListSerializer
    {
        public async Task Serialize(ListNode head, Stream s)
        {
            ListNode node = head;
            
            BinaryFormatter binFormat = new BinaryFormatter();

            while (node != null)
            {
                binFormat.Serialize(s, node);
                node = node.Next;
            }
        }

        public async Task<ListNode> Deserialize(Stream s)
        {
            ListNode head = null;
            ListNode tail = null;

            BinaryFormatter binFormat = new BinaryFormatter();
            
            while (s.Position != s.Length)
            { 
                if (head == null)
                {
                    head = (ListNode) binFormat.Deserialize(s);
                    tail = head;
                }
                else
                {
                    tail.Next = (ListNode) binFormat.Deserialize(s);
                    tail.Next.Previous = tail;
                    tail = tail.Next;
                } }

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