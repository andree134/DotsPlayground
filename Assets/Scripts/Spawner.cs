using Unity.Entities;
using Unity.Mathematics;

public struct Spawner : IComponentData
{
    public Entity cube;
    public float3 spawnPosition;
    public float3 spawnRotation;
    public float nextSpawnTime;
    public float spawnRate;
    public int3 gridSize; //grid size (x,y,z)
    public int spacing; //spacing in between cubes
    public bool hasSpawned; //have cubes already spawned?
}
