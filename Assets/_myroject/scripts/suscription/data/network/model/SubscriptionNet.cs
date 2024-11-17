

using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

[Serializable]
    public class SubscriptionNet
    {
        [JsonProperty("i")] public string Id { get; set; }
        [JsonProperty("d")] public int DurationDays { get; set; }
        [JsonProperty("s")] public string StoreItemId { get; set; }
        [JsonProperty("r")] public Dictionary<int, List<RewardReference>> DailyRewards { get; set; }
    }

    [Serializable]
    public class SubscriptionSettings
    {
        [JsonProperty("s")] public List<SubscriptionNet> Subscriptions { get; set; }
    }

    [Serializable]
    public class RewardReference
    {
        [JsonProperty("id")] public string Id { get; set; }
    }