using UnityEngine;

public class PlaceObject : MonoBehaviour
{

    //���� � ������� ������������� ���
    public LayerMask layer;
    public float rotSpeed = 60f;
    private void Start()
    {
        PositionObject();


    }

    void Update()
    {
        PositionObject();

        //�� ��� ������ � ������� ������
        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject.GetComponent<PlaceObject>());
        //�� ������� ������ ���� ������� ������
        if(Input.GetKey(KeyCode.Tab))
            transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);

    }
    private void PositionObject()
    {
        //����� �������� (����) ������ ��������� ���� � ���� ����� ��������� ������
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //������ ������ �� ����� ��������� ����
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, layer))
            transform.position = hit.point;
    }
}
