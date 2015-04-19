using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemManager {
  public Vector3 center;
  public SystemLayout layout;

  private List<Planet> planets;

  public SystemManager(Vector3 center, SystemLayout layout){
    this.center = center;
    this.layout = layout;

    this.planets = new List<Planet>();
  }

  public void AddPlanet(Planet planet){
    // Add a planet to the list of system planets and generate the proper orbit.
    Planet[] planetsArray = this.planets.ToArray();
    if(planetsArray.Length > 0){
      // If there is at least one orbiting planet, then we calculate padding distance from the outermost planet.
      Orbit furthestOrbit = planetsArray[planetsArray.Length - 1].orbit;
      planet.orbit = this.layout.ComputeNextFurthestOrbit(furthestOrbit, planet);
    }
    else{
      // There aren't any planets in orbit, so we calculate padding distance from the center.
      planet.orbit = this.layout.ComputeNextFurthestOrbit(this.center, planet);
    }
    this.planets.Add(planet);
  }

  public void Draw(){
    Planet[] planetsArray = this.planets.ToArray();
    for(int i=0; i<planetsArray.Length; i++){
      Planet planet = planetsArray[i];
      planet.orbit.UpdateAngle();
      planet.Draw();
    }
  }

  public void DrawOrbits(){
    Planet[] planetsArray = this.planets.ToArray();
    for(int i=0; i<planetsArray.Length; i++){
      Planet planet = planetsArray[i];
      planet.Draw();
      planet.DrawOrbit();
    }
  }
}

public class SystemLayout {
  public float minimumSeparationDistance;
  public float maximumSeparationDistance;
  public float minimumOrbitalVelocity;
  public float maximumOrbitalVelocity;

  public SystemLayout(float minimumSeparationDistance, float maximumSeparationDistance, float minimumOrbitalVelocity, float maximumOrbitalVelocity){
    this.minimumSeparationDistance = minimumSeparationDistance;
    this.maximumSeparationDistance = maximumSeparationDistance;
    this.minimumOrbitalVelocity = minimumOrbitalVelocity;
    this.maximumOrbitalVelocity = maximumOrbitalVelocity;
  }

  public Orbit ComputeNextFurthestOrbit(Orbit furthestOrbit, Planet janet){
    float planetOffset = janet.prefab.transform.localScale.x;
    float distance = this.computeRandomDistance(furthestOrbit.distance, planetOffset);
    float angle = this.computeRandomAngleInDegrees();
    float velocity = this.computeRandomVelocity();
    return new Orbit(furthestOrbit.center, distance, angle, velocity);
  }

  public Orbit ComputeNextFurthestOrbit(Vector3 center, Planet janet){
    float planetOffset = janet.prefab.transform.localScale.x;
    float distance = this.computeRandomDistance(0, planetOffset);
    float angle = this.computeRandomAngleInDegrees();
    float velocity = this.computeRandomVelocity();
    return new Orbit(center, distance, angle, velocity);
  }

  private float computeRandomDistance(float startingDistance, float planetOffset){
    return Random.Range(this.minimumSeparationDistance, this.maximumSeparationDistance) + startingDistance + planetOffset;
  }

  private float computeRandomAngleInDegrees(){
    return Random.Range(0, 360);
  }
	
  private float computeRandomVelocity(){
    return Random.Range(minimumOrbitalVelocity, maximumOrbitalVelocity);
  }
}

public class SystemGenerator : MonoBehaviour {
  public GameObject[] prefabs;
  public int planetCount = 10;

  public float minimumSeparationDistance = 100.0f;
  public float maximumSeparationDistance = 300.0f;
  public float minimumOrbitalVelocity = 1.0f;
  public float maximumOrbitalVelocity = 10.0f;
	
  private SystemManager systemManager;
  private GameObject go;

  // Use this for initialization
  void Start(){
    SystemLayout layout = new SystemLayout(minimumSeparationDistance, maximumSeparationDistance, minimumOrbitalVelocity, maximumOrbitalVelocity);
    systemManager = new SystemManager(transform.position, layout);

    for(int i=0; i < planetCount; i++){
      GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
      Planet planet = new Planet(prefab);
      systemManager.AddPlanet(planet);
    }

    // Only draw these once...or else.
    systemManager.DrawOrbits();
  }

  // Update is called once per frame
  void Update(){
    systemManager.Draw();
  }
}
