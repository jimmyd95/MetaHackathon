using System.IO;
using UnityEngine;
using System.Collections.Generic;

    [System.Serializable]
    public class ConstellationDataArray
    {
        public ConstellationData[] constellations;
    }

    [System.Serializable]
    public class StarData
    {
        public string name;
        public float ra; // right ascension
        public float dec; // declination
    }

    [System.Serializable]
    public class ConnectionData
    {
        public string start;
        public string end;
    }

    [System.Serializable]
    public class ConstellationData
    {
        public string name;
        public StarData[] stars;
        public ConnectionData[] connections;
    }

public class ConstellationLoader : MonoBehaviour
{
    private string constellation_dataPath = "_CITS/09_Data/ConstellationData.json";
    private TextAsset jsonFile;
    private string jsonText;


    private float range = 5f; // The range around the origin for the parent object


    private void Start()
    {
        LoadJSON();
        ParseJSON();
    }

    private void LoadJSON()
    {
        Debug.Log("Loading JSON...");
        string filePath = Path.Combine(Application.dataPath, constellation_dataPath);

        if(File.Exists(filePath)){
            jsonText = File.ReadAllText(filePath);
        }
        else{
            Debug.LogError("Failed to load JSON file.");
        }
    }

    private void ParseJSON()
    {
        if (jsonText != null)
        {
            Debug.Log("Parsing JSON...");
            ConstellationDataArray constellationDataArray = JsonUtility.FromJson<ConstellationDataArray>(jsonText);

            foreach (ConstellationData constellation in constellationDataArray.constellations)
            {
                Debug.Log("Constellation: " + constellation.name);
                Dictionary<string, Vector2> constellationStars = new Dictionary<string, Vector2>();
                List<string[]> constellationConnections = new List<string[]>();

                foreach (StarData star in constellation.stars)
                {
                    Debug.Log("Star: " + star.name + ", RA: " + star.ra + ", Dec: " + star.dec);
                    // add to constellationStars
                    constellationStars.Add(star.name, new Vector2(star.ra, star.dec));

                }
                foreach (ConnectionData connection in constellation.connections)
                {
                    Debug.Log("Connection: " + connection.start + " -> " + connection.end);
                    // add to constellationConnections
                    constellationConnections.Add(new string[] {connection.start, connection.end});
                }

                // create a new parent object named after the constellation
                Debug.Log("Making Constellation Object...");
                GameObject parentObject = new GameObject(constellation.name);
                parentObject.transform.parent = transform;
                // Generate random values for x, y, and z coordinates within the range
                // float randomX = Random.Range(-range, range);
                // float randomY = Random.Range(0, range);
                // float randomZ = Random.Range(-range, range);
                // Set the position of the parent constellation GameObject
                parentObject.transform.position = new Vector3(0, 0, 0);
                Debug.Log(constellation.name+"'s position in loader: "+parentObject.transform.position.x +", "+parentObject.transform.position.y+", "+parentObject.transform.position.z);
                

                // Attach ConstellationManager component
                ConstellationManager manager = parentObject.AddComponent<ConstellationManager>();
                manager.celestialCoordinates = constellationStars;
                manager.celestialConnections = constellationConnections;
                manager.scaleFactor = 10f;
                // add the constellation manager component
                // pass the stars and connection information
            }
        }
    }

}
