using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecalUpdater : MonoBehaviour {
	private GameObject[] affectedObjects;
	public Decal decal;
	GameObject TargetGO;
	// Use this for initialization
	void Start () {
		//decal = GetComponent<Decal>();
	//	BuildDecal(decal);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void UpdateDecalTo(GameObject go, bool OneDecal)
	{
		TargetGO = go;
		BuildDecal(decal, OneDecal);
	}
	
	public void BuildDecal(Decal decal, bool OneDecal) {
		MeshFilter filter = decal.GetComponent<MeshFilter>();
		if(filter == null) filter = decal.gameObject.AddComponent<MeshFilter>();
		if(decal.GetComponent<Renderer>() == null) decal.gameObject.AddComponent<MeshRenderer>();
		decal.GetComponent<Renderer>().material = decal.material;
		
		if(decal.material == null || decal.sprite == null) {
			filter.mesh = null;
			return;
		}

		if (!OneDecal)
			{
				affectedObjects = GetAffectedObjects(decal.GetBounds(), decal.affectedLayers);
				foreach(GameObject go in affectedObjects) {
				DecalBuilder.BuildDecalForObject( decal, go );
			}

			DecalBuilder.Push( decal.pushDistance );
		}
		else
		{
		DecalBuilder.BuildDecalForObject( decal, TargetGO ); //ECs fix
		DecalBuilder.Push( decal.pushDistance ); //ECs fix
		}

		Mesh mesh = DecalBuilder.CreateMesh();
		if(mesh != null) {
			mesh.name = "DecalMesh";
			filter.mesh = mesh;
		}

		decal.transform.parent = TargetGO.transform; //ECs fix
		decal.gameObject.layer = 2; //ECs fix - IgnoreRaycast
	}

	private static bool IsLayerContains(LayerMask mask, int layer) {
		return (mask.value & 1<<layer) != 0;
	}

	private static GameObject[] GetAffectedObjects(Bounds bounds, LayerMask affectedLayers) {
		MeshRenderer[] renderers = (MeshRenderer[]) GameObject.FindObjectsOfType<MeshRenderer>();
		List<GameObject> objects = new List<GameObject>();
		foreach(Renderer r in renderers) {
			if( !r.enabled ) continue;
			if( !IsLayerContains(affectedLayers, r.gameObject.layer) ) continue;
			if( r.GetComponent<Decal>() != null ) continue;
			
			if( bounds.Intersects(r.bounds) ) {
				objects.Add(r.gameObject);
			}
		}
		return objects.ToArray();
	}

}
