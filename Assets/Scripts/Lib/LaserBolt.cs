using UnityEngine;
using System.Collections;

public class LaserBolt : MonoBehaviour {

  public float effectiveRange = 50.0f;

  private float distanceTravelled = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if(distanceTravelled > effectiveRange){
      Destruct();
    }
 
    distanceTravelled += Time.deltaTime * 100.0f;
		transform.Translate(Vector3.forward * Time.deltaTime * 100.0f);
	}

  void OnCollisionEnter(){
    Destruct();
  }

  private void Destruct(bool collision = false){
    Destroy(gameObject);
  }
}
