using UnityEngine;

public struct CardTransform
{
    public Vector3 pos;

    public Quaternion rotation;

    public CardTransform(Vector3 vectro3, Quaternion quaternion)
    {
        pos = vectro3;

        rotation = quaternion;
    }
}
