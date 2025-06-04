using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // Queries for all Spawner components. Uses RefRW because this system wants
        // to read from and write to the component. If the system only needed read-only
        // access, it would use RefRO instead.
        foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            ProcessSpawner(ref state, spawner);
        }
    }

    private void ProcessSpawner(ref SystemState state, RefRW<Spawner> spawner)
    {
        //check if cubes spawned
        if (spawner.ValueRO.hasSpawned)
            return; //exit if yes

        // If the next spawn time has passed.
        if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            //get grid size and shit
            int3 gridSize = spawner.ValueRO.gridSize;
            int spacing = spawner.ValueRO.spacing;

            //loop through to add them cubes
            for(int x= 0; x<gridSize.x; x++)
            {
                for(int y= 0; y<gridSize.y; y++)
                {
                    for(int z= 0; z<gridSize.z; z++)
                    {
                        //calculate spawn pos for this cube
                        float3 offset = new float3(x * spacing, y * spacing, z * spacing);
                        float3 spawnPos = spawner.ValueRO.spawnPosition + offset;
                        float3 rotation = spawner.ValueRO.spawnRotation;

                        //Instantiate new cube at calculated pos
                        Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.cube);
                        //state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(spawnPos));
                        state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPositionRotation(spawnPos, quaternion.Euler(rotation)));
                        //state.EntityManager.AddComponentData(newEntity, LocalTransform.FromRotation(quaternion.Euler(rotation)));
                        //state.EntityManager.SetComponentData(newEntity, quaternion.Euler(rotation));
                    }
                }
            }
           //cubes have spawned
           spawner.ValueRW.hasSpawned = true;
        }
    }
}
