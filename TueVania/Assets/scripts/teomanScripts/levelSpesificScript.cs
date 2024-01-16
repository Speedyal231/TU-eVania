using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSpesificScript : MonoBehaviour
{
    public int levelNumber;
    private LevelButtonScript lb;
    public GameObject toDestroy0;
    public GameObject toDestroy1;
    public GameObject toDestroy2;
    public GameObject toDestroy3;
    // Start is called before the first frame update
    void Start()
    {
        lb = FindObjectOfType<LevelButtonScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lb.pressed) {
            if (levelNumber == 3){
                level3TODO();
            }
        }
    }

    public void level3TODO(){
        Destroy(toDestroy0);
        Destroy(toDestroy1);
        Destroy(toDestroy2);
        Destroy(toDestroy3);
    }
}
