using Microsoft.AspNetCore.SignalR;
using SignalR.Events;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;

        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _pizzaManager.AddUser();
            await Clients.All.SendAsync("UpdatePage", new UpdateNbUsers(_pizzaManager.NbConnectedUsers));
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            _pizzaManager.RemoveUser();
            await Clients.All.SendAsync("UpdatePage", new UpdateNbUsers(_pizzaManager.NbConnectedUsers));
        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            // joindre le groupe de la pizza
            string groupName = _pizzaManager.GetGroupName(choice);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            // envoyer le message au groupe de clients concerné pour le prix de la pizza
            int pizzaPrice = _pizzaManager.GetPizzaPrice(choice);
            var pizzaEvent = new UpdatePizzaPrice(pizzaPrice);

            // mettre à jour le groupe de clients le nombre de pizzas et la quantité d'argent
            int nbPizzas = _pizzaManager.GetNbPizzas(choice);
            int money = _pizzaManager.GetMoney(choice);

            pizzaEvent.Events = new List<PizzaEvent>
            {
                new UpdateNbPizzasAndMoney(nbPizzas, money)
            };

            await Clients.Group(groupName).SendAsync("UpdatePage", pizzaEvent);
        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            // quitter le groupe de cette pizza
            string groupName = _pizzaManager.GetGroupName(choice);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task AddMoney(PizzaChoice choice)
        {
            string groupName = _pizzaManager.GetGroupName(choice);
            _pizzaManager.IncreaseMoney(choice);

            // mettre les clients concernés à jour
            int money = _pizzaManager.GetMoney(choice);
            var pizzaEvent = new UpdateMoney(money);
            await Clients.Group(groupName).SendAsync("UpdatePage", pizzaEvent);
        }

        public async Task BuyPizza(PizzaChoice choice)
        {
            string groupName = _pizzaManager.GetGroupName(choice);

            // acheter la pizza
            _pizzaManager.BuyPizza(choice);

            // mettre les clients concernés à jour
            int nbPizzas = _pizzaManager.GetNbPizzas(choice);
            int money = _pizzaManager.GetMoney(choice);
            var pizzaEvent = new UpdateNbPizzasAndMoney(nbPizzas, money);

            await Clients.Group(groupName).SendAsync("UpdatePage", pizzaEvent);
        }
    }
}
