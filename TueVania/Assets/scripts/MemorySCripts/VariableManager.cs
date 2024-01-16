using UnityEngine;

public class VariableManager : MonoBehaviour
{
    // Variables to transfer between scenes
    public static int hTransfer;
    public static int sTransfer;
    public static bool p1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}