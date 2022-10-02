using UnityEngine;

public class TileController : MonoBehaviour
{
    public bool isColored = false;

    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        isColored = true;

        GameManager.singleton.CheckComplete();
    }
}
