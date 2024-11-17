using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

public class PurchasedPlayerSubscription
{
    public string Id { get; private set; }
    public DateTime PurchaseDate { get; }
    public DateTime LastClaimedDate { get; private set; }
    public List<int> DaysClaimed { get; private set; }
    public int DurationDays { get; }

    public DateTime ExpirationDate => PurchaseDate.AddDays(DurationDays);

    public PurchasedPlayerSubscription(
        string id,
        DateTime purchaseDate,
        DateTime lastClaimedDate,
        List<int> daysClaimed,
        int durationDays
    )
    {
        Id = id;
        PurchaseDate = purchaseDate;
        LastClaimedDate = lastClaimedDate;
        DaysClaimed = daysClaimed;
        DurationDays = durationDays;
    }

    public bool IsActive(DateTime currentDate)
    {
        return (currentDate - PurchaseDate).Days < DurationDays;
    }

    public bool CanClaimToday(DateTime currentDate)
    {
        var dayToClaim = (currentDate - PurchaseDate).Days;
        if (dayToClaim >= DurationDays)
        {
            return false;
        }

        return !DaysClaimed.Contains(dayToClaim) && IsActive(currentDate);
    }

    public void Claim(DateTime claimDate)
    {
        var dayToClaim = (claimDate - PurchaseDate).Days;

        if (dayToClaim > DurationDays)
        {
            throw new EvaluateException();
        }

        LastClaimedDate = claimDate;

        DaysClaimed ??= new List<int>();
        DaysClaimed.Add(dayToClaim);
    }
}