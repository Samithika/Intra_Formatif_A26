using SignalR.Services;

namespace SignalR.Events
{
    public class UpdatePizzaPrice : PizzaEvent
    {
        public override string EventType { get { return "UpdatePizzaPrice"; } }
        public int Price { get; set; }

        public UpdatePizzaPrice(int price)
        {
            Price = price;
        }
    }
}
