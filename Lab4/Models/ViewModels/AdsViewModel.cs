using Assignment02NET.Models;

namespace Assignment02NET.Models.ViewModels
{
    public class AdsViewModel
    {
        public Brokerage Brokerages { get; set; }
        public IEnumerable<Advertisements> Advertisements { get; set; }

    }
}
