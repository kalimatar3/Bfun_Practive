using UnityEngine;

public static class UIIDManager  {
    private static int currentID = 0;
    public static int GetUIID()
    {
        return ++currentID;
    }
}