using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Crawler : MazeGeneration
{
    public int maxPasses = 10;
    int passes = 0;
    public override void Generate()
    {

        // CrawlRandom();
        // PrimsCrawl();
        // RestrictedCrawlX();
        // RestrictedCrawlZ();
        RecursiveCrawl();
        RecursiveCrawl();

    }
    public void CrawlRandom()
    {
        bool done = false;
        int x = width / 2;
        int z = depth / 2;

        while (!done)
        {
            map[x, z] = 0;
            int nx = UnityEngine.Random.Range(-1, 2);
            int nz = UnityEngine.Random.Range(-1, 2);

            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                x += nx;
            }
            else
            {
                z += nz;
            }

            // crawl until you hit the edge of the map
            done = (x < 0 || x >= width || z < 0 || z >= depth);
        }
    }

    public void CrawlX()
    {
        bool done = false;
        int x = 0;
        int z = depth / 2;

        while (!done)
        {
            map[x, z] = 0;
            int nx = UnityEngine.Random.Range(0, 2);
            int nz = UnityEngine.Random.Range(-1, 2);

            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                x += nx;
            }
            else
            {
                z += nz;
            }

            // crawl until you hit the edge of the map
            done = (x < 0 || x >= width || z < 0 || z >= depth);
        }
    }

    public void CrawlZ()
    {
        bool done = false;
        int x = width / 2;
        int z = 0;

        while (!done)
        {
            map[x, z] = 0;
            int nx = UnityEngine.Random.Range(-1, 2);
            int nz = UnityEngine.Random.Range(0, 2);

            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                x += nx;
            }
            else
            {
                z += nz;
            }

            // crawl until you hit the edge of the map
            done = (x < 0 || x >= width || z < 0 || z >= depth);
        }
    }

    public void RestrictedCrawlX()
    {
        bool done = false;
        int x = 1;
        int z = UnityEngine.Random.Range(1, depth - 1);

        while (!done)
        {
            map[x, z] = 0;
            int nx = UnityEngine.Random.Range(0, 2);
            int nz = UnityEngine.Random.Range(-1, 2);

            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                x += nx;
            }
            else
            {
                z += nz;
            }

            // crawl until you hit the edge of the map
            done = (x < 1 || x >= width - 1 || z < 1 || z >= depth - 1);
        }
    }

    public void RestrictedCrawlZ()
    {
        bool done = false;
        int x = UnityEngine.Random.Range(1, width - 1);
        int z = 1;

        while (!done)
        {
            map[x, z] = 0;
            int nx = UnityEngine.Random.Range(-1, 2);
            int nz = UnityEngine.Random.Range(0, 2);

            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                x += nx;
            }
            else
            {
                z += nz;
            }

            // crawl until you hit the edge of the map
            done = (x < 1 || x >= width - 1 || z < 1 || z >= depth - 1);
        }
    }

    public void PrimsCrawl()
    {
        bool done = false;
        int x = width / 2;
        int z = depth / 2;

        var walls = new Dictionary<string, MapLocation>();
        walls.Add(GetKey(x + 1, z), new MapLocation(x + 1, z));
        walls.Add(GetKey(x - 1, z), new MapLocation(x - 1, z));
        walls.Add(GetKey(x, z + 1), new MapLocation(x, z + 1));
        walls.Add(GetKey(x, z - 1), new MapLocation(x, z - 1));

        while (!done)
        {
            List<string> keyList = walls.Keys.ToList();
            int randomIndex = UnityEngine.Random.Range(0, keyList.Count);
            string randomKey = keyList[randomIndex];
            var randomWall = walls[randomKey];
            x = randomWall.x;
            z = randomWall.z;
            walls.Remove(randomKey);

            if (CountSquareCorridors(x, z) <= 1)
            {
                map[x, z] = 0;

                if (!walls.ContainsKey(GetKey(x + 1, z)))
                {
                    walls.Add(GetKey(x + 1, z), new MapLocation(x + 1, z));
                }
                if (!walls.ContainsKey(GetKey(x - 1, z)))
                {
                    walls.Add(GetKey(x - 1, z), new MapLocation(x - 1, z));
                }
                if (!walls.ContainsKey(GetKey(x, z + 1)))
                {
                    walls.Add(GetKey(x, z + 1), new MapLocation(x, z + 1));
                }
                if (!walls.ContainsKey(GetKey(x, z - 1)))
                {
                    walls.Add(GetKey(x, z - 1), new MapLocation(x, z - 1));
                }
            }
            done = (x < 1 || x >= width - 1 || z < 1 || z >= depth - 1);
        }
    }

    private string GetKey(int x, int z)
    {
        return $"{x},{z}";
    }

    public void RecursiveCrawl()
    {
        int x = UnityEngine.Random.Range(1, width);
        int z = UnityEngine.Random.Range(1, depth);
        MapLocation ml = new MapLocation(x, z);
        passes = 0;
        InnerRecursiveCrawl(ml);
    }

    private void InnerRecursiveCrawl(MapLocation ml)
    {
        if (passes >= maxPasses) return;
        if (CountSquareCorridors(ml.x, ml.z) >= 2) return;

        map[ml.x, ml.z] = 0;
        List<MapLocation> shuffledML = ShuffleNewMapLocations(ml.x, ml.z);
        InnerRecursiveCrawl(shuffledML[0]);
        InnerRecursiveCrawl(shuffledML[1]);
        InnerRecursiveCrawl(shuffledML[2]);
        InnerRecursiveCrawl(shuffledML[3]);
        passes++;

    }

    private List<MapLocation> ShuffleNewMapLocations(int x, int z)
    {
        List<MapLocation> mapLocations = new List<MapLocation>(){
            new MapLocation(x + 1, z),
            new MapLocation(x - 1, z),
            new MapLocation(x, z + 1),
            new MapLocation(x, z - 1)
        };
        List<MapLocation> shuffledMapLocations = new List<MapLocation>();

        while (mapLocations.Count() > 0)
        {
            int index = UnityEngine.Random.Range(0, mapLocations.Count);
            shuffledMapLocations.Add(mapLocations[index]);
            mapLocations.RemoveAt(index);
        }

        return shuffledMapLocations;
    }

    private void PrintList(List<MapLocation> list)
    {
        for (int i = 0; i < list.Count(); i++)
        {
            Debug.Log(list[i].Show());
        }
    }
}
