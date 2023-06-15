using Barsa.Modules.Interfaces;

namespace Caronte.Utils.ApiUrl;

public class WebServiceUrls : IWebServiceURL
{
    public string GetWebServiceUrl()
    {
        return "https://localhost:44318";
    }

    public string ValidateClient()
    {
        return $"{GetWebServiceUrl()}/client/Validate";
    }

    public string SendErrorToServer()
    {
        return $"{GetWebServiceUrl()}/error";
    }
}
