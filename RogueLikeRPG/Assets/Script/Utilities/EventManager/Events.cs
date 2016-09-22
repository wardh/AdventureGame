using UnityEngine;
using System.Collections.Generic;

public enum eEvent
{
    SPAWN_COIN,
    SHOW_DIALOG,
    LOCK_PLAYER_MOVEMENT,
    OPTIONS_MENU,
    PLAYER_HIT_WATER,
    FADE_EVENT,
    TOGGLE_SAFE_POSITION_SAVING,
}


public class BaseEvent
{
    public eEvent myEventType;
}


public class CoinEvent : BaseEvent
{
    public Vector3 myPosition;
    public int myGoldAmount;
    public CoinEvent() : base()
    {
        myEventType = eEvent.SPAWN_COIN;
    }
}

public struct DialogMessage
{
    public bool myNameExists;
    public string myName;
    public string myDialogText;
}


public class DialogEvent : BaseEvent
{
    public List<DialogMessage> myDialogMessages;
    public DialogEvent() : base()
    {
        myEventType = eEvent.SHOW_DIALOG;
    }
}

public class LockPlayerMovementEvent : BaseEvent
{
    public bool myLockPlayer;
    public LockPlayerMovementEvent() : base()
    {
        myEventType = eEvent.LOCK_PLAYER_MOVEMENT;
    }
}

public class OptionsMenuEvent : BaseEvent
{
    public bool myOpenOptions;
    public OptionsMenuEvent() : base()
    {
        myEventType = eEvent.OPTIONS_MENU;
    }
}

public class PlayerHitWaterEvent : BaseEvent
{
    public PlayerHitWaterEvent() : base()
    {
        myEventType = eEvent.PLAYER_HIT_WATER;
    }
}

public class FadeEvent : BaseEvent
{
    public bool myShouldFadeIn;
    public float myFadeTime;
    public FadeEvent() : base()
    {
        myEventType = eEvent.FADE_EVENT;
    }
}
public class ToggleSafePositionSavingEvent : BaseEvent
{
    public bool myShouldSavePosition;
    public ToggleSafePositionSavingEvent() : base()
    {
        myEventType = eEvent.TOGGLE_SAFE_POSITION_SAVING;
    }
}