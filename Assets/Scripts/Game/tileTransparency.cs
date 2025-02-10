using UnityEngine;
using UnityEngine.Tilemaps;

public class tileTransparency : MonoBehaviour
{
    public float tileAlpha = 0.8f;
    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        SetTileAlpha(1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetTileAlpha(tileAlpha); // Reduce transparency
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetTileAlpha(1f); // Restore original transparency
        }
    }

    private void SetTileAlpha(float alpha)
    {
        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null)
            {
                tilemap.SetTileFlags(position, TileFlags.None); // Allow color changes
                Color currentColor = tilemap.GetColor(position);
                tilemap.SetColor(position, new Color(currentColor.r, currentColor.g, currentColor.b, alpha));
            }
        }
    }
}
