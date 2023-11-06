using UnityEngine;

public class MoveByAI : MonoBehaviour
{
    [SerializeField] private Environment environment;
    [SerializeField] private string btGraphFilename;

    private BT.BTData data;
    private BT.BTGraph graph;

    private int frame;
    private GameObject prefab;

    private void Start()
    {
        prefab = Resources.Load("Bullet") as GameObject;

        data = new BT.BTData();
        data.transformList = environment.pointList;
        data.self = transform;

        graph = BT.BTGraphFactory.Load(string.Format("BTGraph/{0}", btGraphFilename));

        //
        var go = GameObject.Find("MoveByInput");
        if (go != null)
        {
            data.paramDict.Add("PlayerPos", "MoveByInput");
            data.transformList.Add(go.transform);
        }
    }

    public void Attack(Vector3 targetPos)
    {
        var c = GameObject.Instantiate(prefab);
        c.GetComponent<Bullet>().launcherLayer = c.layer;
        c.transform.localScale = 0.1f * Vector3.one;
        c.transform.position = transform.position + 0.3f * (Vector3.up + transform.forward);
        var r = c.GetComponent<Rigidbody>();
        r.isKinematic = false;
        r.useGravity = false;
        var dir = targetPos - transform.position;
        dir.y = 0f;
        r.AddForce(500f * dir.normalized);
    }


    private void Update()
    {
        UpdateBT();
        UpdateBTAction();
    }

    private void UpdateBTAction()
    {
        if (data.runningAction != null)
        {
            data.runningAction.OnUpdate(data);
        }
    }

    private void UpdateBT()
    {
        if (frame % 13 == 0)
        {
            data.runningAction = null;
            graph.Reset();

            graph.Exec(data);
            frame = 1;
        }
        frame++;
    }
}
