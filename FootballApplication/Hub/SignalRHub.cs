using DataAccessLayer.Concrete;
using EntityLayer.Entites;
using FootballApplication1.ModelView;
using Microsoft.AspNetCore.SignalR;

namespace FootballApplication1
{
    public class SignalRHub : Hub
    {
        public async Task SendWeek(int week)
        {
            
            await Clients.All.SendAsync("sendWeek", week);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        // sendlist metodunu ekleyin
        public async Task SendList(MatchViewModel matchDatesList)
        {
            await Clients.All.SendAsync("ReceiveMatchDatesList", matchDatesList);
        }

        public async Task SendAttack(MatchResult matchResult)
        {
            await Clients.All.SendAsync("ReceiveAttackList", matchResult);
        }
        public async Task SendAttackResult(string attackResult)
        {
            await Clients.All.SendAsync("ReceiveAttackResult", attackResult);
        }

        public async Task SendTimeMinute(int minute)
        {
            await Clients.All.SendAsync("ReceiverTimeMinute", minute);
        }
        public async Task SendTimeSecond(int second)
        {
            await Clients.All.SendAsync("ReceiverTimeSecond", second);
        }
    }
}
