using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Framework 
{
    private GameObject model = null;

    private GameObject activeGo = null;

    public Framework Blueprint(GameObject frameworkPF) 
    {
        model = Object.Instantiate<GameObject>(frameworkPF);

        return(this);
    }

    public Framework Assemble(GameObject[] additionList, string anchorName, float turn = 0.0f)
    {
        int selection = Random.Range(0, additionList.Length);

        return(Assemble(additionList[selection], anchorName, turn));
    }

    public Framework Assemble(GameObject go, string anchorName, float yRotate = 0.0f, bool create = true)
    {
        if ((go != null) && (create))
        {
            Transform anchors = model.transform.Find("Anchors");
            Transform anchor = anchors.Find(anchorName);
            
            if (IsAPreFab(go))
            {
                activeGo = Object.Instantiate(go, anchor);
                activeGo.transform.rotation = Quaternion.Euler(new Vector3(0.0f, yRotate, 0.0f));
            } else {
                go.transform.position = anchor.transform.position;
                go.transform.rotation = Quaternion.Euler(new Vector3(0.0f, yRotate, 0.0f));
                go.transform.parent = anchor;
                activeGo = go;
            }
        }

        return(this);
    }

    public Framework Decorate(GameObject[] go, int count, float xRange, float zRange, float yRotate = 0.0f) 
    {
        for (int i = 0; i < count; i++) {
        int choice = Random.Range(0, go.Length);

        Decorate(go[choice], 1, xRange, zRange, yRotate);
        }

        return(this);
    }

    public Framework Decorate(GameObject go, int count, float xRange, float zRange, float yRotate = 0.0f) 
    {
        int ndecorations = Random.Range(0, count);
        for(int i = 0; i < count; i++) 
        {
            float xDelta = Random.Range(-xRange/2.0f, xRange/2.0f);
            float zDelta = Random.Range(-zRange/2.0f, zRange/2.0f);

            Vector3 position = activeGo.transform.position;
            Vector3 deltaPos = new Vector3(position.x + xDelta, 0.0f, position.z + zDelta);
            float turn = Random.Range(-yRotate, yRotate);

            GameObject decore = Object.Instantiate(go, activeGo.transform);
            decore.transform.rotation = Quaternion.Euler(new Vector3(0.0f, turn, 0.0f));
            decore.transform.position = deltaPos;
        }

        return(this);
    }

    public Framework Parent(Transform transform)
    {
        model.transform.parent = transform;

        return(this);
    }

    public Framework Position(Vector3 position)
    {
        model.transform.position = position;

        return(this);
    }

    public Framework Rotate(Vector3 rotate)
    {
        model.transform.rotation = Quaternion.Euler(rotate);

        return(this);
    }

    public GameObject Build()
    {
        return(model);
    }

    private bool IsAPreFab(GameObject thing) 
    {
        return(
            PrefabUtility.GetPrefabInstanceStatus(thing) != PrefabInstanceStatus.NotAPrefab 
            || PrefabUtility.GetPrefabAssetType(thing) != PrefabAssetType.NotAPrefab
        );
    }
}
