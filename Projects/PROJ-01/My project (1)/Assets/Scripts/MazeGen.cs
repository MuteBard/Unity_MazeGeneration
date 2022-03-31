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
    public int probability = 0;
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
    public int CountSquareCorridors(int x, int z){
        int neighbors = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 100;
        if (map[x - 1, z] == 0) neighbors++; //N
        if (map[x + 1, z] == 0) neighbors++; //S
        if (map[x, z + 1] == 0) neighbors++; //W
        if (map[x, z - 1] == 0) neighbors++; //E
        
        return neighbors;
    }

    public int CountDiagonalCorridors(int x, int z){
        int neighbors = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 100;
        if (map[x - 1, z - 1] == 0) neighbors++; //NW
        if (map[x + 1, z + 1] == 0) neighbors++; //SE
        if (map[x - 1, z + 1] == 0) neighbors++; //NE
        if (map[x + 1, z - 1] == 0) neighbors++; //SW
    
        return neighbors;
    }

// https://stackoverflow.com/questions/32911977/prevent-autocomplete-in-visual-studio-code
    public void debugging (int x, int z){
        Debug.Log("_______________");
        Debug.Log($"Center {x},{z}");
        Debug.Log($"N {x - 1},{z}");
        Debug.Log($"S {x + 1},{z}");
        Debug.Log($"W {x},{z + 1}");
        Debug.Log($"E {x},{z - 1}");
        Debug.Log($"NW {x - 1},{z - 1}");
        Debug.Log($"SE {x + 1},{z + 1}");
        Debug.Log($"NE {x - 1},{z + 1}");
        Debug.Log($"SW {x + 1},{z - 1}");
    }

    public int CountAllCorridors(int x, int z){
        return CountSquareCorridors(x, z) + CountDiagonalCorridors(x, z);
    }
}

