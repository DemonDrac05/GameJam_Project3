using UnityEngine;

namespace Assets
{
    internal interface IComponents
    {
        Rigidbody2D Rigidbody2D { get; }
        Collider2D Collider2D { get; }
        Animator Animator { get; }
    }
}
