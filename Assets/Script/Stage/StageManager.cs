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
    [Tooltip("���̺� ����Ʈ\n�������� �ʹ� �κ��� 0������ ��ġ")]
    public List<Vector2Int> stageSavePoint;
    [Tooltip("Gizmo ũ��\n���̺� ����Ʈ ũ��� ��������\n����� ũ�� = ���̺� ����Ʈ �ν� �ڽ� ũ��")]
    public Vector2 collisionSize = Vector2.one;
    [Tooltip("Gizmo ����")]
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
