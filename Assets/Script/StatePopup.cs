using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePopup : MonoBehaviour
{

    [SerializeField] private Button CPU;
    [SerializeField] private Button GPU;
    [SerializeField] private Button SKILL;

    [SerializeField]
    private GameObject CPUPage;
    [SerializeField]
    private GameObject GPUPage;
    [SerializeField]
    private GameObject SKILLPage;

 


    private void OnButtonCpu()
    {
        CPUPage.SetActive(true);
        GPUPage.SetActive(false);
        SKILLPage.SetActive(false);

        
    }
    private void OnButtonGpu()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(true);
        SKILLPage.SetActive(false);

     
    }
    private void OnButtonSkill()
    {
        CPUPage.SetActive(false);
        GPUPage.SetActive(false);
        SKILLPage.SetActive(true);


    }
}
