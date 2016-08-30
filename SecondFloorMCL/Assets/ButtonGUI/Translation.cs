using UnityEngine;
using System.Collections;

public class Translation : MonoBehaviour {

    public float speed = 0.001f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, speed * Time.deltaTime, 0);
	}
}
