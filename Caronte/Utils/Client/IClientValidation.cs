using System.Threading.Tasks;

namespace Caronte.Utils.Client
{
    public interface IClientValidation
    {
        public Task Validate();
    }
}