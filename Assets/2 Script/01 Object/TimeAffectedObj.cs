using UnityEngine;
using System.Collections;

public abstract class TimeAffectedObj : MonoBehaviour
{

    public virtual void Awake()
    {
        TimeManager.Instance.AddObj(this);
    }
    public virtual void MyFixedUpdate() { }
    public virtual void MyUpdate() { }
    public virtual void MyLateUpdate() { }
}
