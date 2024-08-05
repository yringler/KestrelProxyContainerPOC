using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KestrelProxyContainerPOC.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public readonly IPAddress[] IpAddresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

    public readonly string HostName = Dns.GetHostName();

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}