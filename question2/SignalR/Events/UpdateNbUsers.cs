namespace SignalR.Events
{
    public class UpdateNbUsers : PizzaEvent
    {
        public override string EventType { get { return "UpdateNbUsers"; } }
        public int NbUsers { get; set; }
        public UpdateNbUsers(int nbUsers)
        {
            NbUsers = nbUsers;
        }
    }
}
