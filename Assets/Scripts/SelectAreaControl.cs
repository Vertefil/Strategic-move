using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;

public class SelectAreaControl : MonoBehaviour
{
    public GameObject area;
    public LayerMask layer, layerMask;
    private Camera _cam;
    private GameObject _areaSelection;
    private RaycastHit _hit;
    public List<GameObject> players;

    private void Awake()
    {
        _cam = GetComponent<Camera>();

    }

    private void Update()
    {

        if(Input.GetMouseButtonDown(1) && players.Count > 0)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            //Ставим объект на место попадания луча

            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, layer))
                foreach (var el in players)
                    if (el != null)
                        el.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach(var el in players)
                if(el != null)
                    el.transform.GetChild(1).gameObject.SetActive(false);
            players.Clear();

            //Через рейкасты (Лучи) узнаем положение мыши и куда можем поставить объект
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            //Ставим объект на место попадания луча
            
            if (Physics.Raycast(ray, out _hit, 1000f, layer))
            {
                _areaSelection = Instantiate(area, new Vector3(_hit.point.x, 1, _hit.point.z),
                    Quaternion.identity);
            }
        }
        //Обработка зоны выделения
        if (_areaSelection)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                //Обработка отрицательного колайдера
                float xScale = (_hit.point.x - hitDrag.point.x) * -1;
                float zSacle = _hit.point.z - hitDrag.point.z;

                if(xScale < 0.0f && zSacle < 0.0f)
                {
                    _areaSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                else if (xScale < 0.0f)
                {
                    _areaSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }
                else if (zSacle < 0.0f)
                {
                    _areaSelection.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
                }
                else
                {
                    _areaSelection.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }

                _areaSelection.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zSacle) );
            }
        }



        if (Input.GetMouseButtonUp(0) && _areaSelection)
        {
            //Создаём массив попаданий по коллайдерам и те кто попали добавляются в него
             RaycastHit[] hits =  Physics.BoxCastAll(
                _areaSelection.transform.GetChild(0).transform.position,
                _areaSelection.transform.localScale / 2,
                Vector3.up,
                Quaternion.identity,
                0,
                layerMask);

            //Перебираем тех, кто попал и добавляем в список
            foreach(var el in hits)
            {
                if (el.collider.CompareTag("Enemy")) continue;
                players.Add(el.transform.gameObject);
                el.transform.GetChild(1).gameObject.SetActive(true);
            }


            Destroy(_areaSelection);
        }


    }
}
