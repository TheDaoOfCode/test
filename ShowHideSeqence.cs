//MD5Hash:437bd5bf2f1feb3cfd83718fdffaf40a;
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
    public Slider               slider      = null;
    public int                  slider_i_Now;

    public List  <GameObject>   children_List;
    
	public List <int>          	slider_Values;
	private List  <Renderer>    currentRenderersAll;

    private GameObject          SculptureHolder;

    public int                  child_Count;
	public float 				build_Time;

    public bool                 quickShowHide;// for QUICK VERSION  
    public AudioSource          musicSource = null;
    public AudioClip            musicClip = null;
    public int                  slider_PreviousValue;
    public int                  slider_CurrentValue;

    //public bool                 removeDuplicates; // FOR CHECKIGN CURRENT.PREVISOUS VERSION
    public bool                 show;
    public bool                 hide;

    void Start()
	{
        SculptureHolder = GameObject.Find("Sculpture 1");
        slider_i_Now = Mathf.RoundToInt(slider.value);

        child_Count = 0;
        slider.value = 0;
        
        musicSource.clip = musicClip;
    }

    void Update()
    {
        


        if (Input.GetMouseButtonDown(0)&&(quickShowHide == true) ){  //ON MOUSE DOWN

             // DRIVE SLIDER WITH CHILD COUNT
                
			MakeSculpture(); // a-MAKE LIST

			//StartCoroutine(MakeSculpture());

            child_Count     = SculptureHolder.transform.childCount; //b-SET VAR TO CHILD COUNT
            slider_i_Now = Mathf.RoundToInt(slider.value);


        
                //{Show(); print("PASSED << SHOW >> CONDITION ");}

                //{HideArray(); print("PASSED << HIDE >> CONDITION ");}

			StartCoroutine (Show ());
			StartCoroutine (Hide ());
				

                print("CREATING SCULPTURE SETTING CHILD COUNT AND SLIDER i  ");
 
        }
    
		//slider.onValueChanged.AddListener(delegate { StartCoroutine(Show());});
		//slider.onValueChanged.AddListener(delegate { StartCoroutine(Hide()); });
          

    }



//==========================  FUNCTIONS FOR QUICK FOR LOOP ==== //
IEnumerator Show() {

      for (var j = 0; j < slider_i_Now; j++){                    // SHOW - TO LEFT OF SLIDER HANDLE: 0 -> slider.value

        children_List.ElementAt(j).SetActive(true); 

        print("SETTING 0 - Slider.Value [ " + j + " ] SHOWING " );

		yield return new WaitForSeconds (build_Time);
        }
    }   
       

IEnumerator Hide () {
if (slider_i_Now < child_Count){
  for (var k = slider_i_Now ; k < child_Count; k++) {   // HIDE - TO RIGHT OF SLIDER HANDLE: slider.value -> .count

        children_List.ElementAt(k).SetActive(false);

        print("SETTING 0 - Slider.Value [ " + k + " ] HIDING " );

		yield return new WaitForSeconds (build_Time);
        }
    }
}



	public void MakeSculpture(){  //MAKE  ON FIRST DRAG OF THE SLIDER ( SHOULD BE MADE DYNAMIC, IF MADE TO CHECK FOR NEW OBJECTS PRESENT )

        //CLEAR OLD VALUES*
        children_List.Clear();

        slider_i_Now    = child_Count; //
        //slider.value    = child_Count; // INCREMENT SLIDER POS BASED ON NUMBER OF CHILDREN
        slider.maxValue = child_Count; // KEEP MAX SLIDER VALUE CONNECTED TO NUMBER OF CHILDREN 

        //ADD EACH CHILD IN SCULPTURE TO A *
        for (var i = 0; i < SculptureHolder.transform.childCount; i++)                     
        {
            children_List.Add(SculptureHolder.transform.GetChild(i).gameObject);

            print("CREATING  OF CHILDREN IN SCULPTURE");

		//yield return new WaitForSeconds (build_Time);
        }

    }

    public void DeleteSculpture (){

        foreach (var i in children_List)
        {
            DestroyObject(i);
            slider_Values.Clear();
        }
    }

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

 //   //=========================== WORKS KINDOFF END ===//


  //IF THE SLIDER IS PRESSED RANDOMLY THIS APPROACH IS NOT ENOUGH - NEEDS A FOR LOOP or A CURRENT TO PRESSED VALUE
   // public void ValueChangeCheck(){

        //STORE CURRENT SLIDER VALUE*
        //store_OfSliderValues.Add(sliderIntValue);

        //slider_CurrentValue = store_OfSliderValues.ElementAt(store_OfSliderValues.Count()       );
        //slider_PreviousValue = store_OfSliderValues.ElementAt(store_OfSliderValues.Count() - 1  );

        //if (removeDuplicates == true && (slider_CurrentValue == slider_PreviousValue) && (store_OfSliderValues.Count > 1)) //IF CURRENT VALUE IS NOT THE SAME AS PREVIOUS LOG THE NUMBER IN THE 
        //{
        //    //REMOVE DUPLICATE*
        //    store_OfSliderValues.RemoveAt(store_OfSliderValues.Count);

        //    //EVALUATE CURRENT AND PREVIOUS*
        //    if (slider_CurrentValue > slider_PreviousValue)
        //    {
        //        children_List.ElementAt(store_OfSliderValues.Count() - 1).SetActive(true);
        //        print("CURRENT VALUE IS " + slider_CurrentValue + "BIGGER: MAKE VISIBLE");
        //    }
        //    if (slider_CurrentValue < slider_PreviousValue)
        //    {
        //        children_List.ElementAt(store_OfSliderValues.Count() - 1).SetActive(false);
        //        print("PREVIOUS VALUE IS " + slider_PreviousValue + "SMALLER: MAKE INVISIBLE");
        //    }

        //}

    //}


    /* 
   
    public void ShowHidePerID () //=======WORKS THE OLDE WAY==============//
    {
        SculptureHolder     = GameObject.Find("Sculpture 1")                    ; //FindParentByName("Scupture1")

        currentChild        = SculptureHolder.transform.GetChild(sliderInt)              ; //Get current child by slider int=index and set it to new variable

        currentRenderer     = currentChild.GetComponentsInChildren<Renderer>()  ; //Get render component from current child game object.

        currentRenderer[0].enabled = !currentRenderer[0].enabled            ; //revese the current renderer state

        currentChild.localScale = new Vector3 (1, 4, 1)                     ; //add scale as you pass over current object
    }
      */
}
