using Microsoft.AspNetCore.SignalR;
using Restaurant.Logic.SiteLayout;
using System.Threading.Tasks;

namespace Restaurant.Logic
{
    public partial class MessageHub : Hub<IMessageClient>
    {
        public async Task GetMenuItems(string accountStatus) => await Clients.Caller.SendMenuItems(MenuItem.GetMenuItems(accountStatus));

        public async Task TestConnection() => await Clients.Caller.ReturnConnected();
    }
}
