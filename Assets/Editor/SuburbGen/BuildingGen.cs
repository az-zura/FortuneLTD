using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using Range = UnityEngine.SocialPlatforms.Range;

public class BuildingGen : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject housePrefab;
    public GameObject sidewalkPrefab;
    public GameObject roadPrefab;
    public GameObject lanternPrefab;
    public GameObject[] smallRocks;
    public GameObject[] trashPrefab;
    public GameObject[] randomGardenProps;
    public Bounds garden;

    public int rockMin;
    public int rockMax;

    public int gardenAssetsMax;
    public int gardenAssetsMin;


    public int terrainWidth;
    public int terrainLength;

    public Vector2 housePosition;
    public Vector2 lanternPosition;
    public Vector2 trashPosition;
    public float rotation;

    private const int GROUND_TILE_SIZE = 5;

    private List<GameObject> placed = new List<GameObject>();

    public void generate()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);

        clear();

        placeGround();
        placeSingleObjects();
        fillGarden();

        this.transform.rotation = Quaternion.Euler(0, rotation, 0);
    }


    private void clear()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
        }

        foreach (GameObject child in placed)
        {
            DestroyImmediate(child);
        }
    }

    private GameObject place(GameObject gameObject, Vector3 pos, Quaternion rotation = default, bool navMesh = false)
    {
        if (rotation == default)
        {
            rotation = this.transform.rotation;
        }

        GameObject instantiated = Instantiate(gameObject, pos, rotation, transform);
        instantiated.isStatic = true;
        if (navMesh) GameObjectUtility.SetStaticEditorFlags(instantiated, StaticEditorFlags.NavigationStatic);
        placed.Add(instantiated);
        return instantiated;
    }

    private void placeGround()
    {
        //street
        for (int z = 1; z < terrainWidth + 1; z++)
        {
            var pos = this.transform.position + new Vector3(GROUND_TILE_SIZE, 0, GROUND_TILE_SIZE * z);
            place(roadPrefab, pos, navMesh: true);
        }

        //street
        for (int z = 1; z < terrainWidth + 1; z++)
        {
            var pos = this.transform.position + new Vector3(GROUND_TILE_SIZE * 2, 0, GROUND_TILE_SIZE * z);
            pos.x -= GROUND_TILE_SIZE;
            place(sidewalkPrefab, pos, Quaternion.Euler(0, -90, 0), navMesh: true);
        }


        //terrain
        for (int z = 1; z < terrainWidth + 1; z++)
        {
            for (int x = 3; x < terrainLength + 4; x++)
            {
                var pos = this.transform.position + new Vector3(GROUND_TILE_SIZE * x, 0, GROUND_TILE_SIZE * z);
                var rotation = this.transform.rotation;
                int r = Random.Range(0, 3);
                switch (r)
                {
                    case 0:
                        rotation = Quaternion.Euler(0, 90, 0);
                        pos.z -= GROUND_TILE_SIZE;
                        break;
                    case 1:
                        rotation = Quaternion.Euler(0, 180, 0);
                        pos.z -= GROUND_TILE_SIZE;
                        pos.x -= GROUND_TILE_SIZE;
                        break;
                    case 2:
                        rotation = Quaternion.Euler(0, 270, 0);
                        pos.x -= GROUND_TILE_SIZE;
                        break;
                }

                place(floorPrefab, pos, rotation, navMesh: true);
            }
        }
    }

    public void reloadHouse()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);

        //remove old
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name.Contains("SuburbHouseEmpty"))
            {
                var o = gameObject.transform.GetChild(i).gameObject;
                DestroyImmediate(o);
                placed.Remove(o);
            }
        }
        place(housePrefab, transform.position + new Vector3(housePosition.x, 0, housePosition.y));
        this.transform.rotation = Quaternion.Euler(0, rotation, 0);
        
    }

    private void placeSingleObjects()
    {
        var position = this.transform.position;
        place(housePrefab, position + new Vector3(housePosition.x, 0, housePosition.y));
        place(lanternPrefab, position + new Vector3(lanternPosition.x, 0, lanternPosition.y),
            Quaternion.Euler(0, -90, 0));
        place(trashPrefab[Random.Range(0, trashPrefab.Length)],
            position + new Vector3(trashPosition.x, 0, trashPosition.y), Quaternion.Euler(0, Random.Range(0, 360), 0),
            navMesh: true);
    }

    private void placeRadomInBox(Bounds bounds, GameObject[] gameObjects, float height, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject toInstantiate = gameObjects[Random.Range(0, gameObjects.Length)];
            Vector3 pos = new Vector3(Random.Range(bounds.min.x, bounds.max.x), height,
                Random.Range(bounds.min.z, bounds.max.z));
            pos += this.transform.position;
            place(toInstantiate, pos, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }

    private void fillGarden()
    {
        placeRadomInBox(garden, smallRocks, 0, Random.Range(rockMin, rockMax));
        placeRadomInBox(garden, randomGardenProps, 0, Random.Range(gardenAssetsMin, gardenAssetsMax));
    }
}