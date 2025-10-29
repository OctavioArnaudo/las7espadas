using UnityEngine;

public class InitSprite : InitSpawn
{
    [Header("Displacement Orientation")]
    public SpriteModel displacementOrientation = SpriteModel.Right;
    public Sprite[] damageStates; // Sprites para cada nivel de daño
}