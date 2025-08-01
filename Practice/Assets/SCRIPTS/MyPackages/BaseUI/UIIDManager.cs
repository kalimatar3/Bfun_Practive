using UnityEngine;

public static class UIIDManager  {
    private static long currentID = 0;
    public static long GetUIID() => ++currentID;
}