using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MazeGen
{

    public override void Generate(){
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
}
