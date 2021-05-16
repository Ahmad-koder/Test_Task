using System;

namespace Test_Task
{
    public class List
    {
        private static int col = 0;
        private ListNode Head { get; set; }
        public ListNode Tail { get; set; }

        public void SetHead(ListNode head)
        {
            col++;
            this.Head = head;
            ListNode node = head;

            while (node.Next != null)
            {
                node = node.Next;
                col++;
            }

            Tail = node;
        }

        public ListNode GetHead()
        {
            return Head;
        }

        public void Insert(string data)
        {
            col++;
            
            if (Head == null)
            {
                Head = new ListNode {Data = data}; 
                Tail = Head;
            }
            else
            {
                Tail.Next = new ListNode() {Previous = Tail, Next = null, Random = null, Data = data};
                Tail = Tail.Next;
            }
        }

        public void PrintAllElements()
        {
            ListNode cur = Head;

            while (cur != null)
            {
                Console.Write(cur.Data);

                cur = cur.Next;
            }
        }

        public void EnterRandomNode(ListNode node)
        {
            Random random = new Random();

            int id = random.Next(1, col);

            int i = 1;

            ListNode randomNode = Head;
            
            while (i != id)
            {
                randomNode = randomNode.Next;
                i++;
            }

            node.Random = randomNode;
        }

        public override bool Equals(object other)
        {
            var otherList = other as List;
            if (otherList == null)
                return false;
            
            ListNode originHead = Head;
            ListNode otherHead = otherList.GetHead();

            while (originHead != null && otherHead != null)
            {
                if (originHead.Data != otherHead.Data)
                    return false;

                if (originHead.Random == null && otherHead.Random == null)
                {
                    originHead = originHead.Next;
                    otherHead = otherHead.Next;
                    continue;
                }

                if (originHead.Random == null)
                    return false;
                
                if (otherHead.Random == null)
                    return false;

                if (otherHead.Random.Data != originHead.Random.Data)
                    return false;
                
                originHead = originHead.Next;
                otherHead = otherHead.Next;
            }

            if (originHead == null && otherHead == null)
                return true;
            else
                return false;
        }
    }
}