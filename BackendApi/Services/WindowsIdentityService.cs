using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace BackendApi.Services
{
    public interface IIdentityService
    {
        string GetCurrentUserPernr();
    }

    public class WindowsIdentityService : IIdentityService
    {
        private readonly ILogger<WindowsIdentityService> _logger;
        private readonly IConfiguration _configuration;

        public WindowsIdentityService(ILogger<WindowsIdentityService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string GetCurrentUserPernr()
        {
            try
            {
                // Get the current Windows identity
                var windowsIdentity = WindowsIdentity.GetCurrent();
                if (windowsIdentity == null)
                {
                    _logger.LogWarning("Could not get Windows identity");
                    return GetFallbackPernr();
                }

                // Get the user's domain account name
                string domainAccount = windowsIdentity.Name;
                
                // Extract the username (remove domain prefix if present)
                string username = domainAccount.Contains('\\') 
                    ? domainAccount.Split('\\')[1] 
                    : domainAccount;

                // Use PrincipalContext to get more user details from Active Directory
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    var user = UserPrincipal.FindByIdentity(context, username);
                    if (user != null)
                    {
                        // The employee ID might be stored in a custom attribute
                        // The actual attribute name will depend on your AD setup
                        DirectoryEntry entry = (DirectoryEntry)user.GetUnderlyingObject();
                        
                        // Replace "employeeId" with the actual AD attribute name that stores PERNR
                        if (entry.Properties["employeeId"].Value != null)
                        {
                            return entry.Properties["employeeId"].Value.ToString();
                        }
                    }
                }

                _logger.LogWarning("Could not find PERNR in Active Directory for user: {Username}", username);
                return GetFallbackPernr();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user PERNR");
                return GetFallbackPernr();
            }
        }

        private string GetFallbackPernr()
        {
            // Try to get from configuration, otherwise use a default value
            return _configuration["DefaultPernr"] ?? "12345";
        }
    }
}