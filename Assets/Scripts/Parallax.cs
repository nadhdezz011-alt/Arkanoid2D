using UnityEngine;

public class Parallax : MonoBehaviour
{
public float parallaxMulpiplayer = 0.5f;
    private Material parallaxMaterial;
    private Transform player;
    private float lastplayerX;
    void Start()
    {
        parallaxMaterial = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastplayerX = player.position.x;
    }

    void Update()
    {
        float deltaX = player.position.x - lastplayerX;
        Vector2 offset = parallaxMaterial.mainTextureOffset;
        offset.x = Mathf.Repeat(offset.x + deltaX * parallaxMulpiplayer, 1f);
        parallaxMaterial.mainTextureOffset = offset;
        lastplayerX = player.position.x;
    }

}
