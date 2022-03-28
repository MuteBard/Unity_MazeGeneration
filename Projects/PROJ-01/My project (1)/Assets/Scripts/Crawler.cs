using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MazeGen
{
    public override void Generate(){
        CrawlZ();
        CrawlX();
        CrawlRandom();
        CrawlX();
    }
    public void CrawlRandom(){
        bool done = false;
        int x = width / 2;
        int z = depth / 2;

        while (!done){
            map[x, z] = 0;
            int nx = UnityEngine.Random.Range(-1, 2);
            int nz = UnityEngine.Random.Range(-1, 2);

            if(Random.Range(0,100) < 50) {
                x += nx;
            }else{
                z += nz;
            }
            
            // crawl until you hit the edge of the map
            done = ( x < 0 || x >= width || z < 0 || z >= depth );
        } 
    }

    public void CrawlX(){
        bool done = false;
        int x = 0;
        int z = depth / 2;

        while (!done){
            map[x, z] = 0;
            int nx = UnityEngine.Random.Range(0, 2);
            int nz = UnityEngine.Random.Range(-1, 2);

            if(Random.Range(0,100) < 50) {
                x += nx;
            }else{
                z += nz;
            }
            
            // crawl until you hit the edge of the map
            done = ( x < 0 || x >= width || z < 0 || z >= depth );
        } 
    }

        public void CrawlZ(){
        bool done = false;
        int x = width / 2;
        int z = 0;

        while (!done){
            map[x, z] = 0;
            int nx = UnityEngine.Random.Range(-1, 2);
            int nz = UnityEngine.Random.Range(0, 2);

            if(Random.Range(0,100) < 50) {
                x += nx;
            }else{
                z += nz;
            }
            
            // crawl until you hit the edge of the map
            done = ( x < 0 || x >= width || z < 0 || z >= depth );
        } 
    }
}
