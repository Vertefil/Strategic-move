using UnityEngine;

public class CamContrl : MonoBehaviour
{
    public float rotateSpeed = 10.0f, CamSpeed = 10.0f, ZomSped = 250f;
    private float _mult = 1f;

    //Передвижение камеры через клавиши
    private void Update()
    {
        //Передвижеине камеры по гориз и по верт
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        //Поворот
        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q)) 
            rotate = -1f;
        else if(Input.GetKey(KeyCode.E))
            rotate = 1f;

        //Ускорение
        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f :1f ;
        //Вращение
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World);
        //Передвижение
        transform.Translate(new Vector3(hor,0 ,ver)* Time.deltaTime * _mult * CamSpeed, Space.Self);
        //Приближение
        transform.position += transform.up * ZomSped * Time.deltaTime * _mult * Input.GetAxis("Mouse ScrollWheel");

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y,20f,51f),
            transform.position.z);
    }
}
