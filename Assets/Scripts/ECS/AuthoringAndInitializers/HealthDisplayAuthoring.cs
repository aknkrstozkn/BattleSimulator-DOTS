using Unity.Entities;
using UnityEngine;
namespace ECS.AuthoringAndInitializers
{
	public class HealthDisplayAuthoring : MonoBehaviour, IConvertGameObjectToEntity
	{
		public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
		{
			var textMesh = gameObject.GetComponent<TextMesh>();
			dstManager.AddComponentObject(entity, textMesh);
		}
	}
}
