using UnityEngine;
using System.Collections;

public class FrontChecker : MonoBehaviour {
	public float DistanceAway;
	public GameObject watchObj;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var pos = watchObj.transform.position;
		var rote = watchObj.transform.rotation;
		transform.position = new Vector3(pos.x + DistanceAway * Mathf.Cos(rote.y), pos.y, pos.z + DistanceAway * Mathf.Sin(rote.y));
		transform.LookAt(watchObj.transform);
	}
}
