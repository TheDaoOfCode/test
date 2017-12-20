//JUST DOING TESTS - THIS UNITY3D CODE DOESNT DO WHAT IT NEEDS TO AT THE MOMENT
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.EventSystems;
using System.Collections;

//Find parent with name//
//Get slider.value set it to child.iD//
//Get child.id.renderer - set it to flipToggle//

public class ShowHideSeqence : MonoBehaviour
{
    public GameObject[]             sculptureChildren   = null;
    public Slider                   sliderShowHide      = null;
    public Slider sliderNumberOfChildrenInfo = null;
    public int                      sliderInt;
    public int                      childID;

    public Transform       currentChild                ;
    private Transform[]     currentChildren             ;
    public  List <GameObject> allChildrenInSculpture    ;

    private bool            flip                        ;
    private GameObject      SculptureHolder             ;
    private Renderer[]      currentRenderer             ;
    private List <Renderer> currentRenderersAll         ;
    public int numOfChildren;
    public bool listAlreadyMade;
    public GameObject currentChildInViz;
    public GameObject currentChildViz;
    public float ShowHideWaitTime;
    public bool quickShowHide;
    public AudioSource musicSource = null;
    public AudioClip musicClip = null;
    public GameObject objectHit;
    public Transform objectHitTrans;
    public GameObject objectHitParent;
    public List<int> storeOfSliderValues;
    private RaycastHit hit;
    private List<GameObject> listOfChildrenOnHitInstance;
    private int indexOfClickedNote;
    public int sliderPreviousValue;
    public int sliderCurrentValue;
    public bool removeDuplicates;

    void Start()
	{
        SculptureHolder = GameObject.Find("Sculpture 1");
        listAlreadyMade = false;
        musicSource.clip = musicClip;
        sliderShowHide.value = 0;

    }

    void Update()
    {
        sliderInt = Mathf.RoundToInt(sliderShowHide.value);
      
        //sliderShowHide.value        = numOfChildren;
        //sliderShowHide.maxValue     = numOfChildren;


        if (Input.GetMouseButtonDown(0)) //ON MOUSE DOWN
        {
            //BroadcastMessage("ClickedLeftButton");

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            //if (Physics.Raycast(ray, out hit, 100))
            //{

            //    //Debug.DrawLine (ray.origin, hit.point);

            //    objectHit = hit.transform.gameObject;
            //    objectHitTrans = hit.transform;
            //    objectHitParent = hit.transform.parent.gameObject;

            //}

            //current slider value == needs own slider separate from hide show


            numOfChildren = SculptureHolder.transform.childCount;


            for (var i = 0; i < numOfChildren; i++)                // A - POPULATE THE LIST OF CHILDREN
            {
                print("CREATING LIST OF CHILDREN IN SCULPTURE");

                currentChild = SculptureHolder.transform.GetChild(i); //get current child transform

                allChildrenInSculpture.Add(currentChild.gameObject); //

            }

            sliderShowHide.value = numOfChildren;

            sliderShowHide.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

            print("PRESSED LEFT BUTTON");


        }
    }

 


    public void MakeSculptureList()  //MAKE LIST ON FIRST DRAG OF THE SLIDER ( SHOULD BE MADE DYNAMIC, IF MADE TO CHECK FOR NEW OBJECTS PRESENT )
    {

        //allChildrenInSculpture.Clear();



    }

    public void ValueChangeCheck()
    {

        storeOfSliderValues.Add(sliderInt);

        sliderCurrentValue = sliderInt;
        sliderPreviousValue = storeOfSliderValues.ElementAt(storeOfSliderValues.Count() - 1);

        if (removeDuplicates == true && (sliderCurrentValue == sliderPreviousValue) && (storeOfSliderValues.Count > 1)) //IF CURRENT VALUE IS NOT THE SAME AS PREVIOUS LOG THE NUMBER IN THE LIST
        {

            storeOfSliderValues.RemoveAt(storeOfSliderValues.Count); //REMOVE DUPLICATE
         
        }
        else { storeOfSliderValues.Add(sliderInt); }

        if      (sliderCurrentValue > sliderPreviousValue)
        {
            allChildrenInSculpture.ElementAt(storeOfSliderValues.Count() - 1).SetActive(true);
            print("CURRENT VALUE IS " + sliderCurrentValue + "BIGGER: MAKE VISIBLE")     ;
        }
        else if (sliderCurrentValue < sliderPreviousValue)      {

            allChildrenInSculpture.ElementAt(storeOfSliderValues.Count() -1).SetActive(false);
            print("PREVIOUS VALUE IS " + sliderPreviousValue + "SMALLER: MAKE INVISIBLE")  ;}

    }

    public void ShowHideBaseOnAdding()
    {

    }

    public void DeleteSculpture ()
    {

        foreach (var i in allChildrenInSculpture)
        {
            DestroyObject(i);
            storeOfSliderValues.Clear();
        }
    }

    //public void OnMouseDown()
    //{
    //    if (Input.GetMouseButtonDown(0)) //ON MOUSE DOWN
    //    {
    //        BroadcastMessage("ClickedLeftButton");

    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


    //        if (Physics.Raycast(ray, out hit, 100))
    //        {

