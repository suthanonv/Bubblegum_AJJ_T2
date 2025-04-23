using System.Collections;
using UnityEngine;
public class Cam_Idle : MonoBehaviour
{



    [SerializeField] Vector2 RangeX = new Vector2(0.1f, 0.1f);
    [SerializeField] Vector2 RangeY = new Vector2(0.1f, 0.1f);
    Vector2 StatPostion = new Vector2();
    Vector2 Destination;

    [SerializeField] float Period = 3;
    private void Awake()
    {
        StatPostion = this.transform.position;
        Destination = GetDestination();


    }


    bool onRun;
    private void Update()
    {

        if ((Vector2)this.transform.position != Destination)
        {
            if (onRun == false)
            {
                onRun = true;
                StartCoroutine(LerpPosition(this.transform, Destination, Period));
            }
        }
        else
        {
            Destination = GetDestination();
        }
    }

    IEnumerator LerpPosition(Transform obj, Vector2 end, float time)
    {
        Vector2 start = obj.position;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            obj.position = Vector2.Lerp(start, end, t);
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.localPosition.y, -10);
            yield return null;
        }

        obj.position = end; // Snap to exact final position
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.localPosition.y, -10);

        onRun = false;

    }

    Vector2 GetDestination()
    {
        float x = Random.Range(-RangeX.x, RangeX.y);
        float y = Random.Range(-RangeY.y, RangeY.y);

        return StatPostion + new Vector2(x, y);
    }





}
