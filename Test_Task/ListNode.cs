using System;

namespace Test_Task
{

    [Serializable]
    public class ListNode
    {
        [NonSerialized] public ListNode Previous;

        [NonSerialized] public ListNode Next;

        public ListNode Random;

        public string Data;
        
    }
}