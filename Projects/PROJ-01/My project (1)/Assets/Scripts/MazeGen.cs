using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation
{
    public int x;
    public int z;
    public MapLocation(int x, int z){
        this.x = x;
        this.z = z;
    }
}
public class MazeGen : MonoBehaviour
{
    public int width = 30; // x length
    public int depth = 30; // z length
    public int probability = 50;
    public byte [,] map;
    public int scale = 6;
    void Start()
    {
        InitializeMap();
        Generate();
        DrawMap();
    }

    byte CorridorProbability(int percent){
        int value = UnityEngine.Random.Range(1, 100);
        return value <= percent ? (byte) 1 : (byte) 0;
    }

    void InitializeMap(){
        map = new byte[width, depth];
        for(int z = 0; z < depth; z++) {
            for(int x = 0; x < width; x++) {
                map[x, z] = 1; //1 = wall, 0 = cooridor
            }
        }
    }

    public virtual void Generate(){
        for(int z = 0; z < depth; z++) {
            for(int x = 0; x < width; x++) {
                map[x, z] = CorridorProbability(probability);
            }
        }
    }

    void DrawMap(){
        for(int z = 0; z < depth; z++) {
            for(int x = 0; x < width; x++) {
                Vector3 pos = new Vector3(x * scale, 0, z * scale);
                if(map[x, z] == 1){
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.localScale = new Vector3(scale, scale, scale);
                    wall.transform.position = pos;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
