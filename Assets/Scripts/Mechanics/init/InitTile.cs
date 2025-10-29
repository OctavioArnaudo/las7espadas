using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InitTile : InitSprite
{

    protected void DestroyTile(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        TileBase currentTile = tilemap.GetTile(cellPosition);

        if (currentTile != null)
        {
            tilemap.SetTile(cellPosition, null);
        }
    }

    public void LoadDestroyedTiles()
    {
        string data = PlayerPrefs.GetString("DestroyedTiles", "");

        if (string.IsNullOrEmpty(data)) return;

        string[] positions = data.Split('|');
        foreach (string posStr in positions)
        {
            string[] coords = posStr.Split(',');
            if (coords.Length == 3)
            {
                int x = int.Parse(coords[0]);
                int y = int.Parse(coords[1]);
                int z = int.Parse(coords[2]);

                tilemap.SetTile(new Vector3Int(x, y, z), null);
            }
        }
    }

    public void SaveDestroyedTiles()
    {
        List<string> destroyedPositions = new List<string>();

        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.GetTile(pos) == null)
            {
                destroyedPositions.Add($"{pos.x},{pos.y},{pos.z}");
            }
        }

        string data = string.Join("|", destroyedPositions);
        PlayerPrefs.SetString("DestroyedTiles", data);
        PlayerPrefs.Save();
    }

    public void LoadFileMap(Tilemap tilemap, Dictionary<string, TileBase> tileDict)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/map.json");
        TileMapModel wrapper = JsonUtility.FromJson<TileMapModel>(json);

        tilemap.ClearAllTiles();

        foreach (TileSetModel data in wrapper.tiles)
        {
            if (tileDict.ContainsKey(data.name))
            {
                tilemap.SetTile(data.position, tileDict[data.name]);
            }
        }
    }

    public List<TileSetModel> SaveMap(Tilemap tilemap)
    {
        List<TileSetModel> savedTiles = new List<TileSetModel>();

        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                savedTiles.Add(new TileSetModel
                {
                    position = pos,
                    name = tile.name
                });
            }
        }

        return savedTiles;
    }

    void LoadObjects(List<ObjectModel> states)
    {
        foreach (ObjectModel state in states)
        {
            GameObject obj = GameObject.Find(state.objectID);
            if (obj != null && state.isDestroyed)
            {
                Destroy(obj);
            }
        }
    }

    public void SaveObjectDestroyed(string objectID)
    {
        PlayerPrefs.SetInt("Destroyed_" + objectID, 1);
        PlayerPrefs.Save();
    }

    public bool IsObjectDestroyed(string objectID)
    {
        return PlayerPrefs.GetInt("Destroyed_" + objectID, 0) == 1;
    }

    protected override void Start()
    {
        base.Start();
        if (IsObjectDestroyed("Wall_01"))
        {
            Destroy(GameObject.Find("Wall_01"));
        }
    }

    public void SaveFileMap(List<TileSetModel> savedTiles)
    {
        string json = JsonUtility.ToJson(new TileMapModel { tiles = savedTiles });
        File.WriteAllText(Application.persistentDataPath + "/map.json", json);
    }

    public List<TileSetModel> LoadFileTiles()
    {
        string path = Application.persistentDataPath + "/map.json";

        if (!File.Exists(path))
        {
            Debug.LogWarning("Archivo de mapa no encontrado.");
            return new List<TileSetModel>();
        }

        string json = File.ReadAllText(path);
        TileMapModel mapModel = JsonUtility.FromJson<TileMapModel>(json);
        return mapModel.tiles;
    }

    public void LoadMap(Tilemap tilemap, Dictionary<string, TileBase> tileDictionary)
    {
        List<TileSetModel> tiles = LoadFileTiles();

        foreach (TileSetModel tile in tiles)
        {
            if (tileDictionary.TryGetValue(tile.name, out TileBase tileAsset))
            {
                tilemap.SetTile(tile.position, tileAsset);
            }
            else
            {
                Debug.LogWarning($"Tile '{tile.name}' no encontrado en el diccionario.");
            }
        }
    }

    public Dictionary<string, TileBase> BuildTileDictionary(TileBase[] tileAssets)
    {
        Dictionary<string, TileBase> dict = new Dictionary<string, TileBase>();
        foreach (TileBase tile in tileAssets)
        {
            dict[tile.name] = tile;
        }
        return dict;
    }

}
