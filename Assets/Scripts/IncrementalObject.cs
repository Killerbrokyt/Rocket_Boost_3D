using UnityEngine;
using DG.Tweening;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float maxScale = 2f;
    [SerializeField] private float time = 2f;

    void Start()
    {
        transform
            .DOScale(transform.localScale * maxScale, time)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject);
    }
}
