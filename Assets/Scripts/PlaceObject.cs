using UnityEngine;

public class PlaceObject : MonoBehaviour
{

    //Слой с которым сталкиваеться луч
    public LayerMask layer;
    public float rotSpeed = 60f;
    private void Start()
    {
        PositionObject();


    }

    void Update()
    {
        PositionObject();

        //На лкм ставим и убераем скрипт
        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject.GetComponent<PlaceObject>());
        //На среднюю кнопку мыши вращаем объект
        if(Input.GetKey(KeyCode.Tab))
            transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);

    }
    private void PositionObject()
    {
        //Через рейкасты (Лучи) узнаем положение мыши и куда можем поставить объект
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ставим объект на место попадания луча
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, layer))
            transform.position = hit.point;
    }
}
