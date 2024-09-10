//with the exception of some minor additions, this is not the work of GameDevStudent. The script was downloaded from here: https://github.com/masterprompt/ModelStitching 

using UnityEngine;
using System.Collections.Generic;

public class Stitcher
{
	/// <summary>
	/// Stitch clothing onto an avatar.  Both clothing and avatar must be instantiated however clothing may be destroyed after.
	/// </summary>
	/// <param name="sourceClothing"></param>
	/// <param name="targetAvatar"></param>
	/// <returns>Newly created clothing on avatar</returns>
    
	public GameObject Stitch (GameObject sourceClothing, GameObject targetAvatar)
	{
		TransformCatalog boneCatalog = new TransformCatalog (targetAvatar.transform);
		SkinnedMeshRenderer[] skinnedMeshRenderers = sourceClothing.GetComponentsInChildren<SkinnedMeshRenderer> ();
		GameObject targetClothing = AddChild (sourceClothing, targetAvatar.transform);
		foreach (SkinnedMeshRenderer sourceRenderer in skinnedMeshRenderers) {
			SkinnedMeshRenderer targetRenderer = AddSkinnedMeshRenderer (sourceRenderer, targetClothing);
			targetRenderer.bones = TranslateTransforms (sourceRenderer.bones, boneCatalog);
		}
		return targetClothing;
	}

	private GameObject AddChild (GameObject source, Transform parent)
	{
		GameObject target = new GameObject (source.name);
		target.transform.parent = parent;
		target.transform.localPosition = source.transform.localPosition;
		target.transform.localRotation = source.transform.localRotation;
		target.transform.localScale = source.transform.localScale;

        // Copy all components from source to target
        CopyAllComponents(source, target);

        return target;
	}

	private SkinnedMeshRenderer AddSkinnedMeshRenderer (SkinnedMeshRenderer source, GameObject parent)
	{
		SkinnedMeshRenderer target = parent.AddComponent<SkinnedMeshRenderer> ();
		target.sharedMesh = source.sharedMesh;
		target.materials = source.sharedMaterials;
		return target;
	}

	private Transform[] TranslateTransforms (Transform[] sources, TransformCatalog transformCatalog)
	{
		Transform[] targets = new Transform[sources.Length];
		for (int index = 0; index < sources.Length; index++)
			targets [index] = DictionaryExtensions.Find (transformCatalog, sources [index].name);
		return targets;
	}

    /// <summary>
    /// Copies all components (including scripts) from the source to the target GameObject.
    /// </summary>
    private void CopyAllComponents(GameObject source, GameObject target)
    {
        foreach (Component component in source.GetComponents<Component>())
        {
            if (component is Transform)
                continue;  // Skip copying Transform, it's handled separately.

            System.Type type = component.GetType();
            Component copy = target.AddComponent(type);  // Add component of the same type.

            // Copy field values from source component to target component.
            foreach (var field in type.GetFields(System.Reflection.BindingFlags.Public |
                                                 System.Reflection.BindingFlags.NonPublic |
                                                 System.Reflection.BindingFlags.Instance))
            {
                field.SetValue(copy, field.GetValue(component));
            }
        }
    }
    #region TransformCatalog
    private class TransformCatalog : Dictionary<string, Transform>
	{
        #region Constructors
		public TransformCatalog (Transform transform)
		{
			Catalog (transform);
		}
        #endregion

        #region Catalog
		private void Catalog (Transform transform)
		{
            if(ContainsKey(transform.name))
            {
                Remove(transform.name); 
                Add(transform.name, transform);
            } 
            else
                Add(transform.name, transform);
            foreach (Transform child in transform)
				Catalog (child);
		}
        #endregion
	}
    #endregion


    #region DictionaryExtensions
	private class DictionaryExtensions
	{
		public static TValue Find<TKey, TValue> (Dictionary<TKey, TValue> source, TKey key)
		{
			TValue value;
			source.TryGetValue (key, out value);
			return value;
		}
	}
    #endregion

}

