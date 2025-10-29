using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer.View
{
    /// <summary>
    /// 
    /// AnimatedTile is a scriptable object that defines a tile with animated sprites.
    /// 
    /// It inherits from TileBase and provides functionality to get tile data and animation data.
    /// 
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Animated Tile", menuName = "Tiles/Animated Tile")]
    public class AnimatedTile : TileBase
    {
        /// <summary>
        /// 
        /// An array of sprites that will be used for the animation.
        /// 
        /// </summary>
        public Sprite[] m_AnimatedSprites;
        /// <summary>
        /// 
        /// The minimum speed of the animation.
        /// 
        /// </summary>
        public float m_MinSpeed = 1f;
        /// <summary>
        /// 
        /// The maximum speed of the animation.
        /// 
        /// </summary>
        public float m_MaxSpeed = 1f;
        /// <summary>
        /// 
        /// The start time of the animation.
        /// 
        /// </summary>
        public float m_AnimationStartTime;
        /// <summary>
        /// 
        /// The type of collider used for the tile.
        /// 
        /// </summary>
        public Tile.ColliderType m_TileColliderType;

        /// <summary>
        /// 
        /// This method is called to get the tile data for the specified location in the tilemap.
        ///
        /// It sets the transform, color, sprite, and collider type for the tile.
        /// 
        /// </summary>
        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            // Set the tile data properties
            tileData.transform = Matrix4x4.identity;
            // Set the transform to identity matrix (no transformation)
            tileData.color = Color.white;
            // Set the color to white (no tint)
            if (m_AnimatedSprites != null && m_AnimatedSprites.Length > 0)
            {
                // If there are animated sprites, set the sprite to the first one
                tileData.sprite = m_AnimatedSprites[m_AnimatedSprites.Length - 1];
                // Set the sprite to the last one in the array
                tileData.colliderType = m_TileColliderType;
            }
        }

        /// <summary>
        /// 
        /// This method is called to get the tile animation data for the specified location in the tilemap.
        /// 
        /// It sets the animated sprites, animation speed, and start time for the tile animation.
        /// 
        /// </summary>
        public override bool GetTileAnimationData(Vector3Int location, ITilemap tileMap, ref TileAnimationData tileAnimationData)
        {
            // Check if there are animated sprites defined
            if (m_AnimatedSprites.Length > 0)
            {
                // If there are animated sprites, set the tile animation data
                tileAnimationData.animatedSprites = m_AnimatedSprites;
                // Set the animated sprites to the ones defined in the tile
                tileAnimationData.animationSpeed = Random.Range(m_MinSpeed, m_MaxSpeed);
                // Set the animation speed to a random value between the minimum and maximum speed
                tileAnimationData.animationStartTime = m_AnimationStartTime;
                // Set the start time of the animation
                return true;
            }
            // If there are no animated sprites, return false
            return false;
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// 
    /// This class provides a custom editor for the AnimatedTile scriptable object.
    /// 
    /// </summary>
    [CustomEditor(typeof(AnimatedTile))]
    public class AnimatedTileEditor : Editor
    {
        /// <summary>
        /// 
        /// This property provides access to the AnimatedTile being edited.
        /// 
        /// </summary>
        private AnimatedTile tile { get { return (target as AnimatedTile); } }

        /// <summary>
        /// 
        /// This method is called to draw the inspector GUI for the AnimatedTile.
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Begin checking for changes in the inspector.
            EditorGUI.BeginChangeCheck();
            // Display a header for the animated tile editor.
            int count = EditorGUILayout.DelayedIntField("Number of Animated Sprites", tile.m_AnimatedSprites != null ? tile.m_AnimatedSprites.Length : 0);
            // Ensure the count is non-negative.
            if (count < 0)
                // If the count is negative, set it to zero.
                count = 0;

            // Check if the number of animated sprites has changed.
            if (tile.m_AnimatedSprites == null || tile.m_AnimatedSprites.Length != count)
            {
                // If the number of animated sprites has changed, resize the array to match the new count.
                System.Array.Resize<Sprite>(ref tile.m_AnimatedSprites, count);
            }

            // If the count is zero, we don't need to display any animated sprites.
            if (count == 0)
                // If there are no animated sprites, display a message and return early.
                return;

            // Display a label to inform the user about the purpose of the animated sprites.
            EditorGUILayout.LabelField("Place sprites shown based on the order of animation.");
            // Add some space for better readability.
            EditorGUILayout.Space();

            // Loop through the number of animated sprites and create an ObjectField for each one.
            for (int i = 0; i < count; i++)
            {
                // Allow the user to set each animated sprite.
                tile.m_AnimatedSprites[i] = (Sprite)EditorGUILayout.ObjectField("Sprite " + (i + 1), tile.m_AnimatedSprites[i], typeof(Sprite), false, null);
            }

            // Allow the user to set the minimum and maximum speed for the animation.
            float minSpeed = EditorGUILayout.FloatField("Minimum Speed", tile.m_MinSpeed);
            // Ensure the minimum speed is not negative.
            float maxSpeed = EditorGUILayout.FloatField("Maximum Speed", tile.m_MaxSpeed);
            // Ensure the minimum speed is not negative.
            if (minSpeed < 0.0f)
                // If the minimum speed is negative, set it to zero.
                minSpeed = 0.0f;

            // Ensure the minimum speed is not negative.
            if (maxSpeed < 0.0f)
                // If the maximum speed is negative, set it to zero.
                maxSpeed = 0.0f;

            // Ensure the maximum speed is not less than the minimum speed.
            if (maxSpeed < minSpeed)
                // If the maximum speed is less than the minimum speed, set the maximum speed to the minimum speed.
                maxSpeed = minSpeed;

            // Set the minimum and maximum speed for the animation.
            tile.m_MinSpeed = minSpeed;
            // Ensure the minimum speed is non-negative.
            tile.m_MaxSpeed = maxSpeed;

            // Allow the user to set the start time for the animation.
            tile.m_AnimationStartTime = EditorGUILayout.FloatField("Start Time", tile.m_AnimationStartTime);
            // Ensure the start time is non-negative.
            tile.m_TileColliderType = (Tile.ColliderType)EditorGUILayout.EnumPopup("Collider Type", tile.m_TileColliderType);
            // Ensure the collider type is valid.
            if (EditorGUI.EndChangeCheck())
                // If any changes were made, mark the tile as dirty so it gets saved.
                EditorUtility.SetDirty(tile);
        }
    }
#endif
}