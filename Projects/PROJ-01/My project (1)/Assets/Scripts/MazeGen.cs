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
    public int CountSquareNeighbors(int x, int z){
        int neighbors = 0;
        if( x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x-1,z] == 0) neighbors++; //N
        if (map[x,z+1] == 0) neighbors++; //W
        if (map[x,z-1] == 0) neighbors++; //E
        if (map[x+1,z] == 0) neighbors++; //S
        return neighbors ;
    }

    public int CountDiagonalNeighbors(int x, int z){
        int neighbors = 0;
        if( x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x-1,z-1] == 0) neighbors++; //NW
        if (map[x-1,z+1] == 0) neighbors++; //NE
        if (map[x+1,z-1] == 0) neighbors++; //SW
        if (map[x+1,z+1] == 0) neighbors++; //SE
        return neighbors ;
    }

    public int CountAllNeighbors(int x, int z){
        return CountSquareNeighbors(x, z) + CountDiagonalNeighbors(x, z);
    }
}

