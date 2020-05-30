using System.Threading.Tasks;

namespace AutoRest.Models
{
    public interface IFileManager
    {
        Task Delete(string id);
        Task<byte[]> Download(string id);
        Task<string> Upload(byte[] content, string filename);
    }
}