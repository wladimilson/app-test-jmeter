using System.Threading.Tasks;

namespace app_test_jmeter.Data
{
    public interface ISeedData
    {
        Task Initialize();
        Task EnsureRoles();
        Task EnsureDefaultUser();
    }
}