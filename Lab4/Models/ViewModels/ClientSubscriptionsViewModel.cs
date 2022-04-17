using Assignment02NET.Models;

namespace Assignment02NET.Models.ViewModels
{
    public class ClientSubscriptionsViewModel
    {
        public Client Client { get; set; }
        public IEnumerable<BrokerageSubscriptionsViewModel> Subscriptions { get; set; }

    }
}
