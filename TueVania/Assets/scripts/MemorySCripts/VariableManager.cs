using UnityEngine;

public class VariableManager : MonoBehaviour
{
    // Variables to transfer between scenes
    public static int hTransfer;
    public static int sTransfer;
    public static bool p1;
    public static bool p2;
    public static bool p3;
    public static bool p4;
    public static bool L1e;
    public static bool L2e;
    public static bool L3e;
    public static bool L4e;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}