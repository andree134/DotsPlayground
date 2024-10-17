
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

class SpawnerAuthoring : MonoBehaviour
{
    public GameObject cube;
    public float spawnRate;
    public Vector3Int gridSize = new Vector3Int(100,100,100); //grid size
    public int spacing = 10; //space between cubes size
}
class SpawnerBaker : Baker<SpawnerAuthoring>
{
    public override void Bake(SpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Spawner
        {
            cube = GetEntity(authoring.cube, TransformUsageFlags.Dynamic),
            spawnPosition = authoring.transform.position,
            nextSpawnTime = 0.0f,
            spawnRate = authoring.spawnRate,
            gridSize = new int3(authoring.gridSize.x, authoring.gridSize.y, authoring.gridSize.z),
            spacing = authoring.spacing
        });
    }
}
