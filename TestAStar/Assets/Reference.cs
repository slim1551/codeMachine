using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reference : MonoBehaviour {

    //颜色材质区分
    public Material startMat;
    public Material endMat;
    public Material obstacleMat;

    //显示信息Text
    private Text text;

    //当前格子坐标
    public int x;
    public int y;

    void Awake()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
    }

    //判断当前格子的类型
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Start")
        {
            GetComponent<MeshRenderer>().material = startMat;
            MyAStar.Instance.grids[x, y].type = GridType.Start;
            MyAStar.Instance.openList.Add(MyAStar.Instance.grids[x, y]);
            MyAStar.Instance.startX = x;
            MyAStar.Instance.startY = y;
        }
        else if (other.name == "End")
        {
            GetComponent<MeshRenderer>().material = endMat;
            MyAStar.Instance.grids[x, y].type = GridType.End;
            MyAStar.Instance.targetX = x;
            MyAStar.Instance.targetY = y;
        }
        else if (other.name == "Obstacle")
        {
            GetComponent<MeshRenderer>().material = obstacleMat;
            MyAStar.Instance.grids[x, y].type = GridType.Obstacle;
        }
    }

    /// <summary>
    /// 鼠标点击显示当前格子基础信息
    /// </summary>
    void OnMouseDown()
    {
        text.text = "XY(" + x + "," + y + ")" + "\n" + "FGH(" + MyAStar.Instance.grids[x, y].f + "," + MyAStar.Instance.grids[x, y].g + "," + MyAStar.Instance.grids[x, y].h + ")";
        text.color = GetComponent<MeshRenderer>().material.color;
    }

}
