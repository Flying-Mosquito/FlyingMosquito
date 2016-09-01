using UnityEngine;
using System.Collections;

//TouchManager와 연관
public abstract class BaseButton : MonoBehaviour
{
    public bool isMouseDown = false;
    public virtual void OnTouchBegin(Vector2 _pos) { }
    public virtual void OnTouchMove(Vector2 _pos) { }
    public virtual void OnTouchEnd(Vector2 _pos) { }
}
