using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.InputSystem.Interactions;

public class InputController : MonoBehaviour
{
    //音乐控制
    public PlayableDirector musicController;
    //timeline资源
    public TimelineAsset timelineResource;
    //用于区分字符串使用键
    public Dictionary<string, List<IMarker>> dictionary = new Dictionary<string, List<IMarker>>();

    //单例化
    public static InputController instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        //预先记录timeline中的所有节奏(事件)点
        foreach(var trackResource in timelineResource.GetOutputTracks())
        {
            //print(trackResource.name);
            if (trackResource.name.Contains("Check"))
            {
                List<IMarker> pointList = new List<IMarker>();
                foreach(var point in trackResource.GetMarkers())
                {
                    pointList.Add(point);
                }
                //print(pointList.Count);
                dictionary.Add(trackResource.name, pointList);
            }
        }
        //print("-------");
        //foreach(var item in dictionary)
        //{
        //    print(item.Key);
        //}
    }


    /*
     * 
     * 以下代码用于按下按键后消除节奏点生成的图形(即蓝方块)
     * 
     * 
     */
    //创建图形判断点的枚举类型
    public enum musicPtType
    {
        ptQ,ptW,ptE,ptR
    }
    //创建按键的图形判断点队列
    Queue<GameObject> queQ = new Queue<GameObject>();
    Queue<GameObject> queW = new Queue<GameObject>();
    Queue<GameObject> queE = new Queue<GameObject>();
    Queue<GameObject> queR = new Queue<GameObject>();
    //添加判定点
    public void addMusicPt(GameObject obj,musicPtType type)
    {
        switch (type)
        {
            case musicPtType.ptQ:
                queQ.Enqueue(obj);
                //print("Qadd");
                break;
            case musicPtType.ptW:
                queW.Enqueue(obj);
                //print("Wadd");
                break;
            case musicPtType.ptE:
                queE.Enqueue(obj);
                //print("Eadd");
                break;
            case musicPtType.ptR:
                queR.Enqueue(obj);
                //print("Radd");
                break;
            default:
                break;
        }
    }

    /*
     * 
     * 以下用于更新分数
     * 
     */
    public GameObject score_obj;
    int score = 0;

    public GameObject perfect_image;
    public GameObject good_image;
    public GameObject miss_image;
    private void Update()
    {
        score_obj.GetComponent<Text>().text = score.ToString();
    }

    /*
     * 
     * 以下代码用于键盘输入判定
     * 
     * 
     */
    [Range(0, 1)]
    public float none = 0.5f;
    [Range(0, 1)]
    public float miss = 0.4f;
    [Range(0, 1)]
    public float good = 0.25f;
    [Range(0, 1)]
    public float perfect = 0.15f;

    //Q的输入
    public void inputQ(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //按键按下时触发一次
            case InputActionPhase.Performed:
                if(context.interaction is PressInteraction)
                {
                    string checkPoint = "CheckQ";
                    if(dictionary[checkPoint].Count > 0)
                    {
                        //消去图形块
                        if((float)dictionary[checkPoint][0].time - (float)musicController.time >= 0 
                            && (float)dictionary[checkPoint][0].time - (float)musicController.time <= 0.4)
                        {
                            while(queQ.Count > 0)
                            {
                                Destroy(queQ.Dequeue());
                            }
                        }

                        getScore(checkPoint, pressQ);
                    }
                }
                break;
        }
    }

    //W的输入
    public void inputW(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //按键按下时触发一次
            case InputActionPhase.Performed:
                if (context.interaction is PressInteraction)
                {
                    string checkPoint = "CheckW";
                    if (dictionary[checkPoint].Count > 0)
                    {
                        //消去图形块
                        if ((float)dictionary[checkPoint][0].time - (float)musicController.time >= 0
                            && (float)dictionary[checkPoint][0].time - (float)musicController.time <= 0.4)
                        {
                            while (queW.Count > 0)
                            {
                                Destroy(queW.Dequeue());
                            }
                        }

                        getScore(checkPoint, pressW);
                    }
                }
                break;
        }
    }

    //E的输入
    public void inputE(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //按键按下时触发一次
            case InputActionPhase.Performed:
                if (context.interaction is PressInteraction)
                {
                    string checkPoint = "CheckE";
                    if (dictionary[checkPoint].Count > 0)
                    {
                        //消去图形块
                        if ((float)dictionary[checkPoint][0].time - (float)musicController.time >= 0
                            && (float)dictionary[checkPoint][0].time - (float)musicController.time <= 0.4)
                        {
                            while (queE.Count > 0)
                            {
                                Destroy(queE.Dequeue());
                            }
                        }

                        getScore(checkPoint, pressE);
                    }
                }
                break;
        }
    }

    //R的输入
    public void inputR(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            //按键按下时触发一次
            case InputActionPhase.Performed:
                if (context.interaction is PressInteraction)
                {
                    string checkPoint = "CheckR";
                    if (dictionary[checkPoint].Count > 0)
                    {
                        //消去图形块
                        if ((float)dictionary[checkPoint][0].time - (float)musicController.time >= 0
                            && (float)dictionary[checkPoint][0].time - (float)musicController.time <= 0.4)
                        {
                            while (queR.Count > 0)
                            {
                                Destroy(queR.Dequeue());
                            }
                        }

                        getScore(checkPoint, pressR);
                    }
                }
                break;
        }
    }

    
    //得分的判断
    void getScore(string checkPoint,IEnumerator pressKey)
    {
        float value = (float)dictionary[checkPoint][0].time - (float)musicController.time;
        if(value > none)
        {
            print("空按");
        }
        else if (value > miss)
        {
            print("miss");
            dictionary[checkPoint].RemoveAt(0);
            Instantiate(miss_image,new Vector3(0,1,0),Quaternion.identity);
        }
        else if(value > good)
        {
            print("good");
            dictionary[checkPoint].RemoveAt(0);
            score += 77;
            Instantiate(good_image, new Vector3(0, 1, 0), Quaternion.identity);
        }
        else if(value > perfect)
        {
            print("perfect");
            dictionary[checkPoint].RemoveAt(0);
            score += 99;
            Instantiate(perfect_image, new Vector3(0, 1, 0), Quaternion.identity);
        }
        else if(value > -perfect)
        {
            print("perfect");
            if (pressKey != null)
            {
                StopCoroutine(pressKey);
                pressKey = null;
            }
            dictionary[checkPoint].RemoveAt(0);
            score += 99;
            Instantiate(perfect_image, new Vector3(0, 1, 0), Quaternion.identity);
        }
        else if (value > -good)
        {
            print("good");
            if (pressKey != null)
            {
                StopCoroutine(pressKey);
                pressKey = null;
            }
            dictionary[checkPoint].RemoveAt(0);
            score += 77;
        }
        else if (value > -miss)
        {
            print("miss");
            if (pressKey != null)
            {
                StopCoroutine(pressKey);
                pressKey = null;
            }
            dictionary[checkPoint].RemoveAt(0);
        }
    }



    //Q键的miss判断
    IEnumerator pressQ;
    public void checkQ(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            if (dictionary[name].Count > 0)
            {
                if(dictionary[name][0].time - musicController.time <= 0)
                {
                    pressQ = Qmiss(name);
                    StartCoroutine(pressQ);
                }
            }
        }
    }
    IEnumerator Qmiss(string name)
    {
        yield return new WaitForSeconds(miss);
        print("miss");
        dictionary[name].RemoveAt(0);
        yield return null;
    }



    //W键的miss判断
    IEnumerator pressW;
    public void checkW(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            if(dictionary[name].Count > 0)
            {
                if(dictionary[name][0].time - musicController.time <= 0)
                {
                    pressW = Wmiss(name);
                    StartCoroutine(pressW);
                }
            }
        }
    }
    IEnumerator Wmiss(string name)
    {
        yield return new WaitForSeconds(miss);
        print("miss");
        dictionary[name].RemoveAt(0);
        yield return null;
    }

    //E键的miss判断
    IEnumerator pressE;
    public void checkE(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            if (dictionary[name].Count > 0)
            {
                if (dictionary[name][0].time - musicController.time <= 0)
                {
                    pressE = Emiss(name);
                    StartCoroutine(pressE);
                }
            }
        }
    }
    IEnumerator Emiss(string name)
    {
        yield return new WaitForSeconds(miss);
        print("miss");
        dictionary[name].RemoveAt(0);
        yield return null;
    }

    //R键的miss判断
    IEnumerator pressR;
    public void checkR(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            if (dictionary[name].Count > 0)
            {
                if (dictionary[name][0].time - musicController.time <= 0)
                {
                    pressR = Rmiss(name);
                    StartCoroutine(pressR);
                }
            }
        }
    }
    IEnumerator Rmiss(string name)
    {
        yield return new WaitForSeconds(miss);
        print("miss");
        dictionary[name].RemoveAt(0);
        yield return null;
    }
}



