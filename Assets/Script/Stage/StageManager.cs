using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StageManager : MonoBehaviour
{
    public GameObject SavePointOBJ;
    [Header("Gizmo")]
    [Tooltip("세이브 포인트\n스테이지 초반 부분을 0번부터 배치")]
    public List<Vector2Int> stageSavePoint;
    [Tooltip("Gizmo 크기\n세이브 포인트 크기와 관련있음\n기즈모 크기 = 세이브 포인트 인식 박스 크기")]
    public Vector2 collisionSize = Vector2.one;
    [Tooltip("Gizmo 색깔")]
    public Color collisionColor = Color.yellow;
    [Space]
    [Header("SavePoint")]
    private int nowSave = 0;
    private Dictionary<int, Vector3> savePoints = new Dictionary<int, Vector3>();
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
}
