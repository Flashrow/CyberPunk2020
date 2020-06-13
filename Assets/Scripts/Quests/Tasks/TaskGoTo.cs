using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName ="TaskGoTo", menuName ="Tasks/Go to", order = 2), System.Serializable]
public class TaskGoTo : Task
{
    [SerializeField] private PathContainer pathContainer;
    private PathContainer pathContainerTemp;
    public string hierarchy = null;

    private int step = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        step = 0;
    }

    public override void Run()
    {
        status = QuestStatus.IN_PROGRESS;
        if(hierarchy != null)
        {
            pathContainerTemp = Instantiate(pathContainer, GameObject.Find(hierarchy).transform);
        }
        else
            pathContainerTemp = Instantiate(pathContainer);

        Debug.Log($"Instantiate");

        pathContainerTemp.pathEventHandler.AddListener((other, order) =>
        {
            if (other.TryGetComponent<Hero>(out Hero hero) && order == step)
            {
                step++;
                EventListener.instance.Path.Invoke(new PathElementData { order = order, QuestId = this.QuestId, TaskId = this.id });
                Debug.Log($"Step {step}");
            }
            if(pathContainerTemp.GetComponentsInChildren<PathElement>().Length == step)
            {
                Finish();
            }
        });
    }

    public override void Finish()
    {
        status = QuestStatus.DONE;
        onFinish.Invoke();
        Destroy(pathContainerTemp.gameObject);
    }
}