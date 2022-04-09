using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public enum BlockSide {
        BOTTOM,
        TOP,
        LEFT,
        RIGHT,
        FRONT,
        BACK
    }
    public Mesh mesh;
    Chunk parentChunk;
    public Block(Vector3 offset, byte blockType, Chunk chunk) {

        parentChunk = chunk;
        if(blockType == 0) return;

        List<Quad> quads = new List<Quad>();
        if(!HasSolidNeighbor( (int) offset.x, (int) offset.y - 1, (int) offset.z ))
            quads.Add(new Quad(BlockSide.BOTTOM, offset));
        if(!HasSolidNeighbor( (int) offset.x, (int) offset.y + 1, (int) offset.z ))
            quads.Add(new Quad(BlockSide.TOP, offset));
        if(!HasSolidNeighbor( (int) offset.x - 1, (int) offset.y, (int) offset.z ))
            quads.Add(new Quad(BlockSide.LEFT, offset));
        if(!HasSolidNeighbor( (int) offset.x + 1, (int) offset.y, (int) offset.z ))
            quads.Add(new Quad(BlockSide.RIGHT, offset));
        if(!HasSolidNeighbor( (int) offset.x, (int) offset.y, (int) offset.z + 1 ))
            quads.Add( new Quad(BlockSide.FRONT, offset));
        if(!HasSolidNeighbor( (int) offset.x, (int) offset.y, (int) offset.z - 1 ))
            quads.Add(new Quad(BlockSide.BACK, offset));


        if(quads.Count == 0) return;

        Mesh[] sideMeshes = new Mesh[quads.Count];
        int m = 0;
        foreach(Quad q in quads){
            sideMeshes[m] = q.mesh;
            m++;
        }

        mesh = MeshUtils.MergeMeshes(sideMeshes);
        mesh.name = $"Cube_{offset.x}_{offset.y}_{offset.z}";
    }

    public bool HasSolidNeighbor(int x, int y, int z)
    {
        if (
            x < 0 || x >= parentChunk.width ||
            y < 0 || y >= parentChunk.height ||
            z < 0 || z >= parentChunk.depth
        )
        {
            return false;
        }
        if (parentChunk.chunkData[x + parentChunk.width * (y + parentChunk.depth * z)] == 0)
        {
            return false;
        }
        return true;
    }
}