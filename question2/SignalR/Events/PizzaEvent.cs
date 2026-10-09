using System.Text.Json.Serialization;

namespace SignalR.Events
{
    [JsonDerivedType(typeof(UpdateNbUsers))]
    [JsonDerivedType(typeof(UpdatePizzaPrice))]
    [JsonDerivedType(typeof(UpdateNbPizzasAndMoney))]
    [JsonDerivedType(typeof(UpdateMoney))]
    public abstract class PizzaEvent
    {
        public abstract string EventType { get; }
        public List<PizzaEvent>? Events { get; set; }
    }
}
