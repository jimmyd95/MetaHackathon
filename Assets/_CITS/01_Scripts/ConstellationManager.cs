using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class ConstellationManager : MonoBehaviour
{
    public List<string[]> celestialConnections = new List<string[]>();
    public Dictionary<string, Vector2> celestialCoordinates = new Dictionary<string, Vector2>();
    public Dictionary<string, GameObject> celestialStars = new Dictionary<string, GameObject>();
    public float scaleFactor = 5f; // Scaling factor for spreading out the stars
    private GameObject _bolt;

    [SerializeField] private int interactiveThreshold = 10;

    // Start is called before the first frame update
    void Start()
    {
        _bolt = GameObject.FindWithTag("Bolt");
        // Debug.Log("Bolt found: " + _bolt);

        Debug.Log("Inside Constellation Manager...");

        // check for the threshold
        interactiveThreshold = (celestialCoordinates.Count < interactiveThreshold) ? celestialCoordinates.Count: interactiveThreshold; 
        int interactiveThresholdCounter = 0;
        foreach((string starName, Vector2 coordinates) in celestialCoordinates){
            bool interactiveStar = (interactiveThresholdCounter++ < interactiveThreshold);
            Debug.Log("Bool: "+interactiveStar);
            GameObject star = CreateStar(starName, coordinates, interactiveStar);
            celestialStars.Add(starName, star);
        }

        Debug.Log("Total Connections :"+ celestialConnections.Count);
        // Debug.Log("Total Connections :"+ celestialConnections.Count);
        // foreach(string[] connection in celestialConnections) {
        //     Debug.Log(connection[0] + " connects to "+ connection[1]);
        //     Transform starA = celestialStars[connection[0]].transform;
        //     Transform starB = celestialStars[connection[1]].transform;
        //     ConnectStars(starA, starB);
        // }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

        // Create a star GameObject at the given celestial coordinates
    // Make sure your Quest is connected and runnable BEFORE YOU WORK ON THIS
    // Read through FindSpawnPositions.cs
    // var prefabBounds = Utilities.GetPrefabBounds(_bolt);
    // generate a random location within the room, use the spawn, and instantiate within the room
    // GameObject star = Instantiate(_bolt, new Vector3(1, 1, 1), quaternion.identity);

    // Create a star GameObject at the given celestial coordinates
    GameObject CreateStar(string name, Vector2 celestialCoord, bool interactive)
    {
        // Convert celestial coordinates to Cartesian coordinates
        Vector3 cartesianCoord = CelestialToCartesian(celestialCoord.x, celestialCoord.y); // can be utilized after the bolts turned into stars and constellation

        // Instantiate star GameObject
        GameObject star = Instantiate(_bolt); // need to adjust so it can spawn the location where we want it to be
        star.transform.parent = transform; // Set parent to "Constellation"
        star.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // Adjust scale if necessary
        // add floating component
        // DynamicFloat floatValue = star.AddComponent<DynamicFloat>();
        // floatValue.starCoordinate = cartesianCoord; // store star Coordinate
        // floatValue.floatHeight = 0.1f;
        // floatValue.floatSpeed = 1f;
        // star.name = name;
        // star.transform.position = cartesianCoord;
        Debug.Log("Parent position in manager: "+transform.position.x +", "+transform.position.y+", "+transform.position.z);
        Vector3 randomPosition = new Vector3(Random.Range((cartesianCoord.x-5), cartesianCoord.x), Random.Range(0, cartesianCoord.y), Random.Range((cartesianCoord.z-5), cartesianCoord.z));


        // add a component to update position
        if(interactive){
            // position will be random
            // star.transform.position = transform.position + cartesianCoord;
            star.transform.position = randomPosition;
            Debug.Log("Star "+name+" is interactive");
            // star.AddComponent<InteractionUpdates>();
        }
        else{
            star.transform.position = transform.position + cartesianCoord;
        }
        return star;
    }

        // Function to convert celestial coordinates to Cartesian coordinates
    Vector3 CelestialToCartesian(float ra, float dec)
    {
        // Implementation of CelestialToCartesian function
                // Convert RA and Dec to radians
        float ra_rad = Mathf.Deg2Rad * ra;  // Convert hours to degrees
        float dec_rad = Mathf.Deg2Rad * dec;

        // Calculate Cartesian coordinates
        float x = Mathf.Cos(dec_rad) * Mathf.Cos(ra_rad);
        float y = Mathf.Cos(dec_rad) * Mathf.Sin(ra_rad);
        float z = Mathf.Sin(dec_rad);

        return new Vector3(x, y, z)*scaleFactor; // spread out stars with scale factor
    }

        // Connect two stars with a Line Renderer
    void ConnectStars(Transform star1, Transform star2)
    {
        Debug.Log("Connecting Stars...");
        // Create empty GameObject for line
        GameObject lineObj = new GameObject(star1.name + "->" + star2.name);
        lineObj.transform.parent = transform;

        // Add Line Renderer component
        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        //

        // Set positions for the Line Renderer
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, star1.position);
        lineRenderer.SetPosition(1, star2.position);

        // Add DynamicLineRenderer script to the GameObject
        DynamicLineRenderer dynamicLineRenderer = lineObj.AddComponent<DynamicLineRenderer>();
        dynamicLineRenderer.lineRenderer = lineRenderer;
        dynamicLineRenderer.startPoint = star1;
        dynamicLineRenderer.endPoint = star2;
    }
}