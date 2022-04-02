using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelExplosionOnDestroy : MonoBehaviour
{
    /*private void Start()
    {
        ShatterSprite();
    }*/

    bool isQuitting = false;
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public GameObject QuadPrefab;

    private void OnDestroy()
    {
        if (isQuitting)
            return;

        ShatterSprite();
    }


    void ShatterSprite()
    { 
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();

        Texture2D texture = sr.sprite.texture;
        int width = texture.width;
        int height = texture.height;

        GameObject parentGO = new GameObject();
        parentGO.transform.position = this.transform.position;



        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (sr.sprite.texture.GetPixel(x, y).a < 0.1f)
                    continue;

                GameObject go = Instantiate(QuadPrefab, parentGO.transform);
                go.AddComponent<PixelDebris>().ExplosionPoint = this.transform.position;

                go.transform.localScale = Vector3.one / width;
                MeshRenderer mr = go.GetComponent<MeshRenderer>();
                mr.material.mainTexture = sr.sprite.texture;

                MeshFilter meshFilter = go.GetComponent<MeshFilter>();
                Vector2[] uv = meshFilter.mesh.uv;
                Vector2 v = new Vector2((float)x / (float)(width - 1), (float)y / (float)(height - 1));
                uv[0] = uv[1] = uv[2] = uv[3] = v;
                meshFilter.mesh.uv = uv;



                go.transform.localPosition = new Vector3((float)x / (float)(width), (float)y / (float)(height), 0) - Vector3.one/2f;
            }
        }

        parentGO.transform.localScale = this.transform.localScale;
    }
}
