using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class AuthoringRotateSpeed : MonoBehaviour
{
    public float value;

    private class Baker : Baker<AuthoringRotateSpeed>
    {
        public override void Bake(AuthoringRotateSpeed authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);//can use none if the component does not need to move
            AddComponent(entity, new RotateSpeed
            {
                value = authoring.value
            });
        }
    }
}