    //            //Debug.DrawLine (ray.origin, hit.point);

    //            objectHit = hit.transform.gameObject;
    //            objectHitTrans = hit.transform;
    //            objectHitParent = hit.transform.parent.gameObject;

    //        }

    //        sliderShowHide.value        = numOfChildren;


    //        //listOfChildrenOnHitInstance = objectHitParent.GetComponent<ArrayOfChildObjects>().ArrayOfChildObjInNote; //RETURNS LIST OF CHILDREN ON THE NOTE (Texture..buttons etc)

    //        //indexOfClickedNote = objectHitParent.GetComponent<ArrayOfChildObjects>().myINDEX;                       //GET INDEX OF THE NOTE - gets it by finding the parent of the texture
    //        //indexOfClickedNote 	= listOfInstances.IndexOf (objectHitParent) ;            						// GET PARENT ID OF THE PARENT OF THE HIT TEXTURE/FIELD OBJECT >>doesnt seem to return correct id



    //    }
    //}





 ////==========================  WORKS KINDOFF ====
 //   public void ShowHidePerIDArray() {
 //       if (quickShowHide == true)
 //       {

 //           for (var j = 0; j < sliderInt; j++)                     // A - SET GOS TO LEFT OF SLIDER HANDLE VISIBLE
 //           {
 
 //               currentChildViz = allChildrenInSculpture.ElementAt(j);
 //               Invoke("ShowObjects", 0.1f);

 //               print("SETTING 0 - Slider.Value [ " + j + " ] VISIBLE" + currentChildViz);



 //               for (var k = sliderInt; k < numOfChildren; k++)     // B - SET GOS TO RIGHT OF SLIDER HANDLE NOT VISIBLE
 //               {

 //                   currentChildInViz = allChildrenInSculpture.ElementAt(k);
 //                   Invoke("HideObjects", 0.1f);

 //                   print("SETTING 0 - Slider.Value [ " + k + " ] NOT !VISIBLE" + currentChildInViz);

                   
 //               }
 //           }

 //       }
 //       else StartCoroutine(HideAndShowGos());
 //   }



 //   public void ShowObjects()
 //   {
 //       currentChildViz.SetActive(true);
 //       print(" SETTING LEFT ACTIVE");
 //   }

 //   public void HideObjects()
 //   {
 //       currentChildInViz.SetActive(false);
 //       print(" SETTING RIGHT INACTIVE");

 //   }


 //   IEnumerator HideAndShowGos()
 //   {
 //       var pitch = gameObject.GetComponent<AudioSource>().pitch;

 //       for (var j = 0; j < sliderInt; j++) // B - SET 0->SliderID GOS VISIBLE
            
 //       {
 //           //currentChild.localScale = new Vector3(1, 2, 1);               //add scale as you pass over current object.
 //           //currentChild.gameObject.SetActive(true);                       //add scale as you pass over current object.
 //           currentChildViz = allChildrenInSculpture.ElementAt(j);
 //           allChildrenInSculpture.ElementAt(j).SetActive (true);

 //           //currentChildViz.MoveBy(new Vector3(0, 1, 0), 1, 1); //FUN!!! makes them move on both instance and show

 //           currentChildViz.PunchPosition (new Vector3(0, 1, 0), 1, 1);
 //           pitch = 1.7f;
 //           musicSource.Play();

 //           print("SETTING 0 - Slider.Value [ " + j + " ] VISIBLE" + currentChildViz);

 //           yield return new WaitForSeconds(ShowHideWaitTime);
 //           //currentRenderersAll[0].enabled = !currentRenderersAll[0].enabled;       //revese the current renderer state


 //           for (var k = sliderInt; k < numOfChildren; k++)     // C - SET EVERYTHING AFTER THE SLIDER VALUE TO INVISBLE
 //           {
 //               //currentChild.localScale = new Vector3(0.3f, 0.3f, 0.3f);      //add scale as you pass over current object.
 //               //currentChild.gameObject.SetActive(false);                     //add scale as you pass over current object.
 //               currentChildInViz = allChildrenInSculpture.ElementAt(k);
 //               allChildrenInSculpture.ElementAt(k).SetActive(false);

                
 //               pitch = 0.3f;
 //               musicSource.Play();
                

 //               print("SETTING 0 - Slider.Value [ " + k + " ] NOT !VISIBLE" + currentChildInViz);

 //               yield return new WaitForSeconds(ShowHideWaitTime);
 //           }
 //       }
 //   }

 //   //=========================== WORKS KINDOFF ===

    /*
   
    public void ShowHidePerID () //WORKS THE OLDE WAY
    {
        SculptureHolder     = GameObject.Find("Sculpture 1")                    ; //FindParentByName("Scupture1")

        currentChild        = SculptureHolder.transform.GetChild(sliderInt)              ; //Get current child by slider int=index and set it to new variable

        currentRenderer     = currentChild.GetComponentsInChildren<Renderer>()  ; //Get render component from current child game object.

        currentRenderer[0].enabled = !currentRenderer[0].enabled            ; //revese the current renderer state

        currentChild.localScale = new Vector3 (1, 4, 1)                     ; //add scale as you pass over current object
    }
      */
}
