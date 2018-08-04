using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class MainMenuScrollView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private MainMenuBtnInfo startBtnInfo, achievementBtnInfo, quitBtnInfo;
    private MainMenuBtnInfo goalBtnInfo;

    private MainMenuWindow MainMenuWindow;
    private ScrollRect scrollRect;

    private MainMenuBtnInfo GoalBtnInfo
    {
        get
        {
            return goalBtnInfo;
        }
        set
        {
            StartCoroutine(LerpToCameraDestination(goalBtnInfo, value));
            goalBtnInfo = value;
        }
    }

    private void Awake()
    {
        MainMenuWindow = transform.parent.GetComponent<MainMenuWindow>();
        scrollRect = transform.GetComponent<ScrollRect>();
    }

    // Use this for initialization
    void Start()
    {
        startBtnInfo = new MainMenuBtnInfo(MainMenuWindow.buttonsDic["Btn_StartGame"], -50f, new Vector3(11, -2, 9), Vector3.zero);
        achievementBtnInfo = new MainMenuBtnInfo(MainMenuWindow.buttonsDic["Btn_Achievement"], 0, new Vector3(3.25f, -3.5f, -7), new Vector3(35, -105, 0));
        quitBtnInfo = new MainMenuBtnInfo(MainMenuWindow.buttonsDic["Btn_Quit"], 50f, new Vector3(5, -2, -15), new Vector3(20, 6, 0));

        goalBtnInfo = startBtnInfo;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnContentPosChange(Vector2 vector2)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StopAllCoroutines();
    }

    public void OnDrag(PointerEventData eventData)
    {


    }

    /// <summary>
    /// 拖拽完毕
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(scrollRect.content.anchoredPosition.y);
        //设置按钮吸附目标点以及目标Info
        SetContentDestination(scrollRect.content.anchoredPosition.y);
        //启动吸附动画携程
        StartCoroutine(LerpToContentDestination(scrollRect.content.anchoredPosition.y));
    }
    
    /// <summary>
    /// 设置按钮吸附目标点以及
    /// </summary>
    /// <param name="contentY"></param>
    private void SetContentDestination(float contentY)
    {
        if (contentY >= 50)
        {
            GoalBtnInfo = quitBtnInfo;
        }
        else if (contentY <= -50)
        {
            GoalBtnInfo = startBtnInfo;
        }
        else
        {
            if (-25 < contentY && contentY < 25)
            {
                GoalBtnInfo = achievementBtnInfo;
            }
            else if (contentY >= 25)
            {
                GoalBtnInfo = quitBtnInfo;
            }
            else if (contentY <= -25)
            {
                GoalBtnInfo = startBtnInfo;
            }
        }
    }

    /// <summary>
    /// 相机动画携程
    /// </summary>
    /// <param name="startInfo"></param>
    /// <param name="goalInfo"></param>
    /// <returns></returns>
    private IEnumerator LerpToCameraDestination(MainMenuBtnInfo startInfo, MainMenuBtnInfo goalInfo)
    {
        float l = 0;
        //锁组建作调颜色,动画播放过程中不能点击和拖动
        scrollRect.enabled = false;

        startBtnInfo.button.enabled = false;
        startBtnInfo.button.GetComponentInChildren<Text>().color = Color.gray;
        achievementBtnInfo.button.enabled = false;
        achievementBtnInfo.button.GetComponentInChildren<Text>().color = Color.gray;
        quitBtnInfo.button.enabled = false;
        quitBtnInfo.button.GetComponentInChildren<Text>().color = Color.gray;

        //播放动画
        while (true)
        {
            yield return null;
            l += Time.deltaTime;
            Camera.main.transform.position = Vector3.Lerp(startInfo.cameraPos, goalInfo.cameraPos, l);
            Camera.main.transform.rotation = Quaternion.Euler(Vector3.Lerp(startInfo.cameraRot, goalInfo.cameraRot, l));
            //播放动画完毕
            if (l >= 1)
            {
                //解锁操作，使选定的按钮可以被使用
                scrollRect.enabled = true;
                GoalBtnInfo.button.enabled = true;
                GoalBtnInfo.button.GetComponentInChildren<Text>().color = Color.black;
                break;
            }
        }
    }

    /// <summary>
    /// 按钮吸附动画携程
    /// </summary>
    /// <param name="startY"></param>
    /// <returns></returns>
    private IEnumerator LerpToContentDestination(float startY)
    {
        float l = 0;
        while (true)
        {
            yield return null;
            l += 0.1f;
            scrollRect.content.anchoredPosition = Vector2.up * Mathf.Lerp(startY, GoalBtnInfo.btnYPos, l);
            if (l >= 1)
            {
                scrollRect.enabled = true;
                GoalBtnInfo.button.enabled = true;
                break;
            }
        }
    }

}
/// <summary>
/// 主菜单按钮信息，用此来做动画
/// </summary>
public struct MainMenuBtnInfo
{
    public MainMenuBtnInfo(UnityEngine.UI.Button button, float btnYPos, Vector3 cameraPos, Vector3 cameraRot)
    {
        this.button = button;
        this.btnYPos = btnYPos;
        this.cameraPos = cameraPos;
        this.cameraRot = cameraRot;
    }
    public UnityEngine.UI.Button button;
    public float btnYPos;
    public Vector3 cameraPos;
    public Vector3 cameraRot;
}