using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

[Serializable]
    public class PlayerSubscriptionNet
    {
        [JsonProperty("i")] public string Id { get; set; }
        [JsonProperty("p")] public DateTime PurchaseDate { get; set; }
        [JsonProperty("c")] public DateTime LastClaimedDate { get; set; }
        [JsonProperty("d")] public List<int> DaysClaimed { get; set; }
    }

    [Serializable]
    public class PlayerSubscriptions
    {
        [JsonProperty("s")] public List<PlayerSubscriptionNet> Subscriptions { get; set; }
    }