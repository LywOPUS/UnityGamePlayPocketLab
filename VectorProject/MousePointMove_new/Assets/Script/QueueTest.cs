using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QueueTest : MonoBehaviour
{
    public Queue queue;
    public Text queueCount;
    public Text queueContent;
    public Text queuePeek;

    private void Start()
    {
        queue = new Queue();
    }

    private void Update()
    {
        if (queue.Count > 0)
        {
            queuePeek.text = "queuePeek : " + queue.Peek().ToString();
        }
        else
        {
            queuePeek.text = "queuePeek:";
        }

        //增加元素
        if (Input.GetKeyDown(KeyCode.A))
        {
            InsetTime();
            ShowQueue();
        }

        //减去元素
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (queue.Count > 0)
            {
                Debug.Log(queue.Dequeue());
            }
        }

        //清除队列所有元素
        if (Input.GetKeyDown(KeyCode.C))
        {
            queue.Clear();
        }
        queueCount.text = "queueCount:" + queue.Count.ToString();
    }

    private void InsetTime()
    {
        queue.Enqueue(Time.time);
    }

    private void ShowQueue()
    {
        queueContent.text = "ququeContent: ";
        foreach (var value in queue)
        {
            queueContent.text += value.ToString() + "|";
        }
    }
}