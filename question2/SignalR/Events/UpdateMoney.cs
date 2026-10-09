namespace SignalR.Events
{
    public class UpdateMoney : PizzaEvent
    {
        public override string EventType { get { return "UpdateMoney"; } }
        public int Money { get; set; }
        public UpdateMoney(int money)
        {
            Money = money;
        }
    }
}
