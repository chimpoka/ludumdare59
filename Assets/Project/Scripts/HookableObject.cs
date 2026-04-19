using UnityEngine;

public class HookableObject : MonoBehaviour
{
    public HookType type;
}

public enum HookType
{
    Carry,     // можно поднять на борт
    Drag       // можно только тащить
}