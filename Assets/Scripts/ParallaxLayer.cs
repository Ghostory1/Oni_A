using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class ParallaxLayer
{
    private float imageFullWidth;
    private float imageHalfWidth;
    private float Offset =10;
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

    public void Lookbackground(float cameraLeftEdge,float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imageHalfWidth) - Offset;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + Offset;

        if(cameraLeftEdge > imageRightEdge)
        {
            background.position += Vector3.right * imageFullWidth;
        }
        else if(cameraRightEdge < imageLeftEdge)
        {
            background.position += Vector3.right * -imageFullWidth;
        }
    }
}
