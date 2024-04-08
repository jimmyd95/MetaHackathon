using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationManager : MonoBehaviour
{
    public List<string[]> celestialConnections = new List<string[]>();
    public Dictionary<string, Vector2> celestialCoordinates = new Dictionary<string, Vector2>();

    public Dictionary<string, GameObject> celestialStars = new Dictionary<string, GameObject>();
    public float scaleFactor = 5f; // Scaling factor for spreading out the stars

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Inside Constellation Manager...");
        foreach((string starName, Vector2 coordinates) in celestialCoordinates){
            GameObject star = CreateStar(starName, coordinates);
            celestialStars.Add(starName, star);
        }

        Debug.Log("Total Connections :"+ celestialConnections.Count);
        foreach(string[] connection in celestialConnections) {
            Debug.Log(connection[0] + " connects to "+ connection[1]);
            Transform starA = celestialStars[connection[0]].transform;
            Transform starB = celestialStars[connection[1]].transform;
            ConnectStars(starA, starB);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        // Create a star GameObject at the given celestial coordinates
    GameObject CreateStar(string name, Vector2 celestialCoord)
    {
        // Convert celestial coordinates to Cartesian coordinates
        Vector3 cartesianCoord = CelestialToCartesian(celestialCoord.x, celestialCoord.y);

        // Instantiate star GameObject
        GameObject star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        star.transform.parent = transform; // Set parent to "Constellation"
        star.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // Adjust scale if necessary
        Float floatValue = star.AddComponent<Float>();
        floatValue.floatHeight = 0.1f;
        floatValue.floatSpeed = 1f;
        star.name = name;
        // star.transform.position = cartesianCoord;
        Debug.Log("Parent position in manager: "+transform.position.x +", "+transform.position.y+", "+transform.position.z);
        star.transform.position = transform.position + cartesianCoord;
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
