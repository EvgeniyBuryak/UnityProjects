using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGenerator : MonoBehaviour
{
    public Font m_Font;
    public List<MeshFilter> m_PlaneFilters;

    private int m_AmmoCount = 60;

    private void Awake()
    {
        // Use the current material, just update the main texture
        foreach (MeshFilter meshFilter in m_PlaneFilters)
            meshFilter.GetComponent<MeshRenderer>().material.mainTexture = m_Font.material.mainTexture;
    }

    private void Start()
    {
        StartCoroutine(AmmoCountDown());
    }

    private void CreatFont(string output)
    {
        // Get the texture based on the font, and characters needed
        m_Font.RequestCharactersInTexture(output);

        // For each character in the string
        for (int i = 0; i < output.Length; i++)
        {
            // Get character data
            CharacterInfo character;
            m_Font.GetCharacterInfo(output[i], out character);

            // Set UVs
            Vector2[] uvs = new Vector2[4];
            uvs[0] = character.uvBottomLeft;
            uvs[1] = character.uvTopRight;
            uvs[2] = character.uvBottomRight;
            uvs[3] = character.uvTopLeft;

            // Apply UVs
            m_PlaneFilters[i].mesh.uv = uvs;

            // Get basic scale
            Vector3 newScale = m_PlaneFilters[i].transform.localScale;
            newScale.x = character.glyphWidth * 0.02f;

            // Set
            m_PlaneFilters[i].transform.localScale = newScale;
        }
    }

    private IEnumerator AmmoCountDown()
    {
        while (m_AmmoCount > 0) 
        {
            DisplayAmmo(m_AmmoCount);
            m_AmmoCount--;

            yield return new WaitForSeconds(1.0f);
        }
    }

    private void DisplayAmmo(int ammoCount)
    {
        string output = ammoCount.ToString();

        if (ammoCount < 10)
            output = "0" + output;

        CreatFont(output);
    }
}
