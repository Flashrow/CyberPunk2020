using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CreateAssetMenu(fileName ="TaskGoTo", menuName ="Tasks/Go to", order = 2), System.Serializable]
public class TaskGoTo : Task
{
    [SerializeField] private PathContainer pathContainer;
    private PathContainer pathContainerTemp;

    private int step = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        step = 0;
    }

    public override void Run()
    {
        pathContainerTemp = Instantiate(pathContainer);
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
        onFinish.Invoke();
        Destroy(pathContainerTemp.gameObject);
    }
}