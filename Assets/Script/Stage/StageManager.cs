using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class StageManager : MonoBehaviour
{
    public GameObject Player_pref;
    public GameObject SavePointOBJ;
    [Header("★Debug Tool★")]
    [Tooltip("If you want to test the room, activate the check box")]
    public bool DebugRoom;
    [Tooltip("Please enter the element number of the room you want to test\nThe place in 'Element 0' represents 0")]
    public int StartPoint;

    [Space]
    [Header("Gizmo")]
    [Tooltip("Specify the location by adding an element.\nIt is loaded in order of the number of elements.")]
    public List<Vector2Int> stageSavePoint;
    [Tooltip("Indicates the size of the savepoint")]
    public Vector2 collisionSize = Vector2.one;
    [Tooltip("Indicates the color of the savepoint")]
    public Color collisionColor = Color.yellow;
    [Space]
    [Header("SavePoint")]
    private int nowSave = 0;
    private Dictionary<int, Vector3> savePoints = new Dictionary<int, Vector3>();
    [SerializeField]
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        foreach (var pos in stageSavePoint)
        {
            savePoints.Add(count, new Vector3(pos.x,pos.y,0));
            count++;
        }
        foreach (var dic in savePoints)
        {
            var obj = Instantiate(SavePointOBJ, this.transform);
            obj.transform.position = dic.Value+transform.position;
            obj.transform.localScale = collisionSize;
            obj.GetComponent<StageBox>().setNum(dic.Key);
            Debug.Log("Dictionary: " + dic.Key +" | "+dic.Value);
        }

        //Find "Player" Object and replace start point
        if (GameObject.Find("Player"))
        {
            Player = GameObject.Find("Player");
            Player.transform.position = GetNowSave();
        }
        else Player = Instantiate(Player_pref, GetNowSave(), Quaternion.identity);

        if (DebugRoom) Player.transform.position = savePoints[StartPoint] + this.transform.position;

    }
    private void OnDrawGizmosSelected()
    { 
        Gizmos.color = collisionColor;
        Gizmos.matrix = Matrix4x4.identity;

        var positions = stageSavePoint;

        foreach (Vector2 position in positions)
        {
            Gizmos.DrawWireCube(position + (Vector2)transform.position, collisionSize);
        }
    }
    public void SavePointUpdate(int v)
    {
        nowSave = v > nowSave ? v : nowSave;
    }

    public Vector3 GetNowSave()
    {
        if(savePoints.ContainsKey(nowSave)){
            return savePoints[nowSave]+this.transform.position;
        }
        else{
            Debug.Log("Not matched Key");
            return Vector3.zero;
        }
    }
}
