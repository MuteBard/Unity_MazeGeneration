using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGen : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    public int width = 30; // x length
    public int depth = 30; // z length
    void Start()
    {
        for(int z = 0; z < depth; z++) {
            for(int x = 0; x < width; x++) {
                Vector3 pos = new Vector3(x, 0, z);
                Instantiate(cube, pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
