using System.Threading.Tasks;

namespace SampleFive.ServiceLayer.Interfaces
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
