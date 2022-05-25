public class OnPlayerDeathEvent : Event<OnPlayerDeathEvent> { }
public class OnLevelUpEvent : Event<OnLevelUpEvent> { public int newlevel; }
public class OnXpChangedEvent : Event<OnXpChangedEvent> { public float currentXP; public float xpToLevelUp; }
public class OnMinionDeathEvent : Event<OnMinionDeathEvent> { }
public class OnSpellLevelUpEvent : Event<OnSpellLevelUpEvent> { }

