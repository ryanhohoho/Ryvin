using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    public GameObject cube; // the cube gameObject to instantiate
    public GameObject player; // the player gameObject to fetch position from
    public int maxCubes; // controls the amount of cubes to generate
    public float minWidth; // minimum width of the cubes
    public float maxWidth; // maximum width of the cubes
    public float minHeight; // minimum height of the cubes
    public float maxHeight; // maximum height of the cubes
    public float heightRange; // the range away from 0 for the tops of the cubes
    public float spacing; // the minimum spacing between cubes
    public float playerDistance; // the distance between player and spawn

    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

    void Start()
    {
        GenerateCubes(maxCubes, -50f, 200f);
    }

    void Update()
    {
        //generates more cubes in order to extend the reach of my generating newCubes relative to my player's position.
        //check if there are any cubes 205 units away from the player
        playerDistance = player.transform.position.z + 205f;
        Vector3 distancePos = new Vector3(0f, 0f, playerDistance);
        if (!IsPositionOccupied(distancePos, 5f, spacing))
        {
            //generates cubes at 200 to 210 units away from the player
            GenerateCubes(5, playerDistance - 5f, playerDistance + 5f);
        }

        // Checks and deletes cubes behind the player.
        playerDistance = player.transform.position.z - 25f;
        Vector3 deleteDistancePos = new Vector3(0f, 0f, playerDistance);
        GameObject cubeToDelete = IsColliding(deleteDistancePos, 20f, spacing);
        if (cubeToDelete != null)
        {
            Destroy(cubeToDelete);
        }
    }

    void GenerateCubes(int numCubes, float minRange, float maxRange)
    {
        //Main procedural generation code for generating cubes
        int counter = 0;
        for (int i = 0; i < numCubes; i++)
        {
            // generate random position within the specified space (20x100/20x10)
            Vector3 randomPos = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(minRange, maxRange));

            // generate random width within the min and max range
            float randomWidth = Random.Range(minWidth, maxWidth);
            
            // check if the position is occupied
            if (IsPositionOccupied(randomPos, randomWidth, spacing))
            {
                // if it is, try again with a new random position
                i--;
                continue;
            }

            // increment counter
            counter++;

            // check if counter has reached threshold
            if (counter >= 10000)
            {
                Debug.LogError("Error: Loop has reached maximum iterations. Breaking loop.");
                break;
            }

            // add the position to the occupied positions set
            occupiedPositions.Add(randomPos);

            //generate a random height
            float randomHeight = Random.Range(minHeight, maxHeight);

            // instantiate the cube at the random position and with the random width and a random height within a range.
            GameObject newCube = Instantiate(cube, randomPos, Quaternion.identity) as GameObject;
            newCube.transform.localScale = new Vector3(randomWidth, Mathf.Clamp(Random.Range(minHeight, maxHeight), -heightRange, heightRange), randomWidth);
            newCube.tag = "Cube";
        }
    }
    
    private bool IsPositionOccupied(Vector3 pos, float width, float spacing)
    {
        // check if the position is occupied and returns a boolean
        Bounds bounds = new Bounds(pos, new Vector3(width + spacing, 1, width + spacing));
        foreach (Vector3 occupiedPos in occupiedPositions)
        {
            if (bounds.Contains(occupiedPos))
            {
                return true;
            }
        }
        return false;
    }

    private GameObject IsColliding(Vector3 pos, float width, float spacing)
    {
        // check if the position is occupied and returns the other gameObject if it is. Null if not.
        Bounds bounds = new Bounds(pos, new Vector3(width + spacing, 1, width + spacing));
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag("Cube"))
        {
            if (bounds.Contains(cube.transform.position))
            {
                return cube;
            }
        }
        return null;
    }
}