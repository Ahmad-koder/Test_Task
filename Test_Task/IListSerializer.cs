using System.IO;
using System.Threading.Tasks;

namespace Test_Task
{
    public interface IListSerializer
    {
        Task Serialize(ListNode head, Stream s);

        Task<ListNode> Deserialize(Stream s);

        Task<ListNode> DeepCopy(ListNode head);
    }
}