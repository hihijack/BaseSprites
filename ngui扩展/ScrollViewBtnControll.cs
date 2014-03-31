using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIScrollView))]
public class ScrollViewBtnControll : MonoBehaviour {

    public UIButton btnBack;
    public UIButton btnForward;
    UIGrid grid;
    public int index;

    UIScrollView usv;

    Vector3 oriPos;
    float cellWidth;
    float cellHeigth;
    // Use this for initialization
	void Start () {
        btnBack.onClick.Add(new EventDelegate(OnBack));
        btnForward.onClick.Add(new EventDelegate(OnForward));
        usv = GetComponent<UIScrollView>();
        grid = GetComponentInChildren<UIGrid>();
        oriPos = transform.localPosition;
        cellWidth = grid.cellWidth;
        cellHeigth = grid.cellHeight;
        Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnBack() {
        index--;
        if(index < 0){
            index = 0;
        }
        Init();
    }

    void OnForward() {
        index++;
        int maxIndex = grid.transform.childCount - 1;
        if(index > maxIndex)
        {
            index = maxIndex;
        }
        Init();
    }

    void Init() 
    {
        if (usv.movement == UIScrollView.Movement.Horizontal)
	    {
            float x = oriPos.x - index * cellWidth;
            SpringPanel.Begin(usv.gameObject, new Vector3(x, oriPos.y, oriPos.z), 8f);
	    }
    }
}
