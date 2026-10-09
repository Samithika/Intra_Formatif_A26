namespace SignalR.Events
{
    public class UpdateNbPizzasAndMoney : PizzaEvent
    {
        public override string EventType { get { return "UpdateNbPizzasAndMoney"; } }
        public int NbPizzas { get; set; }
        public int Money { get; set; }
        public UpdateNbPizzasAndMoney(int nbPizzas, int money)
        {
            NbPizzas = nbPizzas;
            Money = money;
        }
    }
}
