using SampleFive.PresentaionLayer;

namespace SampleFive.ServiceLayer
{
    public interface IMessagesService
    {
        string GetSiteName();
        string GetSiteName2();
        string GetSiteName3();
        string GetSiteWelcome(string title = "my Kingdom");
        SiteCopyRightConfig GetSiteCopyRightConfig();
    }
}