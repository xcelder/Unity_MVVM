

public interface SubscriptionError {}

public class SubscriptionNotFoundError : SubscriptionError {}

public class PurchaseNotCompletedError : SubscriptionError {}

public class SubscriptionNotActiveError : SubscriptionError {}

public class SubscriptionAlreadyClaimedError : SubscriptionError {}