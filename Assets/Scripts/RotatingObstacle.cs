using UnityEngine;
using DG.Tweening;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] private float duration = 2f;

    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .SetLink(gameObject); // Para que al destruir el objeto se destruya tambien el tween

    }
}
