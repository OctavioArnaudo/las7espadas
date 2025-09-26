using System.Collections.Generic;
using UnityEngine;

public class TransformPointings : MonoController
{
    public Dictionary<string, List<Transform>> transformsByTag = new Dictionary<string, List<Transform>>();

    [SerializeField]
    public List<string> selectedTags = new List<string>();

    private void Awake()
    {
        transformsByTag.Clear();

        foreach (string tag in selectedTags)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
            List<Transform> transforms = new List<Transform>();

            foreach (GameObject obj in gameObjects)
            {
                transforms.Add(obj.transform);
            }

            transformsByTag.Add(tag, transforms);
        }
    }
}